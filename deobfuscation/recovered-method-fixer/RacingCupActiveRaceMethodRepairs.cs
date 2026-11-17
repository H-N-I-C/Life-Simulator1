using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class RacingCupActiveRaceMethodRepairs
{
    // Five RacingCupSeriesActiveRace methods, recovered from ARM64 (task continuation, 2026-11-17).
    //
    // Layout facts (calibrated against native offsets, see HANDOFF §27):
    //   RacingCupSeriesActiveRace: raceCupRegistration (struct, 32B) @0x10, _raceResults @0x30,
    //     _trackIndex @0x38, _trackCompleted @0x3c, _pivotalMomentScore @0x40.
    //   RacingCupSeriesRegistration (struct): simRacing @0x0, simRacingCup @0x8, simRaceCar @0x10,
    //     simOpponents @0x18  →  [activeRace+0x28] == reg.simOpponents (List<SimRaceCarDriver>).
    //   TrackResults: Score @0x10, Placement @0x14, FinishTime (Nullable<TimeSpan>: HasValue bool
    //     @0x18, ticks @0x20) , TrackCompleted @0x28, CompetitorScores (Dictionary<SimRaceCarDriver,
    //     TrackResults>) @0x30.
    //
    // Shared native helpers identified:
    //   0x63369f0 List<T>::GetEnumerator (generic thunk), 0x5f0089c MoveNext, 0x5f00898 Dispose,
    //   0x5db5bd8 Dictionary<K,V>::TryGetValue, 0x5db3e18 Dictionary<K,V>::get_Item,
    //   0x69c7990 TimeSpan::op_Addition (adds ticks), 0x69c7aa4 TimeSpan::op_GreaterThan
    //   (verified: cmp x0,x1; cset w0,gt), TimeSpan.Zero read from the class static fields.
    //
    // Recovered C# (native-faithful):
    //
    //   int GetPlayerTotalPoints()                                   // 0x3953eb8
    //   {
    //       var total = 0;
    //       foreach (var trackResults in _raceResults) total += trackResults.Score;
    //       return total;
    //   }
    //
    //   TimeSpan GetPlayerTotalTime()                               // 0x3953ffc
    //   {
    //       var total = TimeSpan.Zero;
    //       foreach (var trackResults in _raceResults)
    //           total = total + (trackResults.FinishTime ?? TimeSpan.Zero);
    //       return total;
    //   }
    //
    //   int GetRacerTotalPoints(SimRaceCarDriver racer)             // 0x3954214
    //   {
    //       var total = 0;
    //       foreach (var trackResults in _raceResults)
    //           if (trackResults.CompetitorScores.TryGetValue(racer, out var result))
    //               total += result.Score;
    //       return total;
    //   }
    //
    //   TimeSpan GetRacerTotalTime(SimRaceCarDriver racer)          // 0x39543bc
    //   {
    //       var total = TimeSpan.Zero;
    //       foreach (var trackResults in _raceResults)
    //           total = total + (trackResults.CompetitorScores[racer].FinishTime ?? TimeSpan.Zero);
    //       return total;
    //   }
    //
    //   int GetCurrentPlacement()                                   // 0x3954624
    //   {
    //       var playerPoints = GetPlayerTotalPoints();
    //       var playerTime = GetPlayerTotalTime();
    //       var placement = 1;
    //       foreach (var racer in raceCupRegistration.simOpponents)
    //       {
    //           var racerPoints = GetRacerTotalPoints(racer);
    //           var racerTime = GetRacerTotalTime(racer);
    //           if (racerPoints > playerPoints) { placement += 1; continue; }
    //           if (racerPoints != playerPoints) continue;
    //           if (playerTime > racerTime) placement += 1;        // tie on points: slower player ranks below
    //       }
    //       return placement;
    //   }
    //
    // All enumeration uses the try/finally GetEnumerator/MoveNext/Dispose shape; every generic
    // instantiation is re-instantiated with concrete arguments (never the open !0/!1 forms that
    // caused build 44's DataModelBuilder crash — see RacingCupMethodRepairs.cs).

    public static void FixGetPlayerTotalPoints(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var listField = Field(module, owner, "_raceResults");
        var listType = (GenericInstanceType)listField.FieldType;          // List<TrackResults>
        var itemType = listType.GenericArguments[0];                    // TrackResults
        var scoreField = Field(module, itemType, "Score");

        var getEnumerator = ImportMethod(module, listType, "GetEnumerator", 0, true);
        var enumeratorType = GenericType((GenericInstanceType)getEnumerator.ReturnType, listType.GenericArguments.ToArray());
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);

        var body = FreshBody(method);
        var il = body.Instructions;
        var enumerator = new VariableDefinition(enumeratorType);
        var current = new VariableDefinition(itemType);
        var total = new VariableDefinition(module.TypeSystem.Int32);
        body.Variables.Add(enumerator);
        body.Variables.Add(current);
        body.Variables.Add(total);

        // total = 0;
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, total));

        // foreach (var trackResults in _raceResults) total += trackResults.Score;
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, listField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));

        EmitPointSumLoop(il, body, enumerator, current, total, scoreField,
            getCurrent, moveNext, dispose);

        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static void EmitPointSumLoop(Mono.Collections.Generic.Collection<Instruction> il, MethodBody body,
        VariableDefinition enumerator, VariableDefinition current, VariableDefinition total,
        FieldReference scoreField, MethodReference getCurrent, MethodReference moveNext, MethodReference dispose)
    {
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Ldloc, total);

        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, current));
        il.Add(Instruction.Create(OpCodes.Ldloc, current));
        il.Add(Instruction.Create(OpCodes.Ldfld, scoreField));
        il.Add(Instruction.Create(OpCodes.Ldloc, total));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, total));

        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, done));

        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));

        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = loop,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = done
        });

        il.Add(done);
    }

    public static void FixGetRacerTotalPoints(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var listField = Field(module, owner, "_raceResults");
        var listType = (GenericInstanceType)listField.FieldType;
        var itemType = listType.GenericArguments[0];
        var scoresField = Field(module, itemType, "CompetitorScores");
        var scoresType = (GenericInstanceType)scoresField.FieldType;      // Dictionary<SimRaceCarDriver,TrackResults>
        var scoreField = Field(module, itemType, "Score");
        var tryGetValue = ImportMethod(module, scoresType, "TryGetValue", 2, true);

        var getEnumerator = ImportMethod(module, listType, "GetEnumerator", 0, true);
        var enumeratorType = GenericType((GenericInstanceType)getEnumerator.ReturnType, listType.GenericArguments.ToArray());
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);

        var body = FreshBody(method);
        var il = body.Instructions;
        var enumerator = new VariableDefinition(enumeratorType);
        var current = new VariableDefinition(itemType);
        var result = new VariableDefinition(itemType);
        var total = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var v in new[] { enumerator, current, result, total }) body.Variables.Add(v);

        // total = 0;
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, total));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, listField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));

        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Ldloc, total);

        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, current));
        // if (current.CompetitorScores.TryGetValue(racer, out result)) total += result.Score;
        il.Add(Instruction.Create(OpCodes.Ldloc, current));
        il.Add(Instruction.Create(OpCodes.Ldfld, scoresField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldloca_S, result));
        il.Add(Instruction.Create(OpCodes.Callvirt, tryGetValue));
        il.Add(Instruction.Create(OpCodes.Brfalse, loopCheck));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldfld, scoreField));
        il.Add(Instruction.Create(OpCodes.Ldloc, total));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, total));

        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, done));

        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));

        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = loop,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = done
        });

        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixGetPlayerTotalTime(MethodDefinition method)
    {
        FixTotalTime(method, includeRacer: false);
    }

    public static void FixGetRacerTotalTime(MethodDefinition method)
    {
        FixTotalTime(method, includeRacer: true);
    }

    private static void FixTotalTime(MethodDefinition method, bool includeRacer)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var listField = Field(module, owner, "_raceResults");
        var listType = (GenericInstanceType)listField.FieldType;
        var itemType = listType.GenericArguments[0];
        var finishTimeField = Field(module, itemType, "FinishTime");     // Nullable<TimeSpan>
        var finishTimeType = (GenericInstanceType)finishTimeField.FieldType;
        var timeSpanType = finishTimeType.GenericArguments[0];

        MethodReference getItem = null;
        FieldReference scoresField = null;
        if (includeRacer)
        {
            scoresField = Field(module, itemType, "CompetitorScores");
            getItem = ImportMethod(module, (GenericInstanceType)scoresField.FieldType, "get_Item", 1, true);
        }

        var getEnumerator = ImportMethod(module, listType, "GetEnumerator", 0, true);
        var enumeratorType = GenericType((GenericInstanceType)getEnumerator.ReturnType, listType.GenericArguments.ToArray());
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);

        var hasValue = ImportMethod(module, finishTimeType, "get_HasValue", 0, true);
        var getValueOrDefault = ImportMethod(module, finishTimeType, "GetValueOrDefault", 0, true);
        var zeroField = Field(module, timeSpanType, "Zero");
        var opAddition = ImportMethod(module, timeSpanType, "op_Addition", 2, false,
            "System.TimeSpan", "System.TimeSpan");

        var body = FreshBody(method);
        var il = body.Instructions;
        var enumerator = new VariableDefinition(enumeratorType);
        var current = new VariableDefinition(itemType);
        var entry = new VariableDefinition(itemType);
        var finishTime = new VariableDefinition(finishTimeType);
        var total = new VariableDefinition(timeSpanType);
        foreach (var v in new[] { enumerator, current, entry, finishTime, total }) body.Variables.Add(v);

        // total = TimeSpan.Zero;
        il.Add(Instruction.Create(OpCodes.Ldsfld, zeroField));
        il.Add(Instruction.Create(OpCodes.Stloc, total));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, listField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));

        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Ldloc, total);

        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, current));

        // entry = current[.CompetitorScores[racer]]
        if (includeRacer)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, current));
            il.Add(Instruction.Create(OpCodes.Ldfld, scoresField));
            il.Add(Instruction.Create(OpCodes.Ldarg_1));
            il.Add(Instruction.Create(OpCodes.Callvirt, getItem));
            il.Add(Instruction.Create(OpCodes.Stloc, entry));
        }
        else
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, current));
            il.Add(Instruction.Create(OpCodes.Stloc, entry));
        }

        // finishTime = entry.FinishTime;
        il.Add(Instruction.Create(OpCodes.Ldloc, entry));
        il.Add(Instruction.Create(OpCodes.Ldfld, finishTimeField));
        il.Add(Instruction.Create(OpCodes.Stloc, finishTime));

        // total = total + (finishTime ?? TimeSpan.Zero);
        il.Add(Instruction.Create(OpCodes.Ldloc, total));
        var withValue = Instruction.Create(OpCodes.Ldloca_S, finishTime);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, finishTime));
        il.Add(Instruction.Create(OpCodes.Call, hasValue));
        il.Add(Instruction.Create(OpCodes.Brtrue, withValue));
        il.Add(Instruction.Create(OpCodes.Ldsfld, zeroField));
        var join = Instruction.Create(OpCodes.Call, opAddition);
        il.Add(Instruction.Create(OpCodes.Br, join));
        il.Add(withValue);
        il.Add(Instruction.Create(OpCodes.Call, getValueOrDefault));
        il.Add(join);
        il.Add(Instruction.Create(OpCodes.Stloc, total));

        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, done));

        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));

        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = loop,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = done
        });

        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixGetCurrentPlacement(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var getPlayerTotalPoints = ImportMethod(module, owner, "GetPlayerTotalPoints", 0, true);
        var getPlayerTotalTime = ImportMethod(module, owner, "GetPlayerTotalTime", 0, true);
        var getRacerTotalPoints = ImportMethod(module, owner, "GetRacerTotalPoints", 1, true);
        var getRacerTotalTime = ImportMethod(module, owner, "GetRacerTotalTime", 1, true);

        var registrationField = Field(module, owner, "raceCupRegistration");
        var opponentsField = Field(module, registrationField.FieldType, "simOpponents");
        var opponentsType = (GenericInstanceType)opponentsField.FieldType;   // List<SimRaceCarDriver>
        var driverType = opponentsType.GenericArguments[0];

        var timeSpanType = getPlayerTotalTime.ReturnType;
        var opGreaterThan = ImportMethod(module, timeSpanType, "op_GreaterThan", 2, false,
            "System.TimeSpan", "System.TimeSpan");

        var getEnumerator = ImportMethod(module, opponentsType, "GetEnumerator", 0, true);
        var enumeratorType = GenericType((GenericInstanceType)getEnumerator.ReturnType, opponentsType.GenericArguments.ToArray());
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);

        var body = FreshBody(method);
        var il = body.Instructions;
        var playerPoints = new VariableDefinition(module.TypeSystem.Int32);
        var playerTime = new VariableDefinition(timeSpanType);
        var placement = new VariableDefinition(module.TypeSystem.Int32);
        var enumerator = new VariableDefinition(enumeratorType);
        var racer = new VariableDefinition(driverType);
        var racerPoints = new VariableDefinition(module.TypeSystem.Int32);
        var racerTime = new VariableDefinition(timeSpanType);
        foreach (var v in new[] { playerPoints, playerTime, placement, enumerator, racer, racerPoints, racerTime })
            body.Variables.Add(v);

        // playerPoints = GetPlayerTotalPoints(); playerTime = GetPlayerTotalTime();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getPlayerTotalPoints));
        il.Add(Instruction.Create(OpCodes.Stloc, playerPoints));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getPlayerTotalTime));
        il.Add(Instruction.Create(OpCodes.Stloc, playerTime));

        // placement = 1;
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, placement));

        // foreach (var racer in raceCupRegistration.simOpponents)
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldflda, registrationField));
        il.Add(Instruction.Create(OpCodes.Ldfld, opponentsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));

        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Ldloc, placement);

        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, racer));

        // racerPoints = GetRacerTotalPoints(racer); racerTime = GetRacerTotalTime(racer);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, racer));
        il.Add(Instruction.Create(OpCodes.Call, getRacerTotalPoints));
        il.Add(Instruction.Create(OpCodes.Stloc, racerPoints));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, racer));
        il.Add(Instruction.Create(OpCodes.Call, getRacerTotalTime));
        il.Add(Instruction.Create(OpCodes.Stloc, racerTime));

        // if (racerPoints > playerPoints) { placement += 1; }
        // else if (racerPoints == playerPoints && playerTime > racerTime) placement += 1;
        var incrementOne = Instruction.Create(OpCodes.Ldc_I4_1);
        il.Add(Instruction.Create(OpCodes.Ldloc, racerPoints));
        il.Add(Instruction.Create(OpCodes.Ldloc, playerPoints));
        il.Add(Instruction.Create(OpCodes.Bgt, incrementOne));
        il.Add(Instruction.Create(OpCodes.Ldloc, racerPoints));
        il.Add(Instruction.Create(OpCodes.Ldloc, playerPoints));
        il.Add(Instruction.Create(OpCodes.Bne_Un, loopCheck));
        il.Add(Instruction.Create(OpCodes.Ldloc, playerTime));
        il.Add(Instruction.Create(OpCodes.Ldloc, racerTime));
        il.Add(Instruction.Create(OpCodes.Call, opGreaterThan));
        var addIncrement = Instruction.Create(OpCodes.Ldloc, placement);
        il.Add(Instruction.Create(OpCodes.Br, addIncrement));
        il.Add(incrementOne);
        il.Add(addIncrement);
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, placement));

        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, done));

        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));

        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = loop,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = done
        });

        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static GenericInstanceType GenericType(GenericInstanceType template, params TypeReference[] arguments)
    {
        var elementType = template is TypeSpecification specification ? specification.ElementType : template;
        var instance = new GenericInstanceType(elementType);
        foreach (var argument in arguments) instance.GenericArguments.Add(argument);
        return instance;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name,
        int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.Single(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var field = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields).Single(candidate => candidate.Name == name);
        return module.ImportReference(field);
    }

    private static IEnumerable<TypeDefinition> Hierarchy(TypeDefinition type)
    {
        for (var current = type; current is not null; current = current.BaseType?.Resolve()) yield return current;
    }
}
