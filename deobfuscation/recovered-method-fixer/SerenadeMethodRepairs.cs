using Mono.Cecil;
using Mono.Cecil.Cil;

public static class SerenadeMethodRepairs
{
    public static void FixBuild(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var hustlerType = FindType(module, "SimSpecialJobHustler");
        var physicalType = FindType(module, "SimMusicInstrumentPhysical");
        var gameReferenceType = FindType(module, "GameReference");
        var gameManagerType = FindType(module, "GameManager");

        var hustlerField = Field(module, owner, "_currentSpecialJobHustler");
        var currentHustleField = Field(module, hustlerType, "<CurrentHustle>k__BackingField");
        var hustleTypeField = Field(module, currentHustleField.FieldType, "<HustleType>k__BackingField");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var lifeType = lifeField.FieldType;

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var arrayListContains = FrameworkMethod(module, "System.Collections.ArrayList", "Contains", 1, true);
        var arrayListGetEnumerator = FrameworkMethod(module, "System.Collections.ArrayList", "GetEnumerator", 0, true);
        var getPhysicalInstruments = ImportMethod(module, lifeType, "get_PhysicalInstrumentArray", 0, true);
        var getCurrentBuskingInstrument = ImportMethod(module, currentHustleField.FieldType,
            "GetCurrentBuskingInstrument", 0, true);
        var getData = ImportMethod(module, physicalType, "get_Data", 0, true);
        var dataType = getData.ReturnType;
        var nameField = Field(module, dataType, "<Name>k__BackingField");
        var canBuskField = Field(module, dataType, "<CanBusk>k__BackingField");
        var capitalize = ImportMethod(module, gameReferenceType, "CapitalizeFirstLetterOfSentenceString", 1, false);

        var enumeratorInterface = FrameworkType(module, "System.Collections", "IEnumerator");
        var disposableInterface = FrameworkType(module, "System", "IDisposable");
        var getCurrent = FrameworkMethod(module, "System.Collections.IEnumerator", "get_Current", 0, true);
        var moveNext = FrameworkMethod(module, "System.Collections.IEnumerator", "MoveNext", 0, true);
        var dispose = FrameworkMethod(module, "System.IDisposable", "Dispose", 0, true);

        var body = FreshBody(method);
        var result = new VariableDefinition(arrayListType);
        var currentInstrument = new VariableDefinition(physicalType);
        var instruments = new VariableDefinition(arrayListType);
        var enumerator = new VariableDefinition(enumeratorInterface);
        var disposable = new VariableDefinition(disposableInterface);
        var instrument = new VariableDefinition(physicalType);
        var data = new VariableDefinition(dataType);
        var name = new VariableDefinition(module.TypeSystem.String);
        foreach (var local in new[] { result, currentInstrument, instruments, enumerator, disposable, instrument, data, name })
            body.Variables.Add(local);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, result));

        var noCurrentHustler = Instruction.Create(OpCodes.Nop);
        var noCurrentHustle = Instruction.Create(OpCodes.Nop);
        var noCurrentInstrument = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, hustlerField));
        il.Add(Instruction.Create(OpCodes.Brfalse, noCurrentHustler));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, hustlerField));
        il.Add(Instruction.Create(OpCodes.Ldfld, currentHustleField));
        il.Add(Instruction.Create(OpCodes.Brfalse, noCurrentHustle));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, hustlerField));
        il.Add(Instruction.Create(OpCodes.Ldfld, currentHustleField));
        il.Add(Instruction.Create(OpCodes.Ldfld, hustleTypeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Bne_Un, noCurrentHustle));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, hustlerField));
        il.Add(Instruction.Create(OpCodes.Ldfld, currentHustleField));
        il.Add(Instruction.Create(OpCodes.Call, getCurrentBuskingInstrument));
        il.Add(Instruction.Create(OpCodes.Stloc, currentInstrument));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentInstrument));
        il.Add(Instruction.Create(OpCodes.Brfalse, noCurrentInstrument));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentInstrument));
        il.Add(Instruction.Create(OpCodes.Call, getData));
        il.Add(Instruction.Create(OpCodes.Stloc, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Brfalse, noCurrentInstrument));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(noCurrentInstrument);
        il.Add(noCurrentHustle);
        il.Add(noCurrentHustler);

        var enumerationDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Call, getPhysicalInstruments));
        il.Add(Instruction.Create(OpCodes.Stloc, instruments));
        il.Add(Instruction.Create(OpCodes.Ldloc, instruments));
        il.Add(Instruction.Create(OpCodes.Brfalse, enumerationDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, instruments));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));

        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var nextInstrument = Instruction.Create(OpCodes.Nop);
        var addName = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var disposalEnd = Instruction.Create(OpCodes.Nop);
        var noDisposable = Instruction.Create(OpCodes.Nop);
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, getCurrent));
        il.Add(Instruction.Create(OpCodes.Castclass, physicalType));
        il.Add(Instruction.Create(OpCodes.Stloc, instrument));
        il.Add(Instruction.Create(OpCodes.Ldloc, instrument));
        il.Add(Instruction.Create(OpCodes.Call, getData));
        il.Add(Instruction.Create(OpCodes.Stloc, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextInstrument));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Ldfld, canBuskField));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextInstrument));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Stloc, name));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, name));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListContains));
        il.Add(Instruction.Create(OpCodes.Brfalse, addName));
        il.Add(Instruction.Create(OpCodes.Br, nextInstrument));
        il.Add(addName);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, name));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(nextInstrument);
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, enumerationDone));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Isinst, disposableInterface));
        il.Add(Instruction.Create(OpCodes.Stloc, disposable));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Brfalse, noDisposable));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Callvirt, dispose));
        il.Add(noDisposable);
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(disposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = tryStart,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = disposalEnd
        });
        il.Add(enumerationDone);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixChoice(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var personType = FindType(module, "SimPerson");
        var streetPersonType = FindType(module, "SimStreetPerson");
        var hustlerType = FindType(module, "SimSpecialJobHustler");
        var physicalType = FindType(module, "SimMusicInstrumentPhysical");
        var knowledgeType = FindType(module, "SimMusicInstrument");
        var gameReferenceType = FindType(module, "GameReference");
        var dataHubType = FindType(module, "DataHub");
        var gameManagerType = FindType(module, "GameManager");
        var attackType = FindType(module, "ScenarioAttackInbound");
        var lawsuitType = FindType(module, "SimLawsuitTarget");

        var narrativeField = Field(module, owner, "_narrative");
        var streetPersonField = Field(module, owner, "_thePerson");
        var hustlerField = Field(module, owner, "_currentSpecialJobHustler");
        var nextScenarioField = Field(module, scenarioType, "<NextScenario>k__BackingField");
        var personField = Field(module, streetPersonType, "<Person>k__BackingField");
        var descriptionField = Field(module, streetPersonType, "<DescriptionString>k__BackingField");
        var genderField = Field(module, personType, "<Gender>k__BackingField");
        var happinessField = Field(module, personType, "<Att_happiness>k__BackingField");
        var disciplineField = Field(module, personType, "<Att_discipline>k__BackingField");
        var generosityField = Field(module, personType, "<Att_generosity>k__BackingField");
        var crazinessField = Field(module, personType, "<Att_craziness>k__BackingField");
        var conditionField = Field(module, physicalType, "<Condition>k__BackingField");
        var skillField = Field(module, knowledgeType, "<Att_skill>k__BackingField");
        var streetField = Field(module, hustlerType, "<Street>k__BackingField");
        var crimeField = Field(module, streetField.FieldType, "<Crime>k__BackingField");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var lifeType = lifeField.FieldType;
        var heroField = Field(module, lifeType, "<Hero>k__BackingField");
        var financesField = Field(module, lifeType, "<Finances>k__BackingField");
        var lifeLogField = Field(module, lifeType, "<LifeLog>k__BackingField");
        var lifeInterfaceField = Field(module, lifeType, "LifeInterface");

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var arrayListRemove = FrameworkMethod(module, "System.Collections.ArrayList", "Remove", 1, true);
        var arrayListGetEnumerator = FrameworkMethod(module, "System.Collections.ArrayList", "GetEnumerator", 0, true);
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var concatArray = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");
        var concat2 = FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String");
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false,
            "System.String", "System.String", "System.String");
        var concat4 = FrameworkMethod(module, "System.String", "Concat", 4, false,
            "System.String", "System.String", "System.String", "System.String");
        var replace = FrameworkMethod(module, "System.String", "Replace", 2, true, "System.String", "System.String");
        var isNullOrEmpty = FrameworkMethod(module, "System.String", "IsNullOrEmpty", 1, false);
        var formatObject = FrameworkMethod(module, "System.String", "Format", 2, false, "System.String", "System.Object");
        var formatArray = FrameworkMethod(module, "System.String", "Format", 2, false, "System.String", "System.Object[]");

        var enumeratorInterface = FrameworkType(module, "System.Collections", "IEnumerator");
        var disposableInterface = FrameworkType(module, "System", "IDisposable");
        var getEnumeratorCurrent = FrameworkMethod(module, "System.Collections.IEnumerator", "get_Current", 0, true);
        var enumeratorMoveNext = FrameworkMethod(module, "System.Collections.IEnumerator", "MoveNext", 0, true);
        var dispose = FrameworkMethod(module, "System.IDisposable", "Dispose", 0, true);

        var getMusicInstrument = ImportMethod(module, dataHubType, "GetMusicInstrumentForName", 1, false);
        var getSerenadeSong = ImportMethod(module, dataHubType, "GetSerenadeSongForName", 1, false);
        var getRandomCompliment = ImportMethod(module, dataHubType, "GetRandomComplimentForGender", 1, false);
        var getRandomInsult = ImportMethod(module, dataHubType, "GetRandomHustlerInsultForGender", 1, false);
        var getRandomPanhandleItem = ImportMethod(module, dataHubType, "GetRandomPanhandleItem", 0, false);
        var insultField = Field(module, getRandomInsult.ReturnType, "<Insult>k__BackingField");
        var itemField = Field(module, getRandomPanhandleItem.ReturnType, "<Item>k__BackingField");
        var pluralField = Field(module, getRandomPanhandleItem.ReturnType, "<Plural>k__BackingField");
        var indefiniteArticleField = Field(module, getRandomPanhandleItem.ReturnType,
            "<UsesIndefiniteArticle>k__BackingField");
        var reactionScoreField = Field(module, getSerenadeSong.ReturnType, "<ReactionScore>k__BackingField");
        var getStreetPersonData = ImportMethod(module, streetPersonType, "get_StreetPersonData", 0, true);
        var moneyScoreField = Field(module, getStreetPersonData.ReturnType, "<MoneyScore>k__BackingField");
        var getPhysicalData = ImportMethod(module, physicalType, "get_Data", 0, true);
        var getKnowledge = ImportMethod(module, physicalType, "GetKnowledgeRepresentation", 0, true);
        var impactSkill = ImportMethod(module, knowledgeType, "ImpactSkill", 1, true);
        var random = ImportMethod(module, gameReferenceType, "GetRandomNumber", 2, false);
        var pickTitle = ImportMethod(module, gameReferenceType, "PickRandomFromStringArray", 1, false);
        var indefiniteArticle = ImportMethod(module, gameReferenceType, "AddIndefiniteArticleToString", 2, false);
        var currencyString = ImportMethod(module, gameReferenceType, "CurrencyStringForAmount", 1, false);
        var impactHappiness = ImportMethod(module, personType, "ImpactHappiness", 1, true);
        var impactStreetSmarts = ImportMethod(module, hustlerType, "ImpactStreetSmarts", 2, true);
        var endBuskingHustle = ImportMethod(module, hustlerType, "EndBuskingHustleIfInstrumentRemoved", 1, false,
            "SimMusicInstrumentPhysical");
        var getPhysicalInstruments = ImportMethod(module, lifeType, "get_PhysicalInstrumentArray", 0, true);
        var getLawsuitTargets = ImportMethod(module, lifeType, "get_LawsuitTargetArray", 0, true);
        var getPlaceOfResidence = ImportMethod(module, personType, "get_PlaceOfResidence", 0, true);
        var getCountry = ImportMethod(module, getPlaceOfResidence.ReturnType, "get_Country", 0, true);
        var localizeCost = ImportMethod(module, getCountry.ReturnType,
            "GetLocalCostForAmountWithMinimumCostAndRoundTo", 3, true);
        var adjustBalance = ImportMethod(module, financesField.FieldType, "AdjustBankBalanceByAmount", 1, true);
        var getDamages = ImportMethod(module, streetPersonType, "GetDamagesBaseline", 0, true);
        var log = ImportMethod(module, lifeLogField.FieldType, "Log", 1, true);
        var refreshUI = ImportMethod(module, lifeInterfaceField.FieldType, "RefreshUI", 0, true);
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true,
            "System.String", "System.String", "Scenario");
        var attackConstructor = ImportMethod(module, attackType, ".ctor", 5, true,
            "SimPerson", "System.Int32", "System.String", "System.Boolean", "System.Int32");
        var lawsuitConstructor = ImportMethod(module, lawsuitType, ".ctor", 5, true,
            "System.Int32", "System.String", getCountry.ReturnType.FullName, "System.Single", "System.String");

        var body = FreshBody(method);
        var instrumentName = new VariableDefinition(module.TypeSystem.String);
        var songName = new VariableDefinition(module.TypeSystem.String);
        var instrumentData = new VariableDefinition(getMusicInstrument.ReturnType);
        var song = new VariableDefinition(getSerenadeSong.ReturnType);
        var physical = new VariableDefinition(physicalType);
        var knowledge = new VariableDefinition(knowledgeType);
        var skill = new VariableDefinition(module.TypeSystem.Single);
        var score = new VariableDefinition(module.TypeSystem.Single);
        var generosityPassed = new VariableDefinition(module.TypeSystem.Boolean);
        var happinessPassed = new VariableDefinition(module.TypeSystem.Boolean);
        var crazinessPassed = new VariableDefinition(module.TypeSystem.Boolean);
        var crimePassed = new VariableDefinition(module.TypeSystem.Boolean);
        var performancePassed = new VariableDefinition(module.TypeSystem.Boolean);
        var randomValue = new VariableDefinition(module.TypeSystem.Int32);
        var enumerator = new VariableDefinition(enumeratorInterface);
        var disposable = new VariableDefinition(disposableInterface);
        var currentPhysical = new VariableDefinition(physicalType);
        var title = new VariableDefinition(module.TypeSystem.String);
        var resultBody = new VariableDefinition(module.TypeSystem.String);
        var logText = new VariableDefinition(module.TypeSystem.String);
        var chainedScenario = new VariableDefinition(scenarioType);
        var complimentOrReaction = new VariableDefinition(module.TypeSystem.String);
        var insult = new VariableDefinition(getRandomInsult.ReturnType);
        var panhandleItem = new VariableDefinition(getRandomPanhandleItem.ReturnType);
        var itemText = new VariableDefinition(module.TypeSystem.String);
        var country = new VariableDefinition(getCountry.ReturnType);
        var amount = new VariableDefinition(module.TypeSystem.Single);
        var attack = new VariableDefinition(attackType);
        var lawsuit = new VariableDefinition(lawsuitType);
        foreach (var local in new[]
                 {
                     instrumentName, songName, instrumentData, song, physical, knowledge, skill, score,
                     generosityPassed, happinessPassed, crazinessPassed, crimePassed, performancePassed,
                     randomValue, enumerator, disposable, currentPhysical, title, resultBody, logText,
                     chainedScenario, complimentOrReaction, insult, panhandleItem, itemText, country,
                     amount, attack, lawsuit
                 })
            body.Variables.Add(local);
        var il = body.Instructions;
        var dropDownStrings = method.Parameters.Single(parameter => parameter.Name == "dropDownStrings");

        void Int(int value) => il.Add(Instruction.Create(OpCodes.Ldc_I4, value));
        void LoadLife()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        }
        void LoadHero()
        {
            LoadLife();
            il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        }
        void LoadStreetPerson()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, streetPersonField));
        }
        void LoadTargetPerson()
        {
            LoadStreetPerson();
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
        }
        void LoadDescription()
        {
            LoadStreetPerson();
            il.Add(Instruction.Create(OpCodes.Ldfld, descriptionField));
        }
        void LoadHustler()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, hustlerField));
        }
        void LoadNarrative()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        }
        void Random(int minimum, int maximum)
        {
            Int(minimum);
            Int(maximum);
            il.Add(Instruction.Create(OpCodes.Call, random));
        }
        void StringArray(params Action[] values)
        {
            Int(values.Length);
            il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.String));
            for (var index = 0; index < values.Length; index++)
            {
                il.Add(Instruction.Create(OpCodes.Dup));
                Int(index);
                values[index]();
                il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            }
        }
        void Concat(params Action[] values)
        {
            StringArray(values);
            il.Add(Instruction.Create(OpCodes.Call, concatArray));
        }
        void TargetPronoun()
        {
            var female = Instruction.Create(OpCodes.Nop);
            var done = Instruction.Create(OpCodes.Nop);
            LoadTargetPerson();
            il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
            il.Add(Instruction.Create(OpCodes.Brtrue, female));
            il.Add(Instruction.Create(OpCodes.Ldstr, "He"));
            il.Add(Instruction.Create(OpCodes.Br, done));
            il.Add(female);
            il.Add(Instruction.Create(OpCodes.Ldstr, "She"));
            il.Add(done);
        }
        void ImpactHappiness(int value)
        {
            LoadHero();
            Int(value);
            il.Add(Instruction.Create(OpCodes.Callvirt, impactHappiness));
        }
        void Title(params string[] choices)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            foreach (var choice in choices)
            {
                il.Add(Instruction.Create(OpCodes.Dup));
                il.Add(Instruction.Create(OpCodes.Ldstr, choice));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Pop));
            }
            il.Add(Instruction.Create(OpCodes.Call, pickTitle));
            il.Add(Instruction.Create(OpCodes.Stloc, title));
        }
        void ChainScenario(Action bodyText)
        {
            bodyText();
            il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
            il.Add(Instruction.Create(OpCodes.Ldloc, title));
            il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
            il.Add(Instruction.Create(OpCodes.Ldnull));
            il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, chainedScenario));
            il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
            il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        }
        void AppendNarrative(Action text, bool onlyWhenNotEmpty = false)
        {
            text();
            il.Add(Instruction.Create(OpCodes.Stloc, logText));
            var done = Instruction.Create(OpCodes.Nop);
            if (onlyWhenNotEmpty)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, logText));
                il.Add(Instruction.Create(OpCodes.Call, isNullOrEmpty));
                il.Add(Instruction.Create(OpCodes.Brtrue, done));
            }
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            LoadNarrative();
            il.Add(Instruction.Create(OpCodes.Ldstr, "\n"));
            il.Add(Instruction.Create(OpCodes.Ldloc, logText));
            il.Add(Instruction.Create(OpCodes.Call, concat3));
            il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
            il.Add(done);
        }
        void SetFlag(VariableDefinition flag, Action attribute, int firstMinimum, int firstMaximum,
            bool attributePassesWhenLessOrEqual, int scoreMinimum, int scoreMaximum, bool scorePassesWhenGreaterOrEqual)
        {
            var secondCheck = Instruction.Create(OpCodes.Nop);
            var passed = Instruction.Create(OpCodes.Nop);
            var done = Instruction.Create(OpCodes.Nop);
            attribute();
            Random(firstMinimum, firstMaximum);
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(attributePassesWhenLessOrEqual ? OpCodes.Bgt : OpCodes.Blt, secondCheck));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stloc, flag));
            il.Add(Instruction.Create(OpCodes.Br, done));
            il.Add(secondCheck);
            il.Add(Instruction.Create(OpCodes.Ldloc, score));
            Random(scoreMinimum, scoreMaximum);
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(scorePassesWhenGreaterOrEqual ? OpCodes.Bge : OpCodes.Ble, passed));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Stloc, flag));
            il.Add(Instruction.Create(OpCodes.Br, done));
            il.Add(passed);
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stloc, flag));
            il.Add(done);
        }

        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Callvirt, toLower));
        il.Add(Instruction.Create(OpCodes.Stloc, instrumentName));
        il.Add(Instruction.Create(OpCodes.Ldloc, instrumentName));
        il.Add(Instruction.Create(OpCodes.Call, getMusicInstrument));
        il.Add(Instruction.Create(OpCodes.Stloc, instrumentData));
        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, songName));
        il.Add(Instruction.Create(OpCodes.Ldloc, songName));
        il.Add(Instruction.Create(OpCodes.Call, getSerenadeSong));
        il.Add(Instruction.Create(OpCodes.Stloc, song));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, physical));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0f));
        il.Add(Instruction.Create(OpCodes.Stloc, skill));

        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getPhysicalInstruments));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var matchingInstrument = Instruction.Create(OpCodes.Nop);
        var enumerationDone = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var disposalEnd = Instruction.Create(OpCodes.Nop);
        var noDisposable = Instruction.Create(OpCodes.Nop);
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumeratorCurrent));
        il.Add(Instruction.Create(OpCodes.Castclass, physicalType));
        il.Add(Instruction.Create(OpCodes.Stloc, currentPhysical));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentPhysical));
        il.Add(Instruction.Create(OpCodes.Call, getPhysicalData));
        il.Add(Instruction.Create(OpCodes.Ldloc, instrumentData));
        il.Add(Instruction.Create(OpCodes.Beq, matchingInstrument));
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(matchingInstrument);
        il.Add(Instruction.Create(OpCodes.Ldloc, currentPhysical));
        il.Add(Instruction.Create(OpCodes.Stloc, physical));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentPhysical));
        il.Add(Instruction.Create(OpCodes.Call, getKnowledge));
        il.Add(Instruction.Create(OpCodes.Stloc, knowledge));
        il.Add(Instruction.Create(OpCodes.Ldloc, knowledge));
        il.Add(Instruction.Create(OpCodes.Ldfld, skillField));
        il.Add(Instruction.Create(OpCodes.Stloc, skill));
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, enumeratorMoveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, enumerationDone));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Isinst, disposableInterface));
        il.Add(Instruction.Create(OpCodes.Stloc, disposable));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Brfalse, noDisposable));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Callvirt, dispose));
        il.Add(noDisposable);
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(disposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = tryStart,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = disposalEnd
        });
        il.Add(enumerationDone);

        il.Add(Instruction.Create(OpCodes.Ldloc, skill));
        Random(1, 5);
        il.Add(Instruction.Create(OpCodes.Ldloc, song));
        il.Add(Instruction.Create(OpCodes.Ldfld, reactionScoreField));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Ldloc, physical));
        il.Add(Instruction.Create(OpCodes.Ldfld, conditionField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 100f));
        il.Add(Instruction.Create(OpCodes.Div));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Stloc, score));

        SetFlag(generosityPassed,
            () => { LoadTargetPerson(); il.Add(Instruction.Create(OpCodes.Ldfld, generosityField)); },
            0, 101, true, 40, 101, false);
        SetFlag(happinessPassed,
            () => { LoadTargetPerson(); il.Add(Instruction.Create(OpCodes.Ldfld, happinessField)); },
            0, 101, true, 60, 101, false);
        SetFlag(crazinessPassed,
            () => { LoadTargetPerson(); il.Add(Instruction.Create(OpCodes.Ldfld, crazinessField)); },
            0, 101, true, 20, 101, true);

        // The native implementation draws this random number before combining the crime tests.
        Random(75, 101);
        il.Add(Instruction.Create(OpCodes.Stloc, randomValue));
        var crimeFlagPassed = Instruction.Create(OpCodes.Nop);
        var crimeFlagDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, score));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 75f));
        il.Add(Instruction.Create(OpCodes.Bge_Un, crimeFlagPassed));
        LoadHustler();
        il.Add(Instruction.Create(OpCodes.Ldfld, streetField));
        il.Add(Instruction.Create(OpCodes.Ldfld, crimeField));
        il.Add(Instruction.Create(OpCodes.Ldloc, randomValue));
        il.Add(Instruction.Create(OpCodes.Ble, crimeFlagPassed));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, crimePassed));
        il.Add(Instruction.Create(OpCodes.Br, crimeFlagDone));
        il.Add(crimeFlagPassed);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, crimePassed));
        il.Add(crimeFlagDone);

        var performanceDisciplineCheck = Instruction.Create(OpCodes.Nop);
        var performanceScoreCheck = Instruction.Create(OpCodes.Nop);
        var performanceFlagDone = Instruction.Create(OpCodes.Nop);
        LoadTargetPerson();
        il.Add(Instruction.Create(OpCodes.Ldfld, crazinessField));
        Random(0, 101);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Bgt, performanceDisciplineCheck));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, performancePassed));
        il.Add(Instruction.Create(OpCodes.Br, performanceFlagDone));
        il.Add(performanceDisciplineCheck);
        LoadTargetPerson();
        il.Add(Instruction.Create(OpCodes.Ldfld, disciplineField));
        Random(0, 101);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Blt, performanceScoreCheck));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, performancePassed));
        il.Add(Instruction.Create(OpCodes.Br, performanceFlagDone));
        il.Add(performanceScoreCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, score));
        Random(10, 101);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        var performanceScorePassed = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Bge, performanceScorePassed));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, performancePassed));
        il.Add(Instruction.Create(OpCodes.Br, performanceFlagDone));
        il.Add(performanceScorePassed);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, performancePassed));
        il.Add(performanceFlagDone);

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "I played '")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, songName)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "' for ")),
            () =>
            {
                LoadDescription();
                il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
                il.Add(Instruction.Create(OpCodes.Call, indefiniteArticle));
            },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " on my ")),
            () =>
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, physical));
                il.Add(Instruction.Create(OpCodes.Call, getPhysicalData));
                il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, getPhysicalData.ReturnType, "<Name>k__BackingField")));
            },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, ".")));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));

        var generosityBranch = Instruction.Create(OpCodes.Nop);
        var happinessBranch = Instruction.Create(OpCodes.Nop);
        var crazinessBranch = Instruction.Create(OpCodes.Nop);
        var performanceBranch = Instruction.Create(OpCodes.Nop);
        var crimeBranch = Instruction.Create(OpCodes.Nop);
        var cleanup = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, generosityPassed));
        il.Add(Instruction.Create(OpCodes.Brfalse, generosityBranch));
        il.Add(Instruction.Create(OpCodes.Ldloc, happinessPassed));
        il.Add(Instruction.Create(OpCodes.Brfalse, happinessBranch));
        il.Add(Instruction.Create(OpCodes.Ldloc, crazinessPassed));
        il.Add(Instruction.Create(OpCodes.Brfalse, crazinessBranch));
        il.Add(Instruction.Create(OpCodes.Ldloc, performancePassed));
        il.Add(Instruction.Create(OpCodes.Brfalse, performanceBranch));
        il.Add(Instruction.Create(OpCodes.Ldloc, crimePassed));
        il.Add(Instruction.Create(OpCodes.Brfalse, crimeBranch));

        ImpactHappiness(-1);
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Stloc, complimentOrReaction));
        Random(0, 3);
        il.Add(Instruction.Create(OpCodes.Stloc, randomValue));
        il.Add(Instruction.Create(OpCodes.Ldloc, randomValue));
        var reactionOne = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Beq, reactionOne));
        il.Add(Instruction.Create(OpCodes.Ldloc, randomValue));
        var reactionTwo = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Beq, reactionTwo));
        il.Add(Instruction.Create(OpCodes.Ldloc, randomValue));
        var reactionSelected = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brtrue, reactionSelected));
        il.Add(Instruction.Create(OpCodes.Ldstr, "ignored you"));
        il.Add(Instruction.Create(OpCodes.Stloc, complimentOrReaction));
        il.Add(Instruction.Create(OpCodes.Br, reactionSelected));
        il.Add(reactionOne);
        il.Add(Instruction.Create(OpCodes.Ldstr, "said it was alright"));
        il.Add(Instruction.Create(OpCodes.Stloc, complimentOrReaction));
        il.Add(Instruction.Create(OpCodes.Br, reactionSelected));
        il.Add(reactionTwo);
        il.Add(Instruction.Create(OpCodes.Ldstr, "smiled awkwardly and walked away"));
        il.Add(Instruction.Create(OpCodes.Stloc, complimentOrReaction));
        il.Add(reactionSelected);
        Title("It's not you, it's the song", "That bad, huh?", "Ouch", "Don't hold back");
        ChainScenario(() => Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")),
            LoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, complimentOrReaction)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "."))));
        AppendNarrative(() => Concat(
            TargetPronoun,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " didn't enjoy the song."))));
        il.Add(Instruction.Create(OpCodes.Br, cleanup));

        il.Add(generosityBranch);
        ImpactHappiness(2);
        LoadHustler();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, impactStreetSmarts));
        Random(0, 3);
        var moneyGift = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brtrue, moneyGift));

        il.Add(Instruction.Create(OpCodes.Call, getRandomPanhandleItem));
        il.Add(Instruction.Create(OpCodes.Stloc, panhandleItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, panhandleItem));
        il.Add(Instruction.Create(OpCodes.Ldfld, indefiniteArticleField));
        var plainItem = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brfalse, plainItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, panhandleItem));
        il.Add(Instruction.Create(OpCodes.Ldfld, pluralField));
        var singularItem = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brfalse, singularItem));
        il.Add(Instruction.Create(OpCodes.Ldstr, "some "));
        il.Add(Instruction.Create(OpCodes.Ldloc, panhandleItem));
        il.Add(Instruction.Create(OpCodes.Ldfld, itemField));
        il.Add(Instruction.Create(OpCodes.Call, concat2));
        il.Add(Instruction.Create(OpCodes.Stloc, itemText));
        var itemTextReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, itemTextReady));
        il.Add(singularItem);
        il.Add(Instruction.Create(OpCodes.Ldloc, panhandleItem));
        il.Add(Instruction.Create(OpCodes.Ldfld, itemField));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Call, indefiniteArticle));
        il.Add(Instruction.Create(OpCodes.Ldstr, "an used"));
        il.Add(Instruction.Create(OpCodes.Ldstr, "a used"));
        il.Add(Instruction.Create(OpCodes.Callvirt, replace));
        il.Add(Instruction.Create(OpCodes.Stloc, itemText));
        il.Add(Instruction.Create(OpCodes.Br, itemTextReady));
        il.Add(plainItem);
        il.Add(Instruction.Create(OpCodes.Ldloc, panhandleItem));
        il.Add(Instruction.Create(OpCodes.Ldfld, itemField));
        il.Add(Instruction.Create(OpCodes.Stloc, itemText));
        il.Add(itemTextReady);
        Title("Thank youuuu!", "Kind gesture", "Great audience", "Encore?");
        ChainScenario(() => Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")),
            LoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " gave you ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, itemText)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!"))));
        AppendNarrative(() => Concat(
            TargetPronoun,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " gave me ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, itemText)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!"))), true);
        var generosityDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, generosityDone));

        il.Add(moneyGift);
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Stloc, country));
        var defaultMoneyScore = Instruction.Create(OpCodes.Nop);
        LoadStreetPerson();
        il.Add(Instruction.Create(OpCodes.Call, getStreetPersonData));
        il.Add(Instruction.Create(OpCodes.Brfalse, defaultMoneyScore));
        il.Add(Instruction.Create(OpCodes.Ldloc, country));
        LoadStreetPerson();
        il.Add(Instruction.Create(OpCodes.Call, getStreetPersonData));
        il.Add(Instruction.Create(OpCodes.Ldfld, moneyScoreField));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        var moneyScoreReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, moneyScoreReady));
        il.Add(defaultMoneyScore);
        il.Add(Instruction.Create(OpCodes.Ldloc, country));
        Random(1, 20);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(moneyScoreReady);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, localizeCost));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Stloc, amount));
        Title("Thank youuuu!", "Kind gesture", "Great audience", "Encore?", "Tip jar = Happy");
        ChainScenario(() =>
        {
            TargetPronoun();
            il.Add(Instruction.Create(OpCodes.Ldstr, " gave you "));
            il.Add(Instruction.Create(OpCodes.Ldloc, amount));
            il.Add(Instruction.Create(OpCodes.Conv_R8));
            il.Add(Instruction.Create(OpCodes.Call, currencyString));
            il.Add(Instruction.Create(OpCodes.Ldstr, "."));
            il.Add(Instruction.Create(OpCodes.Call, concat4));
        });
        AppendNarrative(() =>
        {
            TargetPronoun();
            il.Add(Instruction.Create(OpCodes.Ldstr, " gave me "));
            il.Add(Instruction.Create(OpCodes.Ldloc, amount));
            il.Add(Instruction.Create(OpCodes.Conv_R8));
            il.Add(Instruction.Create(OpCodes.Call, currencyString));
            il.Add(Instruction.Create(OpCodes.Ldstr, "."));
            il.Add(Instruction.Create(OpCodes.Call, concat4));
        }, true);
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Ldfld, financesField));
        il.Add(Instruction.Create(OpCodes.Ldloc, amount));
        il.Add(Instruction.Create(OpCodes.Conv_R8));
        il.Add(Instruction.Create(OpCodes.Call, adjustBalance));
        il.Add(generosityDone);
        il.Add(Instruction.Create(OpCodes.Br, cleanup));

        il.Add(happinessBranch);
        il.Add(Instruction.Create(OpCodes.Ldloc, physical));
        il.Add(Instruction.Create(OpCodes.Call, getKnowledge));
        il.Add(Instruction.Create(OpCodes.Stloc, knowledge));
        il.Add(Instruction.Create(OpCodes.Ldloc, knowledge));
        Random(0, 2);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ceq));
        il.Add(Instruction.Create(OpCodes.Call, impactSkill));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
        il.Add(Instruction.Create(OpCodes.Call, getRandomCompliment));
        il.Add(Instruction.Create(OpCodes.Stloc, complimentOrReaction));
        Title("Thank you, thank you", "I'm blushing, stop", "Music to my ears", "Kidnapped tunes");
        ChainScenario(() => Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")),
            LoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " called you ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, complimentOrReaction)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!"))));
        AppendNarrative(() => Concat(
            TargetPronoun,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " called me ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, complimentOrReaction)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!"))));
        il.Add(Instruction.Create(OpCodes.Br, cleanup));

        il.Add(crazinessBranch);
        ImpactHappiness(-1);
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
        il.Add(Instruction.Create(OpCodes.Call, getRandomInsult));
        il.Add(Instruction.Create(OpCodes.Stloc, insult));
        Title("You trying to scrap?!", "Keep it moving, jabroni", "You can keep walking", "That bad, huh?");
        ChainScenario(() => Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")),
            LoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " called you ")),
            () =>
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, insult));
                il.Add(Instruction.Create(OpCodes.Ldfld, insultField));
            },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!"))));
        AppendNarrative(() => Concat(
            TargetPronoun,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " called me ")),
            () =>
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, insult));
                il.Add(Instruction.Create(OpCodes.Ldfld, insultField));
            },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!"))));
        il.Add(Instruction.Create(OpCodes.Br, cleanup));

        il.Add(performanceBranch);
        var noAttack = Instruction.Create(OpCodes.Nop);
        LoadStreetPerson();
        il.Add(Instruction.Create(OpCodes.Brfalse, noAttack));
        LoadTargetPerson();
        LoadTargetPerson();
        il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
        il.Add(Instruction.Create(OpCodes.Ldstr, "The "));
        LoadDescription();
        il.Add(Instruction.Create(OpCodes.Call, concat2));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Newobj, attackConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, attack));
        il.Add(Instruction.Create(OpCodes.Ldloc, attack));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, attack));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));

        il.Add(Instruction.Create(OpCodes.Ldstr, "the {0}"));
        LoadDescription();
        il.Add(Instruction.Create(OpCodes.Call, formatObject));
        il.Add(Instruction.Create(OpCodes.Stloc, itemText));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Stloc, country));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, itemText));
        il.Add(Instruction.Create(OpCodes.Ldloc, country));
        LoadStreetPerson();
        il.Add(Instruction.Create(OpCodes.Call, getDamages));
        il.Add(Instruction.Create(OpCodes.Ldstr, "assaulting <you/me>"));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.Object));
        il.Add(Instruction.Create(OpCodes.Call, formatArray));
        il.Add(Instruction.Create(OpCodes.Newobj, lawsuitConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, lawsuit));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getLawsuitTargets));
        il.Add(Instruction.Create(OpCodes.Ldloc, lawsuit));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(noAttack);
        il.Add(Instruction.Create(OpCodes.Br, cleanup));

        il.Add(crimeBranch);
        ImpactHappiness(-3);
        LoadHustler();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_M1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, impactStreetSmarts));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getPhysicalInstruments));
        il.Add(Instruction.Create(OpCodes.Ldloc, physical));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListRemove));
        il.Add(Instruction.Create(OpCodes.Ldloc, physical));
        il.Add(Instruction.Create(OpCodes.Call, endBuskingHustle));
        Title("WTF!", "THIEF", "Kidnapped tunes", "Get back here!", "Unbelievable");
        ChainScenario(() => Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "The ")),
            LoadDescription,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " stole your ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, instrumentName)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " and ran away!"))));
        AppendNarrative(() => Concat(
            TargetPronoun,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " ran off with my ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, instrumentName)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "!"))));
        il.Add(Instruction.Create(OpCodes.Br, cleanup));

        il.Add(cleanup);
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeLogField));
        LoadNarrative();
        il.Add(Instruction.Create(OpCodes.Call, log));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeInterfaceField));
        il.Add(Instruction.Create(OpCodes.Call, refreshUI));
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
        return new TypeReference(namespaceName, name, module, module.TypeSystem.Object.Scope);
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName,
        int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = FrameworkType(module, typeName.Substring(0, separator), typeName.Substring(separator + 1));
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
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
