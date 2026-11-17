using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class OutdoorMethodRepairs
{
    public static void FixCaveButtonIndex(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var closureType = owner.NestedTypes.Single(type => type.Name == "<>c__DisplayClass17_0");
        var closureConstructor = ImportMethod(module, closureType, ".ctor", 0, true);
        var buttonNameField = Field(module, closureType, "buttonName");
        var predicate = ImportMethod(module, closureType, "<GetButtonIndex>b__0", 1, true);
        var riskButtonField = Field(module, owner, "_riskButton");
        var riskButtonType = (GenericInstanceType)riskButtonField.FieldType;
        var tupleType = (GenericInstanceType)riskButtonType.GenericArguments[0];
        var funcType = GenericType(FrameworkType(module, "System", "Func`2"), tupleType, module.TypeSystem.Boolean);
        var funcConstructor = ImportMethod(module, funcType, ".ctor", 2, true);
        var firstOrDefault = LinqMethod(module, "FirstOrDefault", 2, tupleType);
        var item1Field = Field(module, tupleType, "Item1");
        item1Field.DeclaringType = tupleType;

        var body = FreshBody(method);
        var closure = new VariableDefinition(closureType);
        var tuple = new VariableDefinition(tupleType);
        body.Variables.Add(closure);
        body.Variables.Add(tuple);
        var il = body.Instructions;

        // Native 0x38c03c4: locate the choice by its ordinal-ignore-case title.
        il.Add(Instruction.Create(OpCodes.Newobj, closureConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, closure));
        il.Add(Instruction.Create(OpCodes.Ldloc, closure));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Stfld, buttonNameField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, riskButtonField));
        il.Add(Instruction.Create(OpCodes.Ldloc, closure));
        il.Add(Instruction.Create(OpCodes.Ldftn, predicate));
        il.Add(Instruction.Create(OpCodes.Newobj, funcConstructor));
        il.Add(Instruction.Create(OpCodes.Call, firstOrDefault));
        il.Add(Instruction.Create(OpCodes.Stloc, tuple));
        il.Add(Instruction.Create(OpCodes.Ldloc, tuple));
        il.Add(Instruction.Create(OpCodes.Ldfld, item1Field));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Ceq));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ceq));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixRandomOutdoorAnimal(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var animalSetField = Field(module, owner, "_outdoorAnimalSet");
        var animalSetType = (GenericInstanceType)animalSetField.FieldType;
        var animalType = animalSetType.GenericArguments[0];
        var loadAnimalSet = ImportMethod(module, owner, "LoadOutdoorAnimalSet", 0, false);
        var getHabitat = ImportMethod(module, animalType, "get_Habitat", 0, true);
        var getCommonnessScore = ImportMethod(module, animalType, "get_CommonnessScore", 0, true);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 2, true,
            "System.String", "System.StringComparison");
        var listConstructor = ImportMethod(module, animalSetType, ".ctor", 0, true);
        var add = ImportMethod(module, animalSetType, "Add", 1, true);
        var getCount = ImportMethod(module, animalSetType, "get_Count", 0, true);
        var getItem = ImportMethod(module, animalSetType, "get_Item", 1, true);
        var getEnumerator = ImportMethod(module, animalSetType, "GetEnumerator", 0, true);
        var enumeratorType = ListEnumeratorType((GenericInstanceType)getEnumerator.ReturnType, animalType);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var getRandomNumber = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false,
            "System.Int32", "System.Int32");

        var body = FreshBody(method);
        var candidates = new VariableDefinition(animalSetType);
        var enumerator = new VariableDefinition(enumeratorType);
        var animal = new VariableDefinition(animalType);
        var weightIndex = new VariableDefinition(module.TypeSystem.Int32);
        var randomIndex = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { candidates, enumerator, animal, weightIndex, randomIndex }) body.Variables.Add(local);
        var il = body.Instructions;
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var nextAnimal = Instruction.Create(OpCodes.Nop);
        var weightLoop = Instruction.Create(OpCodes.Nop);
        var weightCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Nop);

        // Native 0x3d2e73c: habitat filtering and CommonnessScore-weighted selection.
        il.Add(Instruction.Create(OpCodes.Call, loadAnimalSet));
        il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldsfld, animalSetField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, animal));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Call, getHabitat));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Callvirt, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextAnimal));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, weightIndex));
        il.Add(Instruction.Create(OpCodes.Br, weightCheck));
        il.Add(weightLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Callvirt, add));
        il.Add(Instruction.Create(OpCodes.Ldloc, weightIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, weightIndex));
        il.Add(weightCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, weightIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Call, getCommonnessScore));
        il.Add(Instruction.Create(OpCodes.Blt, weightLoop));
        il.Add(nextAnimal);
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, done));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
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
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        il.Add(Instruction.Create(OpCodes.Stloc, randomIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldloc, randomIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, getItem));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static GenericInstanceMethod LinqMethod(ModuleDefinition module, string name, int parameterCount,
        params TypeReference[] arguments)
    {
        var type = FrameworkType(module, "System.Linq", "Enumerable");
        var definition = type.Resolve().Methods.Single(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount);
        return GenericMethod(module, definition, arguments);
    }

    private static GenericInstanceMethod GenericMethod(ModuleDefinition module, MethodReference definition,
        params TypeReference[] arguments)
    {
        var method = new GenericInstanceMethod(module.ImportReference(definition.Resolve()));
        foreach (var argument in arguments) method.GenericArguments.Add(argument);
        return method;
    }

    private static GenericInstanceType GenericType(TypeReference template, params TypeReference[] arguments)
    {
        var elementType = template is TypeSpecification specification ? specification.ElementType : template;
        var instance = new GenericInstanceType(elementType);
        foreach (var argument in arguments) instance.GenericArguments.Add(argument);
        return instance;
    }

    private static GenericInstanceType ListEnumeratorType(GenericInstanceType template, TypeReference argument)
    {
        return GenericType(template, argument);
    }

    private static TypeReference FrameworkType(ModuleDefinition module, string typeNamespace, string typeName)
    {
        var scope = module.TypeSystem.Object.Scope;
        if (typeNamespace == "System.Linq")
            scope = module.AssemblyReferences.Single(reference => reference.Name == "System.Core");
        return new TypeReference(typeNamespace, typeName, module, scope);
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
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName)
                .SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName,
        int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = FrameworkType(module, typeName.Substring(0, separator), typeName.Substring(separator + 1));
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var field = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields)
            .Single(candidate => candidate.Name == name);
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
