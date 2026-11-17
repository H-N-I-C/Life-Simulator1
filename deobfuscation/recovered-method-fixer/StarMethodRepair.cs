using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class StarMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var pathParameter = method.Parameters.Single(parameter => parameter.Name == "path");
        var colorParameter = method.Parameters.Single(parameter => parameter.Name == "color");
        var sizeField = Field(module, owner, "_size");
        var vector2Type = sizeField.FieldType;
        var vector2X = Field(module, vector2Type, "x");
        var vector2Y = Field(module, vector2Type, "y");
        var vector2Constructor = ImportMethod(module, vector2Type, ".ctor", 2, true, "System.Single", "System.Single");
        var color32Type = ((ArrayType)method.ReturnType).ElementType;
        var color32Constructor = ImportMethod(module, color32Type, ".ctor", 4, true, "System.Byte", "System.Byte", "System.Byte", "System.Byte");
        var spriteType = UnityType(module, "Sprite", vector2Type.Scope);
        var textureType = UnityType(module, "Texture2D", vector2Type.Scope);
        var unityObjectType = UnityType(module, "Object", vector2Type.Scope);
        var getTexture = ImportMethod(module, spriteType, "get_texture", 0, true);
        var getPixels = ImportMethod(module, textureType, "GetPixels32", 0, true);
        var getWidth = ImportMethod(module, textureType, "get_width", 0, true);
        var getHeight = ImportMethod(module, textureType, "get_height", 0, true);
        var objectInequality = ImportMethod(module, unityObjectType, "op_Inequality", 2, false, "UnityEngine.Object", "UnityEngine.Object");
        var destroy = ImportMethod(module, unityObjectType, "Destroy", 1, false, "UnityEngine.Object");
        var ceilToInt = ImportMethod(module, UnityType(module, "Mathf", vector2Type.Scope), "CeilToInt", 1, false, "System.Single");
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");
        var voltronType = FindType(module, "VoltronManager");
        var getVoltron = ImportMethod(module, voltronType, "get_SharedVoltronManager", 0, false);
        var copyTexture = ImportMethod(module, voltronType, "CopyTexture", 3, true, "UnityEngine.Texture2D", "System.Int32", "System.Int32");
        var combine = ImportMethod(module, owner, "CombineTextures", 7, true);
        var resourcesType = UnityType(module, "Resources", vector2Type.Scope);
        var loadDefinition = resourcesType.Resolve().Methods.Single(candidate =>
            candidate.Name == "Load" && candidate.HasGenericParameters && candidate.GenericParameters.Count == 1 &&
            candidate.Parameters.Count == 1 && candidate.Parameters[0].ParameterType.FullName == "System.String");
        var loadSprite = new GenericInstanceMethod(module.ImportReference(loadDefinition));
        loadSprite.GenericArguments.Add(module.ImportReference(spriteType));

        var body = FreshBody(method);
        var result = new VariableDefinition(method.ReturnType);
        var sprite = new VariableDefinition(spriteType);
        var texture = new VariableDefinition(textureType);
        var pixels = new VariableDefinition(method.ReturnType);
        var originalWidth = new VariableDefinition(module.TypeSystem.Int32);
        var offset = new VariableDefinition(vector2Type);
        foreach (var local in new[] { result, sprite, texture, pixels, originalWidth, offset }) body.Variables.Add(local);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 364f));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 364f));
        il.Add(Instruction.Create(OpCodes.Newobj, vector2Constructor));
        il.Add(Instruction.Create(OpCodes.Stfld, sizeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 132496));
        il.Add(Instruction.Create(OpCodes.Newarr, color32Type));
        il.Add(Instruction.Create(OpCodes.Stloc, result));

        void EmitTint()
        {
            foreach (var channel in new[] { "r", "g", "b", "a" })
            {
                il.Add(Instruction.Create(OpCodes.Ldarg, colorParameter));
                il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, colorParameter.ParameterType, channel)));
                il.Add(Instruction.Create(OpCodes.Ldc_R4, 255f));
                il.Add(Instruction.Create(OpCodes.Mul));
                il.Add(Instruction.Create(OpCodes.Conv_U1));
            }
            il.Add(Instruction.Create(OpCodes.Newobj, color32Constructor));
        }

        void EmitOffsetComponent(FieldReference sizeComponent, MethodReference textureDimension)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, sizeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, sizeComponent));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.5f));
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Call, textureDimension));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.5f));
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Sub));
            il.Add(Instruction.Create(OpCodes.Call, ceilToInt));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
        }

        void EmitLayer(string suffix, bool colorLayer)
        {
            var layerDone = Instruction.Create(OpCodes.Nop);
            var textureReady = Instruction.Create(OpCodes.Nop);

            il.Add(Instruction.Create(OpCodes.Ldarg, pathParameter));
            il.Add(Instruction.Create(OpCodes.Ldstr, "star"));
            il.Add(Instruction.Create(OpCodes.Ldstr, suffix));
            il.Add(Instruction.Create(OpCodes.Call, concat3));
            il.Add(Instruction.Create(OpCodes.Call, loadSprite));
            il.Add(Instruction.Create(OpCodes.Stloc, sprite));
            il.Add(Instruction.Create(OpCodes.Ldloc, sprite));
            il.Add(Instruction.Create(OpCodes.Ldnull));
            il.Add(Instruction.Create(OpCodes.Call, objectInequality));
            il.Add(Instruction.Create(OpCodes.Brfalse, layerDone));
            il.Add(Instruction.Create(OpCodes.Ldloc, sprite));
            il.Add(Instruction.Create(OpCodes.Call, getTexture));
            il.Add(Instruction.Create(OpCodes.Stloc, texture));
            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Ldnull));
            il.Add(Instruction.Create(OpCodes.Call, objectInequality));
            il.Add(Instruction.Create(OpCodes.Brfalse, layerDone));

            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Call, getWidth));
            il.Add(Instruction.Create(OpCodes.Stloc, originalWidth));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, sizeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, vector2X));
            il.Add(Instruction.Create(OpCodes.Ldloc, originalWidth));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Bge_Un, textureReady));
            il.Add(Instruction.Create(OpCodes.Call, getVoltron));
            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, sizeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, vector2X));
            il.Add(Instruction.Create(OpCodes.Conv_I4));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, sizeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, vector2Y));
            il.Add(Instruction.Create(OpCodes.Conv_I4));
            il.Add(Instruction.Create(OpCodes.Callvirt, copyTexture));
            il.Add(Instruction.Create(OpCodes.Stloc, texture));
            il.Add(textureReady);

            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Call, getPixels));
            il.Add(Instruction.Create(OpCodes.Stloc, pixels));
            EmitOffsetComponent(vector2X, getWidth);
            EmitOffsetComponent(vector2Y, getHeight);
            il.Add(Instruction.Create(OpCodes.Newobj, vector2Constructor));
            il.Add(Instruction.Create(OpCodes.Stloc, offset));

            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, result));
            il.Add(Instruction.Create(OpCodes.Ldloc, pixels));
            il.Add(Instruction.Create(OpCodes.Ldloc, offset));
            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Call, getWidth));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Call, getHeight));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Newobj, vector2Constructor));
            if (colorLayer)
            {
                EmitTint();
            }
            else
            {
                il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
                il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
                il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
                il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
                il.Add(Instruction.Create(OpCodes.Newobj, color32Constructor));
            }
            il.Add(Instruction.Create(OpCodes.Ldc_I4, colorLayer ? 1 : 0));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, colorLayer ? 1 : 0));
            il.Add(Instruction.Create(OpCodes.Call, combine));

            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, sizeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, vector2X));
            il.Add(Instruction.Create(OpCodes.Ldloc, originalWidth));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Bge_Un, layerDone));
            il.Add(Instruction.Create(OpCodes.Ldloc, texture));
            il.Add(Instruction.Create(OpCodes.Call, destroy));
            il.Add(layerDone);
        }

        EmitLayer("-color", true);
        EmitLayer("-shade", false);
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

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.First(candidate =>
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
