using Mono.Cecil;
using Mono.Cecil.Cil;

public static class ParanormalMethodRepairs
{
    public static void FixParanormal(MethodDefinition method)
    {
        Validate(method, "ScenarioParanormalEncounter");
        Fix(method, false);
    }

    public static void FixGhost(MethodDefinition method)
    {
        Validate(method, "ScenarioGhostEncounter");
        Fix(method, true);
    }

    private static void Validate(MethodDefinition method, string declaringType)
    {
        if (method.Name != "ChoiceSelectedAtIndex" ||
            method.DeclaringType.FullName != declaringType ||
            method.ReturnType.FullName != "System.Void" ||
            method.Parameters.Count != 1 ||
            method.Parameters[0].ParameterType.FullName != "System.Int32")
            throw new InvalidOperationException($"Unexpected paranormal encounter signature: {method.FullName}");
    }

    private static void Fix(MethodDefinition method, bool ghost)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var choiceType = FindType(module, "ScenarioChoice");
        var ghostType = FindType(module, "SimGhost");
        var encounterType = FindType(module, "DataParanormalEncounter");
        var gameManagerType = FindType(module, "GameManager");
        var lifeType = FindType(module, "Life");
        var personType = FindType(module, "SimPerson");
        var heroType = FindType(module, "SimHero");
        var loverType = FindType(module, "SimLover");
        var childType = FindType(module, "SimChild");
        var scriptType = FindType(module, "SimThirdPersonScript");
        var deathType = FindType(module, "SimDeath");
        var dataHubType = FindType(module, "DataHub");
        var gameReferenceType = FindType(module, "GameReference");
        var diseaseType = FindType(module, "DataDiseaseType");
        var diseaseInstanceType = FindType(module, "SimDisease");
        var trackerType = FindType(module, "Apollo16ChallengeTracker");
        var holidayManagerType = FindType(module, "HolidayCollectionManager");
        var collectibleType = FindType(module, "HolidayCollectibleMetadata");
        var achievementType = FindType(module, "AchievementManager");
        var lifeLogType = FindType(module, "LifeLog");
        var lifeInterfaceType = FindType(module, "LifeInterface");
        var fatalHeartAttackType = FindType(module, "ScenarioFatalHeartAttack");
        var diseaseContractType = FindType(module, "ScenarioDiseaseContract");
        var holidayFoundType = FindType(module, "ScenarioHolidayCollectionItemFound");
        var attackType = FindType(module, "ScenarioGhostAttackInbound");
        var relativeDeathType = FindType(module, "ScenarioDeathRelative");
        var obituaryType = FindType(module, "ObituaryGenerator");

        var encounterField = Field(module, owner, ghost ? "_ghost" : "_encounter");
        var didSummonField = ghost ? Field(module, owner, "_didSummon") : null;
        var narrativeField = ghost ? null : Field(module, owner, "_narrative");
        var heroTerrorField = ghost ? null : Field(module, owner, "_heroTerror");
        var ghostFriendlinessField = ghost ? null : Field(module, owner, "_ghostFriendliness");
        var choiceArrayField = Field(module, scenarioType, "ChoiceArray");
        var nextScenarioField = Field(module, scenarioType, "<NextScenario>k__BackingField");
        var ghostNameField = Field(module, ghostType, "<Name>k__BackingField");
        var ghostGenderField = Field(module, ghostType, "<Gender>k__BackingField");
        var ghostFriendlinessAttributeField = Field(module, ghostType, "<Att_friendliness>k__BackingField");
        var entityField = Field(module, encounterType, "<Entity>k__BackingField");
        var encounterGenderField = Field(module, encounterType, "<Gender>k__BackingField");
        var dangerScoreField = Field(module, encounterType, "<DangerScore>k__BackingField");
        var healthField = Field(module, personType, "<Att_health>k__BackingField");
        var wilinessField = Field(module, personType, "<Att_wiliness>k__BackingField");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var aliveField = Field(module, personType, "<Alive>k__BackingField");
        var deathField = Field(module, scriptType, "<Death>k__BackingField");
        var ageAtDeathField = Field(module, deathType, "<AgeAtDeath>k__BackingField");
        var diseaseNameField = Field(module, diseaseType, "<Name>k__BackingField");
        var collectibleNameField = Field(module, collectibleType, "<Name>k__BackingField");
        var collectibleEmojiField = Field(module, collectibleType, "<EmojiString>k__BackingField");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var lifeInterfaceField = Field(module, lifeType, "LifeInterface");
        var sharedHolidayManagerField = Field(module, holidayManagerType, "SharedHolidayCollectionManager");
        var sharedAchievementField = Field(module, achievementType, "sharedAchievement");

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true, "System.Object");
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true, "System.Int32");
        var arrayListGetCount = FrameworkMethod(module, "System.Collections.ArrayList", "get_Count", 0, true);
        var arrayListGetEnumerator = FrameworkMethod(module, "System.Collections.ArrayList", "GetEnumerator", 0, true);
        var enumeratorType = FrameworkType(module, "System.Collections", "IEnumerator");
        var disposableType = FrameworkType(module, "System", "IDisposable");
        var getEnumeratorCurrent = FrameworkMethod(module, "System.Collections.IEnumerator", "get_Current", 0, true);
        var enumeratorMoveNext = FrameworkMethod(module, "System.Collections.IEnumerator", "MoveNext", 0, true);
        var dispose = FrameworkMethod(module, "System.IDisposable", "Dispose", 0, true);
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var contains = FrameworkMethod(module, "System.String", "Contains", 1, true, "System.String");
        var getLength = FrameworkMethod(module, "System.String", "get_Length", 0, true);
        var equals = FrameworkMethod(module, "System.String", "Equals", 1, true, "System.String");
        var replace = FrameworkMethod(module, "System.String", "Replace", 2, true, "System.String", "System.String");
        var concat2 = FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String");
        var format1 = FrameworkMethod(module, "System.String", "Format", 2, false, "System.String", "System.Object");
        var format2 = FrameworkMethod(module, "System.String", "Format", 3, false,
            "System.String", "System.Object", "System.Object");

        var getTitle = ImportMethod(module, choiceType, "get_Title", 0, true);
        var getHero = ImportMethod(module, lifeType, "get_Hero", 0, true);
        var getLover = ImportMethod(module, lifeType, "get_Lover", 0, true);
        var getChildren = ImportMethod(module, lifeType, "get_ChildArray", 0, true);
        var getLifeLog = ImportMethod(module, lifeType, "get_LifeLog", 0, true);
        var getChallengeTracker = ImportMethod(module, lifeType, "get_ChallengeTracker", 0, true);
        var getPeopleInsulted = ImportMethod(module, trackerType, "get_ListPeopleNamesInsulted", 0, true);
        var peopleInsultedType = (GenericInstanceType)getPeopleInsulted.ReturnType;
        var addInsultedName = ImportMethod(module, peopleInsultedType, "Add", 1, true);
        var getNumInsults = ImportMethod(module, lifeType, "get_NumTimesInsultedSomeone", 0, true);
        var setNumInsults = ImportMethod(module, lifeType, "set_NumTimesInsultedSomeone", 1, true, "System.Int32");
        var getScript = ImportMethod(module, personType, "get_Script", 0, true);
        var getDeath = ImportMethod(module, scriptType, "get_Death", 0, true);
        var getAge = ImportMethod(module, personType, "get_Age", 0, true);
        var descriptiveGhostName = ImportMethod(module, ghostType, "DescriptiveGhostName", 0, true);
        var impactFriendliness = ImportMethod(module, ghostType, "ImpactFriendliness", 1, true, "System.Int32");
        var impactDiscipline = ImportMethod(module, personType, "ImpactDiscipline", 1, true, "System.Int32");
        var impactIntelligence = ImportMethod(module, personType, "ImpactIntelligence", 1, true, "System.Int32");
        var impactHappiness = ImportMethod(module, personType, "ImpactHappiness", 1, true, "System.Int32");
        var impactHealth = ImportMethod(module, personType, "ImpactHealth", 1, true, "System.Int32");
        var isVampire = ImportMethod(module, personType, "IsVampire", 0, true);
        var die = ImportMethod(module, personType, "Die", 0, true);
        var updateDeathType = ImportMethod(module, deathType, "UpdateDeathType", 1, true, "System.Int32");
        var isLivingWithHero = ImportMethod(module, childType, "IsLivingWithHero", 0, true);
        var addLoverToExes = ImportMethod(module, heroType, "AddCurrentLoverToExesAndCanBecomeStalkerAndDidDie",
            2, true, "System.Boolean", "System.Boolean");
        var getRandomInsult = ImportMethod(module, dataHubType, "GetRandomInsultForGender", 2, false,
            "System.Int32", "System.Boolean");
        var getRandomCompliment = ImportMethod(module, dataHubType, "GetRandomComplimentForGender", 1, false,
            "System.Int32");
        var getRandomDisease = ImportMethod(module, dataHubType, "GetRandomSawAGhostDiseaseType", 0, false);
        var currentlySuffering = ImportMethod(module, heroType, "CurrentlySufferingFromDiseaseType", 1, true,
            "DataDiseaseType");
        var contractDisease = ImportMethod(module, heroType, "ContractDiseaseTypeAndShouldBeDiagnosedImmediately",
            2, true, "DataDiseaseType", "System.Boolean");
        var getDiseaseType = ImportMethod(module, diseaseInstanceType, "get_DiseaseType", 0, true);
        var random = ImportMethod(module, gameReferenceType, "GetRandomNumber", 2, false,
            "System.Int32", "System.Int32");
        var pickTitle = ImportMethod(module, gameReferenceType, "PickRandomFromStringArray", 1, false,
            "System.Collections.ArrayList");
        var capitalize = ImportMethod(module, gameReferenceType, "CapitalizeFirstLetterOfSentenceString", 1, false,
            "System.String");
        var isScavengerHuntAvailable = ImportMethod(module, holidayManagerType, "IsScavengerHuntAvailable", 0, true);
        var isHalloween2024 = ImportMethod(module, holidayManagerType, "IsHalloween2024", 0, true);
        var hasUserFoundCollectible = ImportMethod(module, holidayManagerType, "HasUserFoundCollectible", 1, true,
            "System.String");
        var completeAchievement = ImportMethod(module, achievementType, "CompleteAchievement", 1, true,
            "System.String");
        var log = ImportMethod(module, lifeLogType, "Log", 1, true, "System.String");
        var refreshUI = ImportMethod(module, lifeInterfaceType, "RefreshUI", 0, true);
        var addObituary = ImportMethod(module, obituaryType, "AddColorString", 1, false, "System.String");
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true,
            "System.String", "System.String", "Scenario");
        var fatalHeartAttackConstructor = ImportMethod(module, fatalHeartAttackType, ".ctor", 0, true);
        var diseaseContractConstructor = ImportMethod(module, diseaseContractType, ".ctor", 1, true, "SimDisease");
        var collectibleConstructor = ImportMethod(module, collectibleType, ".ctor", 3, true,
            "System.String", "System.String", "HOLIDAY_COLLECTIBLE_TYPE");
        var holidayFoundConstructor = ImportMethod(module, holidayFoundType, ".ctor", 2, true,
            "HolidayCollectibleMetadata", "System.Boolean");
        var attackConstructor = ImportMethod(module, attackType, ".ctor", 3, true,
            "System.String", "System.Int32", "System.Boolean");
        var loverDeathConstructor = ImportMethod(module, relativeDeathType, ".ctor", 1, true, "SimLover");
        var childDeathConstructor = ImportMethod(module, relativeDeathType, ".ctor", 1, true, "SimChild");

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var choiceText = new VariableDefinition(module.TypeSystem.String);
        var title = new VariableDefinition(module.TypeSystem.String);
        var resultBody = new VariableDefinition(module.TypeSystem.String);
        var narrative = new VariableDefinition(module.TypeSystem.String);
        var randomText = new VariableDefinition(module.TypeSystem.String);
        var titles = new VariableDefinition(arrayListType);
        var integerSink = new VariableDefinition(module.TypeSystem.Int32);
        var randomValue = new VariableDefinition(module.TypeSystem.Int32);
        var attackScore = new VariableDefinition(module.TypeSystem.Int32);
        var category = new VariableDefinition(module.TypeSystem.Int32);
        var directAttack = new VariableDefinition(module.TypeSystem.Boolean);
        var heartAttack = new VariableDefinition(module.TypeSystem.Boolean);
        var chance = new VariableDefinition(module.TypeSystem.Single);
        var chainedScenario = new VariableDefinition(scenarioType);
        var collectible = new VariableDefinition(collectibleType);
        var selectedDiseaseType = new VariableDefinition(diseaseType);
        var disease = new VariableDefinition(diseaseInstanceType);
        var lover = new VariableDefinition(loverType);
        var children = new VariableDefinition(arrayListType);
        var childArray = new VariableDefinition(arrayListType);
        var child = new VariableDefinition(childType);
        var enumerator = new VariableDefinition(enumeratorType);
        var disposable = new VariableDefinition(disposableType);
        foreach (var local in new[]
                 {
                     choiceText, title, resultBody, narrative, randomText, titles, integerSink, randomValue,
                     attackScore, category, directAttack, heartAttack, chance, chainedScenario, collectible,
                     selectedDiseaseType, disease, lover, children, childArray, child, enumerator, disposable
                 })
            body.Variables.Add(local);

        var il = body.Instructions;
        void Int(int value) => il.Add(Instruction.Create(OpCodes.Ldc_I4, value));
        void LoadLife()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        }
        void LoadHero()
        {
            LoadLife();
            il.Add(Instruction.Create(OpCodes.Call, getHero));
        }
        void LoadEncounter()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, encounterField));
        }
        void LoadTargetGender()
        {
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghost ? ghostGenderField : encounterGenderField));
        }
        void Random(int minimum, int maximum)
        {
            Int(minimum);
            Int(maximum);
            il.Add(Instruction.Create(OpCodes.Call, random));
        }
        void Impact(MethodReference impact, int amount)
        {
            LoadHero();
            Int(amount);
            il.Add(Instruction.Create(OpCodes.Callvirt, impact));
        }
        void Title(params string[] choices)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, titles));
            foreach (var choice in choices)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, titles));
                il.Add(Instruction.Create(OpCodes.Ldstr, choice));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Stloc, integerSink));
            }
            il.Add(Instruction.Create(OpCodes.Ldloc, titles));
            il.Add(Instruction.Create(OpCodes.Call, pickTitle));
            il.Add(Instruction.Create(OpCodes.Stloc, title));
        }
        void Format1(string format, Action argument)
        {
            il.Add(Instruction.Create(OpCodes.Ldstr, format));
            argument();
            il.Add(Instruction.Create(OpCodes.Call, format1));
        }
        void Format2(string format, Action first, Action second)
        {
            il.Add(Instruction.Create(OpCodes.Ldstr, format));
            first();
            second();
            il.Add(Instruction.Create(OpCodes.Call, format2));
        }
        void ChainConstructedScenario()
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
        void SetNarrativeWithText(string format)
        {
            Format2(format,
                () => il.Add(Instruction.Create(OpCodes.Ldloc, narrative)),
                () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)));
            il.Add(Instruction.Create(OpCodes.Stloc, narrative));
        }
        void SetSimpleOutcome(string bodyFormat, string narrativeFormat)
        {
            Format1(bodyFormat, () => il.Add(Instruction.Create(OpCodes.Ldloc, narrative)));
            il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
            Format1(narrativeFormat, () => il.Add(Instruction.Create(OpCodes.Ldloc, narrative)));
            il.Add(Instruction.Create(OpCodes.Stloc, narrative));
        }

        // Native ghost 0x38cf034 and paranormal 0x38d3600: resolve the selected title.
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choiceArrayField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, choiceType));
        il.Add(Instruction.Create(OpCodes.Callvirt, getTitle));
        il.Add(Instruction.Create(OpCodes.Callvirt, toLower));
        il.Add(Instruction.Create(OpCodes.Stloc, choiceText));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Stloc, title));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        Int(0);
        il.Add(Instruction.Create(OpCodes.Stloc, category));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, heartAttack));

        if (ghost)
        {
            Format1("{0} appeared!", () =>
            {
                LoadEncounter();
                il.Add(Instruction.Create(OpCodes.Call, descriptiveGhostName));
                il.Add(Instruction.Create(OpCodes.Call, capitalize));
            });
        }
        else
        {
            Format1("{0}", () =>
            {
                il.Add(Instruction.Create(OpCodes.Ldarg_0));
                il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
            });
        }
        il.Add(Instruction.Create(OpCodes.Stloc, narrative));

        var dispatchDone = Instruction.Create(OpCodes.Nop);
        void ChoiceBranch(string key, Instruction next, Action emit)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, choiceText));
            il.Add(Instruction.Create(OpCodes.Ldstr, key));
            il.Add(Instruction.Create(OpCodes.Callvirt, contains));
            il.Add(Instruction.Create(OpCodes.Brfalse, next));
            emit();
            il.Add(Instruction.Create(OpCodes.Br, dispatchDone));
            il.Add(next);
        }

        var checkCompliment = Instruction.Create(OpCodes.Nop);
        ChoiceBranch("insult", checkCompliment, () =>
        {
            Impact(impactDiscipline, -3);
            Impact(impactIntelligence, -3);
            Impact(impactHappiness, -4);
            if (ghost)
            {
                LoadEncounter();
                Int(-4);
                il.Add(Instruction.Create(OpCodes.Call, impactFriendliness));
            }
            LoadTargetGender();
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Call, getRandomInsult));
            il.Add(Instruction.Create(OpCodes.Stloc, randomText));
            Title("In one ear & out the other", "I can see right through you", "Boo'd away",
                "Reamin' the demon", "Soul shaming");
            if (ghost)
            {
                Format2("You called {0} {1}.", () =>
                {
                    LoadEncounter();
                    il.Add(Instruction.Create(OpCodes.Call, descriptiveGhostName));
                }, () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)));
            }
            else
                Format1("You called it {0}.", () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)));
            il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
            SetNarrativeWithText("{0}\nI called it {1}.");
            LoadLife();
            LoadLife();
            il.Add(Instruction.Create(OpCodes.Call, getNumInsults));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Add));
            il.Add(Instruction.Create(OpCodes.Call, setNumInsults));
            LoadLife();
            il.Add(Instruction.Create(OpCodes.Call, getChallengeTracker));
            il.Add(Instruction.Create(OpCodes.Call, getPeopleInsulted));
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghost ? ghostNameField : entityField));
            il.Add(Instruction.Create(OpCodes.Callvirt, addInsultedName));
            if (!ghost)
                Format2("According to a friend, {0} claims to have once called a ghost {1}.",
                    () => il.Add(Instruction.Create(OpCodes.Ldstr, "<obit-he/she>")),
                    () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)));
            if (!ghost)
                il.Add(Instruction.Create(OpCodes.Call, addObituary));
            Int(5);
            il.Add(Instruction.Create(OpCodes.Stloc, category));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        });

        var checkRun = Instruction.Create(OpCodes.Nop);
        ChoiceBranch("compliment", checkRun, () =>
        {
            Impact(impactIntelligence, -3);
            Impact(impactHappiness, -4);
            if (ghost)
            {
                LoadEncounter();
                il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
                il.Add(Instruction.Create(OpCodes.Call, impactFriendliness));
            }
            LoadTargetGender();
            il.Add(Instruction.Create(OpCodes.Call, getRandomCompliment));
            il.Add(Instruction.Create(OpCodes.Stloc, randomText));
            Title("Raising its spirits", "Ghouls just wanna have fun", "Adulated apparition",
                "Glorified ghost", "Soothing the spooky");
            if (ghost)
            {
                Format2("You called {0} {1}.", () =>
                {
                    LoadEncounter();
                    il.Add(Instruction.Create(OpCodes.Call, descriptiveGhostName));
                }, () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)));
            }
            else
                Format1("You called it {0}.", () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)));
            il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
            SetNarrativeWithText("{0}\nI called it {1}.");

            var collectibleDone = Instruction.Create(OpCodes.Nop);
            il.Add(Instruction.Create(OpCodes.Ldsfld, sharedHolidayManagerField));
            il.Add(Instruction.Create(OpCodes.Callvirt, isScavengerHuntAvailable));
            il.Add(Instruction.Create(OpCodes.Brfalse, collectibleDone));
            il.Add(Instruction.Create(OpCodes.Ldsfld, sharedHolidayManagerField));
            il.Add(Instruction.Create(OpCodes.Callvirt, isHalloween2024));
            il.Add(Instruction.Create(OpCodes.Brfalse, collectibleDone));
            il.Add(Instruction.Create(OpCodes.Ldsfld, sharedHolidayManagerField));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Holey Sheet"));
            il.Add(Instruction.Create(OpCodes.Callvirt, hasUserFoundCollectible));
            il.Add(Instruction.Create(OpCodes.Brtrue, collectibleDone));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Holey Sheet"));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Compliment a spirit"));
            Int(5);
            il.Add(Instruction.Create(OpCodes.Newobj, collectibleConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, collectible));
            il.Add(Instruction.Create(OpCodes.Ldloc, collectible));
            il.Add(Instruction.Create(OpCodes.Ldstr, "halloween_"));
            il.Add(Instruction.Create(OpCodes.Ldloc, collectible));
            il.Add(Instruction.Create(OpCodes.Ldfld, collectibleNameField));
            il.Add(Instruction.Create(OpCodes.Ldstr, " "));
            il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
            il.Add(Instruction.Create(OpCodes.Callvirt, replace));
            il.Add(Instruction.Create(OpCodes.Call, concat2));
            il.Add(Instruction.Create(OpCodes.Stfld, collectibleEmojiField));
            il.Add(Instruction.Create(OpCodes.Ldloc, collectible));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Newobj, holidayFoundConstructor));
            ChainConstructedScenario();
            il.Add(collectibleDone);
            Int(1);
            il.Add(Instruction.Create(OpCodes.Stloc, category));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        });

        var checkAsk = Instruction.Create(OpCodes.Nop);
        ChoiceBranch("run", checkAsk, () =>
        {
            Impact(impactHappiness, -5);
            SetSimpleOutcome("{0}\nI ran for my life!", "{0}\nI ran for my life!");
            Int(2);
            il.Add(Instruction.Create(OpCodes.Stloc, category));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        });

        var checkScream = Instruction.Create(OpCodes.Nop);
        ChoiceBranch("ask", checkScream, () =>
        {
            Impact(impactDiscipline, 2);
            Impact(impactIntelligence, -3);
            Impact(impactHappiness, -4);
            Title("To the Boohamas with you", "Unwanted haunt", "Showed some backbone",
                "Off with the dead", "Push off to purgatory");
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, titles));
            foreach (var verb in new[] { "begged", "asked", "pleaded with" })
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, titles));
                il.Add(Instruction.Create(OpCodes.Ldstr, verb));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Stloc, integerSink));
            }
            il.Add(Instruction.Create(OpCodes.Ldloc, titles));
            il.Add(Instruction.Create(OpCodes.Call, pickTitle));
            il.Add(Instruction.Create(OpCodes.Stloc, randomText));
            if (ghost)
            {
                Format2("You {0} {1} to leave you alone.",
                    () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)),
                    () =>
                    {
                        LoadEncounter();
                        il.Add(Instruction.Create(OpCodes.Call, descriptiveGhostName));
                    });
            }
            else
                Format1("You {0} it to leave you alone.", () => il.Add(Instruction.Create(OpCodes.Ldloc, randomText)));
            il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
            SetNarrativeWithText("{0}\nI {1} it to leave me alone.");
            Int(2);
            il.Add(Instruction.Create(OpCodes.Stloc, category));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        });

        var checkPolice = Instruction.Create(OpCodes.Nop);
        ChoiceBranch("scream", checkPolice, () =>
        {
            Impact(impactHappiness, -5);
            SetSimpleOutcome("{0}\nI screamed!", "{0}\nI screamed!");
            Int(2);
            il.Add(Instruction.Create(OpCodes.Stloc, category));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        });

        var checkCalm = Instruction.Create(OpCodes.Nop);
        ChoiceBranch("police", checkCalm, () =>
        {
            Impact(impactHappiness, -4);
            Title("Very superstitious", "It's spooky szn", "Who ya gonna call?", "Calling a priest next");
            il.Add(Instruction.Create(OpCodes.Ldstr, "The police arrived and warned you for wasting their time."));
            il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
            Format1("{0}\nI called the police!", () => il.Add(Instruction.Create(OpCodes.Ldloc, narrative)));
            il.Add(Instruction.Create(OpCodes.Stloc, narrative));
            il.Add(Instruction.Create(OpCodes.Ldloc, title));
            il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
            ChainConstructedScenario();
            Int(4);
            il.Add(Instruction.Create(OpCodes.Stloc, category));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        });

        ChoiceBranch("calm", dispatchDone, () =>
        {
            Impact(impactHappiness, -3);
            Impact(impactDiscipline, 4);
            SetSimpleOutcome("{0}\nI tried to keep calm.", "{0}\nI tried to keep calm.");
            Int(0);
            il.Add(Instruction.Create(OpCodes.Stloc, category));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Stloc, directAttack));
        });
        il.Add(dispatchDone);

        var finalScenario = Instruction.Create(OpCodes.Nop);
        var chanceStart = Instruction.Create(OpCodes.Nop);
        if (ghost)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, didSummonField));
            il.Add(Instruction.Create(OpCodes.Brtrue, chanceStart));
        }

        var heartAttackDone = Instruction.Create(OpCodes.Nop);
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, healthField));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 1f));
        il.Add(Instruction.Create(OpCodes.Bgt, heartAttackDone));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        Int(30);
        il.Add(Instruction.Create(OpCodes.Blt, heartAttackDone));
        if (ghost)
        {
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessAttributeField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 20f));
            il.Add(Instruction.Create(OpCodes.Bge, heartAttackDone));
        }
        else
        {
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, dangerScoreField));
            Int(3);
            il.Add(Instruction.Create(OpCodes.Blt, heartAttackDone));
        }
        il.Add(Instruction.Create(OpCodes.Newobj, fatalHeartAttackConstructor));
        ChainConstructedScenario();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, heartAttack));
        il.Add(heartAttackDone);

        var healthCheckMiddle = Instruction.Create(OpCodes.Nop);
        var healthLowImpact = Instruction.Create(OpCodes.Nop);
        var healthDone = Instruction.Create(OpCodes.Nop);
        if (ghost)
        {
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessAttributeField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 10f));
            il.Add(Instruction.Create(OpCodes.Bge, healthCheckMiddle));
            Impact(impactHealth, -5);
            il.Add(Instruction.Create(OpCodes.Br, healthDone));
            il.Add(healthCheckMiddle);
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessAttributeField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 40f));
            il.Add(Instruction.Create(OpCodes.Bge, healthLowImpact));
            Impact(impactHealth, -3);
            il.Add(Instruction.Create(OpCodes.Br, healthDone));
        }
        else
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, heroTerrorField));
            Int(91);
            il.Add(Instruction.Create(OpCodes.Blt, healthCheckMiddle));
            Impact(impactHealth, -5);
            il.Add(Instruction.Create(OpCodes.Br, healthDone));
            il.Add(healthCheckMiddle);
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, heroTerrorField));
            Int(71);
            il.Add(Instruction.Create(OpCodes.Blt, healthLowImpact));
            Impact(impactHealth, -3);
            il.Add(Instruction.Create(OpCodes.Br, healthDone));
        }
        il.Add(healthLowImpact);
        Impact(impactHealth, -2);
        il.Add(healthDone);

        var diseaseDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, heartAttack));
        il.Add(Instruction.Create(OpCodes.Brtrue, diseaseDone));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 100f));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, healthField));
        il.Add(Instruction.Create(OpCodes.Sub));
        Random(0, 200);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ble, diseaseDone));
        if (ghost)
        {
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessAttributeField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 50f));
            il.Add(Instruction.Create(OpCodes.Bge, diseaseDone));
        }
        else
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 50f));
            il.Add(Instruction.Create(OpCodes.Bge, diseaseDone));
        }
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Call, isVampire));
        il.Add(Instruction.Create(OpCodes.Brtrue, diseaseDone));
        il.Add(Instruction.Create(OpCodes.Call, getRandomDisease));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedDiseaseType));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedDiseaseType));
        il.Add(Instruction.Create(OpCodes.Callvirt, currentlySuffering));
        il.Add(Instruction.Create(OpCodes.Brtrue, diseaseDone));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedDiseaseType));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, contractDisease));
        il.Add(Instruction.Create(OpCodes.Stloc, disease));
        il.Add(Instruction.Create(OpCodes.Ldloc, disease));
        il.Add(Instruction.Create(OpCodes.Newobj, diseaseContractConstructor));
        ChainConstructedScenario();
        il.Add(Instruction.Create(OpCodes.Ldloc, disease));
        il.Add(Instruction.Create(OpCodes.Call, getDiseaseType));
        il.Add(Instruction.Create(OpCodes.Ldfld, diseaseNameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, "PTSD"));
        il.Add(Instruction.Create(OpCodes.Callvirt, equals));
        il.Add(Instruction.Create(OpCodes.Brfalse, diseaseDone));
        il.Add(Instruction.Create(OpCodes.Ldsfld, sharedAchievementField));
        il.Add(Instruction.Create(OpCodes.Ldstr, "acGhostPTSD"));
        il.Add(Instruction.Create(OpCodes.Callvirt, completeAchievement));
        il.Add(diseaseDone);

        il.Add(chanceStart);
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0f));
        il.Add(Instruction.Create(OpCodes.Stloc, chance));
        var chanceDone = Instruction.Create(OpCodes.Nop);
        if (ghost)
        {
            var middleFriendliness = Instruction.Create(OpCodes.Nop);
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessAttributeField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 40f));
            il.Add(Instruction.Create(OpCodes.Bge, middleFriendliness));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 100f));
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessAttributeField));
            il.Add(Instruction.Create(OpCodes.Sub));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.5f));
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Ldloc, category));
            Int(5);
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Add));
            il.Add(Instruction.Create(OpCodes.Stloc, chance));
            il.Add(Instruction.Create(OpCodes.Br, chanceDone));
            il.Add(middleFriendliness);
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessAttributeField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 70f));
            il.Add(Instruction.Create(OpCodes.Bge, chanceDone));
            il.Add(Instruction.Create(OpCodes.Ldloc, category));
            Int(4);
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Stloc, chance));
        }
        else
        {
            var lowFriendliness = Instruction.Create(OpCodes.Nop);
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 40f));
            il.Add(Instruction.Create(OpCodes.Blt, lowFriendliness));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, ghostFriendlinessField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 70f));
            il.Add(Instruction.Create(OpCodes.Bge, chanceDone));
            il.Add(Instruction.Create(OpCodes.Ldloc, category));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Stloc, chance));
            il.Add(Instruction.Create(OpCodes.Br, chanceDone));
            il.Add(lowFriendliness);
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Ldfld, dangerScoreField));
            il.Add(Instruction.Create(OpCodes.Ldloc, category));
            il.Add(Instruction.Create(OpCodes.Add));
            il.Add(Instruction.Create(OpCodes.Stloc, attackScore));

            var bases = new[] { 0, 5, 10, 15, 20, 30, 40, 50, 80, 90 };
            var minimums = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var maximums = new[] { 5, 10, 20, 20, 20, 20, 20, 20, 20, 10 };
            for (var score = 1; score <= 10; score++)
            {
                var nextScore = Instruction.Create(OpCodes.Nop);
                il.Add(Instruction.Create(OpCodes.Ldloc, attackScore));
                Int(score);
                il.Add(Instruction.Create(OpCodes.Bne_Un, nextScore));
                if (bases[score - 1] != 0)
                    Int(bases[score - 1]);
                Random(minimums[score - 1], maximums[score - 1]);
                if (bases[score - 1] != 0)
                    il.Add(Instruction.Create(OpCodes.Add));
                il.Add(Instruction.Create(OpCodes.Conv_R4));
                il.Add(Instruction.Create(OpCodes.Stloc, chance));
                il.Add(Instruction.Create(OpCodes.Br, chanceDone));
                il.Add(nextScore);
            }
        }
        il.Add(chanceDone);

        var vanish = Instruction.Create(OpCodes.Nop);
        var attackDecision = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, chance));
        Random(0, 100);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ble, vanish));
        il.Add(Instruction.Create(OpCodes.Ldloc, heartAttack));
        il.Add(Instruction.Create(OpCodes.Brtrue, vanish));
        il.Add(Instruction.Create(OpCodes.Br, attackDecision));
        il.Add(vanish);
        il.Add(Instruction.Create(OpCodes.Ldloc, title));
        il.Add(Instruction.Create(OpCodes.Callvirt, getLength));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, finalScenario));
        Format1("{0}\n\nIt vanished as quickly as it appeared.",
            () => il.Add(Instruction.Create(OpCodes.Ldloc, resultBody)));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        il.Add(Instruction.Create(OpCodes.Br, finalScenario));

        il.Add(attackDecision);
        var createAttack = Instruction.Create(OpCodes.Nop);
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, wilinessField));
        Random(0, 150);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ble, createAttack));
        il.Add(Instruction.Create(OpCodes.Ldloc, directAttack));
        il.Add(Instruction.Create(OpCodes.Brtrue, createAttack));

        var selectChildren = Instruction.Create(OpCodes.Nop);
        var killLover = Instruction.Create(OpCodes.Nop);
        Random(0, 2);
        il.Add(Instruction.Create(OpCodes.Brtrue, selectChildren));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getLover));
        il.Add(Instruction.Create(OpCodes.Stloc, lover));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Brfalse, selectChildren));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        Int(18);
        il.Add(Instruction.Create(OpCodes.Blt, selectChildren));
        il.Add(Instruction.Create(OpCodes.Br, killLover));
        il.Add(selectChildren);

        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, children));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getChildren));
        il.Add(Instruction.Create(OpCodes.Stloc, childArray));
        il.Add(Instruction.Create(OpCodes.Ldloc, childArray));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var tryStart = Instruction.Create(OpCodes.Nop);
        var childLoop = Instruction.Create(OpCodes.Nop);
        var nextChild = Instruction.Create(OpCodes.Nop);
        var childLoopCheck = Instruction.Create(OpCodes.Nop);
        var enumerationDone = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var disposalEnd = Instruction.Create(OpCodes.Nop);
        var noDisposable = Instruction.Create(OpCodes.Nop);
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, childLoopCheck));
        il.Add(childLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumeratorCurrent));
        il.Add(Instruction.Create(OpCodes.Castclass, childType));
        il.Add(Instruction.Create(OpCodes.Stloc, child));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextChild));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        Int(22);
        il.Add(Instruction.Create(OpCodes.Bgt, nextChild));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Call, isLivingWithHero));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextChild));
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Stloc, integerSink));
        il.Add(nextChild);
        il.Add(childLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, enumeratorMoveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, childLoop));
        il.Add(Instruction.Create(OpCodes.Leave, enumerationDone));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Isinst, disposableType));
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
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Brfalse, createAttack));
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        Int(0);
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, childType));
        il.Add(Instruction.Create(OpCodes.Stloc, child));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Callvirt, die));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Call, getScript));
        il.Add(Instruction.Create(OpCodes.Call, getDeath));
        Int(3);
        il.Add(Instruction.Create(OpCodes.Callvirt, updateDeathType));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Call, getScript));
        il.Add(Instruction.Create(OpCodes.Call, getDeath));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Call, getAge));
        il.Add(Instruction.Create(OpCodes.Stfld, ageAtDeathField));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Newobj, childDeathConstructor));
        ChainConstructedScenario();
        il.Add(Instruction.Create(OpCodes.Br, finalScenario));

        il.Add(killLover);
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Callvirt, die));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Call, getScript));
        il.Add(Instruction.Create(OpCodes.Call, getDeath));
        Int(3);
        il.Add(Instruction.Create(OpCodes.Callvirt, updateDeathType));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Call, getScript));
        il.Add(Instruction.Create(OpCodes.Call, getDeath));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Call, getAge));
        il.Add(Instruction.Create(OpCodes.Stfld, ageAtDeathField));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, addLoverToExes));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Newobj, loverDeathConstructor));
        ChainConstructedScenario();
        il.Add(Instruction.Create(OpCodes.Br, finalScenario));

        il.Add(createAttack);
        if (ghost)
        {
            LoadEncounter();
            il.Add(Instruction.Create(OpCodes.Call, descriptiveGhostName));
            il.Add(Instruction.Create(OpCodes.Call, capitalize));
        }
        else
        {
            Format1("The {0}", () =>
            {
                LoadEncounter();
                il.Add(Instruction.Create(OpCodes.Ldfld, entityField));
            });
        }
        LoadTargetGender();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Newobj, attackConstructor));
        ChainConstructedScenario();

        il.Add(finalScenario);
        var cleanup = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, title));
        il.Add(Instruction.Create(OpCodes.Callvirt, getLength));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, cleanup));
        il.Add(Instruction.Create(OpCodes.Ldloc, title));
        il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        ChainConstructedScenario();
        il.Add(cleanup);
        if (ghost)
        {
            Format1("Friends roll their eyes when recalling how {0} believed in ghosts.",
                () => il.Add(Instruction.Create(OpCodes.Ldstr, "<obit-he/she>")));
            il.Add(Instruction.Create(OpCodes.Call, addObituary));
        }
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getLifeLog));
        il.Add(Instruction.Create(OpCodes.Ldloc, narrative));
        il.Add(Instruction.Create(OpCodes.Callvirt, log));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeInterfaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, refreshUI));
        // Native normal returns: ghost 0x38d0550; paranormal 0x38d4fa4.
        // Native exception/failure tails extend to 0x38d0b0c and 0x38d51dc respectively.
        il.Add(Instruction.Create(OpCodes.Ret));
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
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.Single(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 ||
             candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var definition = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields)
            .Single(candidate => candidate.Name == name);
        return module.ImportReference(definition);
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
