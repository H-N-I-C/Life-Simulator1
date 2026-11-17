using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class RacingTrackMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var module = method.Module;
        var closureType = method.Parameters[1].ParameterType.GetElementType();
        var racingField = Field(module, closureType, "racing");
        var listItemField = Field(module, closureType, "listItem");
        var listItemsField = Field(module, closureType, "listItems");
        var racingType = racingField.FieldType;
        var tracksField = Field(module, racingType, "<Tracks>k__BackingField");
        var tracksType = (GenericInstanceType)tracksField.FieldType;
        var trackType = tracksType.GenericArguments[0];
        var listItemType = listItemField.FieldType;

        var compilerClass = method.DeclaringType.NestedTypes.Single(type => type.Name == "<>c");
        var selectorCache = Field(module, compilerClass, "<>9__318_1");
        var selectorSingleton = Field(module, compilerClass, "<>9");
        var selector = ImportMethod(module, compilerClass, "<GetRacingTrackRecordList>b__318_1", 1, true);
        var selectorType = (GenericInstanceType)selectorCache.FieldType;
        var selectorConstructor = ImportMethod(module, selectorType, ".ctor", 2, true);

        var existingReferences = method.Body.Instructions.Select(instruction => instruction.Operand).OfType<MethodReference>().ToArray();
        var orderBy = GenericMethod(module, existingReferences.Single(candidate => candidate.Name == "OrderBy" && candidate.Parameters.Count == 2), trackType, module.TypeSystem.Int32);
        var toList = GenericMethod(module, existingReferences.Single(candidate => candidate.Name == "ToList" && candidate.Parameters.Count == 1), trackType);

        var tracksConstructorDefinition = tracksType.Resolve().Methods.Single(candidate =>
            candidate.Name == ".ctor" && candidate.Parameters.Count == 1 &&
            candidate.Parameters[0].ParameterType is GenericInstanceType);
        var tracksConstructor = module.ImportReference(tracksConstructorDefinition);
        tracksConstructor.DeclaringType = tracksType;
        var getEnumerator = ImportMethod(module, tracksType, "GetEnumerator", 0, true);
        var enumeratorType = ListEnumeratorType((GenericInstanceType)getEnumerator.ReturnType, trackType);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var add = ImportMethod(module, listItemsField.FieldType, "Add", 1, true);

        var getCategoryName = ImportMethod(module, racingType, "GetTrackCategoryString", 1, false);
        var capitalize = ImportMethod(module, FindType(module, "GameReference"), "CapitalizeString", 1, false);
        var concat = FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String");
        var dividerConstructor = ImportMethod(module, listItemType, ".ctor", 1, true, "System.String");

        var getTrackData = ImportMethod(module, trackType, "get_TrackData", 0, true);
        var trackDataType = getTrackData.ReturnType;
        var trackTypeField = Field(module, trackDataType, "<TrackType>k__BackingField");
        var trackNameField = Field(module, trackDataType, "<TrackName>k__BackingField");
        var isUnlocked = ImportMethod(module, trackType, "IsUnlocked", 2, true);
        var recordScenarioType = FindType(module, "ScenarioRacingTrackRecordTrack");
        var recordScenarioConstructor = ImportMethod(module, recordScenarioType, ".ctor", 1, true, trackType.FullName);
        var emojiForTrack = ImportMethod(module, FindType(module, "EmojiHub"), "EmojiForRacingTrack", 1, false, trackType.FullName);
        var recordItemConstructor = ImportMethod(module, listItemType, ".ctor", 6, true,
            "System.String", "System.String", "Scenario", "System.String", "System.String", "System.Boolean");
        var hasFinishedField = Field(module, trackType, "<HasFinishedRaceOnce>k__BackingField");
        var getBestRaceTime = ImportMethod(module, trackType, "GetBestRaceTime", 0, true);
        var timeSpanToString = ImportMethod(module, getBestRaceTime.ReturnType, "ToString", 1, true, "System.String");
        var secondLineField = Field(module, listItemType, "SecondLine");
        var disabledField = Field(module, listItemType, "Disabled");

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var tracks = new VariableDefinition(tracksType);
        var selectorLocal = new VariableDefinition(selectorType);
        var enumerator = new VariableDefinition(enumeratorType);
        var track = new VariableDefinition(trackType);
        var trackData = new VariableDefinition(trackDataType);
        var scenario = new VariableDefinition(recordScenarioType);
        var listItem = new VariableDefinition(listItemType);
        var secondLine = new VariableDefinition(module.TypeSystem.String);
        var bestRaceTime = new VariableDefinition(getBestRaceTime.ReturnType);
        var addResult = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { tracks, selectorLocal, enumerator, track, trackData, scenario, listItem, secondLine, bestRaceTime, addResult })
            body.Variables.Add(local);

        var il = body.Instructions;
        var selectorReady = Instruction.Create(OpCodes.Nop);
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var noRaceRecord = Instruction.Create(OpCodes.Nop);
        var assignSecondLine = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Nop);

        // Native 0x46f026c-0x46f0338: copy racing.Tracks, OrderBy(b__318_1), ToList.
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldfld, racingField));
        il.Add(Instruction.Create(OpCodes.Ldfld, tracksField));
        il.Add(Instruction.Create(OpCodes.Newobj, tracksConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, tracks));
        il.Add(Instruction.Create(OpCodes.Ldsfld, selectorCache));
        il.Add(Instruction.Create(OpCodes.Stloc, selectorLocal));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectorLocal));
        il.Add(Instruction.Create(OpCodes.Brtrue, selectorReady));
        il.Add(Instruction.Create(OpCodes.Ldsfld, selectorSingleton));
        il.Add(Instruction.Create(OpCodes.Ldftn, selector));
        il.Add(Instruction.Create(OpCodes.Newobj, selectorConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, selectorLocal));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectorLocal));
        il.Add(Instruction.Create(OpCodes.Stsfld, selectorCache));
        il.Add(selectorReady);
        il.Add(Instruction.Create(OpCodes.Ldloc, tracks));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectorLocal));
        il.Add(Instruction.Create(OpCodes.Call, orderBy));
        il.Add(Instruction.Create(OpCodes.Call, toList));
        il.Add(Instruction.Create(OpCodes.Stloc, tracks));

        // Native 0x46f033c-0x46f03b0: create and add the capitalized category divider.
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getCategoryName));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Ldstr, " Tracks"));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Newobj, dividerConstructor));
        il.Add(Instruction.Create(OpCodes.Stfld, listItemField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldfld, listItemsField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldfld, listItemField));
        il.Add(Instruction.Create(OpCodes.Callvirt, add));
        il.Add(Instruction.Create(OpCodes.Stloc, addResult));

        il.Add(Instruction.Create(OpCodes.Ldloc, tracks));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, track));

        // Native 0x46f0418-0x46f0448: category equality followed by IsUnlocked.
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Callvirt, getTrackData));
        il.Add(Instruction.Create(OpCodes.Stloc, trackData));
        il.Add(Instruction.Create(OpCodes.Ldloc, trackData));
        il.Add(Instruction.Create(OpCodes.Ldfld, trackTypeField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Bne_Un, loopCheck));
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldfld, racingField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, isUnlocked));
        il.Add(Instruction.Create(OpCodes.Brfalse, loopCheck));

        // Native 0x46f044c-0x46f04b8: construct the profile scenario and ListItem.
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Newobj, recordScenarioConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, scenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Callvirt, getTrackData));
        il.Add(Instruction.Create(OpCodes.Stloc, trackData));
        il.Add(Instruction.Create(OpCodes.Ldloc, trackData));
        il.Add(Instruction.Create(OpCodes.Ldfld, trackNameField));
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Call, emojiForTrack));
        il.Add(Instruction.Create(OpCodes.Ldloc, scenario));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Newobj, recordItemConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, listItem));

        // Native 0x46f04bc-0x46f053c: best-time text or the no-record prompt; Disabled = !finished.
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Ldfld, hasFinishedField));
        il.Add(Instruction.Create(OpCodes.Brfalse, noRaceRecord));
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Callvirt, getBestRaceTime));
        il.Add(Instruction.Create(OpCodes.Stloc, bestRaceTime));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Best Time: "));
        il.Add(Instruction.Create(OpCodes.Ldloca, bestRaceTime));
        il.Add(Instruction.Create(OpCodes.Ldstr, "m\\:ss\\.f"));
        il.Add(Instruction.Create(OpCodes.Call, timeSpanToString));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Stloc, secondLine));
        il.Add(Instruction.Create(OpCodes.Br, assignSecondLine));
        il.Add(noRaceRecord);
        il.Add(Instruction.Create(OpCodes.Ldstr, "Race on this track to see your records!"));
        il.Add(Instruction.Create(OpCodes.Stloc, secondLine));
        il.Add(assignSecondLine);
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, secondLine));
        il.Add(Instruction.Create(OpCodes.Stfld, secondLineField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, track));
        il.Add(Instruction.Create(OpCodes.Ldfld, hasFinishedField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ceq));
        il.Add(Instruction.Create(OpCodes.Stfld, disabledField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldfld, listItemsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Callvirt, add));
        il.Add(Instruction.Create(OpCodes.Stloc, addResult));

        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, done));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(done);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = tryStart,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = done
        });
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static GenericInstanceMethod GenericMethod(ModuleDefinition module, MethodReference existing, params TypeReference[] arguments)
    {
        var method = new GenericInstanceMethod(module.ImportReference(existing.Resolve()));
        foreach (var argument in arguments) method.GenericArguments.Add(argument);
        return method;
    }

    private static GenericInstanceType ListEnumeratorType(GenericInstanceType template, TypeReference argument)
    {
        var instance = new GenericInstanceType(template.ElementType);
        instance.GenericArguments.Add(argument);
        return instance;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
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

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName, int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module, module.TypeSystem.Object.Scope);
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var field = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields).Single(candidate => candidate.Name == name);
        return module.ImportReference(field);
    }

    private static TypeDefinition FindType(ModuleDefinition module, string fullName)
    {
        return module.GetType(fullName) ?? module.Types.SelectMany(Flatten).Single(type => type.FullName == fullName);
    }

    private static IEnumerable<TypeDefinition> Hierarchy(TypeDefinition type)
    {
        for (var current = type; current is not null; current = current.BaseType?.Resolve()) yield return current;
    }

    private static IEnumerable<TypeDefinition> Flatten(TypeDefinition type)
    {
        yield return type;
        foreach (var nested in type.NestedTypes)
        foreach (var descendant in Flatten(nested)) yield return descendant;
    }
}
