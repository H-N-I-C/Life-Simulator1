using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class CasinoMethodRepairs
{
    public static void PatchBet(MethodDefinition method)
    {
        PatchBetAmountLists(method);
        var instructions = method.Body.Instructions;
        var enumerator = method.Body.Variables.Single(variable => variable.Index == 18);

        for (var index = 0; index + 2 < instructions.Count; index++)
        {
            if (!IsLoadAddress(instructions[index], enumerator) ||
                instructions[index + 1].OpCode != OpCodes.Call ||
                instructions[index + 1].Operand is not MethodReference call ||
                call.Name != "get_Current" ||
                instructions[index + 2].OpCode != OpCodes.Conv_R8)
                continue;
            if (instructions.Any(instruction => instruction.OpCode == OpCodes.Ldstr && (string)instruction.Operand == " Slots"))
            {
                call.DeclaringType = (GenericInstanceType)enumerator.VariableType;
                return;
            }
        }

        var enumeratorStore = At(instructions, 0x3FE7);
        var currentLoad = At(instructions, 0x4064);
        var bogusString = At(instructions, 0x3FF7);
        var bogusInteger = At(instructions, 0x3FFC);
        var bogusAdd = At(instructions, 0x4001);
        var bogusStore = At(instructions, 0x4002);
        var suffixLoad = At(instructions, 0x407A);
        var disposalLoad = At(instructions, 0x4E14);

        Require(IsStoreLocal(enumeratorStore, 630), "Bet enumerator store did not match IL_3FE7");
        Require(IsLoadLocal(currentLoad, 24), "Bet current-value load did not match IL_4064");
        Require(bogusString.OpCode == OpCodes.Ldstr, "Bet bogus string did not match IL_3FF7");
        Require(bogusInteger.OpCode == OpCodes.Ldc_I4, "Bet bogus integer did not match IL_3FFC");
        Require(bogusAdd.OpCode == OpCodes.Add, "Bet bogus add did not match IL_4001");
        Require(IsStoreLocal(bogusStore, 646), "Bet bogus store did not match IL_4002");
        Require(IsLoadLocal(suffixLoad, 646), "Bet suffix load did not match IL_407A");
        Require(IsLoadLocal(disposalLoad, 25), "Bet disposal load did not match IL_4E14");

        var enumeratorType = (GenericInstanceType)enumerator.VariableType;
        var getCurrent = ImportMethod(method.Module, enumeratorType, "get_Current", 0, true);

        enumeratorStore.OpCode = OpCodes.Stloc;
        enumeratorStore.Operand = enumerator;
        currentLoad.OpCode = OpCodes.Ldloca;
        currentLoad.Operand = enumerator;
        var currentIndex = instructions.IndexOf(currentLoad);
        instructions.Insert(currentIndex + 1, Instruction.Create(OpCodes.Call, getCurrent));
        instructions.Insert(currentIndex + 2, Instruction.Create(OpCodes.Conv_R8));

        foreach (var instruction in new[] { bogusString, bogusInteger, bogusAdd, bogusStore })
        {
            instruction.OpCode = OpCodes.Nop;
            instruction.Operand = null;
        }

        suffixLoad.OpCode = OpCodes.Ldstr;
        suffixLoad.Operand = " Slots";
        disposalLoad.OpCode = OpCodes.Ldloca;
        disposalLoad.Operand = enumerator;
    }

    private static void PatchBetAmountLists(MethodDefinition method)
    {
        var instructions = method.Body.Instructions;
        if (instructions.Any(instruction => instruction.OpCode == OpCodes.Ldc_R4 && (float)instruction.Operand == 50000f)) return;

        var fourValueEntry = At(instructions, 0x31A9);
        var fiveValueEntry = At(instructions, 0x3353);
        var join = At(instructions, 0x3F65);
        var listType = (GenericInstanceType)Field(method.Module, method.DeclaringType, "_slotsBetAmounts").FieldType;
        var listConstructor = ImportMethod(method.Module, listType, ".ctor", 0, true);
        var add = ImportMethod(method.Module, listType, "Add", 1, true);
        var fourValueList = method.Body.Variables.Single(variable => variable.Index == 96);
        var fiveValueList = method.Body.Variables.Single(variable => variable.Index == 97);
        var selectedList = method.Body.Variables.Single(variable => variable.Index == 615);

        var replacement = new List<Instruction>();
        var newFourValueEntry = Instruction.Create(OpCodes.Newobj, listConstructor);
        replacement.Add(newFourValueEntry);
        replacement.Add(Instruction.Create(OpCodes.Stloc, fourValueList));
        foreach (var value in new[] { 10f, 100f, 500f, 1000f })
        {
            replacement.Add(Instruction.Create(OpCodes.Ldloc, fourValueList));
            replacement.Add(Instruction.Create(OpCodes.Ldc_R4, value));
            replacement.Add(Instruction.Create(OpCodes.Callvirt, add));
        }
        replacement.Add(Instruction.Create(OpCodes.Ldloc, fourValueList));
        replacement.Add(Instruction.Create(OpCodes.Stloc, selectedList));
        replacement.Add(Instruction.Create(OpCodes.Br, join));

        var newFiveValueEntry = Instruction.Create(OpCodes.Newobj, listConstructor);
        replacement.Add(newFiveValueEntry);
        replacement.Add(Instruction.Create(OpCodes.Stloc, fiveValueList));
        foreach (var value in new[] { 10f, 100f, 500f, 1000f, 50000f })
        {
            replacement.Add(Instruction.Create(OpCodes.Ldloc, fiveValueList));
            replacement.Add(Instruction.Create(OpCodes.Ldc_R4, value));
            replacement.Add(Instruction.Create(OpCodes.Callvirt, add));
        }
        replacement.Add(Instruction.Create(OpCodes.Ldloc, fiveValueList));
        replacement.Add(Instruction.Create(OpCodes.Stloc, selectedList));

        foreach (var instruction in instructions.ToArray())
        {
            if (instruction.Operand is not Instruction target) continue;
            if (target == fourValueEntry) instruction.Operand = newFourValueEntry;
            else if (target == fiveValueEntry) instruction.Operand = newFiveValueEntry;
        }

        var startIndex = instructions.IndexOf(fourValueEntry);
        var count = instructions.IndexOf(join) - startIndex;
        Require(count > 0, "Casino bet-list replacement range was invalid");
        for (var index = 0; index < count; index++) instructions.RemoveAt(startIndex);
        for (var index = 0; index < replacement.Count; index++) instructions.Insert(startIndex + index, replacement[index]);
    }

    public static void FixMeetChoice(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var celebrityField = Field(module, owner, "_celebrity");
        var casinoField = Field(module, owner, "_casino");
        var roomsField = Field(module, owner, "_gameRooms");
        var dropDownField = Field(module, owner, "_dropDownContents");
        var narrativeField = Field(module, owner, "_narrative");
        var nextScenarioField = Field(module, owner, "<NextScenario>k__BackingField");
        var nameField = Field(module, celebrityField.FieldType, "<Name>k__BackingField");
        var fameField = Field(module, celebrityField.FieldType, "<Att_fame>k__BackingField");
        var themeField = Field(module, casinoField.FieldType, "<Theme>k__BackingField");
        var popularityField = Field(module, casinoField.FieldType, "_popularity");
        var meetCountField = Field(module, casinoField.FieldType, "<NumTimesWentMeetAndGreet>k__BackingField");
        var genreData = ImportMethod(module, celebrityField.FieldType, "get_GenreData", 0, true);
        var genreThemeField = Field(module, genreData.ReturnType, "<Theme>k__BackingField");
        var fullName = ImportMethod(module, nameField.FieldType, "FullName", 0, true);
        var relationshipStatus = ImportMethod(module, celebrityField.FieldType, "RelationshipStatusString", 0, true);
        var random = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 2, true, "System.String", "System.StringComparison");
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var arrayListGetCount = FrameworkMethod(module, "System.Collections.ArrayList", "get_Count", 0, true);
        var roomGetItem = ImportMethod(module, roomsField.FieldType, "get_Item", 1, true);
        var resultConstructor = ImportMethod(module, FindType(module, "ScenarioCasinoCelebrityMeetAndGreetResult"), ".ctor", 5, true);
        var reasonType = (GenericInstanceType)resultConstructor.Parameters[4].ParameterType;
        var reasonConstructor = ImportMethod(module, reasonType, ".ctor", 2, true);
        var scenarioType = FindType(module, "Scenario");
        var failureConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true, "System.String", "System.String", "Scenario");
        var trackFail = ImportMethod(module, FindType(module, "Tracking.EventTracking"), "TrackFunnelEventFail", 2, false);

        var body = FreshBody(method);
        var fee = new VariableDefinition(module.TypeSystem.Single);
        var selectedString = new VariableDefinition(module.TypeSystem.String);
        var matchingIndex = new VariableDefinition(module.TypeSystem.Int32);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        var room = new VariableDefinition(((GenericInstanceType)roomsField.FieldType).GenericArguments[0]);
        var tickets = new VariableDefinition(module.TypeSystem.Int32);
        var reason = new VariableDefinition(reasonType);
        var chainedScenario = new VariableDefinition(scenarioType);
        body.Variables.Add(fee);
        body.Variables.Add(selectedString);
        body.Variables.Add(matchingIndex);
        body.Variables.Add(loopIndex);
        body.Variables.Add(room);
        body.Variables.Add(tickets);
        body.Variables.Add(reason);
        body.Variables.Add(chainedScenario);
        var il = body.Instructions;

        var sliderNumbers = method.Parameters.Single(parameter => parameter.Name == "sliderNumbers");
        var dropDownStrings = method.Parameters.Single(parameter => parameter.Name == "dropDownStrings");
        il.Add(Instruction.Create(OpCodes.Ldarg, sliderNumbers));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Unbox_Any, module.TypeSystem.Single));
        il.Add(Instruction.Create(OpCodes.Stloc, fee));
        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedString));

        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, matchingIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var roomLoop = Instruction.Create(OpCodes.Nop);
        var roomLoopCheck = Instruction.Create(OpCodes.Nop);
        var roomLoopDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, roomLoopCheck));
        il.Add(roomLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedString));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        var nextRoomString = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brfalse, nextRoomString));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Stloc, matchingIndex));
        il.Add(Instruction.Create(OpCodes.Br, roomLoopDone));
        il.Add(nextRoomString);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(roomLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownField));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, roomLoop));
        il.Add(roomLoopDone);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, roomsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, matchingIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, roomGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, room));

        il.Add(Instruction.Create(OpCodes.Ldloca, reason));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, reasonConstructor));

        void SetReason(string text, bool value)
        {
            il.Add(Instruction.Create(OpCodes.Ldloca, reason));
            il.Add(Instruction.Create(OpCodes.Ldstr, text));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, value ? 1 : 0));
            il.Add(Instruction.Create(OpCodes.Call, reasonConstructor));
        }

        void AddRandom(int minimum, int maximum, bool subtract = false)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, tickets));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, minimum));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, maximum));
            il.Add(Instruction.Create(OpCodes.Call, random));
            il.Add(Instruction.Create(subtract ? OpCodes.Sub : OpCodes.Add));
            il.Add(Instruction.Create(OpCodes.Stloc, tickets));
        }

        var noThemeMatch = Instruction.Create(OpCodes.Nop);
        var themeDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Call, genreData));
        il.Add(Instruction.Create(OpCodes.Ldfld, genreThemeField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldfld, themeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, noThemeMatch));
        SetReason("theme", true);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 50));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, tickets));
        il.Add(Instruction.Create(OpCodes.Br, themeDone));
        il.Add(noThemeMatch);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 40));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 80));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Neg));
        il.Add(Instruction.Create(OpCodes.Stloc, tickets));
        il.Add(themeDone);

        var fameNotHigh = Instruction.Create(OpCodes.Nop);
        var fameDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Ldfld, fameField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 80f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, fameNotHigh));
        SetReason("fame", true);
        AddRandom(100, 201);
        il.Add(Instruction.Create(OpCodes.Br, fameDone));
        il.Add(fameNotHigh);
        var fameLow = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Ldfld, fameField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 40f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, fameLow));
        AddRandom(60, 120);
        il.Add(Instruction.Create(OpCodes.Br, fameDone));
        il.Add(fameLow);
        SetReason("fame", false);
        AddRandom(10, 50, true);
        il.Add(fameDone);

        var popularityNotHigh = Instruction.Create(OpCodes.Nop);
        var popularityDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldfld, popularityField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 80f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, popularityNotHigh));
        AddRandom(50, 101);
        il.Add(Instruction.Create(OpCodes.Br, popularityDone));
        il.Add(popularityNotHigh);
        var popularityLow = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldfld, popularityField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 40f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, popularityLow));
        AddRandom(30, 70);
        il.Add(Instruction.Create(OpCodes.Br, popularityDone));
        il.Add(popularityLow);
        AddRandom(20, 50, true);
        il.Add(popularityDone);

        var feeNotLow = Instruction.Create(OpCodes.Nop);
        var feeDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, fee));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 50f));
        il.Add(Instruction.Create(OpCodes.Bge_Un, feeNotLow));
        AddRandom(50, 101);
        il.Add(Instruction.Create(OpCodes.Br, feeDone));
        il.Add(feeNotLow);
        il.Add(Instruction.Create(OpCodes.Ldloc, fee));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 150f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, feeDone));
        AddRandom(20, 50, true);
        il.Add(feeDone);

        var failure = Instruction.Create(OpCodes.Nop);
        var chainDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, tickets));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 10));
        il.Add(Instruction.Create(OpCodes.Ble, failure));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Ldloc, room));
        il.Add(Instruction.Create(OpCodes.Ldloc, fee));
        il.Add(Instruction.Create(OpCodes.Ldloc, tickets));
        il.Add(Instruction.Create(OpCodes.Ldloc, reason));
        il.Add(Instruction.Create(OpCodes.Newobj, resultConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Br, chainDone));

        il.Add(failure);
        il.Add(Instruction.Create(OpCodes.Ldstr, "No dice"));
        il.Add(Instruction.Create(OpCodes.Ldstr, "No one came to the "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Call, fullName));
        il.Add(Instruction.Create(OpCodes.Ldstr, " Meet & Greet."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Newobj, failureConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "I hosted a meet-and-greet for my casino patrons to meet my casino's "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Call, relationshipStatus));
        il.Add(Instruction.Create(OpCodes.Ldstr, ", "));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Call, fullName));
        il.Add(Instruction.Create(OpCodes.Ldstr, ", but no one attended."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 2));
        il.Add(Instruction.Create(OpCodes.Ldstr, "ScenarioCasinoCelebrityMeetAndGreetPickGameRoom"));
        il.Add(Instruction.Create(OpCodes.Call, trackFail));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldfld, meetCountField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stfld, meetCountField));
        il.Add(chainDone);

        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        EmitLifeLogAndRefresh(method, narrativeField);
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixPartyRoom(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var casinoField = Field(module, owner, "_casino");
        var celebrityField = Field(module, owner, "_celebrity");
        var roomsField = Field(module, owner, "_gameRooms");
        var dropDownField = Field(module, owner, "_dropDownContents");
        var hasDismissField = Field(module, owner, "HasDismissButton");
        var exteriorTitleField = Field(module, owner, "ExteriorTitle");
        var titleField = Field(module, owner, "Title");
        var bodyField = Field(module, owner, "Body");
        var dropDownArrayField = Field(module, owner, "DropDownArray");
        var choiceArrayField = Field(module, owner, "ChoiceArray");
        var nameField = Field(module, celebrityField.FieldType, "<Name>k__BackingField");
        var roomNameField = Field(module, ((GenericInstanceType)roomsField.FieldType).GenericArguments[0], "<Name>k__BackingField");
        var fullName = ImportMethod(module, nameField.FieldType, "FullName", 0, true);
        var getGameRooms = ImportMethod(module, casinoField.FieldType, "GetGameRooms", 1, true);
        var roomGetItem = ImportMethod(module, roomsField.FieldType, "get_Item", 1, true);
        var roomGetCount = ImportMethod(module, roomsField.FieldType, "get_Count", 0, true);
        var capitalize = ImportMethod(module, FindType(module, "GameReference"), "CapitalizeAllMajorWordsInString", 1, false);
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");
        var dropDownConstructor = ImportMethod(module, FindType(module, "ScenarioDropDown"), ".ctor", 3, true);
        var choiceConstructor = ImportMethod(module, FindType(module, "ScenarioChoice"), ".ctor", 3, true, "System.String", "System.Boolean", "System.Boolean");
        var trackStart = ImportMethod(module, FindType(module, "Tracking.EventTracking"), "TrackFunnelEventStart", 2, false);
        var nested = owner.NestedTypes.Single(type => type.Name == "<>c");
        var cacheField = nested.Fields.Single(field => field.Name == "<>9__6_0");
        var singletonField = nested.Fields.Single(field => field.Name == "<>9");
        var selector = nested.Methods.Single(candidate => candidate.Name == "<RefreshScenario>b__6_0");
        var selectorType = (GenericInstanceType)cacheField.FieldType;
        var selectorConstructor = ImportMethod(module, selectorType, ".ctor", 2, true);
        var existingOrderBy = method.Body.Instructions.Select(instruction => instruction.Operand).OfType<MethodReference>().Single(candidate => candidate.Name == "OrderBy" && candidate.Parameters.Count == 2);
        var existingToList = method.Body.Instructions.Select(instruction => instruction.Operand).OfType<MethodReference>().Single(candidate => candidate.Name == "ToList" && candidate.Parameters.Count == 1);
        var roomType = ((GenericInstanceType)roomsField.FieldType).GenericArguments[0];
        var orderBy = GenericMethod(module, existingOrderBy, roomType, module.TypeSystem.String);
        var toList = GenericMethod(module, existingToList, roomType);

        var body = FreshBody(method);
        var rooms = new VariableDefinition(roomsField.FieldType);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        body.Variables.Add(rooms);
        body.Variables.Add(loopIndex);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        EmitLoadCasino(module, il);
        il.Add(Instruction.Create(OpCodes.Stfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stfld, hasDismissField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Celebrity"));
        il.Add(Instruction.Create(OpCodes.Stfld, exteriorTitleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Party"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Party with "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, celebrityField));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Call, fullName));
        il.Add(Instruction.Create(OpCodes.Ldstr, " in one of your casino's game rooms today!"));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, getGameRooms));
        il.Add(Instruction.Create(OpCodes.Stloc, rooms));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, rooms));
        il.Add(Instruction.Create(OpCodes.Ldsfld, cacheField));
        il.Add(Instruction.Create(OpCodes.Dup));
        var selectorReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brtrue, selectorReady));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldsfld, singletonField));
        il.Add(Instruction.Create(OpCodes.Ldftn, module.ImportReference(selector)));
        il.Add(Instruction.Create(OpCodes.Newobj, selectorConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Stsfld, cacheField));
        il.Add(selectorReady);
        il.Add(Instruction.Create(OpCodes.Call, orderBy));
        il.Add(Instruction.Create(OpCodes.Call, toList));
        il.Add(Instruction.Create(OpCodes.Stfld, roomsField));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stfld, dropDownField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, roomsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, roomGetItem));
        il.Add(Instruction.Create(OpCodes.Ldfld, roomNameField));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, roomsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, roomGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, loop));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Pick a game room"));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, dropDownConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Stfld, dropDownArrayField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Do it"));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, choiceConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Stfld, choiceArrayField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 2));
        il.Add(Instruction.Create(OpCodes.Ldstr, "ScenarioCasinoCelebrityPartyPickGameRoom"));
        il.Add(Instruction.Create(OpCodes.Call, trackStart));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixAgeUpGamblerLost(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var casinoField = Field(module, owner, "_casino");
        var narrativeField = Field(module, owner, "_narrative");
        var hasDismissField = Field(module, owner, "HasDismissButton");
        var focusPersonField = Field(module, owner, "FocusPerson");
        var titleField = Field(module, owner, "Title");
        var bodyField = Field(module, owner, "Body");
        var tableField = Field(module, owner, "TableDictionary");
        var barGraphArrayField = Field(module, owner, "BarGraphArray");
        var choiceArrayField = Field(module, owner, "ChoiceArray");
        var impactPopularity = ImportMethod(module, casinoField.FieldType, "ImpactPopularity", 1, true);
        var getFallback = ImportMethod(module, casinoField.FieldType, "GetRandomNonMafiaGambler", 0, true);
        var getGamblers = ImportMethod(module, casinoField.FieldType, "GetGamblers", 1, true);
        var removeGambler = ImportMethod(module, casinoField.FieldType, "RemoveGambler", 1, true);
        var gamblerType = getFallback.ReturnType;
        var gamblerName = ImportMethod(module, gamblerType, "GamblerName", 0, true);
        var lastAgeField = Field(module, gamblerType, "<HeroAgeAtLastInteractedWithHero>k__BackingField");
        var gamblingStyleField = Field(module, gamblerType, "<GamblingStyle>k__BackingField");
        var addictionField = Field(module, gamblerType, "_addictionLevel");
        var moneyField = Field(module, gamblerType, "<Att_money>k__BackingField");
        var gamblingStyleString = ImportMethod(module, gamblerType, "GetGamblingStyleString", 1, false);
        var gamblerGetItem = ImportMethod(module, getGamblers.ReturnType, "get_Item", 1, true);
        var gamblerGetCount = ImportMethod(module, getGamblers.ReturnType, "get_Count", 0, true);
        var heroAgeField = Field(module, Field(module, FindType(module, "Life"), "<Hero>k__BackingField").FieldType, "<Age>k__BackingField");
        var capitalize = ImportMethod(module, FindType(module, "GameReference"), "CapitalizeAllMajorWordsInString", 1, false);
        var dictionaryConstructor = ImportMethod(module, tableField.FieldType, ".ctor", 0, true);
        var dictionarySetItem = ImportMethod(module, tableField.FieldType, "set_Item", 2, true);
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var barGraphConstructor = ImportMethod(module, FindType(module, "ScenarioBarGraph"), ".ctor", 2, true);
        var choiceConstructor = ImportMethod(module, FindType(module, "ScenarioChoice"), ".ctor", 3, true, "System.String", "System.Boolean", "System.Boolean");
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");
        var trackStart = ImportMethod(module, FindType(module, "Tracking.EventTracking"), "TrackFunnelEventStart", 2, false);

        var body = FreshBody(method);
        var selected = new VariableDefinition(gamblerType);
        var candidate = new VariableDefinition(gamblerType);
        var gamblers = new VariableDefinition(getGamblers.ReturnType);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        body.Variables.Add(selected);
        body.Variables.Add(candidate);
        body.Variables.Add(gamblers);
        body.Variables.Add(loopIndex);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        EmitLoadCasino(module, il);
        il.Add(Instruction.Create(OpCodes.Stfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_M1));
        il.Add(Instruction.Create(OpCodes.Call, impactPopularity));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Call, getFallback));
        il.Add(Instruction.Create(OpCodes.Stloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, getGamblers));
        il.Add(Instruction.Create(OpCodes.Stloc, gamblers));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var loopDone = Instruction.Create(OpCodes.Nop);
        var nextGambler = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloc, gamblers));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, gamblerGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, candidate));
        il.Add(Instruction.Create(OpCodes.Ldsfld, Field(module, FindType(module, "GameManager"), "Inst")));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, FindType(module, "GameManager"), "Life")));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, FindType(module, "Life"), "<Hero>k__BackingField")));
        il.Add(Instruction.Create(OpCodes.Ldfld, heroAgeField));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
        il.Add(Instruction.Create(OpCodes.Ldfld, lastAgeField));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 5));
        il.Add(Instruction.Create(OpCodes.Blt, nextGambler));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
        il.Add(Instruction.Create(OpCodes.Stloc, selected));
        il.Add(Instruction.Create(OpCodes.Br, loopDone));
        il.Add(nextGambler);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, gamblers));
        il.Add(Instruction.Create(OpCodes.Callvirt, gamblerGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, loop));
        il.Add(loopDone);

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, casinoField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Call, removeGambler));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stfld, hasDismissField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Stfld, focusPersonField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Gambler Lost"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Call, gamblerName));
        il.Add(Instruction.Create(OpCodes.Ldstr, " has stopped gambling at your casino."));
        il.Add(Instruction.Create(OpCodes.Call, FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String")));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, dictionaryConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Gambling Style"));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldfld, gamblingStyleField));
        il.Add(Instruction.Create(OpCodes.Call, gamblingStyleString));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionarySetItem));
        il.Add(Instruction.Create(OpCodes.Stfld, tableField));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Money"));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldfld, moneyField));
        il.Add(Instruction.Create(OpCodes.Newobj, barGraphConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Addiction"));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldfld, addictionField));
        il.Add(Instruction.Create(OpCodes.Newobj, barGraphConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphArrayField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "OK"));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, choiceConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Stfld, choiceArrayField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "My casino's gambler, "));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Call, gamblerName));
        il.Add(Instruction.Create(OpCodes.Ldstr, ", stopped playing at my casino."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 6));
        il.Add(Instruction.Create(OpCodes.Ldstr, "ScenarioCasinoAgeUpGamblerLost"));
        il.Add(Instruction.Create(OpCodes.Call, trackStart));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static void EmitLifeLogAndRefresh(MethodDefinition method, FieldReference narrativeField)
    {
        var module = method.Module;
        var il = method.Body.Instructions;
        var gameManager = FindType(module, "GameManager");
        var lifeField = Field(module, gameManager, "Life");
        var instField = Field(module, gameManager, "Inst");
        var lifeLogField = Field(module, lifeField.FieldType, "<LifeLog>k__BackingField");
        var lifeInterfaceField = Field(module, lifeField.FieldType, "LifeInterface");
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeLogField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, lifeLogField.FieldType, "Log", 1, true)));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeInterfaceField));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, lifeInterfaceField.FieldType, "RefreshUI", 0, true)));
    }

    private static void EmitLoadCasino(ModuleDefinition module, Mono.Collections.Generic.Collection<Instruction> il)
    {
        var gameManager = FindType(module, "GameManager");
        var lifeField = Field(module, gameManager, "Life");
        il.Add(Instruction.Create(OpCodes.Ldsfld, Field(module, gameManager, "Inst")));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, lifeField.FieldType, "<Casino>k__BackingField")));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static GenericInstanceMethod GenericMethod(ModuleDefinition module, MethodReference existing, params TypeReference[] arguments)
    {
        var method = new GenericInstanceMethod(module.ImportReference(existing.Resolve()));
        foreach (var argument in arguments) method.GenericArguments.Add(argument);
        return method;
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
        var scope = module.TypeSystem.Object.Scope;
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module, scope);
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

    private static Instruction At(IEnumerable<Instruction> instructions, int offset)
    {
        return instructions.SingleOrDefault(instruction => instruction.Offset == offset) ?? throw new InvalidOperationException($"Missing original instruction at IL_{offset:x4}");
    }

    private static bool IsLoadLocal(Instruction instruction, int index)
    {
        return (instruction.OpCode == OpCodes.Ldloc || instruction.OpCode == OpCodes.Ldloc_S) && instruction.Operand is VariableDefinition variable && variable.Index == index;
    }

    private static bool IsStoreLocal(Instruction instruction, int index)
    {
        return (instruction.OpCode == OpCodes.Stloc || instruction.OpCode == OpCodes.Stloc_S) && instruction.Operand is VariableDefinition variable && variable.Index == index;
    }

    private static bool IsLoadAddress(Instruction instruction, VariableDefinition expected)
    {
        return (instruction.OpCode == OpCodes.Ldloca || instruction.OpCode == OpCodes.Ldloca_S) && ReferenceEquals(instruction.Operand, expected);
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
