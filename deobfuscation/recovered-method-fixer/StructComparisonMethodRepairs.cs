using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class StructComparisonMethodRepairs
{
    // Restores struct comparisons the decompiler flattened into "struct OP int-constant".
    //
    // The shape is always:
    //   <load a value of a value type>; ldc.i4 <const>; clt | cgt | ceq
    // IL2CPP rejects that with "Cannot get stack type for <T>". C# never emits it: it calls the type's
    // operator overload. When the type defines the matching operator and op_Implicit(int32), the original
    // comparison is recoverable exactly, so the pair is rewritten to:
    //   <load>; ldc.i4 <const>; call op_Implicit(int32) -> T; call op_X(T,T) -> bool
    // Nothing else in the body is touched; the rewrite is idempotent.
    //
    // Currently applied to the six methods the scanner proved have this shape:
    //   ScenarioProfileGrandchild::RefreshScenario, ScenarioProfileNieceNephew::RefreshScenario,
    //   ScenarioProfileLover::RefreshScenario, ScenarioProfileSibling::RefreshScenario,
    //   DataHub::GetRandomRealActor(System.String,System.Int32),
    //   VoltronManager::GetGenderSwapDataForData(System.String)
    public static void FixStructComparison(MethodDefinition method)
    {
        var module = method.Module;
        var body = method.Body;
        var instructions = body.Instructions;
        for (var index = 2; index < instructions.Count; index++)
        {
            var comparison = instructions[index];
            var constant = instructions[index - 1];
            var load = instructions[index - 2];

            var operatorName = comparison.OpCode.Code switch
            {
                Code.Clt or Code.Clt_Un => "op_LessThan",
                Code.Cgt or Code.Cgt_Un => "op_GreaterThan",
                Code.Ceq => "op_Equality",
                _ => null
            };
            if (operatorName is null) continue;
            if (!IsIntConstant(constant)) continue;
            if (!TryGetLoadedType(method, load, out var type)) continue;

            var definition = type.Resolve();
            if (definition is null || !definition.IsValueType) continue;
            var compare = definition.Methods.FirstOrDefault(candidate =>
                candidate.IsStatic && candidate.Name == operatorName && candidate.Parameters.Count == 2 &&
                candidate.Parameters[0].ParameterType.FullName == definition.FullName &&
                candidate.Parameters[1].ParameterType.FullName == definition.FullName);
            var convert = definition.Methods.FirstOrDefault(candidate =>
                candidate.IsStatic && candidate.Name == "op_Implicit" && candidate.Parameters.Count == 1 &&
                candidate.Parameters[0].ParameterType.FullName == "System.Int32");
            if (compare is null || convert is null) continue;

            body.GetILProcessor().InsertAfter(constant, Instruction.Create(OpCodes.Call, module.ImportReference(convert)));
            comparison.OpCode = OpCodes.Call;
            comparison.Operand = module.ImportReference(compare);
        }
    }

    private static bool TryGetLoadedType(MethodDefinition method, Instruction instruction, out TypeReference type)
    {
        type = null;
        var body = method.Body;
        switch (instruction.OpCode.Code)
        {
            case Code.Ldloc:
            case Code.Ldloc_S:
                type = instruction.Operand switch
                {
                    VariableDefinition variable => variable.VariableType,
                    int slot when slot >= 0 && slot < body.Variables.Count => body.Variables[slot].VariableType,
                    _ => null
                };
                return type is not null;
            case Code.Ldloc_0:
            case Code.Ldloc_1:
            case Code.Ldloc_2:
            case Code.Ldloc_3:
            {
                var slot = instruction.OpCode.Code - Code.Ldloc_0;
                if (slot >= body.Variables.Count) return false;
                type = body.Variables[slot].VariableType;
                return true;
            }
            case Code.Ldfld:
            case Code.Ldsfld:
                if (instruction.Operand is FieldReference field)
                {
                    type = field.FieldType;
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    private static bool IsIntConstant(Instruction instruction)
    {
        return instruction.OpCode.Code is Code.Ldc_I4 or Code.Ldc_I4_S or Code.Ldc_I4_0 or Code.Ldc_I4_1 or Code.Ldc_I4_2
            or Code.Ldc_I4_3 or Code.Ldc_I4_4 or Code.Ldc_I4_5 or Code.Ldc_I4_6 or Code.Ldc_I4_7 or Code.Ldc_I4_8
            or Code.Ldc_I4_M1;
    }
}
