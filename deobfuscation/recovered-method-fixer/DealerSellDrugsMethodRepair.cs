using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class DealerSellDrugsMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var closureType = owner.NestedTypes.Single(type => type.Name == "<>c__DisplayClass7_0");
        var scenarioType = FindType(module, "Scenario");
        var arrestType = FindType(module, "ScenarioCrimeArrest");
        var choosePriceType = FindType(module, "ScenarioDealerStreetPersonSellDrugsChoosePrice");
        var personType = FindType(module, "SimDealerStreetPerson");
        var dealerJobType = FindType(module, "SimSpecialJobDealer");
        var crimeType = FindType(module, "SimCrime");
        var gameManagerType = FindType(module, "GameManager");
        var gameReferenceType = FindType(module, "GameReference");
        var dataHubType = FindType(module, "DataHub");
        var bracketParserType = FindType(module, "BracketParser");

        var closureConstructor = ImportMethod(module, closureType, ".ctor", 0, true);
        var closureDrugField = Field(module, closureType, "drug");
        var vicePredicate = ImportMethod(module, closureType, "<ChoiceSelectedAtIndex>b__0", 1, true);
        var drugType = closureDrugField.FieldType;
        var drugTypeField = Field(module, drugType, "<Type>k__BackingField");
        var drugDemandField = Field(module, drugType, "_demand");
        var getDrugData = ImportMethod(module, drugType, "get_Data", 0, true);
        var scheduleField = Field(module, getDrugData.ReturnType, "<Schedule>k__BackingField");
        var getProductName = ImportMethod(module, drugType, "GetProductName", 0, true);
        var drugsField = Field(module, owner, "_drugs");
        var dropDownContentsField = Field(module, owner, "_drugDropDownContents");
        var personField = Field(module, owner, "_person");
        var dealerJobField = Field(module, owner, "_dealerJob");
        var narrativeField = Field(module, owner, "_narrative");
        var nextScenarioField = Field(module, owner, "<NextScenario>k__BackingField");
        var undercoverField = Field(module, personType, "<IsUndercoverCop>k__BackingField");
        var assignedStreetField = Field(module, dealerJobType, "<AssignedStreet>k__BackingField");
        var getGreatestVice = ImportMethod(module, personType, "GetGreatestVice", 0, true);
        var getVices = ImportMethod(module, personType, "GetVices", 0, true);
        var isUniformed = ImportMethod(module, personType, "IsUniformedPoliceOfficer", 0, true);
        var isAddict = ImportMethod(module, personType, "IsAddict", 0, true);
        var getRelationship = ImportMethod(module, personType, "GetHeroRelationshipStrength", 0, true);
        var getWillpower = ImportMethod(module, personType, "GetWillpower", 0, true);
        var impactRelationship = ImportMethod(module, personType, "ImpactRelationshipStrength", 1, true);
        var descriptionString = ImportMethod(module, personType, "DescriptionString", 0, true);
        var pronounString = ImportMethod(module, personType, "PronounStringForType", 1, true);
        var getPerson = ImportMethod(module, personType, "GetPerson", 0, true);
        var getName = ImportMethod(module, personType, "GetName", 1, true);
        var impactInfamy = ImportMethod(module, dealerJobType, "ImpactInfamy", 1, true);
        var impactStanding = ImportMethod(module, dealerJobType, "ImpactStanding", 1, true);
        var impactControl = ImportMethod(module, assignedStreetField.FieldType, "ImpactControl", 1, true);
        var getRandomDrug = ImportMethod(module, owner, "GetRandomDrug", 0, true);
        var getRandomNumber = ImportMethod(module, gameReferenceType, "GetRandomNumber", 2, false);
        var pickRandomTitle = ImportMethod(module, gameReferenceType, "PickRandomFromStringArray", 1, false);
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true, "System.String", "System.String", "Scenario");
        var arrestConstructor = ImportMethod(module, arrestType, ".ctor", 1, true, "System.Collections.ArrayList");
        var choosePriceConstructor = ImportMethod(module, choosePriceType, ".ctor", 2, true, "SimDealerStreetPerson", "SimDealerDrug");
        var crimeConstructor = ImportMethod(module, crimeType, ".ctor", 3, true, "System.Int32", "System.Int32", "SimPlace");
        var bracketParserConstructor = ImportMethod(module, bracketParserType, ".ctor", 0, true);
        var getParsedBody = ImportMethod(module, bracketParserType, "GetParsedBodyString", 1, true);
        var parserPersonField = Field(module, bracketParserType, "<Person>k__BackingField");
        var bodyBracketDictionaryField = Field(module, bracketParserType, "<BodyBracketDictionary>k__BackingField");
        var dictionaryType = bodyBracketDictionaryField.FieldType;
        var dictionaryConstructor = ImportMethod(module, dictionaryType, ".ctor", 0, true);
        var dictionaryAdd = ImportMethod(module, dictionaryType, "Add", 2, true);
        var getRefusalSnippet = ImportMethod(module, dataHubType, "GetRandomDealerDrugSaleRefusalSnippetsForWillpower", 1, false);
        var refusalReasonField = Field(module, getRefusalSnippet.ReturnType, "<Reason>k__BackingField");
        var getPoliceWarning = ImportMethod(module, dataHubType, "GetRandomDealerPoliceWarningsForWillpower", 1, false);
        var policeWarningStringField = Field(module, getPoliceWarning.ReturnType, "<String>k__BackingField");

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var arrayListGetCount = FrameworkMethod(module, "System.Collections.ArrayList", "get_Count", 0, true);
        var drugListGetItem = ImportMethod(module, drugsField.FieldType, "get_Item", 1, true);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 1, true, "System.String");
        var stringEqualsComparison = FrameworkMethod(module, "System.String", "Equals", 2, true, "System.String", "System.StringComparison");
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");
        var concat4 = FrameworkMethod(module, "System.String", "Concat", 4, false, "System.String", "System.String", "System.String", "System.String");
        var concatArray = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");

        var viceType = (GenericInstanceType)getGreatestVice.ReturnType;
        var getViceKey = ImportMethod(module, viceType, "get_Key", 0, true);
        var getViceValue = ImportMethod(module, viceType, "get_Value", 0, true);
        var funcType = GenericType(module, "System", "Func`2", viceType, module.TypeSystem.Boolean);
        var funcConstructor = ImportMethod(module, funcType, ".ctor", 2, true);
        var existingAny = method.Body.Instructions.Select(instruction => instruction.Operand).OfType<GenericInstanceMethod>()
            .Single(candidate => candidate.DeclaringType.FullName == "System.Linq.Enumerable" && candidate.Name == "Any");
        var anyDefinition = module.ImportReference(existingAny.GetElementMethod());
        var anyVice = new GenericInstanceMethod(anyDefinition);
        anyVice.GenericArguments.Add(viceType);

        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var lifeType = lifeField.FieldType;
        var heroField = Field(module, lifeType, "<Hero>k__BackingField");
        var ageField = Field(module, heroField.FieldType, "<Age>k__BackingField");
        var getPlaceOfResidence = ImportMethod(module, heroField.FieldType, "get_PlaceOfResidence", 0, true);
        var lifeLogField = Field(module, lifeType, "<LifeLog>k__BackingField");
        var lifeInterfaceField = Field(module, lifeType, "LifeInterface");
        var lifeLog = ImportMethod(module, lifeLogField.FieldType, "Log", 1, true);
        var refreshUI = ImportMethod(module, lifeInterfaceField.FieldType, "RefreshUI", 0, true);

        var body = FreshBody(method);
        var closure = new VariableDefinition(closureType);
        var selectedString = new VariableDefinition(module.TypeSystem.String);
        var drugIndex = new VariableDefinition(module.TypeSystem.Int32);
        var greatestVice = new VariableDefinition(viceType);
        var vices = new VariableDefinition(getVices.ReturnType);
        var policeFlag = new VariableDefinition(module.TypeSystem.Boolean);
        var chainedScenario = new VariableDefinition(scenarioType);
        var arrayList = new VariableDefinition(arrayListType);
        var title = new VariableDefinition(module.TypeSystem.String);
        var replacementDrug = new VariableDefinition(module.TypeSystem.String);
        var parsedText = new VariableDefinition(module.TypeSystem.String);
        var refusalSnippet = new VariableDefinition(getRefusalSnippet.ReturnType);
        var policeWarning = new VariableDefinition(getPoliceWarning.ReturnType);
        var parser = new VariableDefinition(bracketParserType);
        var hero = new VariableDefinition(heroField.FieldType);
        var crime = new VariableDefinition(crimeType);
        var minimum = new VariableDefinition(module.TypeSystem.Int32);
        var maximum = new VariableDefinition(module.TypeSystem.Int32);
        var arrestThreshold = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { closure, selectedString, drugIndex, greatestVice, vices, policeFlag, chainedScenario, arrayList, title, replacementDrug, parsedText, refusalSnippet, policeWarning, parser, hero, crime, minimum, maximum, arrestThreshold }) body.Variables.Add(local);
        var il = body.Instructions;
        var dropDownStrings = method.Parameters.Single(parameter => parameter.Name == "dropDownStrings");

        void EmitLoadPerson()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        }

        void EmitLoadDrug()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, closure));
            il.Add(Instruction.Create(OpCodes.Ldfld, closureDrugField));
        }

        void EmitLoadProductName()
        {
            EmitLoadDrug();
            il.Add(Instruction.Create(OpCodes.Call, getProductName));
        }

        void EmitLoadDescription()
        {
            EmitLoadPerson();
            il.Add(Instruction.Create(OpCodes.Call, descriptionString));
        }

        void EmitLoadPronoun(int type)
        {
            EmitLoadPerson();
            il.Add(Instruction.Create(OpCodes.Ldc_I4, type));
            il.Add(Instruction.Create(OpCodes.Call, pronounString));
        }

        void EmitLoadViceKey()
        {
            il.Add(Instruction.Create(OpCodes.Ldloca, greatestVice));
            il.Add(Instruction.Create(OpCodes.Call, getViceKey));
        }

        void EmitStringArray(params Action[] values)
        {
            il.Add(Instruction.Create(OpCodes.Ldc_I4, values.Length));
            il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.String));
            for (var index = 0; index < values.Length; index++)
            {
                il.Add(Instruction.Create(OpCodes.Dup));
                il.Add(Instruction.Create(OpCodes.Ldc_I4, index));
                values[index]();
                il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            }
        }

        void EmitRandomTitle(params string[] choices)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, arrayList));
            foreach (var choice in choices)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, arrayList));
                il.Add(Instruction.Create(OpCodes.Ldstr, choice));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Pop));
            }
            il.Add(Instruction.Create(OpCodes.Ldloc, arrayList));
            il.Add(Instruction.Create(OpCodes.Call, pickRandomTitle));
            il.Add(Instruction.Create(OpCodes.Stloc, title));
        }

        void EmitChainScenario()
        {
            il.Add(Instruction.Create(OpCodes.Stloc, chainedScenario));
            il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
            il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        }

        void EmitNewScenario()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, title));
        }

        void EmitArrestScenario()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
            il.Add(Instruction.Create(OpCodes.Stloc, hero));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
            il.Add(Instruction.Create(OpCodes.Ldloc, hero));
            il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
            il.Add(Instruction.Create(OpCodes.Ldloc, hero));
            il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
            il.Add(Instruction.Create(OpCodes.Newobj, crimeConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, crime));
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, arrayList));
            il.Add(Instruction.Create(OpCodes.Ldloc, arrayList));
            il.Add(Instruction.Create(OpCodes.Ldloc, crime));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
            il.Add(Instruction.Create(OpCodes.Ldloc, arrayList));
            il.Add(Instruction.Create(OpCodes.Newobj, arrestConstructor));
            EmitChainScenario();
        }

        il.Add(Instruction.Create(OpCodes.Newobj, closureConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, closure));
        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedString));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, drugIndex));
        var drugLoop = Instruction.Create(OpCodes.Nop);
        var drugLoopCheck = Instruction.Create(OpCodes.Nop);
        var drugSelected = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, drugLoopCheck));
        il.Add(drugLoop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownContentsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, drugIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedString));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEqualsComparison));
        il.Add(Instruction.Create(OpCodes.Brtrue, drugSelected));
        il.Add(Instruction.Create(OpCodes.Ldloc, drugIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, drugIndex));
        il.Add(drugLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, drugIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dropDownContentsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, drugLoop));
        il.Add(drugSelected);
        il.Add(Instruction.Create(OpCodes.Ldloc, closure));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, drugsField));
        il.Add(Instruction.Create(OpCodes.Ldloc, drugIndex));
        il.Add(Instruction.Create(OpCodes.Call, drugListGetItem));
        il.Add(Instruction.Create(OpCodes.Stfld, closureDrugField));

        var ordinarySale = Instruction.Create(OpCodes.Nop);
        var finish = Instruction.Create(OpCodes.Nop);
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Ldfld, undercoverField));
        il.Add(Instruction.Create(OpCodes.Brfalse, ordinarySale));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, -2));
        il.Add(Instruction.Create(OpCodes.Call, impactInfamy));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, -2));
        il.Add(Instruction.Create(OpCodes.Call, impactStanding));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldfld, assignedStreetField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, -2));
        il.Add(Instruction.Create(OpCodes.Call, impactControl));
        EmitArrestScenario();
        EmitRandomTitle("Just my luck", "Is this a prank?", "How could you not tell?", "An obvious trap...", "HANDS IN THE AIR");
        EmitNewScenario();
        il.Add(Instruction.Create(OpCodes.Ldstr, "The "));
        EmitLoadDescription();
        il.Add(Instruction.Create(OpCodes.Ldstr, " turned out to be an undercover cop!\n\nYou are told to stay where you are."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        EmitChainScenario();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        EmitStringArray(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "I offered some ")),
            EmitLoadProductName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " to the ")),
            EmitLoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, ".\n")),
            () => EmitLoadPronoun(5),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " turned out to be an undercover cop!")));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Br, finish));

        il.Add(ordinarySale);
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getGreatestVice));
        il.Add(Instruction.Create(OpCodes.Stloc, greatestVice));
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getVices));
        il.Add(Instruction.Create(OpCodes.Stloc, vices));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, policeFlag));
        var checkVices = Instruction.Create(OpCodes.Nop);
        var secondPoliceRoll = Instruction.Create(OpCodes.Nop);
        var setPoliceFlag = Instruction.Create(OpCodes.Nop);
        var policeFlagReady = Instruction.Create(OpCodes.Nop);
        EmitLoadViceKey();
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Ldfld, drugTypeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEqualsComparison));
        il.Add(Instruction.Create(OpCodes.Brfalse, checkVices));
        il.Add(Instruction.Create(OpCodes.Br, setPoliceFlag));
        il.Add(checkVices);
        il.Add(Instruction.Create(OpCodes.Ldloc, vices));
        il.Add(Instruction.Create(OpCodes.Ldloc, closure));
        il.Add(Instruction.Create(OpCodes.Ldftn, vicePredicate));
        il.Add(Instruction.Create(OpCodes.Newobj, funcConstructor));
        il.Add(Instruction.Create(OpCodes.Call, anyVice));
        il.Add(Instruction.Create(OpCodes.Brfalse, secondPoliceRoll));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 71));
        il.Add(Instruction.Create(OpCodes.Bge, secondPoliceRoll));
        il.Add(Instruction.Create(OpCodes.Br, setPoliceFlag));
        il.Add(secondPoliceRoll);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 61));
        il.Add(Instruction.Create(OpCodes.Clt));
        il.Add(Instruction.Create(OpCodes.Stloc, policeFlag));
        il.Add(Instruction.Create(OpCodes.Br, policeFlagReady));
        il.Add(setPoliceFlag);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, policeFlag));
        il.Add(policeFlagReady);

        var checkDemand = Instruction.Create(OpCodes.Nop);
        var choosePrice = Instruction.Create(OpCodes.Nop);
        var refusal = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, policeFlag));
        il.Add(Instruction.Create(OpCodes.Brfalse, checkDemand));
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isUniformed));
        il.Add(Instruction.Create(OpCodes.Brfalse, choosePrice));
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getRelationship));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 30f));
        il.Add(Instruction.Create(OpCodes.Bgt, choosePrice));
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getWillpower));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 30f));
        il.Add(Instruction.Create(OpCodes.Bge_Un, checkDemand));
        il.Add(Instruction.Create(OpCodes.Br, choosePrice));
        il.Add(checkDemand);
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Ldfld, drugDemandField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 60f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, refusal));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        il.Add(Instruction.Create(OpCodes.Brfalse, choosePrice));
        il.Add(Instruction.Create(OpCodes.Br, refusal));

        il.Add(choosePrice);
        var noSaleImpact = Instruction.Create(OpCodes.Nop);
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isUniformed));
        il.Add(Instruction.Create(OpCodes.Brfalse, noSaleImpact));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, impactInfamy));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, impactStanding));
        il.Add(noSaleImpact);
        EmitLoadPerson();
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Newobj, choosePriceConstructor));
        EmitChainScenario();
        il.Add(Instruction.Create(OpCodes.Br, finish));

        il.Add(refusal);
        var nonUniformedRefusal = Instruction.Create(OpCodes.Nop);
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isUniformed));
        il.Add(Instruction.Create(OpCodes.Brfalse, nonUniformedRefusal));
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Call, getDrugData));
        il.Add(Instruction.Create(OpCodes.Ldfld, scheduleField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        var scheduleThree = Instruction.Create(OpCodes.Nop);
        var scheduleFour = Instruction.Create(OpCodes.Nop);
        var scheduleFive = Instruction.Create(OpCodes.Nop);
        var defaultSchedule = Instruction.Create(OpCodes.Nop);
        var scheduleReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Bne_Un, scheduleThree));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 40));
        il.Add(Instruction.Create(OpCodes.Stloc, minimum));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 50));
        il.Add(Instruction.Create(OpCodes.Stloc, maximum));
        il.Add(Instruction.Create(OpCodes.Br, scheduleReady));
        il.Add(scheduleThree);
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Call, getDrugData));
        il.Add(Instruction.Create(OpCodes.Ldfld, scheduleField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Bne_Un, scheduleFour));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 30));
        il.Add(Instruction.Create(OpCodes.Stloc, minimum));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 40));
        il.Add(Instruction.Create(OpCodes.Stloc, maximum));
        il.Add(Instruction.Create(OpCodes.Br, scheduleReady));
        il.Add(scheduleFour);
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Call, getDrugData));
        il.Add(Instruction.Create(OpCodes.Ldfld, scheduleField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
        il.Add(Instruction.Create(OpCodes.Bne_Un, scheduleFive));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 20));
        il.Add(Instruction.Create(OpCodes.Stloc, minimum));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 30));
        il.Add(Instruction.Create(OpCodes.Stloc, maximum));
        il.Add(Instruction.Create(OpCodes.Br, scheduleReady));
        il.Add(scheduleFive);
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Call, getDrugData));
        il.Add(Instruction.Create(OpCodes.Ldfld, scheduleField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Bne_Un, defaultSchedule));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 10));
        il.Add(Instruction.Create(OpCodes.Stloc, minimum));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 20));
        il.Add(Instruction.Create(OpCodes.Stloc, maximum));
        il.Add(Instruction.Create(OpCodes.Br, scheduleReady));
        il.Add(defaultSchedule);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 40));
        il.Add(Instruction.Create(OpCodes.Stloc, minimum));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 60));
        il.Add(Instruction.Create(OpCodes.Stloc, maximum));
        il.Add(scheduleReady);
        il.Add(Instruction.Create(OpCodes.Ldloc, minimum));
        il.Add(Instruction.Create(OpCodes.Ldloc, maximum));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        il.Add(Instruction.Create(OpCodes.Stloc, arrestThreshold));
        il.Add(Instruction.Create(OpCodes.Ldloc, arrestThreshold));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        var policeWarningPath = Instruction.Create(OpCodes.Nop);
        var arrest = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Blt, policeWarningPath));
        il.Add(Instruction.Create(OpCodes.Br, arrest));

        il.Add(policeWarningPath);
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getWillpower));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Call, getPoliceWarning));
        il.Add(Instruction.Create(OpCodes.Stloc, policeWarning));
        il.Add(Instruction.Create(OpCodes.Newobj, bracketParserConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, parser));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getPerson));
        il.Add(Instruction.Create(OpCodes.Stfld, parserPersonField));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        il.Add(Instruction.Create(OpCodes.Newobj, dictionaryConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "<drug-cartel>"));
        EmitLoadProductName();
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryAdd));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyBracketDictionaryField));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        il.Add(Instruction.Create(OpCodes.Ldloc, policeWarning));
        il.Add(Instruction.Create(OpCodes.Ldfld, policeWarningStringField));
        il.Add(Instruction.Create(OpCodes.Call, getParsedBody));
        il.Add(Instruction.Create(OpCodes.Stloc, parsedText));
        EmitRandomTitle("Stranger danger", "Have a good one...", "Try the next one", "They aren't stopping", "Can't get eye contact");
        EmitNewScenario();
        EmitStringArray(
            () => { EmitLoadPerson(); il.Add(Instruction.Create(OpCodes.Ldc_I4_0)); il.Add(Instruction.Create(OpCodes.Call, getName)); },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " does not want to buy ")),
            EmitLoadProductName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " from you.\n\n")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, parsedText)));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        EmitChainScenario();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Ldstr, "\n"));
        EmitLoadPronoun(5);
        il.Add(Instruction.Create(OpCodes.Ldstr, " threatened to arrest me if I continued dealing drugs"));
        il.Add(Instruction.Create(OpCodes.Call, concat4));
        il.Add(Instruction.Create(OpCodes.Stloc, parsedText));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, parsedText));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Br, finish));

        il.Add(nonUniformedRefusal);
        var checkAddict = Instruction.Create(OpCodes.Nop);
        var alternativeDrug = Instruction.Create(OpCodes.Nop);
        var ordinaryRefusal = Instruction.Create(OpCodes.Nop);
        EmitLoadViceKey();
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Ldfld, drugTypeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEqualsComparison));
        il.Add(Instruction.Create(OpCodes.Brtrue, checkAddict));
        il.Add(Instruction.Create(OpCodes.Ldloca, greatestVice));
        il.Add(Instruction.Create(OpCodes.Call, getViceValue));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 40f));
        il.Add(Instruction.Create(OpCodes.Bgt, alternativeDrug));
        il.Add(checkAddict);
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, isAddict));
        il.Add(Instruction.Create(OpCodes.Brfalse, ordinaryRefusal));
        il.Add(Instruction.Create(OpCodes.Br, alternativeDrug));

        il.Add(ordinaryRefusal);
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Ldc_I4, -2));
        il.Add(Instruction.Create(OpCodes.Call, impactRelationship));
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getWillpower));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Call, getRefusalSnippet));
        il.Add(Instruction.Create(OpCodes.Stloc, refusalSnippet));
        il.Add(Instruction.Create(OpCodes.Newobj, bracketParserConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, parser));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        EmitLoadPerson();
        il.Add(Instruction.Create(OpCodes.Call, getPerson));
        il.Add(Instruction.Create(OpCodes.Stfld, parserPersonField));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        il.Add(Instruction.Create(OpCodes.Ldloc, refusalSnippet));
        il.Add(Instruction.Create(OpCodes.Ldfld, refusalReasonField));
        il.Add(Instruction.Create(OpCodes.Call, getParsedBody));
        il.Add(Instruction.Create(OpCodes.Stloc, parsedText));
        EmitRandomTitle("Actually I'm sober");
        EmitNewScenario();
        EmitStringArray(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")),
            EmitLoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " says ")),
            () => EmitLoadPronoun(0),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " isn't interested in buying ")),
            EmitLoadProductName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " from you.\n\n")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, parsedText)));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        EmitChainScenario();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        EmitStringArray(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "I offered some ")),
            EmitLoadProductName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " to the ")),
            EmitLoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, ", but ")),
            () => EmitLoadPronoun(0),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " refused.")));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Br, finish));

        il.Add(alternativeDrug);
        var chooseRandomReplacement = Instruction.Create(OpCodes.Nop);
        var keepGreatestVice = Instruction.Create(OpCodes.Nop);
        var replacementReady = Instruction.Create(OpCodes.Nop);
        EmitLoadViceKey();
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brtrue, chooseRandomReplacement));
        EmitLoadViceKey();
        EmitLoadDrug();
        il.Add(Instruction.Create(OpCodes.Ldfld, drugTypeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEqualsComparison));
        il.Add(Instruction.Create(OpCodes.Brfalse, keepGreatestVice));
        il.Add(chooseRandomReplacement);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getRandomDrug));
        il.Add(Instruction.Create(OpCodes.Stloc, replacementDrug));
        il.Add(Instruction.Create(OpCodes.Br, replacementReady));
        il.Add(keepGreatestVice);
        EmitLoadViceKey();
        il.Add(Instruction.Create(OpCodes.Stloc, replacementDrug));
        il.Add(replacementReady);
        EmitRandomTitle("There's some interest here", "We got a bite", "What's in stock?", "No menu?", "You do you need?");
        EmitNewScenario();
        EmitStringArray(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")),
            EmitLoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " says ")),
            () => EmitLoadPronoun(7),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " rather buy ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, replacementDrug)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " from you if you've got any.")));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        EmitChainScenario();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        EmitStringArray(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "I offered some ")),
            EmitLoadProductName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " to the ")),
            EmitLoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, ", but ")),
            () => EmitLoadPronoun(0),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " refused and asked for ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, replacementDrug)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " instead.")));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Br, finish));

        il.Add(arrest);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        il.Add(Instruction.Create(OpCodes.Call, impactInfamy));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Call, getRandomNumber));
        il.Add(Instruction.Create(OpCodes.Neg));
        il.Add(Instruction.Create(OpCodes.Call, impactStanding));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, dealerJobField));
        il.Add(Instruction.Create(OpCodes.Ldfld, assignedStreetField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, -2));
        il.Add(Instruction.Create(OpCodes.Call, impactControl));
        EmitArrestScenario();
        il.Add(Instruction.Create(OpCodes.Br, finish));

        il.Add(finish);
        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeLogField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Call, lifeLog));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeInterfaceField));
        il.Add(Instruction.Create(OpCodes.Call, refreshUI));
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static TypeReference FrameworkType(ModuleDefinition module, string namespaceName, string name)
    {
        var type = new TypeReference(namespaceName, name, module, module.TypeSystem.Object.Scope);
        return module.ImportReference(type);
    }

    private static GenericInstanceType GenericType(ModuleDefinition module, string namespaceName, string name, params TypeReference[] arguments)
    {
        var definition = FrameworkType(module, namespaceName, name).Resolve();
        var instance = new GenericInstanceType(module.ImportReference(definition));
        foreach (var argument in arguments) instance.GenericArguments.Add(module.ImportReference(argument));
        return instance;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var definition = FindMethod(declaringType, name, parameterCount, instance, parameterTypes) ?? throw new InvalidOperationException($"Method not found: {declaringType.FullName}::{name}/{parameterCount}");
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static MethodDefinition? FindMethod(TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var match = type.Methods.SingleOrDefault(candidate =>
                candidate.Name == name && candidate.Parameters.Count == parameterCount &&
                (!instance.HasValue || candidate.IsStatic != instance.Value) &&
                (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
            if (match is not null) return match;
        }
        return null;
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName, int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module, module.TypeSystem.Object.Scope);
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
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
        foreach (var descendant in Flatten(nested)) yield return descendant;
    }
}
