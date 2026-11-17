using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class FlipTextureMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var module = method.Module;
        var data = method.Parameters.Single(parameter => parameter.Name == "pData");
        var color32Type = ((ArrayType)data.ParameterType).ElementType;
        var size = Field(module, method.DeclaringType, "_size");
        var sizeX = Field(module, size.FieldType, "x");
        var sizeY = Field(module, size.FieldType, "y");

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var int32Type = module.TypeSystem.Int32;
        var width = new VariableDefinition(int32Type);
        var height = new VariableDefinition(int32Type);
        var row = new VariableDefinition(int32Type);
        var left = new VariableDefinition(int32Type);
        var right = new VariableDefinition(int32Type);
        var temporary = new VariableDefinition(color32Type);
        foreach (var local in new[] { width, height, row, left, right, temporary })
            body.Variables.Add(local);

        var il = body.Instructions;
        var rowLoop = Instruction.Create(OpCodes.Nop);
        var swapLoop = Instruction.Create(OpCodes.Nop);
        var nextRow = Instruction.Create(OpCodes.Nop);
        var returnMethod = Instruction.Create(OpCodes.Nop);

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
        il.Add(Instruction.Create(OpCodes.Ldloc, height));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, returnMethod));

        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, row));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, left));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Stloc, right));

        il.Add(rowLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, left));
        il.Add(Instruction.Create(OpCodes.Ldloc, right));
        il.Add(Instruction.Create(OpCodes.Bge, nextRow));

        il.Add(swapLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, left));
        il.Add(Instruction.Create(OpCodes.Ldelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Stloc, temporary));
        il.Add(Instruction.Create(OpCodes.Ldarg, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, left));
        il.Add(Instruction.Create(OpCodes.Ldarg, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, right));
        il.Add(Instruction.Create(OpCodes.Ldelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Stelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Ldarg, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, right));
        il.Add(Instruction.Create(OpCodes.Ldloc, temporary));
        il.Add(Instruction.Create(OpCodes.Stelem_Any, color32Type));
        il.Add(Instruction.Create(OpCodes.Ldloc, left));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, left));
        il.Add(Instruction.Create(OpCodes.Ldloc, right));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Stloc, right));
        il.Add(Instruction.Create(OpCodes.Ldloc, left));
        il.Add(Instruction.Create(OpCodes.Ldloc, right));
        il.Add(Instruction.Create(OpCodes.Blt, swapLoop));

        il.Add(nextRow);
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, right));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, right));
        il.Add(Instruction.Create(OpCodes.Ldloc, left));
        il.Add(Instruction.Create(OpCodes.Ldloc, width));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, left));
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, height));
        il.Add(Instruction.Create(OpCodes.Bne_Un, rowLoop));

        il.Add(returnMethod);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        return module.ImportReference(declaringType.Resolve().Fields.Single(field => field.Name == name));
    }
}
