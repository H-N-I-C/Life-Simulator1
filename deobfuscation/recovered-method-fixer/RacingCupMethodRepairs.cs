using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class RacingCupMethodRepairs
{
    // SimRaceCarDriver ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults results)
    // Native 0x3967d78.
    //
    // Native reading (independently transcribed in herdr-reports/racing-cup-crosscheck.md):
    //   var dict = results.competitorResults;            // ldr x0,[x19,#0x8]
    //   if (dict == null) throw NullReferenceException;  // cbz -> 0x1eca75c
    //   foreach (var pair in dict)                       // GetEnumerator/MoveNext/Dispose(0x5f64b5c)
    //   {
    //       if (pair.Value.Placement == 1)               // ldr w8,[sp,#0x28]; cmp w8,#1
    //       {
    //           result = pair.Key;                       // ldr x19,[sp,#0x20]
    //           break;
    //       }
    //   }
    //   return result;                                   // null if nobody has Placement 1
    //
    // The decompiler's defect is a generic-identity mismatch: it loads the field as
    // Dictionary<SimRaceCarDriver,RacingCupSeriesResults> but calls
    // Dictionary<System.Object,RacingCupSeriesResults>::GetEnumerator() on it, which is exactly the
    // "Cannot get stack type for RacingCupSeriesResults" rejection.
    public static void FixGetNpcWinningDriver(MethodDefinition method)
    {
        var module = method.Module;
        var resultsType = method.Parameters[0].ParameterType;
        var dictionaryField = Field(module, resultsType, "competitorResults");
        var dictionaryType = (GenericInstanceType)dictionaryField.FieldType;
        var pairType = ((GenericInstanceType)dictionaryType).GenericArguments[0]; // SimRaceCarDriver

        var getEnumerator = ImportMethod(module, dictionaryType, "GetEnumerator", 0, true);
        var arguments = dictionaryType.GenericArguments.ToArray();

        // IMPORTANT: GetEnumerator's return type is Dictionary`2/Enumerator<!0,!1>, whose !0/!1 belong to the
        // *Dictionary* definition. Using it verbatim as a local type leaves unresolved generic parameters in a
        // non-generic method, and IL2CPP's DataModelBuilder TypeReferenceResolver throws NullReferenceException on
        // them (build 44's regression). Re-instantiate with the concrete arguments, as OutdoorAnimalTreatMethodRepair does.
        var enumeratorType = GenericType((GenericInstanceType)getEnumerator.ReturnType, arguments);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var currentType = GenericType((GenericInstanceType)getCurrent.ReturnType, arguments);
        var getKey = ImportMethod(module, currentType, "get_Key", 0, true);
        var getValue = ImportMethod(module, currentType, "get_Value", 0, true);
        var placement = Field(module, resultsType, "Placement");

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var dictionary = new VariableDefinition(dictionaryType);
        var enumerator = new VariableDefinition(enumeratorType);
        var pair = new VariableDefinition(currentType);
        var result = new VariableDefinition(pairType);
        body.Variables.Add(dictionary);
        body.Variables.Add(enumerator);
        body.Variables.Add(pair);
        body.Variables.Add(result);

        var il = body.Instructions;
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Nop);

        il.Add(Instruction.Create(OpCodes.Ldarga_S, method.Parameters[0]));
        il.Add(Instruction.Create(OpCodes.Ldfld, dictionaryField));
        il.Add(Instruction.Create(OpCodes.Stloc, dictionary));

        // callvirt keeps the native null check: a null dictionary throws NullReferenceException here.
        il.Add(Instruction.Create(OpCodes.Ldloc, dictionary));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));

        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, result));

        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));

        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca_S, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, pair));

        il.Add(Instruction.Create(OpCodes.Ldloca_S, pair));
        il.Add(Instruction.Create(OpCodes.Call, getValue));
        il.Add(Instruction.Create(OpCodes.Ldfld, placement));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Bne_Un, loopCheck));

        il.Add(Instruction.Create(OpCodes.Ldloca_S, pair));
        il.Add(Instruction.Create(OpCodes.Call, getKey));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Leave, done));

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
            TryStart = tryStart,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = done
        });

        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static GenericInstanceType GenericType(GenericInstanceType template, params TypeReference[] arguments)
    {
        var instance = new GenericInstanceType(template.ElementType);
        foreach (var argument in arguments) instance.GenericArguments.Add(argument);
        return instance;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name,
        int parameterCount, bool instance, params string[] parameterTypes)
    {
        var candidates = declaringType.Resolve().Methods;
        var definition = candidates.Single(candidate =>
            candidate.Name == name &&
            candidate.Parameters.Count == parameterCount &&
            candidate.IsStatic != instance &&
            (parameterTypes.Length == 0 ||
             candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var definition = declaringType.Resolve();
        var field = definition.Fields.Single(candidate => candidate.Name == name);
        return module.ImportReference(field);
    }
}
