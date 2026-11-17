using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class StarCombineMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var data1 = method.Parameters.Single(parameter => parameter.Name == "pData1");
        var data2 = method.Parameters.Single(parameter => parameter.Name == "pData2");
        var data2Offset = method.Parameters.Single(parameter => parameter.Name == "pData2Offset");
        var data2Size = method.Parameters.Single(parameter => parameter.Name == "pData2Size");
        var color = method.Parameters.Single(parameter => parameter.Name == "pColor");
        var changeColor = method.Parameters.Single(parameter => parameter.Name == "pChangeColor");
        var imposeAlphaLimits = method.Parameters.Single(parameter => parameter.Name == "imposeAlphaLimits");
        var color32Type = ((ArrayType)data1.ParameterType).ElementType;
        var sizeField = Field(module, owner, "_size");
        var vector2Type = sizeField.FieldType;
        var vector2X = Field(module, vector2Type, "x");
        var vector2Y = Field(module, vector2Type, "y");
        var divField = Field(module, owner, "_div");
        var alphaLimitField = Field(module, owner, "_alphaLimit");
        var fullField = Field(module, owner, "_full");
        var color32Constructor = ImportMethod(module, color32Type, ".ctor", 4, true, "System.Byte", "System.Byte", "System.Byte", "System.Byte");
        var clamp01 = ImportMethod(module, UnityType(module, "Mathf", vector2Type.Scope), "Clamp01", 1, false, "System.Single");
        var mathMax = FrameworkMethod(module, "System.Math", "Max", 2, false, "System.Int32", "System.Int32");

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var int32Type = module.TypeSystem.Int32;
        var xMin = new VariableDefinition(int32Type);
        var yMin = new VariableDefinition(int32Type);
        var xMax = new VariableDefinition(int32Type);
        var yMax = new VariableDefinition(int32Type);
        var lastColumn = new VariableDefinition(int32Type);
        var tintIndex = new VariableDefinition(int32Type);
        var oldAlpha = new VariableDefinition(module.TypeSystem.Byte);
        var sourceIndex = new VariableDefinition(int32Type);
        var destinationX = new VariableDefinition(int32Type);
        var destinationY = new VariableDefinition(int32Type);
        var destinationIndex = new VariableDefinition(int32Type);
        var destination = new VariableDefinition(color32Type);
        var source = new VariableDefinition(color32Type);
        var blend = new VariableDefinition(module.TypeSystem.Single);
        foreach (var local in new[]
                 {
                     xMin, yMin, xMax, yMax, lastColumn, tintIndex, oldAlpha,
                     sourceIndex, destinationX, destinationY, destinationIndex,
                     destination, source, blend
                 })
        {
            body.Variables.Add(local);
        }
        var il = body.Instructions;

        void LoadVectorComponent(ParameterDefinition vector, FieldReference component)
        {
            il.Add(Instruction.Create(OpCodes.Ldarga, vector));
            il.Add(Instruction.Create(OpCodes.Ldfld, component));
        }

        void LoadPixelField(VariableDefinition pixel, string channel)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, pixel));
            il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, color32Type, channel)));
        }

        void EmitBlendedChannel(string channel)
        {
            LoadPixelField(destination, channel);
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Ldloc, blend));
            LoadPixelField(source, channel);
            LoadPixelField(destination, channel);
            il.Add(Instruction.Create(OpCodes.Sub));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Add));
            il.Add(Instruction.Create(OpCodes.Conv_U1));
        }

        var setup = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg, data1));
        il.Add(Instruction.Create(OpCodes.Ldarg, data2));
        il.Add(Instruction.Create(OpCodes.Bne_Un, setup));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(setup);

        LoadVectorComponent(data2Offset, vector2X);
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, mathMax));
        il.Add(Instruction.Create(OpCodes.Stloc, xMin));
        LoadVectorComponent(data2Offset, vector2Y);
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, mathMax));
        il.Add(Instruction.Create(OpCodes.Stloc, yMin));

        LoadVectorComponent(data2Size, vector2X);
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Ldloc, xMin));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, xMax));
        LoadVectorComponent(data2Size, vector2Y);
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Ldloc, yMin));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, yMax));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, sizeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, vector2X));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Stloc, lastColumn));

        var afterTint = Instruction.Create(OpCodes.Nop);
        var tintBody = Instruction.Create(OpCodes.Nop);
        var tintNext = Instruction.Create(OpCodes.Nop);
        var tintCondition = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg, changeColor));
        il.Add(Instruction.Create(OpCodes.Brfalse, afterTint));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, tintIndex));
        il.Add(Instruction.Create(OpCodes.Br, tintCondition));
        il.Add(tintBody);

        il.Add(Instruction.Create(OpCodes.Ldarg, data2));
        il.Add(Instruction.Create(OpCodes.Ldloc, tintIndex));
        il.Add(Instruction.Create(OpCodes.Ldelema, color32Type));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, color32Type, "a")));
        il.Add(Instruction.Create(OpCodes.Brfalse, tintNext));
        il.Add(Instruction.Create(OpCodes.Ldarg, data2));
        il.Add(Instruction.Create(OpCodes.Ldloc, tintIndex));
        il.Add(Instruction.Create(OpCodes.Ldelema, color32Type));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, color32Type, "a")));
        il.Add(Instruction.Create(OpCodes.Stloc, oldAlpha));

        foreach (var channel in new[] { "r", "g", "b" })
        {
            il.Add(Instruction.Create(OpCodes.Ldarg, data2));
            il.Add(Instruction.Create(OpCodes.Ldloc, tintIndex));
            il.Add(Instruction.Create(OpCodes.Ldelema, color32Type));
            il.Add(Instruction.Create(OpCodes.Ldarg, color));
            il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, color32Type, channel)));
            il.Add(Instruction.Create(OpCodes.Stfld, Field(module, color32Type, channel)));
        }
        il.Add(Instruction.Create(OpCodes.Ldarg, data2));
        il.Add(Instruction.Create(OpCodes.Ldloc, tintIndex));
        il.Add(Instruction.Create(OpCodes.Ldelema, color32Type));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, divField));
        il.Add(Instruction.Create(OpCodes.Ldloc, oldAlpha));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Ldarg, color));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, color32Type, "a")));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, divField));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 255f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Conv_U1));
        il.Add(Instruction.Create(OpCodes.Stfld, Field(module, color32Type, "a")));
        il.Add(tintNext);
        il.Add(Instruction.Create(OpCodes.Ldloc, tintIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, tintIndex));
        il.Add(tintCondition);
        il.Add(Instruction.Create(OpCodes.Ldloc, tintIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg, data2));
        il.Add(Instruction.Create(OpCodes.Ldlen));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Blt, tintBody));
        il.Add(afterTint);

        var sourceIndexZero = Instruction.Create(OpCodes.Nop);
        var sourceIndexReady = Instruction.Create(OpCodes.Nop);
        LoadVectorComponent(data2Offset, vector2Y);
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0f));
        il.Add(Instruction.Create(OpCodes.Bge_Un, sourceIndexZero));
        LoadVectorComponent(data2Offset, vector2Y);
        LoadVectorComponent(data2Size, vector2X);
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Neg));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Br, sourceIndexReady));
        il.Add(sourceIndexZero);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceIndex));
        il.Add(sourceIndexReady);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationIndex));

        var loopBody = Instruction.Create(OpCodes.Nop);
        var loopIncrement = Instruction.Create(OpCodes.Nop);
        var loopCondition = Instruction.Create(OpCodes.Nop);
        var advanceSource = Instruction.Create(OpCodes.Nop);
        var advanceCoordinates = Instruction.Create(OpCodes.Nop);
        var copySource = Instruction.Create(OpCodes.Nop);
        var afterWrite = Instruction.Create(OpCodes.Nop);
        var resetColumn = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, loopCondition));
        il.Add(loopBody);
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldloc, xMin));
        il.Add(Instruction.Create(OpCodes.Blt, advanceCoordinates));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldloc, xMax));
        il.Add(Instruction.Create(OpCodes.Bgt, advanceCoordinates));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Ldloc, yMin));
        il.Add(Instruction.Create(OpCodes.Blt, advanceCoordinates));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Ldloc, yMax));
        il.Add(Instruction.Create(OpCodes.Bgt, advanceCoordinates));

        il.Add(Instruction.Create(OpCodes.Ldloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg, data2));
        il.Add(Instruction.Create(OpCodes.Ldlen));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Bge, advanceSource));
        il.Add(Instruction.Create(OpCodes.Ldarg, data1));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Stloc, destination));
        il.Add(Instruction.Create(OpCodes.Ldarg, data2));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Ldelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Stloc, source));
        LoadPixelField(source, "a");
        il.Add(Instruction.Create(OpCodes.Brfalse, advanceSource));
        LoadPixelField(destination, "a");
        il.Add(Instruction.Create(OpCodes.Brfalse, copySource));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, divField));
        LoadPixelField(source, "a");
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Call, clamp01));
        il.Add(Instruction.Create(OpCodes.Stloc, blend));
        il.Add(Instruction.Create(OpCodes.Ldarg, data1));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        EmitBlendedChannel("r");
        EmitBlendedChannel("g");
        EmitBlendedChannel("b");
        EmitBlendedChannel("a");
        il.Add(Instruction.Create(OpCodes.Newobj, color32Constructor));
        il.Add(Instruction.Create(OpCodes.Stelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Br, afterWrite));
        il.Add(copySource);
        il.Add(Instruction.Create(OpCodes.Ldarg, data1));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, source));
        il.Add(Instruction.Create(OpCodes.Stelem_Any, color32Type));
        il.Add(afterWrite);

        il.Add(Instruction.Create(OpCodes.Ldarg, imposeAlphaLimits));
        il.Add(Instruction.Create(OpCodes.Brfalse, advanceSource));
        il.Add(Instruction.Create(OpCodes.Ldarg, data1));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldelema, color32Type));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, color32Type, "a")));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, alphaLimitField));
        il.Add(Instruction.Create(OpCodes.Ble_Un, advanceSource));
        il.Add(Instruction.Create(OpCodes.Ldarg, data1));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldelema, color32Type));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, fullField));
        il.Add(Instruction.Create(OpCodes.Stfld, Field(module, color32Type, "a")));
        il.Add(advanceSource);
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceIndex));
        il.Add(advanceCoordinates);

        il.Add(Instruction.Create(OpCodes.Ldloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldloc, lastColumn));
        il.Add(Instruction.Create(OpCodes.Beq, resetColumn));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Br, loopIncrement));
        il.Add(resetColumn);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationY));
        il.Add(loopIncrement);
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationIndex));
        il.Add(loopCondition);
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg, data1));
        il.Add(Instruction.Create(OpCodes.Ldlen));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Blt, loopBody));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static TypeReference UnityType(ModuleDefinition module, string name, IMetadataScope scope)
    {
        return module.ImportReference(new TypeReference("UnityEngine", name, module, scope));
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.First(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
        return module.ImportReference(definition);
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

    private static IEnumerable<TypeDefinition> Hierarchy(TypeDefinition type)
    {
        for (var current = type; current is not null; current = current.BaseType?.Resolve()) yield return current;
    }
}
