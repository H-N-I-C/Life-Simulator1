using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class DealerMethodRepairs
{
    public static void FixTurfWar(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var dealerType = FindType(module, "SimSpecialJobDealer");
        var cartelType = FindType(module, "SimDealerCartel");
        var memberType = FindType(module, "SimDealerCartelMember");
        var drugType = FindType(module, "SimDealerDrug");
        var scenarioType = FindType(module, "Scenario");
        var dealerField = Field(module, owner, "_dealerJob");
        var narrativeField = Field(module, owner, "_narrative");
        var cartelField = Field(module, dealerType, "<Cartel>k__BackingField");
        var rivalsField = Field(module, dealerType, "<RivalCartels>k__BackingField");
        var rivalNameField = Field(module, cartelType, "<Name>k__BackingField");
        var attackedAgeField = Field(module, cartelType, "<HeroAgeAtLastAttacked>k__BackingField");
        var nicknameField = Field(module, memberType, "<Nickname>k__BackingField");
        var nextScenarioField = Field(module, scenarioType, "<NextScenario>k__BackingField");
        var customSoundField = Field(module, scenarioType, "CustomSoundFileName");
        var dismissField = Field(module, scenarioType, "HasDismissButton");
        var exteriorTitleField = Field(module, scenarioType, "ExteriorTitle");
        var titleField = Field(module, scenarioType, "Title");
        var bodyField = Field(module, scenarioType, "Body");
        var tableField = Field(module, scenarioType, "TableDictionary");
        var tableOrderField = Field(module, scenarioType, "TableOrderArray");
        var choiceArrayField = Field(module, scenarioType, "ChoiceArray");
        var drugTypeField = Field(module, drugType, "<Type>k__BackingField");
        var gameManager = FindType(module, "GameManager");
        var instField = Field(module, gameManager, "Inst");
        var lifeField = Field(module, gameManager, "Life");
        var heroField = Field(module, lifeField.FieldType, "<Hero>k__BackingField");
        var ageField = Field(module, heroField.FieldType, "<Age>k__BackingField");
        var getDealer = ImportMethod(module, dealerType, "GetCurrentSpecialJobDealer", 0, false);
        var getPureDrug = ImportMethod(module, cartelType, "GetRandomPureDrug", 1, true);
        var getKingpin = ImportMethod(module, cartelType, "GetKingpin", 0, true);
        var getRandomMember = ImportMethod(module, cartelType, "GetRandomMember", 2, true);
        var dosesAvailable = ImportMethod(module, drugType, "DosesAvailable", 0, true);
        var pronoun = ImportMethod(module, memberType, "PronounStringForType", 1, true);
        var impactMoney = ImportMethod(module, cartelType, "ImpactMoneyScore", 1, true);
        var impactInfamy = ImportMethod(module, cartelType, "ImpactInfamy", 1, true);
        var random = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false);
        var grouping = ImportMethod(module, FindType(module, "GameReference"), "NumberGroupingStringForAmount", 1, false);
        var floor = FrameworkMethod(module, "System.MathF", "Floor", 1, false, "System.Single");
        var concatArray = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");
        var rivalsType = (GenericInstanceType)rivalsField.FieldType;
        var rivalGetItem = ImportMethod(module, rivalsType, "get_Item", 1, true);
        var rivalGetCount = ImportMethod(module, rivalsType, "get_Count", 0, true);
        var dictionaryType = (GenericInstanceType)tableField.FieldType;
        var dictionaryConstructor = ImportMethod(module, dictionaryType, ".ctor", 0, true);
        var dictionaryAdd = ImportMethod(module, dictionaryType, "Add", 2, true);
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListType = arrayListConstructor.DeclaringType;
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var deathConstructor = ImportMethod(module, FindType(module, "ScenarioDealerMemberDeath"), ".ctor", 3, true);
        var choiceConstructor = ImportMethod(module, FindType(module, "ScenarioChoice"), ".ctor", 3, true, "System.String", "System.Boolean", "System.Boolean");

        var body = FreshBody(method);
        var rival = new VariableDefinition(cartelType);
        var currentRival = new VariableDefinition(cartelType);
        var drug = new VariableDefinition(drugType);
        var member = new VariableDefinition(memberType);
        var kingpin = new VariableDefinition(memberType);
        var death = new VariableDefinition(FindType(module, "ScenarioDealerMemberDeath"));
        var table = new VariableDefinition(dictionaryType);
        var order = new VariableDefinition(arrayListType);
        var dosesLost = new VariableDefinition(module.TypeSystem.Int32);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { rival, currentRival, drug, member, kingpin, death, table, order, dosesLost, loopIndex }) body.Variables.Add(local);
        var il = body.Instructions;

        void Concat(params Action[] parts)
        {
            il.Add(Instruction.Create(OpCodes.Ldc_I4, parts.Length));
            il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.String));
            for (var index = 0; index < parts.Length; index++)
            {
                il.Add(Instruction.Create(OpCodes.Dup));
                il.Add(Instruction.Create(OpCodes.Ldc_I4, index));
                parts[index]();
                il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            }
            il.Add(Instruction.Create(OpCodes.Call, concatArray));
        }
        void LoadDealer() { il.Add(Instruction.Create(OpCodes.Ldarg_0)); il.Add(Instruction.Create(OpCodes.Ldfld, dealerField)); }
        void LoadCartel() { LoadDealer(); il.Add(Instruction.Create(OpCodes.Ldfld, cartelField)); }
        void LoadRivalName() { il.Add(Instruction.Create(OpCodes.Ldloc, rival)); il.Add(Instruction.Create(OpCodes.Ldfld, rivalNameField)); }
        void LoadDrugType() { il.Add(Instruction.Create(OpCodes.Ldloc, drug)); il.Add(Instruction.Create(OpCodes.Ldfld, drugTypeField)); }
        void AddTableRow(string key, Action amount)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, table));
            il.Add(Instruction.Create(OpCodes.Ldstr, key));
            amount();
            il.Add(Instruction.Create(OpCodes.Call, grouping));
            il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryAdd));
            il.Add(Instruction.Create(OpCodes.Ldloc, order));
            il.Add(Instruction.Create(OpCodes.Ldstr, key));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
        }

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "a16_sndGangWar.aiff"));
        il.Add(Instruction.Create(OpCodes.Stfld, customSoundField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getDealer));
        il.Add(Instruction.Create(OpCodes.Stfld, dealerField));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, rival));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var rivalLoop = Instruction.Create(OpCodes.Nop);
        var rivalCheck = Instruction.Create(OpCodes.Nop);
        var nextRival = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, rivalCheck));
        il.Add(rivalLoop);
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, rivalsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, rivalGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, currentRival));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentRival));
        il.Add(Instruction.Create(OpCodes.Ldfld, attackedAgeField));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
        il.Add(Instruction.Create(OpCodes.Ble, nextRival));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentRival));
        il.Add(Instruction.Create(OpCodes.Stloc, rival));
        il.Add(nextRival);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(rivalCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, rivalsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, rivalGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, rivalLoop));
        var rivalReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, rival));
        il.Add(Instruction.Create(OpCodes.Brtrue, rivalReady));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, rivalsField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, rivalsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, rivalGetCount));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Callvirt, rivalGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, rival));
        il.Add(rivalReady);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stfld, dismissField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Cartel"));
        il.Add(Instruction.Create(OpCodes.Stfld, exteriorTitleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Turf War"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        LoadCartel();
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 100));
        il.Add(Instruction.Create(OpCodes.Call, getPureDrug));
        il.Add(Instruction.Create(OpCodes.Stloc, drug));
        il.Add(Instruction.Create(OpCodes.Ldloc, drug));
        il.Add(Instruction.Create(OpCodes.Call, dosesAvailable));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 10));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 26));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 100f));
        il.Add(Instruction.Create(OpCodes.Div));
        il.Add(Instruction.Create(OpCodes.Call, floor));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Stloc, dosesLost));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")), LoadRivalName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " cartel attacked one of your cartel's safehouses and stole a shipment of ")),
            LoadDrugType, () => il.Add(Instruction.Create(OpCodes.Ldstr, "!")));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));
        LoadCartel();
        il.Add(Instruction.Create(OpCodes.Call, getKingpin));
        il.Add(Instruction.Create(OpCodes.Stloc, kingpin));
        LoadCartel();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, kingpin));
        il.Add(Instruction.Create(OpCodes.Call, getRandomMember));
        il.Add(Instruction.Create(OpCodes.Stloc, member));
        var deathDone = Instruction.Create(OpCodes.Nop);
        var deathChanceDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, member));
        il.Add(Instruction.Create(OpCodes.Brfalse, deathDone));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Brtrue, deathChanceDone));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        Concat(
            () => { il.Add(Instruction.Create(OpCodes.Ldarg_0)); il.Add(Instruction.Create(OpCodes.Ldfld, bodyField)); },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\nThey murdered ")),
            () => { il.Add(Instruction.Create(OpCodes.Ldloc, member)); il.Add(Instruction.Create(OpCodes.Ldfld, nicknameField)); },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " during the attack.")));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));
        il.Add(Instruction.Create(OpCodes.Ldloc, member));
        il.Add(Instruction.Create(OpCodes.Ldstr, "was murdered"));
        il.Add(Instruction.Create(OpCodes.Ldloc, member));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, pronoun));
        il.Add(Instruction.Create(OpCodes.Ldstr, " was murdered."));
        il.Add(Instruction.Create(OpCodes.Call, FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String")));
        il.Add(Instruction.Create(OpCodes.Newobj, deathConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, death));
        il.Add(Instruction.Create(OpCodes.Ldloc, death));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, death));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Br, deathDone));
        il.Add(deathChanceDone);
        il.Add(deathDone);
        il.Add(Instruction.Create(OpCodes.Newobj, dictionaryConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, table));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, order));
        AddTableRow("Doses Lost", () => il.Add(Instruction.Create(OpCodes.Ldloc, dosesLost)));
        AddTableRow("Doses Available", () => { il.Add(Instruction.Create(OpCodes.Ldloc, drug)); il.Add(Instruction.Create(OpCodes.Call, dosesAvailable)); });
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, table));
        il.Add(Instruction.Create(OpCodes.Stfld, tableField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, order));
        il.Add(Instruction.Create(OpCodes.Stfld, tableOrderField));
        LoadCartel();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_M1));
        il.Add(Instruction.Create(OpCodes.Call, impactMoney));
        LoadCartel();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_M1));
        il.Add(Instruction.Create(OpCodes.Call, impactInfamy));
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
        Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")), LoadRivalName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " cartel stole ")),
            () => { il.Add(Instruction.Create(OpCodes.Ldloc, dosesLost)); il.Add(Instruction.Create(OpCodes.Call, grouping)); },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " doses of ")), LoadDrugType,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " from one of my cartel's safehouses.")));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        EmitLifeLogAndRefresh(method, narrativeField);
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixScapegoat(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var dealerType = FindType(module, "SimSpecialJobDealer");
        var memberType = FindType(module, "SimDealerCartelMember");
        var dealerField = Field(module, owner, "_dealerJob");
        var memberField = Field(module, owner, "_member");
        var membersField = Field(module, owner, "_members");
        var dropDownContentsField = Field(module, owner, "_dropDownContents");
        var dismissField = Field(module, scenarioType, "HasDismissButton");
        var exteriorTitleField = Field(module, scenarioType, "ExteriorTitle");
        var titleField = Field(module, scenarioType, "Title");
        var bodyField = Field(module, scenarioType, "Body");
        var tableField = Field(module, scenarioType, "TableDictionary");
        var tableOrderField = Field(module, scenarioType, "TableOrderArray");
        var dropDownArrayField = Field(module, scenarioType, "DropDownArray");
        var choiceArrayField = Field(module, scenarioType, "ChoiceArray");
        var nicknameField = Field(module, memberType, "<Nickname>k__BackingField");
        var rankField = Field(module, memberType, "<Rank>k__BackingField");
        var ageField = Field(module, memberType, "<Age>k__BackingField");
        var cartelField = Field(module, dealerType, "<Cartel>k__BackingField");
        var getDealer = ImportMethod(module, dealerType, "GetCurrentSpecialJobDealer", 0, false);
        var getMembers = ImportMethod(module, cartelField.FieldType, "GetMembersList", 1, true);
        var getRank = ImportMethod(module, dealerType, "GetRankStringForRank", 3, false);
        var pronoun = ImportMethod(module, memberType, "PronounStringForType", 1, true);
        var jailedCrimes = ImportMethod(module, memberType, "GetJailedCrimesString", 0, true);
        var sentenceCrimes = ImportMethod(module, memberType, "GetSentenceCrimesString", 0, true);
        var capitalize = ImportMethod(module, FindType(module, "GameReference"), "CapitalizeAllMajorWordsInString", 1, false);
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");
        var concat4 = FrameworkMethod(module, "System.String", "Concat", 4, false, "System.String", "System.String", "System.String", "System.String");
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var dictionaryType = (GenericInstanceType)tableField.FieldType;
        var dictionaryConstructor = ImportMethod(module, dictionaryType, ".ctor", 0, true);
        var dictionaryAdd = ImportMethod(module, dictionaryType, "Add", 2, true);
        var membersType = (GenericInstanceType)membersField.FieldType;
        var memberGetItem = ImportMethod(module, membersType, "get_Item", 1, true);
        var memberGetCount = ImportMethod(module, membersType, "get_Count", 0, true);
        var dropDownConstructor = ImportMethod(module, FindType(module, "ScenarioDropDown"), ".ctor", 3, true);
        var choiceConstructor = ImportMethod(module, FindType(module, "ScenarioChoice"), ".ctor", 3, true, "System.String", "System.Boolean", "System.Boolean");
        var nested = owner.NestedTypes.Single(type => type.Name == "<>c");
        var cacheField = nested.Fields.Single(field => field.Name == "<>9__6_0");
        var singletonField = nested.Fields.Single(field => field.Name == "<>9");
        var selector = nested.Methods.Single(candidate => candidate.Name == "<RefreshScenario>b__6_0");
        var selectorType = (GenericInstanceType)cacheField.FieldType;
        var selectorConstructor = ImportMethod(module, selectorType, ".ctor", 2, true);
        var existingOrderBy = method.Body.Instructions.Select(instruction => instruction.Operand).OfType<MethodReference>().Single(candidate => candidate.Name == "OrderBy" && candidate.Parameters.Count == 2);
        var existingToList = method.Body.Instructions.Select(instruction => instruction.Operand).OfType<MethodReference>().Single(candidate => candidate.Name == "ToList" && candidate.Parameters.Count == 1);
        var orderBy = GenericMethod(module, existingOrderBy, memberType, module.TypeSystem.String);
        var toList = GenericMethod(module, existingToList, memberType);

        var body = FreshBody(method);
        var table = new VariableDefinition(dictionaryType);
        var order = new VariableDefinition(arrayListConstructor.DeclaringType);
        var currentMember = new VariableDefinition(memberType);
        var crimeKey = new VariableDefinition(module.TypeSystem.String);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { table, order, currentMember, crimeKey, loopIndex }) body.Variables.Add(local);
        var il = body.Instructions;
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getDealer));
        il.Add(Instruction.Create(OpCodes.Stfld, dealerField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stfld, dismissField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Cartel"));
        il.Add(Instruction.Create(OpCodes.Stfld, exteriorTitleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Scapegoat"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Which of your cartel members will you call on to take the fall for "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, memberField));
        il.Add(Instruction.Create(OpCodes.Ldfld, nicknameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, "'s crime?"));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, memberField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 6));
        il.Add(Instruction.Create(OpCodes.Call, pronoun));
        il.Add(Instruction.Create(OpCodes.Ldstr, " Crime"));
        il.Add(Instruction.Create(OpCodes.Call, FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String")));
        il.Add(Instruction.Create(OpCodes.Stloc, crimeKey));
        il.Add(Instruction.Create(OpCodes.Newobj, dictionaryConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, table));
        il.Add(Instruction.Create(OpCodes.Ldloc, table));
        il.Add(Instruction.Create(OpCodes.Ldloc, crimeKey));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, memberField));
        il.Add(Instruction.Create(OpCodes.Call, jailedCrimes));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryAdd));
        il.Add(Instruction.Create(OpCodes.Ldloc, table));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Potential Sentence"));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, memberField));
        il.Add(Instruction.Create(OpCodes.Call, sentenceCrimes));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryAdd));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, table));
        il.Add(Instruction.Create(OpCodes.Stfld, tableField));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, order));
        il.Add(Instruction.Create(OpCodes.Ldloc, order));
        il.Add(Instruction.Create(OpCodes.Ldloc, crimeKey));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, order));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Potential Sentence"));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, order));
        il.Add(Instruction.Create(OpCodes.Stfld, tableOrderField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerField));
        il.Add(Instruction.Create(OpCodes.Ldfld, cartelField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, getMembers));
        il.Add(Instruction.Create(OpCodes.Ldsfld, cacheField));
        il.Add(Instruction.Create(OpCodes.Dup));
        var selectorReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brtrue, selectorReady));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldsfld, singletonField));
        il.Add(Instruction.Create(OpCodes.Ldftn, selector));
        il.Add(Instruction.Create(OpCodes.Newobj, selectorConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Stsfld, cacheField));
        il.Add(selectorReady);
        il.Add(Instruction.Create(OpCodes.Call, orderBy));
        il.Add(Instruction.Create(OpCodes.Call, toList));
        il.Add(Instruction.Create(OpCodes.Stfld, membersField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stfld, dropDownContentsField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var memberLoop = Instruction.Create(OpCodes.Nop);
        var memberCheck = Instruction.Create(OpCodes.Nop);
        var nextMember = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, memberCheck));
        il.Add(memberLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, membersField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, memberGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, currentMember));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentMember));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, memberField));
        il.Add(Instruction.Create(OpCodes.Beq, nextMember));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownContentsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentMember));
        il.Add(Instruction.Create(OpCodes.Ldfld, nicknameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, " ("));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentMember));
        il.Add(Instruction.Create(OpCodes.Ldfld, rankField));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentMember));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, getRank));
        il.Add(Instruction.Create(OpCodes.Ldstr, ")"));
        il.Add(Instruction.Create(OpCodes.Call, concat4));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(nextMember);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(memberCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, membersField));
        il.Add(Instruction.Create(OpCodes.Callvirt, memberGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, memberLoop));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Choose a cartel member"));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownContentsField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, dropDownConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Stfld, dropDownArrayField));
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
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixDealChoice(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var personType = FindType(module, "SimPerson");
        var drugType = FindType(module, "SimDealerDrug");
        var personField = Field(module, owner, "_person");
        var drugsField = Field(module, owner, "_cartelDrugs");
        var itemsField = Field(module, owner, "_items");
        var narrativeField = Field(module, owner, "_narrative");
        var nextScenarioField = Field(module, scenarioType, "<NextScenario>k__BackingField");
        var streetValueField = Field(module, drugType, "<StreetValue>k__BackingField");
        var itemType = ((GenericInstanceType)itemsField.FieldType).GenericArguments[1];
        var drugListType = (GenericInstanceType)drugsField.FieldType;
        var dictionaryType = (GenericInstanceType)itemsField.FieldType;
        var drugGetItem = ImportMethod(module, drugListType, "get_Item", 1, true);
        var drugGetCount = ImportMethod(module, drugListType, "get_Count", 0, true);
        var dictionaryGetEnumerator = ImportMethod(module, dictionaryType, "GetEnumerator", 0, true);
        var enumeratorType = GenericInstance((GenericInstanceType)dictionaryGetEnumerator.ReturnType, dictionaryType.GenericArguments.ToArray());
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var pairType = GenericInstance((GenericInstanceType)getCurrent.ReturnType, dictionaryType.GenericArguments.ToArray());
        var getKey = ImportMethod(module, pairType, "get_Key", 0, true);
        var getValue = ImportMethod(module, pairType, "get_Value", 0, true);
        var getProductName = ImportMethod(module, drugType, "GetProductName", 0, true);
        var relationshipString = ImportMethod(module, personType, "RelationshipString", 0, true);
        var random = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false);
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 2, true, "System.String", "System.StringComparison");
        var contains = FrameworkMethod(module, "System.String", "Contains", 1, true, "System.String");
        var relatedDrugConstructor = ImportMethod(module, FindType(module, "ScenarioDealerDealItemFriendsLoversFamilyFeedback"), ".ctor", 5, true);
        var relatedItemConstructor = ImportMethod(module, FindType(module, "ScenarioDealerDealItemFriendsLoversFamilyFeedback"), ".ctor", 4, true, "SimPerson", "System.String", "Game.DataDealerDealActivityItem", "System.String");
        var classmateConstructor = ImportMethod(module, FindType(module, "ScenarioDealerDealItemClassmateFeedback"), ".ctor", 3, true);
        var coworkerConstructor = ImportMethod(module, FindType(module, "ScenarioDealerDealItemCoworkerFeedback"), ".ctor", 3, true);
        var gameManager = FindType(module, "GameManager");
        var instField = Field(module, gameManager, "Inst");
        var lifeField = Field(module, gameManager, "Life");
        var fatherField = Field(module, lifeField.FieldType, "<Father>k__BackingField");
        var motherField = Field(module, lifeField.FieldType, "<Mother>k__BackingField");
        var loverField = Field(module, lifeField.FieldType, "<Lover>k__BackingField");

        var body = FreshBody(method);
        var selection = new VariableDefinition(module.TypeSystem.String);
        var lowerSelection = new VariableDefinition(module.TypeSystem.String);
        var drug = new VariableDefinition(drugType);
        var item = new VariableDefinition(itemType);
        var currentDrug = new VariableDefinition(drugType);
        var pair = new VariableDefinition(pairType);
        var enumerator = new VariableDefinition(enumeratorType);
        var scenario = new VariableDefinition(scenarioType);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { selection, lowerSelection, drug, item, currentDrug, pair, enumerator, scenario, loopIndex }) body.Variables.Add(local);
        var il = body.Instructions;
        var dropDownStrings = method.Parameters.Single(parameter => parameter.Name == "dropDownStrings");
        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, selection));
        il.Add(Instruction.Create(OpCodes.Ldloc, selection));
        il.Add(Instruction.Create(OpCodes.Call, toLower));
        il.Add(Instruction.Create(OpCodes.Stloc, lowerSelection));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, drug));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, item));
        var enumerateItems = Instruction.Create(OpCodes.Nop);
        var drugsDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, drugsField));
        il.Add(Instruction.Create(OpCodes.Brfalse, enumerateItems));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, drugsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, drugGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, enumerateItems));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var drugLoop = Instruction.Create(OpCodes.Nop);
        var drugCheck = Instruction.Create(OpCodes.Nop);
        var nextDrug = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, drugCheck));
        il.Add(drugLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, drugsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, drugGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, currentDrug));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentDrug));
        il.Add(Instruction.Create(OpCodes.Call, getProductName));
        il.Add(Instruction.Create(OpCodes.Ldloc, lowerSelection));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextDrug));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentDrug));
        il.Add(Instruction.Create(OpCodes.Stloc, drug));
        il.Add(Instruction.Create(OpCodes.Br, drugsDone));
        il.Add(nextDrug);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(drugCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, drugsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, drugGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, drugLoop));
        il.Add(drugsDone);
        var selectionDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, drug));
        il.Add(Instruction.Create(OpCodes.Brtrue, selectionDone));
        il.Add(enumerateItems);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, itemsField));
        il.Add(Instruction.Create(OpCodes.Brfalse, selectionDone));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, itemsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var itemLoop = Instruction.Create(OpCodes.Nop);
        var itemCheck = Instruction.Create(OpCodes.Nop);
        var nextItem = Instruction.Create(OpCodes.Nop);
        var itemLoopDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, itemCheck));
        il.Add(itemLoop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, pair));
        il.Add(Instruction.Create(OpCodes.Ldloc, lowerSelection));
        il.Add(Instruction.Create(OpCodes.Ldloca, pair));
        il.Add(Instruction.Create(OpCodes.Call, getKey));
        il.Add(Instruction.Create(OpCodes.Callvirt, contains));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextItem));
        il.Add(Instruction.Create(OpCodes.Ldloca, pair));
        il.Add(Instruction.Create(OpCodes.Call, getValue));
        il.Add(Instruction.Create(OpCodes.Stloc, item));
        il.Add(Instruction.Create(OpCodes.Br, itemLoopDone));
        il.Add(nextItem);
        il.Add(itemCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, itemLoop));
        il.Add(itemLoopDone);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(selectionDone);

        var related = Instruction.Create(OpCodes.Nop);
        var relatedDone = Instruction.Create(OpCodes.Nop);
        void BranchRelated(MethodReference predicate)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
            il.Add(Instruction.Create(OpCodes.Call, predicate));
            il.Add(Instruction.Create(OpCodes.Brtrue, related));
        }
        void BranchRelatedType(string typeName)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
            il.Add(Instruction.Create(OpCodes.Isinst, FindType(module, typeName)));
            il.Add(Instruction.Create(OpCodes.Brtrue, related));
        }
        void BranchRelatedLife(FieldReference field)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, field));
            il.Add(Instruction.Create(OpCodes.Beq, related));
        }
        BranchRelated(ImportMethod(module, personType, "IsCurrentLover", 0, true));
        BranchRelated(ImportMethod(module, personType, "IsExLover", 0, true));
        BranchRelated(ImportMethod(module, personType, "IsFriend", 0, true));
        BranchRelatedType("SimParent");
        BranchRelatedType("SimChild");
        BranchRelatedType("SimSibling");
        BranchRelatedLife(fatherField);
        BranchRelatedLife(motherField);
        BranchRelatedLife(loverField);
        il.Add(Instruction.Create(OpCodes.Br, relatedDone));
        il.Add(related);
        var relatedPathDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, drug));
        il.Add(Instruction.Create(OpCodes.Brfalse, relatedPathDone));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Ldloc, drug));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 11));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Ldloc, drug));
        il.Add(Instruction.Create(OpCodes.Ldfld, streetValueField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Call, relationshipString));
        il.Add(Instruction.Create(OpCodes.Newobj, relatedDrugConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, scenario));
        il.Add(Instruction.Create(OpCodes.Br, relatedPathDone));
        il.Add(relatedPathDone);
        var itemRelatedDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, drug));
        il.Add(Instruction.Create(OpCodes.Brtrue, itemRelatedDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, item));
        il.Add(Instruction.Create(OpCodes.Brfalse, itemRelatedDone));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selection));
        il.Add(Instruction.Create(OpCodes.Ldloc, item));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Call, relationshipString));
        il.Add(Instruction.Create(OpCodes.Newobj, relatedItemConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, scenario));
        il.Add(itemRelatedDone);
        var chainReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, chainReady));
        il.Add(relatedDone);
        var classmateDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, item));
        il.Add(Instruction.Create(OpCodes.Brfalse, chainReady));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Isinst, FindType(module, "SimClassmate")));
        il.Add(Instruction.Create(OpCodes.Brfalse, classmateDone));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selection));
        il.Add(Instruction.Create(OpCodes.Ldloc, item));
        il.Add(Instruction.Create(OpCodes.Newobj, classmateConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, scenario));
        il.Add(Instruction.Create(OpCodes.Br, chainReady));
        il.Add(classmateDone);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Isinst, FindType(module, "SimCoworker")));
        il.Add(Instruction.Create(OpCodes.Brfalse, chainReady));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selection));
        il.Add(Instruction.Create(OpCodes.Ldloc, item));
        il.Add(Instruction.Create(OpCodes.Newobj, coworkerConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, scenario));
        il.Add(chainReady);
        var chainDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, scenario));
        il.Add(Instruction.Create(OpCodes.Brfalse, chainDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, scenario));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, scenario));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(chainDone);
        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        EmitLifeLogAndRefresh(method, narrativeField);
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixAttackChoice(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var dealerType = FindType(module, "SimSpecialJobDealer");
        var streetPersonType = FindType(module, "SimDealerStreetPerson");
        var personType = FindType(module, "SimPerson");
        var actionType = FindType(module, "DataCustomAttackAction");
        var bodyPartType = FindType(module, "DataCustomAttackBodyPart");
        var martialType = FindType(module, "DataMartialArtMove");
        var weaponType = FindType(module, "SimAuctionItem");
        var narrativeField = Field(module, owner, "_narrative");
        var dealerField = Field(module, owner, "_dealerJob");
        var personField = Field(module, owner, "_person");
        var movesField = Field(module, owner, "_moves");
        var bodyPartsField = Field(module, owner, "_bodyParts");
        var martialMovesField = Field(module, owner, "_martialArtMoveArray");
        var martialStringsField = Field(module, owner, "_martialArtStringArray");
        var weaponsField = Field(module, owner, "_blackMarketWeaponArray");
        var bodyDropDownField = Field(module, owner, "_bodyPartsDropDownContents");
        var nextScenarioField = Field(module, scenarioType, "<NextScenario>k__BackingField");
        var bodyField = Field(module, scenarioType, "Body");
        var titleField = Field(module, scenarioType, "Title");
        var damageField = Field(module, scenarioType, "Damage");
        var barGraphArrayField = Field(module, scenarioType, "BarGraphArray");
        var actionField = Field(module, actionType, "<Action>k__BackingField");
        var pastVerbField = Field(module, actionType, "<PastVerb>k__BackingField");
        var actionDamageField = Field(module, actionType, "<DamageScore>k__BackingField");
        var bodyPartNameField = Field(module, bodyPartType, "<BodyPart>k__BackingField");
        var bodyPartDamageField = Field(module, bodyPartType, "<DamageScore>k__BackingField");
        var martialPastVerbField = Field(module, martialType, "<PastVerb>k__BackingField");
        var murderWeaponField = Field(module, weaponType, "<IsMurderWeapon>k__BackingField");
        var cartelField = Field(module, dealerType, "<Cartel>k__BackingField");
        var streetField = Field(module, dealerType, "<AssignedStreet>k__BackingField");
        var rankField = Field(module, dealerType, "<Rank>k__BackingField");
        var infamyField = Field(module, dealerType, "_infamy");
        var lastTheftField = Field(module, dealerType, "<LastTheftStreetPerson>k__BackingField");
        var leaveCountField = Field(module, dealerType, "<NumTimesThisYearAttackMakesStreetPersonLeave>k__BackingField");
        var gameManager = FindType(module, "GameManager");
        var instField = Field(module, gameManager, "Inst");
        var heroField = Field(module, gameManager, "Hero");
        var gameVarsField = Field(module, gameManager, "GameVars");
        var monetizationField = Field(module, gameManager, "MonetizationVars");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var nameField = Field(module, personType, "<Name>k__BackingField");
        var genderField = Field(module, personType, "<Gender>k__BackingField");
        var wilinessField = Field(module, personType, "<Att_wiliness>k__BackingField");
        var spikedSettingField = Field(module, gameVarsField.FieldType, "<SettingSpikedKnuckles>k__BackingField");
        var assassinSettingField = Field(module, gameVarsField.FieldType, "<SettingAssassinsBlade>k__BackingField");
        var spikedAccess = ImportMethod(module, monetizationField.FieldType, "DoesUserHaveAccessToSpikedKnuckles", 0, true);
        var assassinAccess = ImportMethod(module, monetizationField.FieldType, "DoesUserHaveAccessToAssassinsBlade", 0, true);
        var martialDamage = ImportMethod(module, owner, "GetDamageFromMartialMove", 2, true);
        var weaponDamage = ImportMethod(module, owner, "GetDamageFromWeapon", 1, true);
        var getItemName = ImportMethod(module, weaponType, "GetItemName", 1, true);
        var tupleType = (GenericInstanceType)weaponDamage.ReturnType;
        var tupleItem1 = Field(module, tupleType, "Item1");
        var tupleItem2 = Field(module, tupleType, "Item2");
        var gameReference = FindType(module, "GameReference");
        var random = ImportMethod(module, gameReference, "GetRandomNumber", 2, false);
        var possession = ImportMethod(module, gameReference, "AddPossessionToNameString", 1, false);
        var indefiniteArticle = ImportMethod(module, gameReference, "AddIndefiniteArticleToString", 2, false, "System.String", "System.String");
        var pickRandom = ImportMethod(module, gameReference, "PickRandomFromStringArray", 1, false);
        var getPerson = ImportMethod(module, streetPersonType, "GetPerson", 0, true);
        var description = ImportMethod(module, streetPersonType, "DescriptionString", 0, true);
        var pronoun = ImportMethod(module, streetPersonType, "PronounStringForType", 1, true);
        var relationship = ImportMethod(module, streetPersonType, "ImpactRelationshipStrength", 1, true);
        var isPolice = ImportMethod(module, streetPersonType, "IsUniformedPoliceOfficer", 0, true);
        var isRival = ImportMethod(module, streetPersonType, "IsRivalDealer", 0, true);
        var impactInfamy = ImportMethod(module, dealerType, "ImpactInfamy", 1, true);
        var impactStanding = ImportMethod(module, dealerType, "ImpactStanding", 1, true);
        var impactControl = ImportMethod(module, streetField.FieldType, "ImpactControl", 1, true);
        var removePerson = ImportMethod(module, streetField.FieldType, "RemovePerson", 2, true);
        var addPerson = ImportMethod(module, streetField.FieldType, "AddNewPerson", 0, true);
        var cartelInfamy = ImportMethod(module, cartelField.FieldType, "ImpactInfamy", 1, true);
        var impactHealth = ImportMethod(module, personType, "ImpactHealth", 1, true);
        var getPlace = ImportMethod(module, personType, "get_PlaceOfResidence", 0, true);
        var fullName = ImportMethod(module, nameField.FieldType, "FullName", 0, true);
        var crimeConstructor = ImportMethod(module, FindType(module, "SimCrime"), ".ctor", 3, true);
        var arrestConstructor = ImportMethod(module, FindType(module, "ScenarioCrimeArrest"), ".ctor", 1, true);
        var sbkConstructor = ImportMethod(module, FindType(module, "ScenarioDealerCartelDrugSaleSBK"), ".ctor", 0, true);
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true);
        var barGraphConstructor = ImportMethod(module, FindType(module, "ScenarioBarGraph"), ".ctor", 4, true);
        var dataHub = FindType(module, "DataHub");
        var getColorAssault = ImportMethod(module, dataHub, "GetRandomColorAssaultForBodyPart", 1, false);
        var getRandomAction = ImportMethod(module, dataHub, "GetRandomCustomAttackAction", 0, false);
        var getRandomBodyPart = ImportMethod(module, dataHub, "GetRandomCustomAttackBodyPartForGender", 1, false);
        var parserConstructor = ImportMethod(module, FindType(module, "BracketParser"), ".ctor", 0, true);
        var parserPersonField = Field(module, FindType(module, "BracketParser"), "<Person>k__BackingField");
        var parseBody = ImportMethod(module, FindType(module, "BracketParser"), "GetParsedBodyString", 1, true);
        var outboundField = Field(module, getColorAssault.ReturnType, "<OutboundScenario>k__BackingField");
        var backButtonManagerType = FindType(module, "BitLife.UI.Managers.UIBackButtonManager");
        var backButtonManagerInstField = Field(module, backButtonManagerType, "Inst");
        var removeBreadcrumb = ImportMethod(module, backButtonManagerType, "RemoveLastBreadcrumb", 0, true);
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListType = arrayListConstructor.DeclaringType;
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var arrayListContains = FrameworkMethod(module, "System.Collections.ArrayList", "Contains", 1, true);
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var arrayListGetCount = FrameworkMethod(module, "System.Collections.ArrayList", "get_Count", 0, true);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 2, true, "System.String", "System.StringComparison");
        var concatArray = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");
        var moveListType = (GenericInstanceType)movesField.FieldType;
        var bodyPartListType = (GenericInstanceType)bodyPartsField.FieldType;
        var martialListType = (GenericInstanceType)martialMovesField.FieldType;
        var martialStringListType = (GenericInstanceType)martialStringsField.FieldType;
        var weaponListType = (GenericInstanceType)weaponsField.FieldType;
        var moveGetItem = ImportMethod(module, moveListType, "get_Item", 1, true);
        var moveGetCount = ImportMethod(module, moveListType, "get_Count", 0, true);
        var bodyPartGetItem = ImportMethod(module, bodyPartListType, "get_Item", 1, true);
        var martialGetItem = ImportMethod(module, martialListType, "get_Item", 1, true);
        var martialStringGetItem = ImportMethod(module, martialStringListType, "get_Item", 1, true);
        var martialStringGetCount = ImportMethod(module, martialStringListType, "get_Count", 0, true);
        var weaponGetItem = ImportMethod(module, weaponListType, "get_Item", 1, true);
        var weaponGetCount = ImportMethod(module, weaponListType, "get_Count", 0, true);

        var body = FreshBody(method);
        var actionSelection = new VariableDefinition(module.TypeSystem.String);
        var bodySelection = new VariableDefinition(module.TypeSystem.String);
        var attackText = new VariableDefinition(module.TypeSystem.String);
        var possessedDescription = new VariableDefinition(module.TypeSystem.String);
        var title = new VariableDefinition(module.TypeSystem.String);
        var bodyText = new VariableDefinition(module.TypeSystem.String);
        var retaliationText = new VariableDefinition(module.TypeSystem.String);
        var runawaySuffix = new VariableDefinition(module.TypeSystem.String);
        var selectedAction = new VariableDefinition(actionType);
        var currentAction = new VariableDefinition(actionType);
        var selectedBodyPart = new VariableDefinition(bodyPartType);
        var currentBodyPart = new VariableDefinition(bodyPartType);
        var martialMove = new VariableDefinition(martialType);
        var weapon = new VariableDefinition(weaponType);
        var currentWeapon = new VariableDefinition(weaponType);
        var weaponResult = new VariableDefinition(tupleType);
        var chained = new VariableDefinition(scenarioType);
        var parser = new VariableDefinition(FindType(module, "BracketParser"));
        var graph = new VariableDefinition(FindType(module, "ScenarioBarGraph"));
        var crimes = new VariableDefinition(arrayListType);
        var graphs = new VariableDefinition(arrayListType);
        var retaliations = new VariableDefinition(arrayListType);
        var titles = new VariableDefinition(arrayListType);
        var arrest = new VariableDefinition(FindType(module, "ScenarioCrimeArrest"));
        var attackDamage = new VariableDefinition(module.TypeSystem.Int32);
        var damage = new VariableDefinition(module.TypeSystem.Int32);
        var retaliationDamage = new VariableDefinition(module.TypeSystem.Int32);
        var threshold = new VariableDefinition(module.TypeSystem.Int32);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        var bodyPartIndex = new VariableDefinition(module.TypeSystem.Int32);
        var roll = new VariableDefinition(module.TypeSystem.Int32);
        var spiked = new VariableDefinition(module.TypeSystem.Boolean);
        var hit = new VariableDefinition(module.TypeSystem.Boolean);
        foreach (var local in new[] { actionSelection, bodySelection, attackText, possessedDescription, title, bodyText, retaliationText, runawaySuffix, selectedAction, currentAction, selectedBodyPart, currentBodyPart, martialMove, weapon, currentWeapon, weaponResult, chained, parser, graph, crimes, graphs, retaliations, titles, arrest, attackDamage, damage, retaliationDamage, threshold, loopIndex, bodyPartIndex, roll, spiked, hit }) body.Variables.Add(local);
        var il = body.Instructions;
        var dropDownStrings = method.Parameters.Single(parameter => parameter.Name == "dropDownStrings");

        void Concat(params Action[] parts)
        {
            il.Add(Instruction.Create(OpCodes.Ldc_I4, parts.Length));
            il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.String));
            for (var index = 0; index < parts.Length; index++)
            {
                il.Add(Instruction.Create(OpCodes.Dup));
                il.Add(Instruction.Create(OpCodes.Ldc_I4, index));
                parts[index]();
                il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            }
            il.Add(Instruction.Create(OpCodes.Call, concatArray));
        }
        void LoadPerson() { il.Add(Instruction.Create(OpCodes.Ldarg_0)); il.Add(Instruction.Create(OpCodes.Ldfld, personField)); }
        void LoadDealer() { il.Add(Instruction.Create(OpCodes.Ldarg_0)); il.Add(Instruction.Create(OpCodes.Ldfld, dealerField)); }
        void LoadStreet() { LoadDealer(); il.Add(Instruction.Create(OpCodes.Ldfld, streetField)); }
        void LoadHero() { il.Add(Instruction.Create(OpCodes.Ldsfld, instField)); il.Add(Instruction.Create(OpCodes.Ldfld, heroField)); }
        void LoadDescription() { LoadPerson(); il.Add(Instruction.Create(OpCodes.Call, description)); }
        void LoadPossessed() { il.Add(Instruction.Create(OpCodes.Ldloc, possessedDescription)); }
        void LoadBodyPart() { il.Add(Instruction.Create(OpCodes.Ldloc, selectedBodyPart)); il.Add(Instruction.Create(OpCodes.Ldfld, bodyPartNameField)); }
        void LoadAttackText() { il.Add(Instruction.Create(OpCodes.Ldloc, attackText)); }
        void LoadPronoun() { LoadPerson(); il.Add(Instruction.Create(OpCodes.Ldc_I4_5)); il.Add(Instruction.Create(OpCodes.Call, pronoun)); }
        void LoadBodyText() { il.Add(Instruction.Create(OpCodes.Ldloc, bodyText)); }
        void LoadNarrative() { il.Add(Instruction.Create(OpCodes.Ldarg_0)); il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField)); }
        void Chain(VariableDefinition value)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, value));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, value));
            il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        }
        void DealerImpact(MethodReference impact, int amount)
        {
            LoadDealer();
            il.Add(Instruction.Create(OpCodes.Ldc_I4, amount));
            il.Add(Instruction.Create(OpCodes.Call, impact));
        }
        void StreetImpact(int amount)
        {
            LoadStreet();
            il.Add(Instruction.Create(OpCodes.Ldc_I4, amount));
            il.Add(Instruction.Create(OpCodes.Call, impactControl));
        }
        void RelationshipImpact(int amount)
        {
            LoadPerson();
            il.Add(Instruction.Create(OpCodes.Ldc_I4, amount));
            il.Add(Instruction.Create(OpCodes.Call, relationship));
        }
        void RemoveAndReplace(bool died)
        {
            LoadStreet();
            LoadPerson();
            il.Add(Instruction.Create(OpCodes.Ldc_I4, died ? 1 : 0));
            il.Add(Instruction.Create(OpCodes.Call, removePerson));
            LoadStreet();
            il.Add(Instruction.Create(OpCodes.Call, addPerson));
        }
        void RemoveBreadcrumb()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, backButtonManagerInstField));
            il.Add(Instruction.Create(OpCodes.Call, removeBreadcrumb));
        }
        void SetNarrative(params Action[] parts)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            Concat(parts);
            il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        }
        void AppendNarrative(params Action[] parts)
        {
            var all = new Action[parts.Length + 1];
            all[0] = LoadNarrative;
            Array.Copy(parts, 0, all, 1, parts.Length);
            SetNarrative(all);
        }
        void AppendBody(params Action[] parts)
        {
            var all = new Action[parts.Length + 1];
            all[0] = LoadBodyText;
            Array.Copy(parts, 0, all, 1, parts.Length);
            Concat(all);
            il.Add(Instruction.Create(OpCodes.Stloc, bodyText));
        }
        void PickTitle(params string[] values)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, titles));
            foreach (var value in values)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, titles));
                il.Add(Instruction.Create(OpCodes.Ldstr, value));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Pop));
            }
            il.Add(Instruction.Create(OpCodes.Ldloc, titles));
            il.Add(Instruction.Create(OpCodes.Call, pickRandom));
            il.Add(Instruction.Create(OpCodes.Stloc, title));
        }
        void BossInfamy()
        {
            var done = Instruction.Create(OpCodes.Nop);
            LoadDealer();
            il.Add(Instruction.Create(OpCodes.Ldfld, rankField));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
            il.Add(Instruction.Create(OpCodes.Bne_Un, done));
            LoadDealer();
            il.Add(Instruction.Create(OpCodes.Ldfld, cartelField));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
            il.Add(Instruction.Create(OpCodes.Call, cartelInfamy));
            il.Add(done);
        }
        void LastTheftControl()
        {
            var done = Instruction.Create(OpCodes.Nop);
            var compare = Instruction.Create(OpCodes.Nop);
            LoadDealer();
            il.Add(Instruction.Create(OpCodes.Ldfld, lastTheftField));
            il.Add(Instruction.Create(OpCodes.Brfalse, done));
            il.Add(compare);
            LoadDealer();
            il.Add(Instruction.Create(OpCodes.Ldfld, lastTheftField));
            LoadPerson();
            il.Add(Instruction.Create(OpCodes.Call, getPerson));
            il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
            il.Add(Instruction.Create(OpCodes.Call, fullName));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
            il.Add(Instruction.Create(OpCodes.Call, stringEquals));
            il.Add(Instruction.Create(OpCodes.Brfalse, done));
            StreetImpact(1);
            il.Add(done);
        }
        void PrepareArrest(int crimeId)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, crimes));
            il.Add(Instruction.Create(OpCodes.Ldloc, crimes));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, crimeId));
            LoadHero();
            il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
            LoadHero();
            il.Add(Instruction.Create(OpCodes.Call, getPlace));
            il.Add(Instruction.Create(OpCodes.Newobj, crimeConstructor));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
        }
        void ChainPreparedArrest()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, crimes));
            il.Add(Instruction.Create(OpCodes.Newobj, arrestConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, arrest));
            Chain(arrest);
        }
        void Arrest(int crimeId)
        {
            PrepareArrest(crimeId);
            ChainPreparedArrest();
        }
        void MarkMurderWeapon()
        {
            var done = Instruction.Create(OpCodes.Nop);
            il.Add(Instruction.Create(OpCodes.Ldloc, weapon));
            il.Add(Instruction.Create(OpCodes.Brfalse, done));
            il.Add(Instruction.Create(OpCodes.Ldloc, weapon));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stfld, murderWeaponField));
            il.Add(done);
        }
        void DeathText()
        {
            AppendBody(() => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\n")), LoadPronoun, () => il.Add(Instruction.Create(OpCodes.Ldstr, " died.")));
            AppendNarrative(() => il.Add(Instruction.Create(OpCodes.Ldstr, "\n")), LoadPronoun, () => il.Add(Instruction.Create(OpCodes.Ldstr, " died.")));
        }
        void PoliceText()
        {
            AppendBody(() => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\n")), LoadPronoun, () => il.Add(Instruction.Create(OpCodes.Ldstr, " called the police on you!")));
            AppendNarrative(() => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\n")), LoadPronoun, () => il.Add(Instruction.Create(OpCodes.Ldstr, " called the police on me!")));
        }
        void SharedScenario(bool withGraph)
        {
            if (withGraph)
            {
                il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
                il.Add(Instruction.Create(OpCodes.Stloc, graphs));
                il.Add(Instruction.Create(OpCodes.Ldstr, "Damage"));
                il.Add(Instruction.Create(OpCodes.Ldloc, damage));
                il.Add(Instruction.Create(OpCodes.Ldc_I4, 20));
                il.Add(Instruction.Create(OpCodes.Mul));
                il.Add(Instruction.Create(OpCodes.Conv_R4));
                il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
                il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
                il.Add(Instruction.Create(OpCodes.Newobj, barGraphConstructor));
                il.Add(Instruction.Create(OpCodes.Stloc, graph));
                il.Add(Instruction.Create(OpCodes.Ldloc, graphs));
                il.Add(Instruction.Create(OpCodes.Ldloc, graph));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Pop));
            }
            il.Add(Instruction.Create(OpCodes.Ldloc, title));
            LoadBodyText();
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, chained));
            if (withGraph)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, chained));
                il.Add(Instruction.Create(OpCodes.Ldloc, graphs));
                il.Add(Instruction.Create(OpCodes.Stfld, barGraphArrayField));
            }
            Chain(chained);
        }
        void GenericMissNarrative(bool runaway)
        {
            if (runaway)
            {
                SetNarrative(
                    () => il.Add(Instruction.Create(OpCodes.Ldstr, "I tried to attack the ")),
                    () => { LoadDescription(); il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty)); il.Add(Instruction.Create(OpCodes.Call, indefiniteArticle)); },
                    () => il.Add(Instruction.Create(OpCodes.Ldstr, ", but missed.")),
                    () => il.Add(Instruction.Create(OpCodes.Ldloc, runawaySuffix)));
            }
            else
            {
                SetNarrative(
                    () => il.Add(Instruction.Create(OpCodes.Ldstr, "I tried to attack the ")),
                    () => { LoadDescription(); il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty)); il.Add(Instruction.Create(OpCodes.Call, indefiniteArticle)); },
                    () => il.Add(Instruction.Create(OpCodes.Ldstr, ", but missed.")));
            }
        }

        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, actionSelection));
        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, bodySelection));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedAction));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var actionLoop = Instruction.Create(OpCodes.Nop);
        var actionCheck = Instruction.Create(OpCodes.Nop);
        var nextAction = Instruction.Create(OpCodes.Nop);
        var actionDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, actionCheck));
        il.Add(actionLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, movesField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, moveGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, currentAction));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentAction));
        il.Add(Instruction.Create(OpCodes.Ldfld, actionField));
        il.Add(Instruction.Create(OpCodes.Ldloc, actionSelection));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextAction));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentAction));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedAction));
        il.Add(Instruction.Create(OpCodes.Br, actionDone));
        il.Add(nextAction);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(actionCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, movesField));
        il.Add(Instruction.Create(OpCodes.Callvirt, moveGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, actionLoop));
        il.Add(actionDone);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Stloc, attackText));
        var noAction = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedAction));
        il.Add(Instruction.Create(OpCodes.Brfalse, noAction));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedAction));
        il.Add(Instruction.Create(OpCodes.Ldfld, actionDamageField));
        il.Add(Instruction.Create(OpCodes.Stloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedAction));
        il.Add(Instruction.Create(OpCodes.Ldfld, pastVerbField));
        il.Add(Instruction.Create(OpCodes.Stloc, attackText));
        il.Add(noAction);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var martialLoop = Instruction.Create(OpCodes.Nop);
        var martialCheck = Instruction.Create(OpCodes.Nop);
        var nextMartial = Instruction.Create(OpCodes.Nop);
        var martialDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, martialCheck));
        il.Add(martialLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, martialStringsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, martialStringGetItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, actionSelection));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextMartial));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, martialMovesField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, martialGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, martialMove));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, actionSelection));
        il.Add(Instruction.Create(OpCodes.Ldloc, martialMove));
        il.Add(Instruction.Create(OpCodes.Call, martialDamage));
        il.Add(Instruction.Create(OpCodes.Stloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Ldloc, martialMove));
        il.Add(Instruction.Create(OpCodes.Ldfld, martialPastVerbField));
        il.Add(Instruction.Create(OpCodes.Stloc, attackText));
        il.Add(Instruction.Create(OpCodes.Br, martialDone));
        il.Add(nextMartial);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(martialCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, martialStringsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, martialStringGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, martialLoop));
        il.Add(martialDone);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var weaponLoop = Instruction.Create(OpCodes.Nop);
        var weaponCheck = Instruction.Create(OpCodes.Nop);
        var nextWeapon = Instruction.Create(OpCodes.Nop);
        var weaponDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, weaponCheck));
        il.Add(weaponLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, weaponsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, weaponGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, currentWeapon));
        il.Add(Instruction.Create(OpCodes.Ldloc, actionSelection));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentWeapon));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, getItemName));
        il.Add(Instruction.Create(OpCodes.Ldstr, " (Weapon)"));
        il.Add(Instruction.Create(OpCodes.Call, FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String")));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextWeapon));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentWeapon));
        il.Add(Instruction.Create(OpCodes.Stloc, weapon));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, weapon));
        il.Add(Instruction.Create(OpCodes.Call, weaponDamage));
        il.Add(Instruction.Create(OpCodes.Stloc, weaponResult));
        il.Add(Instruction.Create(OpCodes.Ldloca, weaponResult));
        il.Add(Instruction.Create(OpCodes.Ldfld, tupleItem1));
        il.Add(Instruction.Create(OpCodes.Stloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Ldloca, weaponResult));
        il.Add(Instruction.Create(OpCodes.Ldfld, tupleItem2));
        il.Add(Instruction.Create(OpCodes.Stloc, attackText));
        il.Add(Instruction.Create(OpCodes.Br, weaponDone));
        il.Add(nextWeapon);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(weaponCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, weaponsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, weaponGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, weaponLoop));
        il.Add(weaponDone);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, bodyPartIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var bodyLoop = Instruction.Create(OpCodes.Nop);
        var bodyCheck = Instruction.Create(OpCodes.Nop);
        var nextBody = Instruction.Create(OpCodes.Nop);
        var bodyDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, bodyCheck));
        il.Add(bodyLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, bodyDropDownField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Ldloc, bodySelection));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextBody));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Stloc, bodyPartIndex));
        il.Add(Instruction.Create(OpCodes.Br, bodyDone));
        il.Add(nextBody);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(bodyCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, bodyDropDownField));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, bodyLoop));
        il.Add(bodyDone);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, bodyPartsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, bodyPartIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, bodyPartGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedBodyPart));
        LoadDescription();
        il.Add(Instruction.Create(OpCodes.Call, possession));
        il.Add(Instruction.Create(OpCodes.Stloc, possessedDescription));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, roll));
        var spikedReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, monetizationField));
        il.Add(Instruction.Create(OpCodes.Call, spikedAccess));
        il.Add(Instruction.Create(OpCodes.Brfalse, spikedReady));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, gameVarsField));
        il.Add(Instruction.Create(OpCodes.Ldfld, spikedSettingField));
        il.Add(Instruction.Create(OpCodes.Stloc, spiked));
        il.Add(spikedReady);
        il.Add(Instruction.Create(OpCodes.Ldloc, spiked));
        il.Add(Instruction.Create(OpCodes.Brtrue, Instruction.Create(OpCodes.Nop)));
        var spikedHit = il[^1].Operand as Instruction;
        il.Add(Instruction.Create(OpCodes.Ldloc, roll));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 30));
        il.Add(Instruction.Create(OpCodes.Clt));
        il.Add(Instruction.Create(OpCodes.Stloc, hit));
        var hitReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, hitReady));
        il.Add(spikedHit!);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, hit));
        il.Add(hitReady);
        PickTitle("I came prepared", "Spiked up", "I got that thang on me", "Brass on that ass", "Brass knuckle sandwich");
        var ordinaryText = Instruction.Create(OpCodes.Nop);
        var textReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, spiked));
        il.Add(Instruction.Create(OpCodes.Brfalse, ordinaryText));
        Concat(() => il.Add(Instruction.Create(OpCodes.Ldstr, "You bashed the ")), LoadPossessed, () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")), LoadBodyPart, () => il.Add(Instruction.Create(OpCodes.Ldstr, " with your spiked brass knuckles.")));
        il.Add(Instruction.Create(OpCodes.Stloc, bodyText));
        SetNarrative(() => il.Add(Instruction.Create(OpCodes.Ldstr, "I beat the ")), LoadPossessed, () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")), LoadBodyPart, () => il.Add(Instruction.Create(OpCodes.Ldstr, " with my spiked brass knuckles.")));
        il.Add(Instruction.Create(OpCodes.Br, textReady));
        il.Add(ordinaryText);
        Concat(() => il.Add(Instruction.Create(OpCodes.Ldstr, "You ")), LoadAttackText, () => il.Add(Instruction.Create(OpCodes.Ldstr, " the ")), LoadPossessed, () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")), LoadBodyPart, () => il.Add(Instruction.Create(OpCodes.Ldstr, ".")));
        il.Add(Instruction.Create(OpCodes.Stloc, bodyText));
        SetNarrative(() => il.Add(Instruction.Create(OpCodes.Ldstr, "I ")), LoadAttackText, () => il.Add(Instruction.Create(OpCodes.Ldstr, " the ")), LoadPossessed, () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")), LoadBodyPart, () => il.Add(Instruction.Create(OpCodes.Ldstr, ".")));
        il.Add(textReady);
        il.Add(Instruction.Create(OpCodes.Newobj, parserConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, parser));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getPerson));
        il.Add(Instruction.Create(OpCodes.Stfld, parserPersonField));
        AppendBody(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " Then ")),
            () => { il.Add(Instruction.Create(OpCodes.Ldloc, parser)); LoadBodyPart(); il.Add(Instruction.Create(OpCodes.Call, getColorAssault)); il.Add(Instruction.Create(OpCodes.Ldfld, outboundField)); il.Add(Instruction.Create(OpCodes.Call, parseBody)); });
        var hitPath = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, hit));
        il.Add(Instruction.Create(OpCodes.Brtrue, hitPath));

        RelationshipImpact(-1);
        DealerImpact(impactInfamy, 2);
        PickTitle("Looney Tunes ahh swing", "How did you miss", "They- they just laughed", "What a bad one", "*Starts interpretive dancing*");
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, title));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        Concat(() => il.Add(Instruction.Create(OpCodes.Ldstr, "You ")), LoadAttackText, () => il.Add(Instruction.Create(OpCodes.Ldstr, " the ")), LoadPossessed, () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")), LoadBodyPart, () => il.Add(Instruction.Create(OpCodes.Ldstr, ", but missed.")));
        il.Add(Instruction.Create(OpCodes.Stloc, bodyText));
        var policeMiss = Instruction.Create(OpCodes.Nop);
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isPolice));
        il.Add(Instruction.Create(OpCodes.Brtrue, policeMiss));
        Concat(() => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\n")), LoadPronoun, () => il.Add(Instruction.Create(OpCodes.Ldstr, " ran away.")));
        il.Add(Instruction.Create(OpCodes.Stloc, runawaySuffix));
        var leaveDone = Instruction.Create(OpCodes.Nop);
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, leaveCountField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Bgt, leaveDone));
        LoadDealer();
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, leaveCountField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stfld, leaveCountField));
        RemoveAndReplace(false);
        var missRivalDone = Instruction.Create(OpCodes.Nop);
        var missNotRival = Instruction.Create(OpCodes.Nop);
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isRival));
        il.Add(Instruction.Create(OpCodes.Brfalse, missNotRival));
        StreetImpact(3);
        BossInfamy();
        il.Add(Instruction.Create(OpCodes.Br, missRivalDone));
        il.Add(missNotRival);
        LastTheftControl();
        il.Add(missRivalDone);
        RemoveBreadcrumb();
        var missSbkDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, missSbkDone));
        il.Add(leaveDone);
        var missAccessDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, monetizationField));
        il.Add(Instruction.Create(OpCodes.Call, spikedAccess));
        il.Add(Instruction.Create(OpCodes.Brtrue, missAccessDone));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 100));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 50));
        il.Add(Instruction.Create(OpCodes.Blt, missAccessDone));
        il.Add(Instruction.Create(OpCodes.Newobj, sbkConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, chained));
        Chain(chained);
        il.Add(missAccessDone);
        il.Add(missSbkDone);
        AppendBody(() => il.Add(Instruction.Create(OpCodes.Ldloc, runawaySuffix)));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        LoadBodyText();
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));
        GenericMissNarrative(true);
        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, done));
        il.Add(policeMiss);
        AppendBody(() => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\n")), LoadDescription, () => il.Add(Instruction.Create(OpCodes.Ldstr, " called the police on you!")));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        LoadBodyText();
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Call, impactInfamy));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Neg));
        il.Add(Instruction.Create(OpCodes.Call, impactStanding));
        StreetImpact(-2);
        Arrest(24);
        GenericMissNarrative(false);
        il.Add(Instruction.Create(OpCodes.Br, done));

        il.Add(hitPath);
        var ordinaryDamage = Instruction.Create(OpCodes.Nop);
        var damageReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, spiked));
        il.Add(Instruction.Create(OpCodes.Brfalse, ordinaryDamage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_6));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, damage));
        il.Add(Instruction.Create(OpCodes.Br, damageReady));
        il.Add(ordinaryDamage);
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedBodyPart));
        il.Add(Instruction.Create(OpCodes.Ldfld, bodyPartDamageField));
        il.Add(Instruction.Create(OpCodes.Ldloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Div));
        il.Add(Instruction.Create(OpCodes.Stloc, damage));
        il.Add(damageReady);
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getPerson));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, damage));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Call, impactHealth));
        var threshold1 = Instruction.Create(OpCodes.Nop);
        var threshold2 = Instruction.Create(OpCodes.Nop);
        var threshold3 = Instruction.Create(OpCodes.Nop);
        var threshold4 = Instruction.Create(OpCodes.Nop);
        var thresholdReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, damage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Beq, threshold1));
        il.Add(Instruction.Create(OpCodes.Ldloc, damage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Beq, threshold2));
        il.Add(Instruction.Create(OpCodes.Ldloc, damage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Beq, threshold3));
        il.Add(Instruction.Create(OpCodes.Ldloc, damage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
        il.Add(Instruction.Create(OpCodes.Beq, threshold4));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 75));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 81));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, threshold));
        il.Add(Instruction.Create(OpCodes.Br, thresholdReady));
        il.Add(threshold1);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 95));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, threshold));
        il.Add(Instruction.Create(OpCodes.Br, thresholdReady));
        il.Add(threshold2);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 85));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 91));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, threshold));
        il.Add(Instruction.Create(OpCodes.Br, thresholdReady));
        il.Add(threshold3);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 90));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 96));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, threshold));
        il.Add(Instruction.Create(OpCodes.Br, thresholdReady));
        il.Add(threshold4);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 80));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 86));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, threshold));
        il.Add(thresholdReady);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, roll));
        var highDeath = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, roll));
        il.Add(Instruction.Create(OpCodes.Ldloc, threshold));
        il.Add(Instruction.Create(OpCodes.Bge, highDeath));
        var eligible = Instruction.Create(OpCodes.Nop);
        var checkInfamyEligibility = Instruction.Create(OpCodes.Nop);
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isPolice));
        il.Add(Instruction.Create(OpCodes.Brtrue, eligible));
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isRival));
        il.Add(Instruction.Create(OpCodes.Brtrue, eligible));
        il.Add(Instruction.Create(OpCodes.Br, checkInfamyEligibility));
        il.Add(checkInfamyEligibility);
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, infamyField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        var notEligible = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Blt, notEligible));
        il.Add(eligible);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, roll));
        var lowDeath = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, spiked));
        il.Add(Instruction.Create(OpCodes.Brtrue, lowDeath));
        il.Add(Instruction.Create(OpCodes.Ldloc, roll));
        il.Add(Instruction.Create(OpCodes.Brfalse, lowDeath));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, infamyField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        var lowFailure = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Blt, lowFailure));
        SharedScenario(true);
        var outcomeDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, outcomeDone));
        il.Add(lowFailure);
        StreetImpact(-2);
        var lowPolice = Instruction.Create(OpCodes.Nop);
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isPolice));
        il.Add(Instruction.Create(OpCodes.Brtrue, lowPolice));

        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, retaliations));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Stloc, retaliationText));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var retaliationLoop = Instruction.Create(OpCodes.Nop);
        var retaliationCheck = Instruction.Create(OpCodes.Nop);
        var nextRetaliation = Instruction.Create(OpCodes.Nop);
        var retaliationReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, retaliationCheck));
        il.Add(retaliationLoop);
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
        il.Add(Instruction.Create(OpCodes.Call, getRandomBodyPart));
        il.Add(Instruction.Create(OpCodes.Stloc, currentBodyPart));
        il.Add(Instruction.Create(OpCodes.Call, getRandomAction));
        il.Add(Instruction.Create(OpCodes.Stloc, currentAction));
        Concat(
            LoadPronoun, () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")),
            () => { il.Add(Instruction.Create(OpCodes.Ldloc, currentAction)); il.Add(Instruction.Create(OpCodes.Ldfld, pastVerbField)); },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " your ")),
            () => { il.Add(Instruction.Create(OpCodes.Ldloc, currentBodyPart)); il.Add(Instruction.Create(OpCodes.Ldfld, bodyPartNameField)); },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!")));
        il.Add(Instruction.Create(OpCodes.Stloc, bodySelection));
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliations));
        il.Add(Instruction.Create(OpCodes.Ldloc, bodySelection));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListContains));
        il.Add(Instruction.Create(OpCodes.Brtrue, nextRetaliation));
        var firstRetaliation = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliations));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Brfalse, firstRetaliation));
        Concat(() => il.Add(Instruction.Create(OpCodes.Ldloc, retaliationText)), () => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\n")), () => il.Add(Instruction.Create(OpCodes.Ldloc, bodySelection)));
        il.Add(Instruction.Create(OpCodes.Stloc, retaliationText));
        var retaliationJoined = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, retaliationJoined));
        il.Add(firstRetaliation);
        il.Add(Instruction.Create(OpCodes.Ldloc, bodySelection));
        il.Add(Instruction.Create(OpCodes.Stloc, retaliationText));
        il.Add(retaliationJoined);
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliations));
        il.Add(Instruction.Create(OpCodes.Ldloc, bodySelection));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentBodyPart));
        il.Add(Instruction.Create(OpCodes.Ldfld, bodyPartDamageField));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentAction));
        il.Add(Instruction.Create(OpCodes.Ldfld, actionDamageField));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Div));
        il.Add(Instruction.Create(OpCodes.Ldloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, attackDamage));
        il.Add(nextRetaliation);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(retaliationCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliations));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Bgt, retaliationReady));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 10));
        il.Add(Instruction.Create(OpCodes.Blt, retaliationLoop));
        il.Add(retaliationReady);
        il.Add(Instruction.Create(OpCodes.Ldloc, attackDamage));
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliations));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Div));
        il.Add(Instruction.Create(OpCodes.Stloc, retaliationDamage));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliationDamage));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Call, impactHealth));
        RelationshipImpact(-3);
        DealerImpact(impactStanding, -2);
        var retaliationAccessDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, monetizationField));
        il.Add(Instruction.Create(OpCodes.Call, spikedAccess));
        il.Add(Instruction.Create(OpCodes.Brtrue, retaliationAccessDone));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 100));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 20));
        il.Add(Instruction.Create(OpCodes.Blt, retaliationAccessDone));
        il.Add(Instruction.Create(OpCodes.Newobj, sbkConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, chained));
        Chain(chained);
        il.Add(retaliationAccessDone);
        PickTitle("We expected retaliation, right?", "I would have done the same", "It's ON!", "Tit for tat", "Score: 1 / 1");
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliationText));
        il.Add(Instruction.Create(OpCodes.Stloc, bodyText));
        SharedScenario(false);
        il.Add(Instruction.Create(OpCodes.Ldloc, chained));
        il.Add(Instruction.Create(OpCodes.Ldloc, retaliationDamage));
        il.Add(Instruction.Create(OpCodes.Stfld, damageField));
        AppendNarrative(() => il.Add(Instruction.Create(OpCodes.Ldstr, "\n\n")), () => il.Add(Instruction.Create(OpCodes.Ldloc, retaliationText)));
        il.Add(Instruction.Create(OpCodes.Br, outcomeDone));

        il.Add(lowPolice);
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Call, impactInfamy));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Neg));
        il.Add(Instruction.Create(OpCodes.Call, impactStanding));
        Arrest(24);
        PoliceText();
        SharedScenario(true);
        il.Add(Instruction.Create(OpCodes.Br, outcomeDone));

        il.Add(lowDeath);
        RelationshipImpact(-4);
        DealerImpact(impactInfamy, 2);
        var lowDeathSpecialDone = Instruction.Create(OpCodes.Nop);
        var lowDeathNotSpecial = Instruction.Create(OpCodes.Nop);
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isRival));
        il.Add(Instruction.Create(OpCodes.Brtrue, lowDeathSpecialDone));
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isPolice));
        il.Add(Instruction.Create(OpCodes.Brfalse, lowDeathNotSpecial));
        il.Add(lowDeathSpecialDone);
        StreetImpact(3);
        BossInfamy();
        var lowDeathEffectsDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, lowDeathEffectsDone));
        il.Add(lowDeathNotSpecial);
        LastTheftControl();
        il.Add(lowDeathEffectsDone);
        DeathText();
        LoadDealer();
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, leaveCountField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stfld, leaveCountField));
        RemoveAndReplace(true);
        RemoveBreadcrumb();
        MarkMurderWeapon();
        SharedScenario(true);
        il.Add(Instruction.Create(OpCodes.Br, outcomeDone));

        il.Add(notEligible);
        RelationshipImpact(-4);
        DealerImpact(impactInfamy, 3);
        DealerImpact(impactStanding, -2);
        StreetImpact(-2);
        PoliceText();
        Arrest(1);
        SharedScenario(true);
        il.Add(Instruction.Create(OpCodes.Br, outcomeDone));

        il.Add(highDeath);
        DealerImpact(impactInfamy, 2);
        DealerImpact(impactStanding, -1);
        var highDeathSpecial = Instruction.Create(OpCodes.Nop);
        var highDeathOrdinary = Instruction.Create(OpCodes.Nop);
        var highDeathEffectsDone = Instruction.Create(OpCodes.Nop);
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isPolice));
        il.Add(Instruction.Create(OpCodes.Brtrue, highDeathSpecial));
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isRival));
        il.Add(Instruction.Create(OpCodes.Brfalse, highDeathOrdinary));
        il.Add(highDeathSpecial);
        DealerImpact(impactStanding, 2);
        StreetImpact(3);
        BossInfamy();
        il.Add(Instruction.Create(OpCodes.Br, highDeathEffectsDone));
        il.Add(highDeathOrdinary);
        DealerImpact(impactStanding, -2);
        var highDeathTheftDone = Instruction.Create(OpCodes.Nop);
        var highDeathNotTheft = Instruction.Create(OpCodes.Nop);
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, lastTheftField));
        il.Add(Instruction.Create(OpCodes.Brfalse, highDeathNotTheft));
        LoadDealer();
        il.Add(Instruction.Create(OpCodes.Ldfld, lastTheftField));
        LoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getPerson));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Call, fullName));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, highDeathNotTheft));
        StreetImpact(1);
        il.Add(Instruction.Create(OpCodes.Br, highDeathTheftDone));
        il.Add(highDeathNotTheft);
        StreetImpact(-1);
        il.Add(highDeathTheftDone);
        il.Add(highDeathEffectsDone);
        PrepareArrest(15);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, roll));
        var highDeathArrestDone = Instruction.Create(OpCodes.Nop);
        var highDeathWiliness = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, gameVarsField));
        il.Add(Instruction.Create(OpCodes.Ldfld, assassinSettingField));
        il.Add(Instruction.Create(OpCodes.Brfalse, highDeathWiliness));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, monetizationField));
        il.Add(Instruction.Create(OpCodes.Call, assassinAccess));
        il.Add(Instruction.Create(OpCodes.Brtrue, highDeathArrestDone));
        il.Add(highDeathWiliness);
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, wilinessField));
        il.Add(Instruction.Create(OpCodes.Ldloc, roll));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Blt, highDeathArrestDone));
        ChainPreparedArrest();
        il.Add(highDeathArrestDone);
        DeathText();
        RemoveAndReplace(true);
        RemoveBreadcrumb();
        MarkMurderWeapon();
        SharedScenario(true);
        il.Add(outcomeDone);

        il.Add(done);
        var logDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Brfalse, logDone));
        EmitLifeLogAndRefresh(method, narrativeField);
        il.Add(logDone);
        il.Add(Instruction.Create(OpCodes.Ret));
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

    private static GenericInstanceType GenericInstance(GenericInstanceType template, params TypeReference[] arguments)
    {
        var instance = new GenericInstanceType(template.ElementType);
        foreach (var argument in arguments) instance.GenericArguments.Add(argument);
        return instance;
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
        var definition = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields).Single(candidate => candidate.Name == name);
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
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
}
