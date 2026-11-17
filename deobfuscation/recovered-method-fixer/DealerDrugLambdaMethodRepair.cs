using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class DealerDrugLambdaMethodRepair
{
    private const string ExpectedParameterType =
        "System.Collections.Generic.KeyValuePair`2<System.String,System.Single>";

    public static void Fix(MethodDefinition method)
    {
        if (method.Parameters.Count != 1 ||
            method.Parameters[0].ParameterType is not GenericInstanceType pairType ||
            pairType.FullName != ExpectedParameterType)
            throw new InvalidOperationException($"Unexpected dealer drug lambda signature: {method.FullName}");

        var module = method.Module;
        var parameter = method.Parameters[0];
        var drugField = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "drug"));
        var drugType = drugField.FieldType.Resolve()
            ?? throw new InvalidOperationException("Could not resolve SimDealerDrug");
        var typeField = module.ImportReference(drugType.Fields.Single(field =>
            field.Name == "<Type>k__BackingField"));

        var getKey = module.ImportReference(pairType.Resolve().Methods.Single(candidate =>
            candidate.Name == "get_Key" && !candidate.IsStatic && candidate.Parameters.Count == 0));
        getKey.DeclaringType = pairType;

        var stringEquals = module.ImportReference(module.TypeSystem.String.Resolve().Methods.Single(candidate =>
            candidate.Name == "Equals" && !candidate.IsStatic && candidate.Parameters.Count == 2 &&
            candidate.Parameters[0].ParameterType.FullName == "System.String" &&
            candidate.Parameters[1].ParameterType.FullName == "System.StringComparison"));

        // Native ARM64: return t.Key.Equals(drug.Type, StringComparison.OrdinalIgnoreCase).
        method.Body = new MethodBody(method) { InitLocals = false };
        var il = method.Body.Instructions;
        il.Add(Instruction.Create(OpCodes.Ldarga, parameter));
        il.Add(Instruction.Create(OpCodes.Call, getKey));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, drugField));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Ret));
    }
}
