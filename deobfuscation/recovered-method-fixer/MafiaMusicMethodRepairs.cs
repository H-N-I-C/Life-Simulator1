using Mono.Cecil;
using Mono.Cecil.Cil;

public static class MafiaMusicMethodRepairs
{
    // Native ARM64: 0x4FDB7E0-0x4FDC740 (method index 24091).
    public static void FixMafiaChoice(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var choiceType = FindType(module, "ScenarioChoice");
        var gameManagerType = FindType(module, "GameManager");
        var lifeType = FindType(module, "Life");
        var heroType = FindType(module, "SimHero");
        var personType = FindType(module, "SimPerson");
        var placeType = FindType(module, "SimPlace");
        var countryType = FindType(module, "DataCountry");
        var nameType = FindType(module, "SimPersonName");
        var emigrationType = FindType(module, "SimEmigration");
        var loverType = FindType(module, "SimLover");
        var challengeType = FindType(module, "Apollo16ChallengeTracker");
        var gameReferenceType = FindType(module, "GameReference");
        var achievementType = FindType(module, "AchievementManager");

        var specialJobField = Field(module, owner, "_specialJob");
        var choice1Field = Field(module, owner, "_choice1Place");
        var choice2Field = Field(module, owner, "_choice2Place");
        var choiceArrayField = Field(module, owner, "ChoiceArray");
        var choiceTitleField = Field(module, choiceType, "<Title>k__BackingField");
        var nextScenarioField = Field(module, owner, "<NextScenario>k__BackingField");
        var tableDictionaryField = Field(module, scenarioType, "TableDictionary");
        var tableOrderField = Field(module, scenarioType, "TableOrderArray");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var heroField = Field(module, gameManagerType, "Hero");
        var pastField = Field(module, gameManagerType, "Past");
        var loverField = Field(module, lifeType, "<Lover>k__BackingField");
        var ageAtNameChangeField = Field(module, lifeType, "<AgeAtLastNameChange>k__BackingField");
        var visitedCountriesField = Field(module, lifeType, "<VisitedCountriesArray>k__BackingField");
        var seniorCenterField = Field(module, lifeType, "<SeniorCenter>k__BackingField");
        var challengeTrackerField = Field(module, lifeType, "<ChallengeTracker>k__BackingField");
        var lifeLogField = Field(module, lifeType, "<LifeLog>k__BackingField");
        var lifeInterfaceField = Field(module, lifeType, "LifeInterface");
        var personNameField = Field(module, personType, "<Name>k__BackingField");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var firstNameField = Field(module, nameType, "<FirstName>k__BackingField");
        var lastNameField = Field(module, nameType, "<LastName>k__BackingField");
        var getCountry = ImportMethod(module, placeType, "get_Country", 0, true);
        var reversesNamesField = Field(module, countryType, "<ReversesNames>k__BackingField");
        var countryNameField = Field(module, countryType, "<Name>k__BackingField");
        var emigrationTypeField = Field(module, emigrationType, "<Type>k__BackingField");
        var emigrationFromField = Field(module, emigrationType, "<FromPlace>k__BackingField");
        var emigrationToField = Field(module, emigrationType, "<ToPlace>k__BackingField");
        var emigrationAgeField = Field(module, emigrationType, "<HeroAgeAtEmigration>k__BackingField");
        var enteredWitnessProtectionField = Field(module, challengeType, "<HasEnteredWitnessProtectionProgram>k__BackingField");

        var arrayListType = ArrayListType(module);
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListCopyConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 1, true, "System.Collections.ICollection");
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var arrayListGetCount = FrameworkMethod(module, "System.Collections.ArrayList", "get_Count", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var arrayListClear = FrameworkMethod(module, "System.Collections.ArrayList", "Clear", 0, true);
        var arrayListContains = FrameworkMethod(module, "System.Collections.ArrayList", "Contains", 1, true);
        var dictionaryType = (GenericInstanceType)tableDictionaryField.FieldType;
        var dictionaryConstructor = ImportMethod(module, dictionaryType, ".ctor", 0, true);
        var dictionaryAdd = ImportMethod(module, dictionaryType, "Add", 2, true);
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var stringContains = FrameworkMethod(module, "System.String", "Contains", 1, true, "System.String");
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 1, true, "System.String");
        var format1 = FrameworkMethod(module, "System.String", "Format", 2, false, "System.String", "System.Object");
        var format2 = FrameworkMethod(module, "System.String", "Format", 3, false, "System.String", "System.Object", "System.Object");
        var format3 = FrameworkMethod(module, "System.String", "Format", 4, false, "System.String", "System.Object", "System.Object", "System.Object");

        var heroLeavesFamily = ImportMethod(module, specialJobField.FieldType, "HeroLeavesFamily", 0, true);
        var leaveSpecialJob = ImportMethod(module, heroType, "LeaveSpecialJobWithTerminationType", 1, true);
        var getPlaceOfResidence = ImportMethod(module, personType, "get_PlaceOfResidence", 0, true);
        var setPlaceOfResidence = ImportMethod(module, personType, "set_PlaceOfResidence", 1, true);
        var placeDescription = ImportMethod(module, placeType, "Description", 0, true);
        var setupCompanies = ImportMethod(module, placeType, "SetupCompanies", 0, true);
        var countryFullName = ImportMethod(module, countryType, "FullName", 0, true);
        var effectiveGender = ImportMethod(module, personType, "EffectiveGender", 0, true);
        var randomFirstName = ImportMethod(module, countryType, "GetRandomFirstNameForGender", 1, true);
        var randomLastName = ImportMethod(module, countryType, "GetRandomLastNameAndCanBeCustom", 1, true);
        var nameConstructor = ImportMethod(module, nameType, ".ctor", 3, true);
        var nameFullName = ImportMethod(module, nameType, "FullName", 0, true);
        var getFriendArray = ImportMethod(module, lifeType, "get_FriendArray", 0, true);
        var pickRandomString = ImportMethod(module, gameReferenceType, "PickRandomFromStringArray", 1, false);
        var stringForPersonArray = ImportMethod(module, gameReferenceType, "StringForPersonArray", 1, false);
        var getEmigrationChildren = ImportMethod(module, emigrationType, "GetEmigrationChildArrayAndIsCurrentLoverGoing", 1, false);
        var emigrationConstructor = ImportMethod(module, emigrationType, ".ctor", 0, true);
        var getEmigrationHistory = ImportMethod(module, pastField.FieldType, "get_EmigrationHistoryArray", 0, true);
        var clearSeniorNpcList = ImportMethod(module, seniorCenterField.FieldType, "ClearSeniorNpcList", 0, true);
        var relationshipStatus = ImportMethod(module, loverType, "RelationshipStatusString", 0, true);
        var getScript = ImportMethod(module, personType, "get_Script", 0, true);
        var emigrateWithHero = ImportMethod(module, FindType(module, "SimThirdPersonScript"), "EmigrateWithHero", 0, true);
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true);
        var getAchievementManager = ImportMethod(module, achievementType, "get_SharedAchievementManager", 0, false);
        var completeAchievement = ImportMethod(module, achievementType, "CompleteAchievement", 1, true);
        var lifeLog = ImportMethod(module, lifeLogField.FieldType, "Log", 1, true);
        var refreshUI = ImportMethod(module, lifeInterfaceField.FieldType, "RefreshUI", 0, true);

        var body = FreshBody(method);
        var game = new VariableDefinition(gameManagerType);
        var life = new VariableDefinition(lifeType);
        var hero = new VariableDefinition(heroType);
        var selectedText = new VariableDefinition(module.TypeSystem.String);
        var originalBody = new VariableDefinition(module.TypeSystem.String);
        var resultBody = new VariableDefinition(module.TypeSystem.String);
        var logText = new VariableDefinition(module.TypeSystem.String);
        var place = new VariableDefinition(placeType);
        var firstName = new VariableDefinition(module.TypeSystem.String);
        var lastName = new VariableDefinition(module.TypeSystem.String);
        var oldLastName = new VariableDefinition(module.TypeSystem.String);
        var title = new VariableDefinition(module.TypeSystem.String);
        var titles = new VariableDefinition(arrayListType);
        var table = new VariableDefinition(dictionaryType);
        var tableOrder = new VariableDefinition(arrayListType);
        var lover = new VariableDefinition(loverType);
        var children = new VariableDefinition(arrayListType);
        var emigration = new VariableDefinition(emigrationType);
        var person = new VariableDefinition(personType);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        var people = new VariableDefinition(module.TypeSystem.String);
        var nextScenario = new VariableDefinition(scenarioType);
        foreach (var local in new[]
                 {
                     game, life, hero, selectedText, originalBody, resultBody, logText, place,
                     firstName, lastName, oldLastName, title, titles, table, tableOrder, lover,
                     children, emigration, person, loopIndex, people, nextScenario
                 })
        {
            body.Variables.Add(local);
        }
        var il = body.Instructions;

        void LoadGame()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Stloc, game));
            il.Add(Instruction.Create(OpCodes.Ldloc, game));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
            il.Add(Instruction.Create(OpCodes.Stloc, life));
            il.Add(Instruction.Create(OpCodes.Ldloc, game));
            il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
            il.Add(Instruction.Create(OpCodes.Stloc, hero));
        }

        void LoadCountry(VariableDefinition destination)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, destination));
            il.Add(Instruction.Create(OpCodes.Call, getCountry));
        }

        void LoadHeroName()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, hero));
            il.Add(Instruction.Create(OpCodes.Ldfld, personNameField));
        }

        void EmitMigratePerson(VariableDefinition migrant, Instruction done)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, migrant));
            il.Add(Instruction.Create(OpCodes.Ldloc, place));
            il.Add(Instruction.Create(OpCodes.Call, setPlaceOfResidence));
            il.Add(Instruction.Create(OpCodes.Ldloc, migrant));
            il.Add(Instruction.Create(OpCodes.Call, getScript));
            il.Add(Instruction.Create(OpCodes.Callvirt, emigrateWithHero));
            il.Add(Instruction.Create(OpCodes.Ldloc, migrant));
            il.Add(Instruction.Create(OpCodes.Ldfld, personNameField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lastNameField));
            il.Add(Instruction.Create(OpCodes.Ldloc, oldLastName));
            il.Add(Instruction.Create(OpCodes.Callvirt, stringEquals));
            il.Add(Instruction.Create(OpCodes.Brfalse, done));
            il.Add(Instruction.Create(OpCodes.Ldloc, migrant));
            il.Add(Instruction.Create(OpCodes.Ldfld, personNameField));
            il.Add(Instruction.Create(OpCodes.Ldloc, lastName));
            il.Add(Instruction.Create(OpCodes.Stfld, lastNameField));
            il.Add(done);
        }

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choiceArrayField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, choiceType));
        il.Add(Instruction.Create(OpCodes.Ldfld, choiceTitleField));
        il.Add(Instruction.Create(OpCodes.Callvirt, toLower));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedText));

        il.Add(Instruction.Create(OpCodes.Ldstr, "I was offered the opportunity to join the Witness Protection Program and relocate to either {0} or {1}."));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choice1Field));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Callvirt, countryFullName));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choice2Field));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Callvirt, countryFullName));
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Stloc, originalBody));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, specialJobField));
        il.Add(Instruction.Create(OpCodes.Callvirt, heroLeavesFamily));
        LoadGame();
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Callvirt, leaveSpecialJob));

        var relocate = Instruction.Create(OpCodes.Nop);
        var finalLogging = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedText));
        il.Add(Instruction.Create(OpCodes.Ldstr, "neither"));
        il.Add(Instruction.Create(OpCodes.Callvirt, stringContains));
        il.Add(Instruction.Create(OpCodes.Brfalse, relocate));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0}\nI decided to stay in {1} and take my chances."));
        il.Add(Instruction.Create(OpCodes.Ldloc, originalBody));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Callvirt, countryFullName));
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Stloc, logText));
        il.Add(Instruction.Create(OpCodes.Br, finalLogging));

        il.Add(relocate);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choice1Field));
        il.Add(Instruction.Create(OpCodes.Stloc, place));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedText));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choice2Field));
        il.Add(Instruction.Create(OpCodes.Callvirt, placeDescription));
        il.Add(Instruction.Create(OpCodes.Callvirt, toLower));
        il.Add(Instruction.Create(OpCodes.Callvirt, stringEquals));
        var firstPlace = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brfalse, firstPlace));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choice2Field));
        il.Add(Instruction.Create(OpCodes.Stloc, place));
        il.Add(firstPlace);
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Callvirt, setupCompanies));

        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Call, effectiveGender));
        il.Add(Instruction.Create(OpCodes.Callvirt, randomFirstName));
        il.Add(Instruction.Create(OpCodes.Stloc, firstName));
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, randomLastName));
        il.Add(Instruction.Create(OpCodes.Stloc, lastName));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0}"));
        LoadHeroName();
        il.Add(Instruction.Create(OpCodes.Ldfld, lastNameField));
        il.Add(Instruction.Create(OpCodes.Call, format1));
        il.Add(Instruction.Create(OpCodes.Stloc, oldLastName));
        LoadHeroName();
        il.Add(Instruction.Create(OpCodes.Ldloc, firstName));
        il.Add(Instruction.Create(OpCodes.Ldloc, lastName));
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Call, getCountry));
        il.Add(Instruction.Create(OpCodes.Ldfld, reversesNamesField));
        il.Add(Instruction.Create(OpCodes.Newobj, nameConstructor));
        il.Add(Instruction.Create(OpCodes.Stfld, personNameField));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Stfld, ageAtNameChangeField));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Call, getFriendArray));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListClear));

        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, titles));
        foreach (var choiceTitle in new[] { "Should I change hairstyles too?", "A new leaf", "Need a new style", "This is my life now" })
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, titles));
            il.Add(Instruction.Create(OpCodes.Ldstr, choiceTitle));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
        }
        il.Add(Instruction.Create(OpCodes.Ldloc, titles));
        il.Add(Instruction.Create(OpCodes.Call, pickRandomString));
        il.Add(Instruction.Create(OpCodes.Stloc, title));
        il.Add(Instruction.Create(OpCodes.Ldstr, "You have started a new life in {0} under the name {1}."));
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Callvirt, placeDescription));
        LoadHeroName();
        il.Add(Instruction.Create(OpCodes.Callvirt, nameFullName));
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));

        il.Add(Instruction.Create(OpCodes.Newobj, dictionaryConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, table));
        il.Add(Instruction.Create(OpCodes.Ldloc, table));
        il.Add(Instruction.Create(OpCodes.Ldstr, "New Name"));
        LoadHeroName();
        il.Add(Instruction.Create(OpCodes.Callvirt, nameFullName));
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryAdd));
        il.Add(Instruction.Create(OpCodes.Ldloc, table));
        il.Add(Instruction.Create(OpCodes.Ldstr, "New Location"));
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Callvirt, placeDescription));
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryAdd));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, tableOrder));
        foreach (var key in new[] { "New Name", "New Location" })
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, tableOrder));
            il.Add(Instruction.Create(OpCodes.Ldstr, key));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
        }

        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, loverField));
        il.Add(Instruction.Create(OpCodes.Stloc, lover));
        var noLoverBody = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Brfalse, noLoverBody));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0}\n\nYour {1}, {2}, came with you."));
        il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Callvirt, relationshipStatus));
        il.Add(Instruction.Create(OpCodes.Callvirt, toLower));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Ldfld, personNameField));
        il.Add(Instruction.Create(OpCodes.Ldfld, firstNameField));
        il.Add(Instruction.Create(OpCodes.Call, format3));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        il.Add(noLoverBody);

        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, loverField));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Cgt_Un));
        il.Add(Instruction.Create(OpCodes.Call, getEmigrationChildren));
        il.Add(Instruction.Create(OpCodes.Stloc, children));
        il.Add(Instruction.Create(OpCodes.Newobj, emigrationConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, emigration));
        il.Add(Instruction.Create(OpCodes.Ldloc, emigration));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
        il.Add(Instruction.Create(OpCodes.Stfld, emigrationTypeField));
        il.Add(Instruction.Create(OpCodes.Ldloc, emigration));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Stfld, emigrationFromField));
        il.Add(Instruction.Create(OpCodes.Ldloc, emigration));
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Stfld, emigrationToField));
        il.Add(Instruction.Create(OpCodes.Ldloc, emigration));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Stfld, emigrationAgeField));
        il.Add(Instruction.Create(OpCodes.Ldloc, game));
        il.Add(Instruction.Create(OpCodes.Ldfld, pastField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEmigrationHistory));
        il.Add(Instruction.Create(OpCodes.Ldloc, emigration));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Call, setPlaceOfResidence));

        var noSeniorCenter = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, seniorCenterField));
        il.Add(Instruction.Create(OpCodes.Brfalse, noSeniorCenter));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, seniorCenterField));
        il.Add(Instruction.Create(OpCodes.Callvirt, clearSeniorNpcList));
        il.Add(noSeniorCenter);

        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, loverField));
        il.Add(Instruction.Create(OpCodes.Stloc, lover));
        var noLoverMigration = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Brfalse, noLoverMigration));
        EmitMigratePerson(lover, noLoverMigration);

        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var childLoop = Instruction.Create(OpCodes.Nop);
        var childCheck = Instruction.Create(OpCodes.Nop);
        var childNext = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, childCheck));
        il.Add(childLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, personType));
        il.Add(Instruction.Create(OpCodes.Stloc, person));
        EmitMigratePerson(person, childNext);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(childCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, childLoop));

        var countryKnown = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, visitedCountriesField));
        LoadCountry(place);
        il.Add(Instruction.Create(OpCodes.Ldfld, countryNameField));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListContains));
        il.Add(Instruction.Create(OpCodes.Brtrue, countryKnown));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, visitedCountriesField));
        LoadCountry(place);
        il.Add(Instruction.Create(OpCodes.Ldfld, countryNameField));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(countryKnown);

        il.Add(Instruction.Create(OpCodes.Ldstr, "{0}\nI started a new life in {1} under the name {2}."));
        il.Add(Instruction.Create(OpCodes.Ldloc, originalBody));
        il.Add(Instruction.Create(OpCodes.Ldloc, place));
        il.Add(Instruction.Create(OpCodes.Callvirt, placeDescription));
        LoadHeroName();
        il.Add(Instruction.Create(OpCodes.Callvirt, nameFullName));
        il.Add(Instruction.Create(OpCodes.Call, format3));
        il.Add(Instruction.Create(OpCodes.Stloc, logText));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, loverField));
        il.Add(Instruction.Create(OpCodes.Stloc, lover));
        var noLoverLog = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Brfalse, noLoverLog));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} {1} came with me."));
        il.Add(Instruction.Create(OpCodes.Ldloc, logText));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Ldfld, personNameField));
        il.Add(Instruction.Create(OpCodes.Ldfld, firstNameField));
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Stloc, logText));
        il.Add(noLoverLog);

        var noChildrenText = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, noChildrenText));
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListCopyConstructor));
        il.Add(Instruction.Create(OpCodes.Call, stringForPersonArray));
        il.Add(Instruction.Create(OpCodes.Stloc, people));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0}\n\nYour {1}, {2}, came with you."));
        il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        var bodySingular = Instruction.Create(OpCodes.Nop);
        var bodyPluralReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ble, bodySingular));
        il.Add(Instruction.Create(OpCodes.Ldstr, "children"));
        il.Add(Instruction.Create(OpCodes.Br, bodyPluralReady));
        il.Add(bodySingular);
        il.Add(Instruction.Create(OpCodes.Ldstr, "child"));
        il.Add(bodyPluralReady);
        il.Add(Instruction.Create(OpCodes.Ldloc, people));
        il.Add(Instruction.Create(OpCodes.Call, format3));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} My {1}, {2}, came with me."));
        il.Add(Instruction.Create(OpCodes.Ldloc, logText));
        il.Add(Instruction.Create(OpCodes.Ldloc, children));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        var logSingular = Instruction.Create(OpCodes.Nop);
        var logPluralReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ble, logSingular));
        il.Add(Instruction.Create(OpCodes.Ldstr, "children"));
        il.Add(Instruction.Create(OpCodes.Br, logPluralReady));
        il.Add(logSingular);
        il.Add(Instruction.Create(OpCodes.Ldstr, "child"));
        il.Add(logPluralReady);
        il.Add(Instruction.Create(OpCodes.Ldloc, people));
        il.Add(Instruction.Create(OpCodes.Call, format3));
        il.Add(Instruction.Create(OpCodes.Stloc, logText));
        il.Add(noChildrenText);

        il.Add(Instruction.Create(OpCodes.Ldloc, title));
        il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, nextScenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, nextScenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, table));
        il.Add(Instruction.Create(OpCodes.Stfld, tableDictionaryField));
        il.Add(Instruction.Create(OpCodes.Ldloc, nextScenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, tableOrder));
        il.Add(Instruction.Create(OpCodes.Stfld, tableOrderField));
        il.Add(Instruction.Create(OpCodes.Ldloc, nextScenario));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, nextScenario));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Call, getAchievementManager));
        il.Add(Instruction.Create(OpCodes.Ldstr, "acMafiaProgram"));
        il.Add(Instruction.Create(OpCodes.Callvirt, completeAchievement));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, challengeTrackerField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stfld, enteredWitnessProtectionField));

        il.Add(finalLogging);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeLogField));
        il.Add(Instruction.Create(OpCodes.Ldloc, logText));
        il.Add(Instruction.Create(OpCodes.Callvirt, lifeLog));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeInterfaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, refreshUI));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    // Native ARM64: 0x3D28650-0x3D28B78 (method index 12609).
    public static void FixMusicVenue(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var entryType = FindType(module, "Game.DataMusicTalentScoutVenueName");
        var venueSetField = Field(module, owner, "_musicTalentScoutVenueNameSet");
        var eventTypeField = Field(module, entryType, "<EventType>k__BackingField");
        var commonnessField = Field(module, entryType, "<CommonnessScore>k__BackingField");
        var venueField = Field(module, entryType, "<Venue>k__BackingField");
        var listType = (GenericInstanceType)venueSetField.FieldType;
        var listConstructor = ImportMethod(module, listType, ".ctor", 0, true);
        var listGetItem = ImportMethod(module, listType, "get_Item", 1, true);
        var listGetCount = ImportMethod(module, listType, "get_Count", 0, true);
        var listAdd = ImportMethod(module, listType, "Add", 1, true);
        var loadVenueSet = ImportMethod(module, owner, "LoadMusicTalentScoutVenueNameSet", 0, false);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 3, false, "System.String", "System.String", "System.StringComparison");
        var concat = FrameworkMethod(module, "System.String", "Concat", 2, false, "System.String", "System.String");
        var random = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false);
        var debugScope = module.AssemblyReferences.Single(reference => reference.Name is "UnityEngine.CoreModule" or "UnityEngine");
        var debugType = module.ImportReference(new TypeReference("UnityEngine", "Debug", module, debugScope));
        var logError = ImportMethod(module, debugType, "LogError", 1, false, "System.Object");

        var body = FreshBody(method);
        var matches = new VariableDefinition(listType);
        var entry = new VariableDefinition(entryType);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        var totalWeight = new VariableDefinition(module.TypeSystem.Int32);
        var selectedWeight = new VariableDefinition(module.TypeSystem.Int32);
        var cumulativeWeight = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { matches, entry, loopIndex, totalWeight, selectedWeight, cumulativeWeight }) body.Variables.Add(local);
        var il = body.Instructions;

        var needsLoad = Instruction.Create(OpCodes.Nop);
        var loaded = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, venueSetField));
        il.Add(Instruction.Create(OpCodes.Brfalse, needsLoad));
        il.Add(Instruction.Create(OpCodes.Ldsfld, venueSetField));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetCount));
        il.Add(Instruction.Create(OpCodes.Brtrue, loaded));
        il.Add(needsLoad);
        il.Add(Instruction.Create(OpCodes.Call, loadVenueSet));
        il.Add(loaded);
        il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, matches));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var filterLoop = Instruction.Create(OpCodes.Nop);
        var filterCheck = Instruction.Create(OpCodes.Nop);
        var filterNext = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, filterCheck));
        il.Add(filterLoop);
        il.Add(Instruction.Create(OpCodes.Ldsfld, venueSetField));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, entry));
        il.Add(Instruction.Create(OpCodes.Ldloc, entry));
        il.Add(Instruction.Create(OpCodes.Ldfld, eventTypeField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, filterNext));
        il.Add(Instruction.Create(OpCodes.Ldloc, matches));
        il.Add(Instruction.Create(OpCodes.Ldloc, entry));
        il.Add(Instruction.Create(OpCodes.Callvirt, listAdd));
        il.Add(filterNext);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(filterCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldsfld, venueSetField));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, filterLoop));

        var hasMatches = Instruction.Create(OpCodes.Nop);
        var noMatchesReturn = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, matches));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetCount));
        il.Add(Instruction.Create(OpCodes.Brtrue, hasMatches));
        il.Add(Instruction.Create(OpCodes.Ldstr, "No music talent scout venue names found for event type: "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Call, logError));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Br, noMatchesReturn));
        il.Add(hasMatches);

        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, totalWeight));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var sumLoop = Instruction.Create(OpCodes.Nop);
        var sumCheck = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, sumCheck));
        il.Add(sumLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, totalWeight));
        il.Add(Instruction.Create(OpCodes.Ldloc, matches));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetItem));
        il.Add(Instruction.Create(OpCodes.Ldfld, commonnessField));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, totalWeight));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(sumCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, matches));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, sumLoop));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, totalWeight));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedWeight));

        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, cumulativeWeight));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var selectLoop = Instruction.Create(OpCodes.Nop);
        var selectCheck = Instruction.Create(OpCodes.Nop);
        var selectNext = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, selectCheck));
        il.Add(selectLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, matches));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, entry));
        il.Add(Instruction.Create(OpCodes.Ldloc, cumulativeWeight));
        il.Add(Instruction.Create(OpCodes.Ldloc, entry));
        il.Add(Instruction.Create(OpCodes.Ldfld, commonnessField));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, cumulativeWeight));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedWeight));
        il.Add(Instruction.Create(OpCodes.Ldloc, cumulativeWeight));
        il.Add(Instruction.Create(OpCodes.Bge, selectNext));
        il.Add(Instruction.Create(OpCodes.Ldloc, entry));
        il.Add(Instruction.Create(OpCodes.Ldfld, venueField));
        il.Add(Instruction.Create(OpCodes.Br, noMatchesReturn));
        il.Add(selectNext);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(selectCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, matches));
        il.Add(Instruction.Create(OpCodes.Callvirt, listGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, selectLoop));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(noMatchesReturn);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static TypeReference ArrayListType(ModuleDefinition module)
    {
        return module.ImportReference(new TypeReference("System.Collections", "ArrayList", module, module.TypeSystem.Object.Scope));
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.FirstOrDefault(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)))
            ?? throw new InvalidOperationException($"Method not found: {declaringType.FullName}::{name}/{parameterCount}");
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
