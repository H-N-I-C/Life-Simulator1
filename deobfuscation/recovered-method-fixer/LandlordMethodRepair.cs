using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class LandlordMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var instructions = method.Body.Instructions;
        var countLocal = method.Body.Variables.Single(variable => variable.Index == 648);
        var knownCountCall = At(instructions, 0x71E6);
        var phantomProvider = At(instructions, 0x7E1D);
        var phantomConversion = At(instructions, 0x7E1E);
        var recoveredCountCall = At(instructions, 0x7E1F);

        Require(knownCountCall.OpCode == OpCodes.Call &&
                knownCountCall.Operand is MethodReference knownCount &&
                knownCount.Name == "get_Count" &&
                knownCount.DeclaringType.FullName == "System.Collections.ArrayList" &&
                knownCount.Parameters.Count == 0,
            "Known ArrayList.Count call did not match IL_71E6");

        if (countLocal.VariableType.FullName == "System.Int32" &&
            recoveredCountCall.Operand is MethodReference existingCount &&
            existingCount.Name == "get_Count" &&
            existingCount.DeclaringType.FullName == "System.Collections.ArrayList" &&
            phantomProvider.OpCode == OpCodes.Nop && phantomConversion.OpCode == OpCodes.Nop)
            return;

        Require(countLocal.VariableType.FullName == "System.Decimal",
            "Landlord achievement count local did not match V_648");
        Require(phantomProvider.OpCode == OpCodes.Ldc_I4_0,
            "Phantom IFormatProvider constant did not match IL_7E1D");
        Require(phantomConversion.OpCode == OpCodes.Conv_I,
            "Phantom IFormatProvider conversion did not match IL_7E1E");
        Require(recoveredCountCall.OpCode == OpCodes.Call &&
                recoveredCountCall.Operand is MethodReference decimalConversion &&
                decimalConversion.Name == "System.IConvertible.ToDecimal" &&
                decimalConversion.ReturnType.FullName == "System.Decimal",
            "Misidentified achievement count call did not match IL_7E1F");

        // Native 0x4EB7CDC-0x4EB7CF4 dispatch through the same ArrayList
        // vtable slot as IL_71E6, compare the Int32 result with 10, and skip
        // acLuxurySecretSociety on inequality. No provider argument exists.
        countLocal.VariableType = method.Module.TypeSystem.Int32;
        recoveredCountCall.Operand = method.Module.ImportReference((MethodReference)knownCountCall.Operand);
        phantomProvider.OpCode = OpCodes.Nop;
        phantomProvider.Operand = null;
        phantomConversion.OpCode = OpCodes.Nop;
        phantomConversion.Operand = null;
    }

    private static Instruction At(IEnumerable<Instruction> instructions, int offset)
    {
        return instructions.SingleOrDefault(instruction => instruction.Offset == offset) ??
               throw new InvalidOperationException($"Missing original instruction at IL_{offset:x4}");
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
