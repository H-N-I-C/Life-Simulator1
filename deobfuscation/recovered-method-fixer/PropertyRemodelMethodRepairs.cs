using Mono.Cecil;
using Mono.Cecil.Cil;

public static class PropertyRemodelMethodRepairs
{
    public static void FixPropertyValue(MethodDefinition method)
    {
        var module = method.Module;
        var body = FreshBody(method);
        var genericParameter = method.GenericParameters.Single();
        var cached = new VariableDefinition(module.TypeSystem.Object);
        var converted = new VariableDefinition(genericParameter);
        var result = new VariableDefinition(genericParameter);
        body.Variables.Add(cached);
        body.Variables.Add(converted);
        body.Variables.Add(result);

        var flags = Field(module, method.DeclaringType, "<flags>k__BackingField");
        var tryGetValue = ImportMethod(module, flags.FieldType, "TryGetValue", 2, true);
        var tryConvertDefinition = FindMethod(method.DeclaringType, "TryConvertCachedValue", 2, false,
                                      "System.Object", "T&") ??
                                   method.DeclaringType.Methods.Single(candidate =>
                                       candidate.Name == "TryConvertCachedValue" && candidate.HasGenericParameters &&
                                       candidate.Parameters.Count == 2);
        var tryConvert = new GenericInstanceMethod(module.ImportReference(tryConvertDefinition));
        tryConvert.GenericArguments.Add(genericParameter);

        var il = body.Instructions;
        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg, method.Parameters[1]));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, flags));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, flags));
        il.Add(Instruction.Create(OpCodes.Ldarg, method.Parameters[0]));
        il.Add(Instruction.Create(OpCodes.Ldloca, cached));
        il.Add(Instruction.Create(OpCodes.Callvirt, tryGetValue));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        il.Add(Instruction.Create(OpCodes.Ldloc, cached));
        il.Add(Instruction.Create(OpCodes.Ldloca, converted));
        il.Add(Instruction.Create(OpCodes.Call, tryConvert));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        il.Add(Instruction.Create(OpCodes.Ldloc, converted));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixRemodelFeature(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var house = method.Parameters[0];
        var amount = method.Parameters[1];
        var capitalize = method.Parameters[2];
        var featureType = FindType(module, "DataLandlordHomeRemodelFeature");
        var houseType = house.ParameterType;
        var houseTypeData = ImportMethod(module, houseType, "get_Type", 0, true).ReturnType;
        var getHouseType = ImportMethod(module, houseType, "get_Type", 0, true);
        var getHouseTypeName = ImportMethod(module, houseTypeData, "get_Name", 0, true);
        var getAddedFeatures = ImportMethod(module, houseType, "get_AddedFeatures", 0, true);
        var addedFeaturesType = (GenericInstanceType)getAddedFeatures.ReturnType;
        var remodelType = addedFeaturesType.GenericArguments.Single();
        var getRemodelFeature = ImportMethod(module, remodelType, "get_Feature", 0, true);
        var getProject = ImportMethod(module, featureType, "get_Project", 0, true);
        var getWhitelist = ImportMethod(module, featureType, "get_HouseTypeWhitelist", 0, true);
        var getBlacklist = ImportMethod(module, featureType, "get_HouseTypeBlacklist", 0, true);
        var sourceField = Field(module, owner, "_landlordHomeRemodelFeatureSet");
        var sourceType = sourceField.FieldType;
        var loadSource = ImportMethod(module, owner, "LoadLandlordHomeRemodelFeatureSet", 0, false);
        var countNotNull = ImportMethod(module, FindType(module, "ArrayExtension"), "CountNotNull", 1, false);
        var capitalizeProject = ImportMethod(module, FindType(module, "GameReference"),
            "CapitalizeFirstLetterOfSentenceString", 1, false, "System.String");

        var arrayListConstructor = ImportMethod(module, sourceType, ".ctor", 0, true);
        var arrayListGetCount = ImportMethod(module, sourceType, "get_Count", 0, true);
        var arrayListGetItem = ImportMethod(module, sourceType, "get_Item", 1, true);
        var arrayListContains = ImportMethod(module, sourceType, "Contains", 1, true);
        var arrayListAdd = ImportMethod(module, sourceType, "Add", 1, true);

        var eligibleType = new GenericInstanceType(addedFeaturesType.ElementType);
        eligibleType.GenericArguments.Add(module.ImportReference(featureType));
        var eligibleConstructor = ImportMethod(module, eligibleType, ".ctor", 0, true);
        var eligibleAdd = ImportMethod(module, eligibleType, "Add", 1, true);
        var eligibleGetCount = ImportMethod(module, eligibleType, "get_Count", 0, true);
        var eligibleGetItem = ImportMethod(module, eligibleType, "get_Item", 1, true);
        var eligibleRemoveAt = ImportMethod(module, eligibleType, "RemoveAt", 1, true);
        var addedFeaturesGetCount = ImportMethod(module, addedFeaturesType, "get_Count", 0, true);
        var addedFeaturesGetItem = ImportMethod(module, addedFeaturesType, "get_Item", 1, true);

        var randomScope = module.AssemblyReferences.Single(reference => reference.Name == "UnityEngine.CoreModule");
        var randomType = new TypeReference("UnityEngine", "Random", module, randomScope);
        var randomRange = ImportMethod(module, randomType, "Range", 2, false, "System.Int32", "System.Int32");

        var body = FreshBody(method);
        var eligible = new VariableDefinition(eligibleType);
        var result = new VariableDefinition(method.ReturnType);
        var data = new VariableDefinition(featureType);
        var addedFeatures = new VariableDefinition(addedFeaturesType);
        var sourceIndex = new VariableDefinition(module.TypeSystem.Int32);
        var addedIndex = new VariableDefinition(module.TypeSystem.Int32);
        var duplicateCount = new VariableDefinition(module.TypeSystem.Int32);
        var randomIndex = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[]
                 {
                     eligible, result, data, addedFeatures, sourceIndex, addedIndex, duplicateCount, randomIndex
                 })
            body.Variables.Add(local);

        var il = body.Instructions;
        var sourceLoaded = Instruction.Create(OpCodes.Nop);
        var sourceLoop = Instruction.Create(OpCodes.Nop);
        var sourceCondition = Instruction.Create(OpCodes.Nop);
        var whitelistAccepted = Instruction.Create(OpCodes.Nop);
        var nextSource = Instruction.Create(OpCodes.Nop);
        var addedLoop = Instruction.Create(OpCodes.Nop);
        var addedCondition = Instruction.Create(OpCodes.Nop);
        var nextAdded = Instruction.Create(OpCodes.Nop);
        var selectionLoop = Instruction.Create(OpCodes.Nop);
        var selectionCondition = Instruction.Create(OpCodes.Nop);
        var notCapitalized = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Nop);

        il.Add(Instruction.Create(OpCodes.Ldsfld, sourceField));
        il.Add(Instruction.Create(OpCodes.Brtrue, sourceLoaded));
        il.Add(Instruction.Create(OpCodes.Call, loadSource));
        il.Add(sourceLoaded);
        il.Add(Instruction.Create(OpCodes.Newobj, eligibleConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, eligible));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Br, sourceCondition));

        il.Add(sourceLoop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, sourceField));
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, featureType));
        il.Add(Instruction.Create(OpCodes.Stloc, data));

        // Native 0x3BA87E8-0x3BA88CC evaluates Contains before CountNotNull.
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Call, getWhitelist));
        il.Add(Instruction.Create(OpCodes.Ldarg, house));
        il.Add(Instruction.Create(OpCodes.Call, getHouseType));
        il.Add(Instruction.Create(OpCodes.Call, getHouseTypeName));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListContains));
        il.Add(Instruction.Create(OpCodes.Brtrue, whitelistAccepted));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Call, getWhitelist));
        il.Add(Instruction.Create(OpCodes.Call, countNotNull));
        il.Add(Instruction.Create(OpCodes.Brtrue, nextSource));
        il.Add(whitelistAccepted);

        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Call, getBlacklist));
        il.Add(Instruction.Create(OpCodes.Ldarg, house));
        il.Add(Instruction.Create(OpCodes.Call, getHouseType));
        il.Add(Instruction.Create(OpCodes.Call, getHouseTypeName));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListContains));
        il.Add(Instruction.Create(OpCodes.Brtrue, nextSource));

        // The native predicate at 0x3E3F618 compares Feature by reference.
        // Count every match rather than stopping at the first duplicate.
        il.Add(Instruction.Create(OpCodes.Ldarg, house));
        il.Add(Instruction.Create(OpCodes.Call, getAddedFeatures));
        il.Add(Instruction.Create(OpCodes.Stloc, addedFeatures));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, duplicateCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, addedIndex));
        il.Add(Instruction.Create(OpCodes.Br, addedCondition));
        il.Add(addedLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, addedFeatures));
        il.Add(Instruction.Create(OpCodes.Ldloc, addedIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, addedFeaturesGetItem));
        il.Add(Instruction.Create(OpCodes.Call, getRemodelFeature));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Bne_Un, nextAdded));
        il.Add(Instruction.Create(OpCodes.Ldloc, duplicateCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, duplicateCount));
        il.Add(nextAdded);
        il.Add(Instruction.Create(OpCodes.Ldloc, addedIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, addedIndex));
        il.Add(addedCondition);
        il.Add(Instruction.Create(OpCodes.Ldloc, addedIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, addedFeatures));
        il.Add(Instruction.Create(OpCodes.Callvirt, addedFeaturesGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, addedLoop));
        il.Add(Instruction.Create(OpCodes.Ldloc, duplicateCount));
        il.Add(Instruction.Create(OpCodes.Brtrue, nextSource));

        il.Add(Instruction.Create(OpCodes.Ldloc, eligible));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Callvirt, eligibleAdd));
        il.Add(nextSource);
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, sourceIndex));
        il.Add(sourceCondition);
        il.Add(Instruction.Create(OpCodes.Ldloc, sourceIndex));
        il.Add(Instruction.Create(OpCodes.Ldsfld, sourceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, sourceLoop));

        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Br, selectionCondition));

        il.Add(selectionLoop);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, eligible));
        il.Add(Instruction.Create(OpCodes.Callvirt, eligibleGetCount));
        il.Add(Instruction.Create(OpCodes.Call, randomRange));
        il.Add(Instruction.Create(OpCodes.Stloc, randomIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, eligible));
        il.Add(Instruction.Create(OpCodes.Ldloc, randomIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, eligibleGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Call, getProject));
        il.Add(Instruction.Create(OpCodes.Ldarg, capitalize));
        il.Add(Instruction.Create(OpCodes.Brfalse, notCapitalized));
        il.Add(Instruction.Create(OpCodes.Call, capitalizeProject));
        il.Add(notCapitalized);
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, eligible));
        il.Add(Instruction.Create(OpCodes.Ldloc, randomIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, eligibleRemoveAt));

        il.Add(selectionCondition);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Ldarg, amount));
        il.Add(Instruction.Create(OpCodes.Bge, done));
        il.Add(Instruction.Create(OpCodes.Ldloc, eligible));
        il.Add(Instruction.Create(OpCodes.Callvirt, eligibleGetCount));
        il.Add(Instruction.Create(OpCodes.Brtrue, selectionLoop));
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name,
        int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var definition = FindMethod(declaringType, name, parameterCount, instance, parameterTypes)
            ?? throw new InvalidOperationException($"Method not found: {declaringType.FullName}::{name}/{parameterCount}");
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static MethodDefinition? FindMethod(TypeReference declaringType, string name, int parameterCount,
        bool? instance = null, params string[] parameterTypes)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var match = type.Methods.SingleOrDefault(candidate =>
                candidate.Name == name && candidate.Parameters.Count == parameterCount &&
                (!instance.HasValue || candidate.IsStatic != instance.Value) &&
                (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName)
                    .SequenceEqual(parameterTypes)));
            if (match is not null) return match;
        }
        return null;
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
        foreach (var descendant in Flatten(nested))
            yield return descendant;
    }
}
