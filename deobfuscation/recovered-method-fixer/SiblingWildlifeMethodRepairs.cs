using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class SiblingWildlifeMethodRepairs
{
    public static void FixSquabbleSibling(MethodDefinition method)
    {
        var module = method.Module;
        var personType = FindType(module, "SimPerson");
        var siblingType = FindType(module, "SimSibling");
        var lifeType = FindType(module, "Life");
        var gameManager = FindType(module, "GameManager");
        var instField = Field(module, gameManager, "Inst");
        var lifeField = Field(module, gameManager, "Life");
        var heroField = Field(module, lifeType, "<Hero>k__BackingField");
        var aliveField = Field(module, personType, "<Alive>k__BackingField");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var petulanceField = Field(module, siblingType, "<Att_petulance>k__BackingField");
        var getSiblings = ImportMethod(module, lifeType, "get_SiblingArray", 0, true);
        var getPlaceOfResidence = ImportMethod(module, personType, "get_PlaceOfResidence", 0, true);
        var getCountry = ImportMethod(module, getPlaceOfResidence.ReturnType, "get_Country", 0, true);
        var countryNameField = Field(module, getCountry.ReturnType, "<Name>k__BackingField");
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 1, true, "System.String");
        var siblingsType = (GenericInstanceType)getSiblings.ReturnType;
        var getEnumerator = ImportMethod(module, siblingsType, "GetEnumerator", 0, true);
        var enumeratorType = ListEnumeratorType((GenericInstanceType)getEnumerator.ReturnType, siblingsType.GenericArguments[0]);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);

        var body = FreshBody(method);
        var selected = new VariableDefinition(siblingType);
        var siblings = new VariableDefinition(siblingsType);
        var enumerator = new VariableDefinition(enumeratorType);
        var candidate = new VariableDefinition(siblingType);
        foreach (var local in new[] { selected, siblings, enumerator, candidate }) body.Variables.Add(local);
        var il = body.Instructions;
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var nextSibling = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var disposalEnd = Instruction.Create(OpCodes.Nop);
        var loopDone = Instruction.Create(OpCodes.Nop);

        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Call, getSiblings));
        il.Add(Instruction.Create(OpCodes.Stloc, siblings));
        il.Add(Instruction.Create(OpCodes.Ldloc, siblings));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Castclass, siblingType));
        il.Add(Instruction.Create(OpCodes.Stloc, candidate));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
        il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextSibling));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Blt, nextSibling));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Ldfld, countryNameField));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Ldfld, countryNameField));
        il.Add(Instruction.Create(OpCodes.Callvirt, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextSibling));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
        il.Add(Instruction.Create(OpCodes.Ldfld, petulanceField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 20f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, nextSibling));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
        il.Add(Instruction.Create(OpCodes.Stloc, selected));
        il.Add(nextSibling);
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, loopDone));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(disposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = tryStart,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = disposalEnd
        });
        il.Add(loopDone);
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixWildlifeCountString(MethodDefinition method)
    {
        var module = method.Module;
        var closureType = method.Parameters[1].ParameterType.GetElementType();
        var outdoorLifestyleField = Field(module, closureType, "outdoorLifestyle");
        var handbookField = Field(module, outdoorLifestyleField.FieldType, "<Handbook>k__BackingField");
        var handbookType = handbookField.FieldType;
        var getFound = ImportMethod(module, handbookType, "GetFoundHandbookList", 1, true, "OUTDOOR_HANDBOOK_ENTRY_TYPE");
        var getCount = ImportMethod(module, getFound.ReturnType, "get_Count", 0, true);
        var getTotal = ImportMethod(module, handbookType, "GetTotalCountForType", 1, false, "OUTDOOR_HANDBOOK_ENTRY_TYPE");
        var intToString = FrameworkMethod(module, "System.Int32", "ToString", 0, true);
        var concat = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");

        var body = FreshBody(method);
        var foundCount = new VariableDefinition(module.TypeSystem.Int32);
        var totalCount = new VariableDefinition(module.TypeSystem.Int32);
        body.Variables.Add(foundCount);
        body.Variables.Add(totalCount);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldfld, outdoorLifestyleField));
        il.Add(Instruction.Create(OpCodes.Ldfld, handbookField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getFound));
        il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
        il.Add(Instruction.Create(OpCodes.Stloc, foundCount));
        il.Add(Instruction.Create(OpCodes.Ldloca, foundCount));
        il.Add(Instruction.Create(OpCodes.Call, intToString));
        il.Add(Instruction.Create(OpCodes.Ldstr, "/"));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getTotal));
        il.Add(Instruction.Create(OpCodes.Stloc, totalCount));
        il.Add(Instruction.Create(OpCodes.Ldloca, totalCount));
        il.Add(Instruction.Create(OpCodes.Call, intToString));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static GenericInstanceType ListEnumeratorType(GenericInstanceType template, TypeReference argument)
    {
        var instance = new GenericInstanceType(template.ElementType);
        instance.GenericArguments.Add(argument);
        return instance;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var match = type.Methods.SingleOrDefault(candidate =>
                candidate.Name == name && candidate.Parameters.Count == parameterCount &&
                (!instance.HasValue || candidate.IsStatic != instance.Value) &&
                (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
            if (match is null) continue;
            var reference = module.ImportReference(match);
            if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
            return reference;
        }
        throw new InvalidOperationException($"Method not found: {declaringType.FullName}::{name}/{parameterCount}");
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName, int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module, module.TypeSystem.Object.Scope);
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var field = type.Fields.SingleOrDefault(candidate => candidate.Name == name);
            if (field is not null) return module.ImportReference(field);
        }
        throw new InvalidOperationException($"Field not found: {declaringType.FullName}::{name}");
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
