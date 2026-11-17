using Mono.Cecil;
using Mono.Cecil.Cil;

public static class NullableGameplayMethodRepairs
{
    public static void FixPotionEligibility(MethodDefinition method)
    {
        RequireMethod(method, "ScenarioItemPromiscuityPotionSideEffectsPushScenario", "ScenarioIsEligibleForRandomPush", "System.Boolean");
        RepairPotionExceptionFlow(method);
        if (HasSequence(method,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 132),
            instruction => instruction.OpCode == OpCodes.Brtrue,
            instruction => instruction.OpCode == OpCodes.Ldc_I4_0,
            instruction => IsLocal(instruction, OpCodes.Stloc, 79),
            instruction => instruction.OpCode == OpCodes.Br)) return;
        var sufferingFromStd = BooleanLocal(method, 132);
        var result = BooleanLocal(method, 79);
        var continueEligibility = At(method, 0x07CB);
        var returnResult = At(method, 0x0E3F);

        // Native 0x4E83514-0x4E83524 constructs Nullable<Boolean>, then rejects
        // both a missing value and false. V_132 already holds the native call result.
        var gateEntry = At(method, 0x0732);
        RedirectObsoleteBranch(method, 0x0753, 0x0758, gateEntry);
        RedirectObsoleteBranch(method, 0x0792, 0x0797, gateEntry);
        ReplaceRange(method, 0x0732, 0x07CB,
            Instruction.Create(OpCodes.Ldloc, sufferingFromStd),
            Instruction.Create(OpCodes.Brtrue, continueEligibility),
            Instruction.Create(OpCodes.Ldc_I4_0),
            Instruction.Create(OpCodes.Stloc, result),
            Instruction.Create(OpCodes.Br, returnResult));
    }

    public static void FixHustleRefresh(MethodDefinition method)
    {
        RequireMethod(method, "ScenarioHustleEarnings", "RefreshScenario", "System.Void");
        var corelib = method.Module.AssemblyReferences.Single(reference => reference.Name == "mscorlib");
        var mathType = new TypeReference("System", "Math", method.Module, corelib);
        var roundDefinition = mathType.Resolve().Methods.Single(candidate =>
            candidate.Name == "Round" && candidate.Parameters.Count == 1 &&
            candidate.Parameters[0].ParameterType.FullName == "System.Double");
        var unityRound = method.Module.ImportReference(roundDefinition);
        foreach (var instruction in method.Body.Instructions.Where(IsMathRound))
            instruction.Operand = unityRound;
        if (HasSequence(method,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 31),
            instruction => instruction.OpCode == OpCodes.Brfalse))
        {
            RepairHustleNullableFlows(method);
            return;
        }
        var validConfiguration = BooleanLocal(method, 31);
        var recoverInvalidHustle = At(method, 0x0D23);

        // Native 0x4E26D4C-0x4E26D58 sends null and false to the recovery path.
        var gateEntry = At(method, 0x0062);
        RedirectObsoleteBranch(method, 0x0083, 0x0088, gateEntry);
        RedirectObsoleteBranch(method, 0x00AE, 0x00B3, gateEntry);
        ReplaceRange(method, 0x0062, 0x00DE,
            Instruction.Create(OpCodes.Ldloc, validConfiguration),
            Instruction.Create(OpCodes.Brfalse, recoverInvalidHustle));
        RepairHustleNullableFlows(method);
    }

    public static void FixFriendshipProcess(MethodDefinition method)
    {
        RequireMethod(method, "ScenarioFriendStatusOutbound", "ProcessFriendship", "System.Void", "System.Collections.ArrayList");
        RepairFriendshipExceptionFlow(method);
        if (HasSequence(method,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 711),
            instruction => instruction.OpCode == OpCodes.Brfalse)) return;
        var friendIsCoworker = BooleanLocal(method, 711);
        var skipFriend = At(method, 0x721B);

        // Native 0x4DDC254-0x4DDC260 treats null and false identically; only
        // a true Contains result reaches the coworker-specific processing path.
        RedirectObsoleteBranch(method, 0x2D66, 0x2D6B, At(method, 0x2D17));
        ReplaceRange(method, 0x2D17, 0x2D9E,
            Instruction.Create(OpCodes.Ldloc, friendIsCoworker),
            Instruction.Create(OpCodes.Brfalse, skipFriend));
    }

    public static void FixHookupChoice(MethodDefinition method)
    {
        RequireMethod(method, "ScenarioFriendHookup", "ChoiceSelectedAtIndex", "System.Void", "System.Int32");
        RepairHookupExceptionFlow(method);
        if (HasSequence(method,
            instruction => instruction.OpCode == OpCodes.Ldarg_0,
            instruction => IsField(instruction, "_loverCandidate"),
            instruction => IsField(instruction, "<Age>k__BackingField"),
            instruction => instruction.OpCode == OpCodes.Ldc_I4 && (int)instruction.Operand == 56,
            instruction => instruction.OpCode == OpCodes.Ceq,
            instruction => instruction.OpCode == OpCodes.Ldc_I4_0,
            instruction => instruction.OpCode == OpCodes.Ceq,
            instruction => instruction.OpCode == OpCodes.Ldloc && instruction.Operand is VariableDefinition,
            instruction => instruction.OpCode == OpCodes.Or,
            instruction => instruction.OpCode == OpCodes.Ldloc && instruction.Operand is VariableDefinition,
            instruction => instruction.OpCode == OpCodes.Or,
            instruction => IsLocal(instruction, OpCodes.Stloc, 48),
            instruction => instruction.OpCode == OpCodes.Br)) return;
        var body = method.Body;
        body.InitLocals = true;
        var proxyIsCoworker = new VariableDefinition(method.Module.TypeSystem.Boolean);
        var loverIsCoworker = new VariableDefinition(method.Module.TypeSystem.Boolean);
        body.Variables.Add(proxyIsCoworker);
        body.Variables.Add(loverIsCoworker);

        var proxyContainsResult = BooleanLocal(method, 484);
        var loverContainsResult = BooleanLocal(method, 472);
        var shouldContinue = BooleanLocal(method, 48);
        var loverField = FieldAt(method, 0x0193, "_loverCandidate");
        var ageField = FieldAt(method, 0x02FF, "<Age>k__BackingField");
        var combineMembership = At(method, 0x659C);
        var evaluateGate = At(method, 0x136E);
        var gateDone = At(method, 0x65B6);
        var nullWorkplaceBranch = At(method, 0x11EF);
        Require(nullWorkplaceBranch.Operand == At(method, 0x6557),
            "Hookup null-workplace branch did not match IL_11EF");

        // Native 0x4DC8770-0x4DC877C extracts the first Contains result. Keep
        // that Boolean directly rather than materializing a Nullable<Boolean>.
        ReplaceRange(method, 0x0533, 0x057D,
            Instruction.Create(OpCodes.Ldloc, proxyContainsResult),
            Instruction.Create(OpCodes.Stloc, proxyIsCoworker));

        // Null workplaces make the corresponding membership test false.
        ReplaceRange(method, 0x1225, 0x122A,
            Instruction.Create(OpCodes.Ldc_I4_0),
            Instruction.Create(OpCodes.Stloc, proxyIsCoworker));
        ReplaceRange(method, 0x127D, 0x1282,
            Instruction.Create(OpCodes.Ldc_I4_0),
            Instruction.Create(OpCodes.Stloc, proxyIsCoworker));

        // Native 0x4DC8B38-0x4DC8B50 extracts the second Contains result.
        ReplaceRange(method, 0x1301, 0x135B,
            Instruction.Create(OpCodes.Ldloc, loverContainsResult),
            Instruction.Create(OpCodes.Stloc, loverIsCoworker),
            Instruction.Create(OpCodes.Br, combineMembership));
        ReplaceRange(method, 0x135B, 0x136E,
            Instruction.Create(OpCodes.Ldc_I4_0),
            Instruction.Create(OpCodes.Stloc, loverIsCoworker),
            Instruction.Create(OpCodes.Br, combineMembership));

        // Skip the corrupt object/nullable AND and the Valentine side path.
        ReplaceRange(method, 0x659C, 0x65B6,
            Instruction.Create(OpCodes.Br, evaluateGate));
        nullWorkplaceBranch.Operand = combineMembership;

        // The obsolete gate contains two internal branches: IL_13A9 -> IL_13C7
        // and IL_13AE -> IL_13B3. Redirect both to the preserved gate entry before
        // deleting them, so range validation sees no references into its interior.
        // External entries IL_1592 and IL_6597 already target that same entry.
        var obsoleteAgeBranch = At(method, 0x13A9);
        var obsoleteMembershipBranch = At(method, 0x13AE);
        Require(obsoleteAgeBranch.Operand == At(method, 0x13C7) &&
                obsoleteMembershipBranch.Operand == At(method, 0x13B3),
            "Hookup gate branches did not match IL_13A9 and IL_13AE");
        obsoleteAgeBranch.Operand = evaluateGate;
        obsoleteMembershipBranch.Operand = evaluateGate;

        // Native 0x4DC8B58-0x4DC8B68 ORs both membership bits and skips scenario
        // creation only for age 56 with neither bit set. Replace the entire gate.
        ReplaceRange(method, 0x136E, 0x13D5,
            Instruction.Create(OpCodes.Ldarg_0),
            Instruction.Create(OpCodes.Ldfld, loverField),
            Instruction.Create(OpCodes.Ldfld, ageField),
            Instruction.Create(OpCodes.Ldc_I4, 56),
            Instruction.Create(OpCodes.Ceq),
            Instruction.Create(OpCodes.Ldc_I4_0),
            Instruction.Create(OpCodes.Ceq),
            Instruction.Create(OpCodes.Ldloc, proxyIsCoworker),
            Instruction.Create(OpCodes.Or),
            Instruction.Create(OpCodes.Ldloc, loverIsCoworker),
            Instruction.Create(OpCodes.Or),
            Instruction.Create(OpCodes.Stloc, shouldContinue),
            Instruction.Create(OpCodes.Br, gateDone));
    }

    private static void RepairPotionExceptionFlow(MethodDefinition method)
    {
        // Native 0x4E835EC-0x4E83618 catches an object, and 0x4E834C8 null-tests
        // it. All three locals only carry that object or null; none carries a Boolean.
        RetypeNullableLocal(method, 21, method.Module.TypeSystem.Object);
        RetypeNullableLocal(method, 26, method.Module.TypeSystem.Object);
        RetypeNullableLocal(method, 68, method.Module.TypeSystem.Object);
    }

    private static void RepairFriendshipExceptionFlow(MethodDefinition method)
    {
        // Native 0x4DDFAC8-0x4DDFAD4 catches an object; 0x4DDFB58-0x4DDFB5C
        // reloads and null-tests that same object before returning or rethrowing.
        RetypeNullableLocal(method, 21, method.Module.TypeSystem.Object);
        RetypeNullableLocal(method, 383, method.Module.TypeSystem.Object);
    }

    private static void RepairHookupExceptionFlow(MethodDefinition method)
    {
        // Native 0x4DCD190-0x4DCD19C catches an object; 0x4DCCFF4-0x4DCCFF8
        // null-tests it. V_25 and V_437 have no non-exception consumers.
        RetypeNullableLocal(method, 25, method.Module.TypeSystem.Object);
        RetypeNullableLocal(method, 437, method.Module.TypeSystem.Object);
    }

    private static void RepairHustleNullableFlows(MethodDefinition method)
    {
        var caughtException = RetypeNullableLocal(method, 55, method.Module.TypeSystem.Object);
        Require(Local(method, 660).VariableType.FullName == "System.Object",
            "Hustle V_660 is not the caught exception object");

        // Native 0x4E37610-0x4E3763C stores the caught object for the null test at
        // 0x4E373B0. The duplicate spill into V_15 must use the object local instead.
        var instructions = method.Body.Instructions;
        for (var index = 1; index < instructions.Count; index++)
        {
            if (IsLocal(instructions[index - 1], OpCodes.Ldloc, 660) &&
                IsLocal(instructions[index], OpCodes.Stloc, 15))
                instructions[index].Operand = caughtException;
        }

        // V_15 is numeric everywhere else. V_632 and V_639 are its damaged rounding
        // spills; make every remaining store/load type-correct before replacing the
        // corrupt consumer block.
        RetypeNullableLocal(method, 15, method.Module.TypeSystem.Double);
        RetypeNullableLocal(method, 632, method.Module.TypeSystem.Double);
        RetypeNullableLocal(method, 639, method.Module.TypeSystem.Double);
        Require(Local(method, 941).VariableType.FullName == "System.Int32",
            "Hustle V_941 is not Int32");
        for (var index = 1; index < instructions.Count; index++)
        {
            if (!IsLocal(instructions[index], OpCodes.Stloc, 15)) continue;
            var source = instructions[index - 1];
            if (source.OpCode == OpCodes.Ldc_I4_0 ||
                (source.OpCode == OpCodes.Ldc_I4 && (int)source.Operand == 0))
            {
                source.OpCode = OpCodes.Ldc_R8;
                source.Operand = 0D;
            }
            else if (IsLocal(source, OpCodes.Ldloc, 941))
            {
                instructions.Insert(index, Instruction.Create(OpCodes.Conv_R8));
                index++;
                source = instructions[index - 1];
            }
            Require(source.OpCode == OpCodes.Ldc_R8 || source.OpCode == OpCodes.Conv_R8,
                "Unexpected store into Hustle numeric local V_15");
        }

        if (HasSequence(method,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 636),
            instruction => IsMathRound(instruction),
            instruction => IsLocal(instruction, OpCodes.Stloc, 565))) return;

        var hasOriginalRoundConsumer = HasSequence(method,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 632),
            instruction => instruction.OpCode == OpCodes.Conv_R8,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 817),
            instruction => instruction.OpCode == OpCodes.Conv_R8,
            instruction => instruction.OpCode == OpCodes.Add,
            instruction => IsLocal(instruction, OpCodes.Stloc, 631));
        if (!hasOriginalRoundConsumer)
        {
            // A serialized rerun may already contain a different complete numeric
            // reconstruction. Never require the original consumer in that state,
            // but refuse to ignore any remaining damaged consumer or native fallback.
            Require(!HasSequence(method,
                        instruction => IsLocal(instruction, OpCodes.Ldloc, 632),
                        instruction => instruction.OpCode == OpCodes.Conv_R8) &&
                    !HasSequence(method,
                        instruction => IsLocal(instruction, OpCodes.Ldloc, 639),
                        instruction => instruction.OpCode == OpCodes.Ldc_I4_1,
                        instruction => instruction.OpCode == OpCodes.And) &&
                    !instructions.Any(instruction => instruction.OpCode == OpCodes.Ldstr &&
                        instruction.Operand is string text && (text.Contains("FRINTP") || text.Contains("FRINTM"))),
                "Hustle numeric reconstruction is incomplete");
            return;
        }

        var roundConsumer = FindSequence(method,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 632),
            instruction => instruction.OpCode == OpCodes.Conv_R8,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 817),
            instruction => instruction.OpCode == OpCodes.Conv_R8,
            instruction => instruction.OpCode == OpCodes.Add,
            instruction => IsLocal(instruction, OpCodes.Stloc, 631));
        var continueRefresh = FindSequence(method,
            instruction => instruction.OpCode == OpCodes.Ldarg_0,
            instruction => instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference callee && callee.Name == "GetAnnualHaulCash");
        var ceilingPath = FindOptionalInstruction(method, instruction =>
            instruction.OpCode == OpCodes.Ldstr && instruction.Operand is string text && text.Contains("FRINTP"));
        var floorPath = FindOptionalInstruction(method, instruction =>
            instruction.OpCode == OpCodes.Ldstr && instruction.Operand is string text && text.Contains("FRINTM"));
        Require((ceilingPath is null) == (floorPath is null),
            "Hustle numeric reconstruction contains only one native rounding fallback");
        var corelib = method.Module.AssemblyReferences.Single(reference => reference.Name == "mscorlib");
        var mathType = new TypeReference("System", "Math", method.Module, corelib);
        var roundDefinition = mathType.Resolve().Methods.Single(candidate =>
            candidate.Name == "Round" && candidate.Parameters.Count == 1 &&
            candidate.Parameters[0].ParameterType.FullName == "System.Double");
        var round = method.Module.ImportReference(roundDefinition);

        // Native 0x4E27370-0x4E2749C implements nearest-integer, ties-to-even
        // rounding, including its FRINTP/FRINTM paths. Math.Round(Double) preserves
        // that behavior for the float value exactly represented by V_636.
        ReplaceRange(method, roundConsumer.Offset, continueRefresh.Offset,
            Instruction.Create(OpCodes.Ldloc, Local(method, 636)),
            Instruction.Create(OpCodes.Call, round),
            Instruction.Create(OpCodes.Stloc, Local(method, 565)),
            Instruction.Create(OpCodes.Br, continueRefresh));
        if (ceilingPath is null || floorPath is null) return;
        var floorPathEnd = FindSequence(method,
            instruction => IsLocal(instruction, OpCodes.Ldloc, 575),
            IsIntegerZero,
            instruction => instruction.OpCode == OpCodes.Cgt);
        ReplaceRange(method, ceilingPath.Offset, floorPath.Offset,
            Instruction.Create(OpCodes.Br, roundConsumer));
        ReplaceRange(method, floorPath.Offset, floorPathEnd.Offset,
            Instruction.Create(OpCodes.Br, roundConsumer));
    }

    private static bool IsIntegerZero(Instruction instruction)
    {
        return instruction.OpCode == OpCodes.Ldc_I4_0 ||
               (instruction.OpCode == OpCodes.Ldc_I4 && (int)instruction.Operand == 0);
    }

    private static Instruction? FindOptionalInstruction(MethodDefinition method, Func<Instruction, bool> predicate)
    {
        var matches = method.Body.Instructions.Where(predicate).ToArray();
        Require(matches.Length <= 1, $"Expected at most one matching instruction in {method.FullName}, found {matches.Length}");
        return matches.SingleOrDefault();
    }

    private static VariableDefinition RetypeNullableLocal(MethodDefinition method, int index, TypeReference targetType)
    {
        var local = Local(method, index);
        Require(local.VariableType.FullName == "System.Nullable`1<System.Boolean>" || local.VariableType.FullName == targetType.FullName,
            $"V_{index} is neither Nullable<Boolean> nor {targetType.FullName} in {method.FullName}");
        local.VariableType = targetType;
        return local;
    }

    private static VariableDefinition Local(MethodDefinition method, int index)
    {
        return method.Body.Variables.SingleOrDefault(variable => variable.Index == index) ??
               throw new InvalidOperationException($"Missing local V_{index} in {method.FullName}");
    }

    private static Instruction FindInstruction(MethodDefinition method, Func<Instruction, bool> predicate)
    {
        var matches = method.Body.Instructions.Where(predicate).ToArray();
        Require(matches.Length == 1, $"Expected one matching instruction in {method.FullName}, found {matches.Length}");
        return matches[0];
    }

    private static Instruction FindSequence(MethodDefinition method, params Func<Instruction, bool>[] predicates)
    {
        var instructions = method.Body.Instructions;
        var matches = new List<Instruction>();
        for (var start = 0; start <= instructions.Count - predicates.Length; start++)
        {
            var matched = true;
            for (var index = 0; index < predicates.Length; index++)
            {
                if (predicates[index](instructions[start + index])) continue;
                matched = false;
                break;
            }
            if (matched) matches.Add(instructions[start]);
        }
        Require(matches.Count == 1, $"Expected one matching instruction sequence in {method.FullName}, found {matches.Count}");
        return matches[0];
    }

    private static bool IsMathRound(Instruction instruction)
    {
        return instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference callee &&
               callee.DeclaringType.FullName == "System.Math" && callee.Name == "Round" &&
               callee.Parameters.Count == 1 && callee.Parameters[0].ParameterType.FullName == "System.Double";
    }

    private static bool HasSequence(MethodDefinition method, params Func<Instruction, bool>[] predicates)
    {
        var instructions = method.Body.Instructions;
        for (var start = 0; start <= instructions.Count - predicates.Length; start++)
        {
            var matched = true;
            for (var index = 0; index < predicates.Length; index++)
            {
                if (predicates[index](instructions[start + index])) continue;
                matched = false;
                break;
            }
            if (matched) return true;
        }
        return false;
    }

    private static bool IsLocal(Instruction instruction, OpCode opCode, int index)
    {
        return instruction.OpCode == opCode && instruction.Operand is VariableDefinition variable && variable.Index == index;
    }

    private static bool IsField(Instruction instruction, string name)
    {
        return instruction.OpCode == OpCodes.Ldfld && instruction.Operand is FieldReference field && field.Name == name;
    }

    private static void ReplaceRange(MethodDefinition method, int startOffset, int endOffset, params Instruction[] replacement)
    {
        Require(replacement.Length > 0, "An IL replacement must contain an instruction");
        var instructions = method.Body.Instructions;
        var start = At(method, startOffset);
        var end = At(method, endOffset);
        var startIndex = instructions.IndexOf(start);
        var endIndex = instructions.IndexOf(end);
        Require(startIndex >= 0 && endIndex > startIndex,
            $"Invalid IL replacement range IL_{startOffset:x4}-IL_{endOffset:x4}");

        var removedInterior = instructions.Skip(startIndex + 1).Take(endIndex - startIndex - 1).ToHashSet();
        foreach (var instruction in instructions)
        {
            if (instruction.Operand is Instruction target && removedInterior.Contains(target))
                throw new InvalidOperationException($"Branch at IL_{instruction.Offset:x4} enters removed range IL_{startOffset:x4}-IL_{endOffset:x4}");
            if (instruction.Operand is Instruction[] targets && targets.Any(removedInterior.Contains))
                throw new InvalidOperationException($"Switch at IL_{instruction.Offset:x4} enters removed range IL_{startOffset:x4}-IL_{endOffset:x4}");
        }
        foreach (var handler in method.Body.ExceptionHandlers)
        {
            foreach (var boundary in new[] { handler.TryStart, handler.TryEnd, handler.HandlerStart, handler.HandlerEnd, handler.FilterStart })
            {
                if (boundary is not null && removedInterior.Contains(boundary))
                    throw new InvalidOperationException($"Exception-handler boundary enters removed range IL_{startOffset:x4}-IL_{endOffset:x4}");
            }
        }
        Require(!replacement.Any(instruction => instruction.Operand is Instruction target && removedInterior.Contains(target)),
            "Replacement IL targets a removed instruction");

        start.OpCode = replacement[0].OpCode;
        start.Operand = replacement[0].Operand;
        for (var index = 0; index < endIndex - startIndex - 1; index++)
            instructions.RemoveAt(startIndex + 1);
        for (var index = 1; index < replacement.Length; index++)
            instructions.Insert(startIndex + index, replacement[index]);
    }

    private static void RedirectObsoleteBranch(MethodDefinition method, int branchOffset, int expectedTargetOffset, Instruction preservedTarget)
    {
        var branch = At(method, branchOffset);
        Require(branch.Operand == At(method, expectedTargetOffset),
            $"Branch at IL_{branchOffset:x4} did not target IL_{expectedTargetOffset:x4}");
        branch.Operand = preservedTarget;
    }

    private static VariableDefinition BooleanLocal(MethodDefinition method, int index)
    {
        var local = method.Body.Variables.SingleOrDefault(variable => variable.Index == index) ??
                    throw new InvalidOperationException($"Missing local V_{index} in {method.FullName}");
        Require(local.VariableType.FullName == "System.Boolean", $"V_{index} is not Boolean in {method.FullName}");
        return local;
    }

    private static FieldReference FieldAt(MethodDefinition method, int offset, string name)
    {
        var instruction = At(method, offset);
        Require(instruction.OpCode == OpCodes.Ldfld && instruction.Operand is FieldReference field && field.Name == name,
            $"Expected field {name} at IL_{offset:x4}");
        return (FieldReference)instruction.Operand;
    }

    private static Instruction At(MethodDefinition method, int offset)
    {
        return method.Body.Instructions.SingleOrDefault(instruction => instruction.Offset == offset) ??
               throw new InvalidOperationException($"Missing IL_{offset:x4} in {method.FullName}");
    }

    private static void RequireMethod(MethodDefinition method, string declaringType, string name, string returnType, params string[] parameters)
    {
        Require(method.DeclaringType.FullName == declaringType && method.Name == name && method.HasBody &&
                method.ReturnType.FullName == returnType && method.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameters),
            $"Unexpected method signature: {method.FullName}");
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
