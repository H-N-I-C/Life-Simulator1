using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class RacingTrophyMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var module = method.Module;
        var closureType = method.Parameters[2].ParameterType.GetElementType();
        var listItemsField = Field(module, closureType, "listItems");
        var racingField = Field(module, closureType, "racing");
        var racingType = racingField.FieldType;
        var cupListType = (GenericInstanceType)method.Parameters[0].ParameterType;
        var cupType = cupListType.GenericArguments[0];
        var listItemType = FindType(module, "ListItem");

        var getCount = ImportMethod(module, cupListType, "get_Count", 0, true);
        var getEnumerator = ImportMethod(module, cupListType, "GetEnumerator", 0, true);
        var enumeratorType = ListEnumeratorType((GenericInstanceType)getEnumerator.ReturnType, cupType);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var add = ImportMethod(module, listItemsField.FieldType, "Add", 1, true);

        var dividerConstructor = ImportMethod(module, listItemType, ".ctor", 1, true, "System.String");
        var scenarioType = FindType(module, "ScenarioRacingTrophyCaseProfile");
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 2, true, racingType.FullName, cupType.FullName);
        var nameField = Field(module, cupType, "<Name>k__BackingField");
        var bestPlacementField = Field(module, cupType, "<BestPlacement>k__BackingField");
        var itemConstructor = ImportMethod(module, listItemType, ".ctor", 6, true,
            "System.String", "System.String", "Scenario", "System.String", "System.String", "System.Boolean");
        var getPlacementString = ImportMethod(module, racingType, "GetPlacementString", 1, false, "System.Int32");
        var concat = FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String");
        var secondLineField = Field(module, listItemType, "SecondLine");

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var enumerator = new VariableDefinition(enumeratorType);
        var cup = new VariableDefinition(cupType);
        var scenario = new VariableDefinition(scenarioType);
        var listItem = new VariableDefinition(listItemType);
        var addResult = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { enumerator, cup, scenario, listItem, addResult })
            body.Variables.Add(local);

        var il = body.Instructions;
        var enumerate = Instruction.Create(OpCodes.Nop);
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Nop);

        // Native 0x46f0708-0x46f0750: add a divider only for a non-null, non-empty list.
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Brfalse, enumerate));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, enumerate));
        il.Add(Instruction.Create(OpCodes.Ldarg_2));
        il.Add(Instruction.Create(OpCodes.Ldfld, listItemsField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Newobj, dividerConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, add));
        il.Add(Instruction.Create(OpCodes.Stloc, addResult));

        il.Add(enumerate);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, cup));

        // Native 0x46f07c0-0x46f0818: create the profile scenario and trophy ListItem.
        il.Add(Instruction.Create(OpCodes.Ldarg_2));
        il.Add(Instruction.Create(OpCodes.Ldfld, racingField));
        il.Add(Instruction.Create(OpCodes.Ldloc, cup));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, scenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, cup));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, "trophy"));
        il.Add(Instruction.Create(OpCodes.Ldloc, scenario));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Newobj, itemConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, listItem));

        // Native 0x46f081c-0x46f0854: set the placement line and add the item.
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, cup));
        il.Add(Instruction.Create(OpCodes.Ldfld, bestPlacementField));
        il.Add(Instruction.Create(OpCodes.Call, getPlacementString));
        il.Add(Instruction.Create(OpCodes.Ldstr, " Place"));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Stfld, secondLineField));
        il.Add(Instruction.Create(OpCodes.Ldarg_2));
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
