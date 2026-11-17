using Mono.Cecil;
using Mono.Cecil.Cil;

public static class FighterTextureMethodRepairs
{
    public static void FixFighterColor(MethodDefinition method)
    {
        var module = method.Module;
        var fightingStyle = module.ImportReference(
            method.DeclaringType.Fields.Single(field => field.Name == "_fightingStyle"));
        var dataHub = module.Types.Single(type => type.Name == "DataHub");
        var getColor = module.ImportReference(dataHub.Methods.Single(candidate =>
            candidate.Name == "GetFightClubStyleColorByStyleType" && candidate.Parameters.Count == 1));
        var text = module.ImportReference(getColor.ReturnType.Resolve().Fields.Single(field =>
            field.Name == "<String>k__BackingField"));

        var il = FreshBody(method).Instructions;
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, fightingStyle));
        il.Add(Instruction.Create(OpCodes.Call, getColor));
        il.Add(Instruction.Create(OpCodes.Ldfld, text));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixRotateTexture(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var data = method.Parameters.Single(parameter => parameter.Name == "pData");
        var angle = method.Parameters.Single(parameter => parameter.Name == "pAngle");
        var color32Type = ((ArrayType)method.ReturnType).ElementType;
        var size = Field(module, owner, "_size");
        var vector2Type = size.FieldType;
        var sizeX = Field(module, vector2Type, "x");
        var sizeY = Field(module, vector2Type, "y");
        var mathf = UnityType(module, "Mathf", vector2Type.Scope);
        var sin = ImportMethod(module, mathf, "Sin", 1, false, "System.Single");
        var cos = ImportMethod(module, mathf, "Cos", 1, false, "System.Single");
        var round = FrameworkMethod(module, "System.Math", "Round", 1, false, "System.Double");

        var body = FreshBody(method);
        var int32Type = module.TypeSystem.Int32;
        var singleType = module.TypeSystem.Single;
        var result = new VariableDefinition(method.ReturnType);
        var transparent = new VariableDefinition(color32Type);
        var width = new VariableDefinition(int32Type);
        var height = new VariableDefinition(int32Type);
        var radians = new VariableDefinition(singleType);
        var sine = new VariableDefinition(singleType);
        var cosine = new VariableDefinition(singleType);
        var centerX = new VariableDefinition(singleType);
        var centerY = new VariableDefinition(singleType);
        var destinationIndex = new VariableDefinition(int32Type);
        var destinationY = new VariableDefinition(int32Type);
        var destinationX = new VariableDefinition(int32Type);
        var offsetY = new VariableDefinition(singleType);
        var sineY = new VariableDefinition(singleType);
        var cosineY = new VariableDefinition(singleType);
        var offsetX = new VariableDefinition(singleType);
        var sourceXFloat = new VariableDefinition(singleType);
        var sourceYFloat = new VariableDefinition(singleType);
        var sourceX = new VariableDefinition(int32Type);
        var sourceY = new VariableDefinition(int32Type);
        foreach (var local in new[]
                 {
                     result, transparent, width, height, radians, sine, cosine, centerX, centerY,
                     destinationIndex, destinationY, destinationX, offsetY, sineY, cosineY, offsetX,
                     sourceXFloat, sourceYFloat, sourceX, sourceY
                 })
        {
            body.Variables.Add(local);
        }

        var il = body.Instructions;
        var outerLoop = Instruction.Create(OpCodes.Nop);
        var innerLoop = Instruction.Create(OpCodes.Nop);
        var outOfBounds = Instruction.Create(OpCodes.Nop);
        var advancePixel = Instruction.Create(OpCodes.Nop);
        var nextRow = Instruction.Create(OpCodes.Nop);
        var returnResult = Instruction.Create(OpCodes.Nop);

        il.Add(Instruction.Create(OpCodes.Ldarg, data));
        il.Add(Instruction.Create(OpCodes.Ldlen));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Newarr, color32Type));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloca, transparent));
        il.Add(Instruction.Create(OpCodes.Initobj, color32Type));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, size));
        il.Add(Instruction.Create(OpCodes.Ldfld, sizeX));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Stloc, width));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, size));
        il.Add(Instruction.Create(OpCodes.Ldfld, sizeY));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Stloc, height));

        il.Add(Instruction.Create(OpCodes.Ldarg, angle));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.0174532924f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Stloc, radians));
        il.Add(Instruction.Create(OpCodes.Ldloc, radians));
        il.Add(Instruction.Create(OpCodes.Call, sin));
        il.Add(Instruction.Create(OpCodes.Stloc, sine));
        il.Add(Instruction.Create(OpCodes.Ldloc, radians));
        il.Add(Instruction.Create(OpCodes.Call, cos));
        il.Add(Instruction.Create(OpCodes.Stloc, cosine));

        il.Add(Instruction.Create(OpCodes.Ldloc, height));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, returnResult));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, returnResult));

        il.Add(Instruction.Create(OpCodes.Ldloc, height));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.5f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Stloc, centerY));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.5f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Stloc, centerX));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationY));

        il.Add(outerLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ldloc, centerY));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Stloc, offsetY));
        il.Add(Instruction.Create(OpCodes.Ldloc, sine));
        il.Add(Instruction.Create(OpCodes.Ldloc, offsetY));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Stloc, sineY));
        il.Add(Instruction.Create(OpCodes.Ldloc, cosine));
        il.Add(Instruction.Create(OpCodes.Ldloc, offsetY));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Stloc, cosineY));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationX));

        il.Add(innerLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ldloc, centerX));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Stloc, offsetX));

        // Native: sourceX = Round(width / 2 + sin(y) + cos(x)); Round is double precision.
        il.Add(Instruction.Create(OpCodes.Ldloc, centerX));
        il.Add(Instruction.Create(OpCodes.Ldloc, sineY));
        il.Add(Instruction.Create(OpCodes.Ldloc, cosine));
        il.Add(Instruction.Create(OpCodes.Ldloc, offsetX));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceXFloat));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceXFloat));
        il.Add(Instruction.Create(OpCodes.Conv_R8));
        il.Add(Instruction.Create(OpCodes.Call, round));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceX));

        // Native: sourceY = Round(height / 2 + cos(y) - sin(x)).
        il.Add(Instruction.Create(OpCodes.Ldloc, centerY));
        il.Add(Instruction.Create(OpCodes.Ldloc, cosineY));
        il.Add(Instruction.Create(OpCodes.Ldloc, sine));
        il.Add(Instruction.Create(OpCodes.Ldloc, offsetX));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceYFloat));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceYFloat));
        il.Add(Instruction.Create(OpCodes.Conv_R8));
        il.Add(Instruction.Create(OpCodes.Call, round));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceY));

        il.Add(Instruction.Create(OpCodes.Ldloc, sourceY));
        il.Add(Instruction.Create(OpCodes.Ldloc, height));
        il.Add(Instruction.Create(OpCodes.Bge, outOfBounds));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceX));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Bge, outOfBounds));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceY));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceX));
        il.Add(Instruction.Create(OpCodes.Or));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Blt, outOfBounds));

        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceY));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceX));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Ldelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Stelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Br, advancePixel));

        il.Add(outOfBounds);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, transparent));
        il.Add(Instruction.Create(OpCodes.Stelem_Any, color32Type));

        il.Add(advancePixel);
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationX));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Bne_Un, innerLoop));

        il.Add(nextRow);
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Ldloc, destinationY));
        il.Add(Instruction.Create(OpCodes.Ldloc, height));
        il.Add(Instruction.Create(OpCodes.Bne_Un, outerLoop));

        il.Add(returnResult);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static TypeReference UnityType(ModuleDefinition module, string name, IMetadataScope scope)
    {
        return module.ImportReference(new TypeReference("UnityEngine", name, module, scope));
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType,
        string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var definition = declaringType.Resolve().Methods.First(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName)
                .SequenceEqual(parameterTypes)));
        return module.ImportReference(definition);
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName,
        string methodName, int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1),
            module, module.TypeSystem.Object.Scope);
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var field = declaringType.Resolve().Fields.Single(candidate => candidate.Name == name);
        return module.ImportReference(field);
    }
}
