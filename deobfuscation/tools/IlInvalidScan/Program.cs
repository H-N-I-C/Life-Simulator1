using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class Program
{
    private static void Main(string[] args)
    {
        var path = args[0];
        using var module = ModuleDefinition.ReadModule(path);
        var dummy = 0;
        var suspicious = new List<string>();
        var surgicalCandidates = new List<string>();
        var imbalanced = new List<string>();
        var methods = 0;
        foreach (var type in module.GetTypes())
        foreach (var method in type.Methods)
        {
            if (!method.HasBody) continue;
            methods++;
            var isDummy = false;
            foreach (var ins in method.Body.Instructions)
                if (ins.OpCode.Code == Code.Call && ins.Operand is MethodReference mr &&
                    mr.DeclaringType.Name == "Cpp2ILHelpers" && mr.Name == "NoteDecompilerIssue")
                { isDummy = true; break; }
            if (isDummy) dummy++;
            if (LooksStackInvalid(method, out var surgical))
            {
                suspicious.Add(method.FullName);
                if (surgical) surgicalCandidates.Add(method.FullName);
            }
            if (HasStackImbalance(method)) imbalanced.Add(method.FullName);
        }
        Console.WriteLine($"methods with body: {methods}");
        Console.WriteLine($"methods still containing Cpp2ILHelpers.NoteDecompilerIssue: {dummy}");
        Console.WriteLine($"methods with a value-type load fed straight into an arithmetic/comparison op: {suspicious.Count}");
        Console.WriteLine($"methods with an inconsistent evaluation-stack height at a join: {imbalanced.Count}");
        foreach (var name in imbalanced.Take(20)) Console.WriteLine("  IMBALANCE " + name);
        Console.WriteLine($"methods whose defect is a struct-vs-int comparison with a restorable operator overload: {surgicalCandidates.Count}");
        foreach (var name in surgicalCandidates) Console.WriteLine("  SURGICAL " + name);
        foreach (var name in suspicious) Console.WriteLine("  " + name);
    }

    private static readonly HashSet<Code> Ops = new()
    {
        Code.Add, Code.Sub, Code.Mul, Code.Div, Code.Rem, Code.And, Code.Or, Code.Xor,
        Code.Shl, Code.Shr, Code.Ceq, Code.Cgt, Code.Clt, Code.Cgt_Un, Code.Clt_Un,
        Code.Beq, Code.Bne_Un, Code.Bge, Code.Bgt, Code.Ble, Code.Blt, Code.Bge_Un, Code.Bgt_Un, Code.Ble_Un, Code.Blt_Un
    };

    private static bool LooksStackInvalid(MethodDefinition method, out bool surgical)
    {
        surgical = false;
        var body = method.Body;
        var instructions = body.Instructions;
        for (var i = 0; i < instructions.Count; i++)
        {
            var ins = instructions[i];
            if (!Ops.Contains(ins.OpCode.Code)) continue;
            // Look back for the most recent producer of a value-type value loaded by value.
            for (var j = i - 1; j >= 0 && j >= i - 4; j--)
            {
                var previous = instructions[j];
                if (TryGetLoadedType(method, previous, out var loadedType) && IsProblemType(loadedType))
                {
                    surgical = IsSurgicalSite(method, instructions, j, loadedType);
                    return true;
                }
            }
        }
        return false;
    }

    // A defect is surgically repairable when the decompiler lost a comparison operator:
    // load <struct>; ldc.i4 <const>; clt/cgt/ceq, and the struct defines the matching operator plus
    // op_Implicit(int32). Rewriting it to op_X(struct, op_Implicit(const)) restores the original C#.
    private static bool IsSurgicalSite(MethodDefinition method, IList<Instruction> instructions, int loadIndex, TypeReference type)
    {
        if (loadIndex + 2 >= instructions.Count) return false;
        var constant = instructions[loadIndex + 1];
        var comparison = instructions[loadIndex + 2];
        if (!IsIntConstant(constant)) return false;
        var operatorName = comparison.OpCode.Code switch
        {
            Code.Clt or Code.Clt_Un => "op_LessThan",
            Code.Cgt or Code.Cgt_Un => "op_GreaterThan",
            Code.Ceq => "op_Equality",
            _ => null
        };
        if (operatorName is null) return false;
        try
        {
            var definition = type.Resolve();
            if (definition is null) return false;
            var hasOperator = definition.Methods.Any(m => m.IsStatic && m.Name == operatorName && m.Parameters.Count == 2);
            var hasIntConversion = definition.Methods.Any(m => m.IsStatic && m.Name == "op_Implicit" && m.Parameters.Count == 1
                && m.Parameters[0].ParameterType.FullName == "System.Int32");
            return hasOperator && hasIntConversion;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsIntConstant(Instruction instruction)
    {
        return instruction.OpCode.Code is Code.Ldc_I4 or Code.Ldc_I4_S or Code.Ldc_I4_0 or Code.Ldc_I4_1 or Code.Ldc_I4_2
            or Code.Ldc_I4_3 or Code.Ldc_I4_4 or Code.Ldc_I4_5 or Code.Ldc_I4_6 or Code.Ldc_I4_7 or Code.Ldc_I4_8
            or Code.Ldc_I4_M1;
    }

    // Resolves the type pushed by an instruction that loads a value by value: locals (short and
    // implicit forms), arguments (short and implicit forms) and fields (instance and static).
    private static bool TryGetLoadedType(MethodDefinition method, Instruction instruction, out TypeReference loadedType)
    {
        loadedType = null;
        var body = method.Body;
        switch (instruction.OpCode.Code)
        {
            case Code.Ldloc:
            case Code.Ldloc_S:
            {
                var index = instruction.Operand switch
                {
                    VariableDefinition v => v.Index,
                    int n => n,
                    _ => -1
                };
                if (index < 0 || index >= body.Variables.Count) return false;
                loadedType = body.Variables[index].VariableType;
                return true;
            }
            case Code.Ldloc_0:
            case Code.Ldloc_1:
            case Code.Ldloc_2:
            case Code.Ldloc_3:
            {
                var index = instruction.OpCode.Code - Code.Ldloc_0;
                if (index >= body.Variables.Count) return false;
                loadedType = body.Variables[index].VariableType;
                return true;
            }
            case Code.Ldarg:
            case Code.Ldarg_S:
            {
                // A ParameterDefinition operand is already in parameter space (this excluded);
                // a raw int operand is in argument-slot space (this included when HasThis).
                if (instruction.Operand is ParameterDefinition parameter)
                {
                    loadedType = parameter.ParameterType;
                    return true;
                }
                var index = instruction.Operand is int n ? n : -1;
                return TryArgumentType(method, index, out loadedType);
            }
            case Code.Ldarg_0:
            case Code.Ldarg_1:
            case Code.Ldarg_2:
            case Code.Ldarg_3:
                return TryArgumentType(method, instruction.OpCode.Code - Code.Ldarg_0, out loadedType);
            case Code.Ldfld:
            case Code.Ldsfld:
                if (instruction.Operand is FieldReference field)
                {
                    loadedType = field.FieldType;
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    private static bool TryArgumentType(MethodDefinition method, int index, out TypeReference type)
    {
        type = null;
        var parameterIndex = method.HasThis ? index - 1 : index;
        if (parameterIndex < 0 || parameterIndex >= method.Parameters.Count) return false;
        type = method.Parameters[parameterIndex].ParameterType;
        return true;
    }

    private static readonly HashSet<string> Primitives = new()
    {
        "System.Boolean", "System.Char", "System.SByte", "System.Byte", "System.Int16", "System.UInt16",
        "System.Int32", "System.UInt32", "System.Int64", "System.UInt64", "System.IntPtr", "System.UIntPtr",
        "System.Single", "System.Double"
    };

    // IL2CPP's StackTypeFor throws for value types it cannot map to a stack type. Primitives map
    // fine, so only non-primitive value types (structs such as Enumerator, Decimal, ValueTuple)
    // are interesting here.
    private static bool IsProblemType(TypeReference type)
    {
        if (Primitives.Contains(type.FullName)) return false;
        if (type.IsPrimitive) return false;
        // Nullable<T> is NOT safe: IL2CPP really does throw 'Cannot get stack type for Nullable`1'
        // (measured on the option-2 diagnostic run), so it must stay in scope.
        try
        {
            var resolved = type.Resolve();
            if (resolved == null) return false;
            if (resolved.IsEnum || resolved.IsPrimitive) return false;
            return resolved.IsValueType;
        }
        catch
        {
            return false;
        }
    }

    // Second failure class: 'Invalid global variables count' thrown by the IL2CPP converter when
    // an instruction is reachable with two different evaluation-stack heights (a broken join).
    private static bool HasStackImbalance(MethodDefinition method)
    {
        var body = method.Body;
        var instructions = body.Instructions;
        if (instructions.Count == 0) return false;
        var heights = new Dictionary<int, int>();
        var work = new Queue<(Instruction Instruction, int Height)>();
        work.Enqueue((instructions[0], 0));
        while (work.Count > 0)
        {
            var (instruction, height) = work.Dequeue();
            if (height < 0) return true;
            var offset = instruction.Offset;
            if (heights.TryGetValue(offset, out var known))
            {
                if (known != height) return true;
                continue;
            }
            heights[offset] = height;
            if (instruction.OpCode.Code == Code.Ret)
            {
                var expected = method.ReturnType.MetadataType == MetadataType.Void ? 0 : 1;
                if (height != expected) return true;
                continue;
            }
            var after = height - PopCount(instruction) + PushCount(instruction);
            if (after < 0) return true;
            if (instruction.Operand is Instruction target) work.Enqueue((target, after));
            else if (instruction.Operand is Instruction[] targets)
                foreach (var t in targets) work.Enqueue((t, after));
            if (instruction.OpCode.FlowControl == FlowControl.Branch
                || instruction.OpCode.FlowControl == FlowControl.Return
                || instruction.OpCode.FlowControl == FlowControl.Throw) continue;
            if (instruction.Next != null) work.Enqueue((instruction.Next, after));
        }
        return false;
    }

    private static int PopCount(Instruction instruction)
    {
        switch (instruction.OpCode.StackBehaviourPop)
        {
            case StackBehaviour.Pop0: return 0;
            case StackBehaviour.Pop1: return 1;
            case StackBehaviour.Pop1_pop1: return 2;
            case StackBehaviour.Popi: return 1;
            case StackBehaviour.Popi_popi: return 2;
            case StackBehaviour.Popi_popi8: return 2;
            case StackBehaviour.Popi_popr4: return 2;
            case StackBehaviour.Popi_popr8: return 2;
            case StackBehaviour.Popref: return 1;
            case StackBehaviour.Popref_pop1: return 2;
            case StackBehaviour.Popref_popi: return 2;
            case StackBehaviour.Popref_popi_popi: return 3;
            case StackBehaviour.Popref_popi_popi8: return 3;
            case StackBehaviour.Popref_popi_popr4: return 3;
            case StackBehaviour.Popref_popi_popr8: return 3;
            case StackBehaviour.Popref_popi_popref: return 3;
            case StackBehaviour.Varpop:
                if (instruction.OpCode.Code == Code.Ret) return 1;
                if (instruction.Operand is MethodReference method)
                    return method.Parameters.Count + (method.HasThis && instruction.OpCode.Code != Code.Newobj ? 1 : 0);
                return 0;
            default: return 0;
        }
    }

    private static int PushCount(Instruction instruction)
    {
        switch (instruction.OpCode.StackBehaviourPush)
        {
            case StackBehaviour.Push0: return 0;
            case StackBehaviour.Push1: return 1;
            case StackBehaviour.Pushi: return 1;
            case StackBehaviour.Pushi8: return 1;
            case StackBehaviour.Pushr4: return 1;
            case StackBehaviour.Pushr8: return 1;
            case StackBehaviour.Pushref: return 1;
            case StackBehaviour.Push1_push1: return 2;
            case StackBehaviour.Varpush:
                if (instruction.Operand is MethodReference method) return method.ReturnType.MetadataType == MetadataType.Void ? 0 : 1;
                return 0;
            default: return 0;
        }
    }
}
