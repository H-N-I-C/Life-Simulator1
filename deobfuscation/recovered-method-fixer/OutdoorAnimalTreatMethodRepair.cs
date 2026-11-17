using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class OutdoorAnimalTreatMethodRepair
{
    // Recovered from the native arm64 body at 0x3d2f06c.
    //
    //   if (_outdoorAnimalTreatSet == null) LoadOutdoorAnimalTreatSet();
    //   var candidates = new List<DataOutdoorAnimalTreat>();
    //   foreach (var treat in _outdoorAnimalTreatSet)
    //   {
    //       if (treat.CommonnessScore < 1) continue;              // ldr w8,[x21,#0x1c]; cmp w8,#1; b.lt
    //       if (treat.AnimalDangerScore > score) continue;        // ldr w9,[x21,#0x18]; cmp w9,w20; b.gt
    //       for (var i = 0; i < treat.CommonnessScore; i++)       // weight loop at 0x3d2f230
    //           candidates.Add(treat);
    //   }
    //   return candidates[GameReference.GetRandomNumber(0, candidates.Count)];
    //
    // The enumerator is only ever used by address (ldloca) inside try/finally, which is exactly
    // what the Cpp2IL placeholder body got wrong (it pushed the Enumerator struct by value).
    public static void FixRandomOutdoorAnimalTreat(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var setField = Field(module, owner, "_outdoorAnimalTreatSet");
        var setType = (GenericInstanceType)setField.FieldType;
        var treatType = setType.GenericArguments[0];

        var loadSet = ImportMethod(module, owner, "LoadOutdoorAnimalTreatSet", 0, false);
        var dangerScore = ImportMethod(module, treatType, "get_AnimalDangerScore", 0, true);
        var commonness = ImportMethod(module, treatType, "get_CommonnessScore", 0, true);
        var setConstructor = ImportMethod(module, setType, ".ctor", 0, true);
        var add = ImportMethod(module, setType, "Add", 1, true);
        var getCount = ImportMethod(module, setType, "get_Count", 0, true);
        var getItem = ImportMethod(module, setType, "get_Item", 1, true);
        var getEnumerator = ImportMethod(module, setType, "GetEnumerator", 0, true);
        var enumeratorType = GenericType((GenericInstanceType)getEnumerator.ReturnType, treatType);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var getRandomNumber = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false,
            "System.Int32", "System.Int32");

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var candidates = new VariableDefinition(setType);
        var enumerator = new VariableDefinition(enumeratorType);
        var treat = new VariableDefinition(treatType);
        var weightIndex = new VariableDefinition(module.TypeSystem.Int32);
        var randomIndex = new VariableDefinition(module.TypeSystem.Int32);
        body.Variables.Add(candidates);
        body.Variables.Add(enumerator);
        body.Variables.Add(treat);
        body.Variables.Add(weightIndex);
        body.Variables.Add(randomIndex);

        var il = body.Instructions;
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var nextTreat = Instruction.Create(OpCodes.Nop);
        var weightLoop = Instruction.Create(OpCodes.Nop);
        var weightCheck = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Nop);

        // Mirrors the working sibling DataHub::GetRandomOutdoorAnimal: load unconditionally
        // rather than branching first, which is the only structural difference between the two
        // bodies and the current suspect for IL2CPP's resolver crash (see HANDOFF 17).
        il.Add(Instruction.Create(OpCodes.Call, loadSet));

        il.Add(Instruction.Create(OpCodes.Newobj, setConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, candidates));

        il.Add(Instruction.Create(OpCodes.Ldsfld, setField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));

        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, treat));

        // CommonnessScore < 1 -> skip this treat.
        il.Add(Instruction.Create(OpCodes.Ldloc, treat));
        il.Add(Instruction.Create(OpCodes.Callvirt, commonness));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, nextTreat));

        // AnimalDangerScore > score -> skip this treat.
        il.Add(Instruction.Create(OpCodes.Ldloc, treat));
        il.Add(Instruction.Create(OpCodes.Callvirt, dangerScore));
        // score is the only parameter and the method is static, so it is argument 0.
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Bgt, nextTreat));

        // Weight loop: add the treat CommonnessScore times.
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, weightIndex));
        il.Add(Instruction.Create(OpCodes.Br, weightCheck));
        il.Add(weightLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldloc, treat));
        il.Add(Instruction.Create(OpCodes.Callvirt, add));
        il.Add(Instruction.Create(OpCodes.Ldloc, weightIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, weightIndex));
        il.Add(weightCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, weightIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, treat));
        il.Add(Instruction.Create(OpCodes.Callvirt, commonness));
        il.Add(Instruction.Create(OpCodes.Blt, weightLoop));

        il.Add(nextTreat);
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

    private static GenericInstanceType GenericType(TypeReference template, params TypeReference[] arguments)
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
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName)
                .SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
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
