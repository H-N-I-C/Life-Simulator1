using Mono.Cecil;
using Mono.Cecil.Cil;

public static class MediaFashionMethodRepairs
{
    public static void FixOnlyFansChoice(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var channelType = FindType(module, "SimSocialMediaChannel");
        var personType = FindType(module, "SimPerson");
        var parserType = FindType(module, "BracketParser");
        var gameReferenceType = FindType(module, "GameReference");
        var gameManagerType = FindType(module, "GameManager");

        var channelField = Field(module, owner, "_channel");
        var otherChannelField = Field(module, owner, "_otherChannel");
        var narrativeField = Field(module, owner, "_narrative");
        var nextScenarioField = Field(module, scenarioType, "<NextScenario>k__BackingField");
        var followersField = Field(module, channelType, "<Followers>k__BackingField");
        var lastPostTypeField = Field(module, channelType, "<LastCollabPostTypeOnlyFans>k__BackingField");
        var controversyField = Field(module, channelType, "<LastCollabPostOnlyFansControversy>k__BackingField");
        var ageAtLastPromotionField = Field(module, channelType, "<AgeAtLastCrossPromotion>k__BackingField");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var heroField = Field(module, lifeField.FieldType, "<Hero>k__BackingField");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var parserPersonField = Field(module, parserType, "<Person>k__BackingField");

        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListType = arrayListConstructor.DeclaringType;
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 2, true,
            "System.String", "System.StringComparison");
        var contains = FrameworkMethod(module, "System.String", "Contains", 1, true, "System.String");
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false,
            "System.String", "System.String", "System.String");
        var concatArray = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");
        var isNullOrEmpty = FrameworkMethod(module, "System.String", "IsNullOrEmpty", 1, false);

        var hasOtherSocials = ImportMethod(module, owner, "HasOtherSocialsAvailable", 0, false);
        var channelListType = (GenericInstanceType)hasOtherSocials.ReturnType;
        var channelGetItem = ImportMethod(module, channelListType, "get_Item", 1, true);
        var channelGetCount = ImportMethod(module, channelListType, "get_Count", 0, true);
        var getChannelName = ImportMethod(module, channelType, "GetChannelName", 0, true);
        var getData = ImportMethod(module, channelType, "get_Data", 0, true);
        var followersTermField = Field(module, getData.ReturnType, "<FollowersTerm>k__BackingField");
        var generateUsername = ImportMethod(module, channelType, "GenerateUsername", 1, true);
        var random = ImportMethod(module, gameReferenceType, "GetRandomNumber", 2, false);
        var pickTitle = ImportMethod(module, gameReferenceType, "PickRandomFromStringArray", 1, false);
        var grouping = ImportMethod(module, gameReferenceType, "NumberGroupingStringForAmount", 1, false);
        var impactHappiness = ImportMethod(module, personType, "ImpactHappiness", 1, true);
        var getPlaceOfResidence = ImportMethod(module, personType, "get_PlaceOfResidence", 0, true);
        var getStreetPerson = ImportMethod(module, FindType(module, "DataHub"),
            "GetRandomStreetPersonAndMustBeInNeed", 1, false);
        var generatePerson = ImportMethod(module, FindType(module, "PersonGenerator"),
            "GeneratePersonFromStreetPerson", 2, false);
        var parserConstructor = ImportMethod(module, parserType, ".ctor", 0, true);
        var parseBody = ImportMethod(module, parserType, "GetParsedBodyString", 1, true);
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true,
            "System.String", "System.String", "Scenario");

        var body = FreshBody(method);
        var selection = new VariableDefinition(module.TypeSystem.String);
        var lowerSelection = new VariableDefinition(module.TypeSystem.String);
        var channels = new VariableDefinition(channelListType);
        var currentChannel = new VariableDefinition(channelType);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        var chance = new VariableDefinition(module.TypeSystem.Int32);
        var heroAge = new VariableDefinition(module.TypeSystem.Int32);
        var newFollowers = new VariableDefinition(module.TypeSystem.Int32);
        var randomValue = new VariableDefinition(module.TypeSystem.Int32);
        var titles = new VariableDefinition(arrayListType);
        var title = new VariableDefinition(module.TypeSystem.String);
        var resultBody = new VariableDefinition(module.TypeSystem.String);
        var streetPerson = new VariableDefinition(getStreetPerson.ReturnType);
        var person = new VariableDefinition(personType);
        var username = new VariableDefinition(module.TypeSystem.String);
        var parser = new VariableDefinition(parserType);
        foreach (var local in new[]
                 {
                     selection, lowerSelection, channels, currentChannel, loopIndex, chance, heroAge,
                     newFollowers, randomValue, titles, title, resultBody, streetPerson, person, username, parser
                 })
            body.Variables.Add(local);
        var il = body.Instructions;
        var dropDownStrings = method.Parameters[2];

        void Int(int value) => il.Add(Instruction.Create(OpCodes.Ldc_I4, value));
        void LoadChannel()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, channelField));
        }
        void LoadOtherChannel()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, otherChannelField));
        }
        void LoadOtherChannelName()
        {
            LoadOtherChannel();
            il.Add(Instruction.Create(OpCodes.Call, getChannelName));
        }
        void LoadOtherFollowersTerm()
        {
            LoadOtherChannel();
            il.Add(Instruction.Create(OpCodes.Call, getData));
            il.Add(Instruction.Create(OpCodes.Ldfld, followersTermField));
        }
        void LoadHero()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        }
        void Random(int minimum, int maximum)
        {
            Int(minimum);
            Int(maximum);
            il.Add(Instruction.Create(OpCodes.Call, random));
        }
        void GroupedFollowers()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, newFollowers));
            il.Add(Instruction.Create(OpCodes.Call, grouping));
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
        void AddTitles(params string[] choices)
        {
            foreach (var choice in choices)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, titles));
                il.Add(Instruction.Create(OpCodes.Ldstr, choice));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Pop));
            }
        }
        void PickTitle()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, titles));
            il.Add(Instruction.Create(OpCodes.Call, pickTitle));
            il.Add(Instruction.Create(OpCodes.Stloc, title));
        }
        void Title(params string[] choices)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, titles));
            AddTitles(choices);
            PickTitle();
        }
        void ImpactHappiness(int value)
        {
            LoadHero();
            Int(value);
            il.Add(Instruction.Create(OpCodes.Callvirt, impactHappiness));
        }
        void SetNarrative(Action text)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            text();
            il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        }
        void ChainScenario()
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, title));
            il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
            il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
            il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        }

        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, selection));
        il.Add(Instruction.Create(OpCodes.Ldloc, selection));
        il.Add(Instruction.Create(OpCodes.Call, toLower));
        il.Add(Instruction.Create(OpCodes.Stloc, lowerSelection));
        il.Add(Instruction.Create(OpCodes.Call, hasOtherSocials));
        il.Add(Instruction.Create(OpCodes.Stloc, channels));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var channelLoop = Instruction.Create(OpCodes.Nop);
        var channelCheck = Instruction.Create(OpCodes.Nop);
        var nextChannel = Instruction.Create(OpCodes.Nop);
        var channelsDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, channelCheck));
        il.Add(channelLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, channels));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, channelGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, currentChannel));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentChannel));
        il.Add(Instruction.Create(OpCodes.Call, getChannelName));
        il.Add(Instruction.Create(OpCodes.Ldloc, selection));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextChannel));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, currentChannel));
        il.Add(Instruction.Create(OpCodes.Stfld, otherChannelField));
        il.Add(Instruction.Create(OpCodes.Br, channelsDone));
        il.Add(nextChannel);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(channelCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, channels));
        il.Add(Instruction.Create(OpCodes.Callvirt, channelGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, channelLoop));
        il.Add(channelsDone);

        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, lowerSelection));
        il.Add(Instruction.Create(OpCodes.Ldstr, "ok"));
        il.Add(Instruction.Create(OpCodes.Call, contains));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, otherChannelField));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));

        LoadChannel();
        il.Add(Instruction.Create(OpCodes.Ldfld, controversyField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Stloc, chance));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Stloc, heroAge));
        var noChanceReduction = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, heroAge));
        LoadChannel();
        il.Add(Instruction.Create(OpCodes.Ldfld, ageAtLastPromotionField));
        il.Add(Instruction.Create(OpCodes.Bne_Un, noChanceReduction));
        il.Add(Instruction.Create(OpCodes.Ldloc, chance));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Div));
        il.Add(Instruction.Create(OpCodes.Stloc, chance));
        il.Add(noChanceReduction);
        LoadChannel();
        il.Add(Instruction.Create(OpCodes.Ldloc, heroAge));
        il.Add(Instruction.Create(OpCodes.Stfld, ageAtLastPromotionField));

        var weakPath = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, chance));
        Random(0, 101);
        il.Add(Instruction.Create(OpCodes.Blt, weakPath));
        Random(10, 100);
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 1000f));
        il.Add(Instruction.Create(OpCodes.Div));
        LoadOtherChannel();
        il.Add(Instruction.Create(OpCodes.Ldfld, followersField));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Stloc, newFollowers));
        il.Add(Instruction.Create(OpCodes.Ldloc, newFollowers));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, weakPath));

        Title("Double-dipping");
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, getStreetPerson));
        il.Add(Instruction.Create(OpCodes.Stloc, streetPerson));
        il.Add(Instruction.Create(OpCodes.Ldloc, streetPerson));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Call, generatePerson));
        il.Add(Instruction.Create(OpCodes.Stloc, person));
        LoadChannel();
        il.Add(Instruction.Create(OpCodes.Ldloc, person));
        il.Add(Instruction.Create(OpCodes.Call, generateUsername));
        il.Add(Instruction.Create(OpCodes.Stloc, username));
        il.Add(Instruction.Create(OpCodes.Newobj, parserConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, parser));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        il.Add(Instruction.Create(OpCodes.Ldloc, person));
        il.Add(Instruction.Create(OpCodes.Stfld, parserPersonField));
        il.Add(Instruction.Create(OpCodes.Ldloc, parser));
        Concat(
            GroupedFollowers,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " of your ")),
            LoadOtherChannelName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")),
            LoadOtherFollowersTerm,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " subscribed to your OnlyFans after seeing a thumbnail of your latest ")),
            () =>
            {
                LoadChannel();
                il.Add(Instruction.Create(OpCodes.Ldfld, lastPostTypeField));
            },
            () => il.Add(Instruction.Create(OpCodes.Ldstr, ".\n\nOne subscriber named ")),
            () => il.Add(Instruction.Create(OpCodes.Ldloc, username)),
            () => il.Add(Instruction.Create(OpCodes.Ldstr,
                " wrote <he/she> <can finally die happy/feels incredibly blessed/can't thank you enough/is overcome with joy/will never be productive at work ever again/worships you/loves you/can't wait to watch all of your videos/is already obsessed/feels as though Christmas came early this year>.")));
        il.Add(Instruction.Create(OpCodes.Callvirt, parseBody));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        LoadChannel();
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldfld, followersField));
        il.Add(Instruction.Create(OpCodes.Ldloc, newFollowers));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stfld, followersField));
        ImpactHappiness(3);
        SetNarrative(() => Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "I promoted my OnlyFans page on ")),
            LoadOtherChannelName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " and gained ")),
            GroupedFollowers,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " new subscribers."))));
        ChainScenario();
        var outcomeDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, outcomeDone));

        il.Add(weakPath);
        Random(0, 100);
        il.Add(Instruction.Create(OpCodes.Stloc, randomValue));
        LoadOtherChannel();
        il.Add(Instruction.Create(OpCodes.Ldfld, followersField));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.05f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Conv_I4));
        il.Add(Instruction.Create(OpCodes.Stloc, newFollowers));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, titles));
        var noSubscribers = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, randomValue));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 81));
        il.Add(Instruction.Create(OpCodes.Blt, noSubscribers));
        il.Add(Instruction.Create(OpCodes.Ldloc, newFollowers));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, noSubscribers));

        AddTitles("Y'all didn't like that?", "Oops", "Tough crowd", "Where's the body positivity");
        PickTitle();
        Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "You posted a teaser promoting your OnlyFans on ")),
            LoadOtherChannelName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, ".\n\nYour post got 0 likes and you lost ")),
            GroupedFollowers,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " followers.")));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        LoadOtherChannel();
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldfld, followersField));
        il.Add(Instruction.Create(OpCodes.Ldloc, newFollowers));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Stfld, followersField));
        ImpactHappiness(-4);
        SetNarrative(() => Concat(
            () => il.Add(Instruction.Create(OpCodes.Ldstr, "I lost ")),
            GroupedFollowers,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " of my ")),
            LoadOtherChannelName,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " ")),
            LoadOtherFollowersTerm,
            () => il.Add(Instruction.Create(OpCodes.Ldstr, " after I promoted my OnlyFans there."))));
        ChainScenario();
        il.Add(Instruction.Create(OpCodes.Br, outcomeDone));

        il.Add(noSubscribers);
        AddTitles("F4F", "Cross promotion failure", "Come on guys!");
        PickTitle();
        il.Add(Instruction.Create(OpCodes.Ldstr, "None of your "));
        LoadOtherChannelName();
        il.Add(Instruction.Create(OpCodes.Ldstr, " followers subscribed to your OnlyFans."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        SetNarrative(() =>
        {
            il.Add(Instruction.Create(OpCodes.Ldstr, "I promoted my OnlyFans page on "));
            LoadOtherChannelName();
            il.Add(Instruction.Create(OpCodes.Ldstr, ", but no one subscribed."));
            il.Add(Instruction.Create(OpCodes.Call, concat3));
        });
        ImpactHappiness(-3);
        ChainScenario();

        il.Add(outcomeDone);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Call, isNullOrEmpty));
        il.Add(Instruction.Create(OpCodes.Brtrue, done));
        EmitLifeLogAndRefresh(method, narrativeField);
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixFashionChoice(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var fashionWeekType = FindType(module, "SimFashionWeek");
        var modelJobType = FindType(module, "SimSpecialJobModel");
        var gameReferenceType = FindType(module, "GameReference");
        var gameManagerType = FindType(module, "GameManager");

        var narrativeField = Field(module, owner, "_narrative");
        var modelJobField = Field(module, owner, "_modelJob");
        var fashionWeekField = Field(module, owner, "_fashionWeek");
        var nextScenarioField = Field(module, scenarioType, "<NextScenario>k__BackingField");
        var resultDictionaryField = Field(module, fashionWeekType, "<ResultDict>k__BackingField");
        var completedClientsField = Field(module, fashionWeekType, "<ClientShowsCompletedList>k__BackingField");
        var agencyField = Field(module, modelJobType, "<Agency>k__BackingField");
        var bookingAgentField = Field(module, agencyField.FieldType, "<BookingAgent>k__BackingField");
        var nameField = Field(module, bookingAgentField.FieldType, "<Name>k__BackingField");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var challengeTrackerField = Field(module, lifeField.FieldType, "<ChallengeTracker>k__BackingField");
        var weeksWalkedField = Field(module, challengeTrackerField.FieldType,
            "<NumModelFashionWeeksWalked>k__BackingField");

        var clientListType = (GenericInstanceType)completedClientsField.FieldType;
        var clientType = clientListType.GenericArguments[0];
        var clientListConstructor = ImportMethod(module, clientListType, ".ctor", 0, true);
        var clientListAdd = ImportMethod(module, clientListType, "Add", 1, true);
        var clientListGetItem = ImportMethod(module, clientListType, "get_Item", 1, true);
        var clientListGetCount = ImportMethod(module, clientListType, "get_Count", 0, true);
        var dictionaryType = (GenericInstanceType)resultDictionaryField.FieldType;
        var dictionaryGetEnumerator = ImportMethod(module, dictionaryType, "GetEnumerator", 0, true);
        var enumeratorType = GenericInstance((GenericInstanceType)dictionaryGetEnumerator.ReturnType,
            dictionaryType.GenericArguments.ToArray());
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var pairType = GenericInstance((GenericInstanceType)getCurrent.ReturnType,
            dictionaryType.GenericArguments.ToArray());
        var getKey = ImportMethod(module, pairType, "get_Key", 0, true);
        var getValue = ImportMethod(module, pairType, "get_Value", 0, true);
        var random = ImportMethod(module, gameReferenceType, "GetRandomNumber", 2, false);
        var capitalize = ImportMethod(module, gameReferenceType, "CapitalizeString", 1, false);
        var fullName = ImportMethod(module, nameField.FieldType, "FullName", 0, true);
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 1, true, "System.Boolean");
        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false,
            "System.String", "System.String", "System.String");

        var body = FreshBody(method);
        var clients = new VariableDefinition(clientListType);
        var enumerator = new VariableDefinition(enumeratorType);
        var pair = new VariableDefinition(pairType);
        var selectedClient = new VariableDefinition(clientType);
        var chainedScenario = new VariableDefinition(scenarioType);
        foreach (var local in new[] { clients, enumerator, pair, selectedClient, chainedScenario })
            body.Variables.Add(local);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Newobj, clientListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, clients));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, fashionWeekField));
        il.Add(Instruction.Create(OpCodes.Ldfld, resultDictionaryField));
        il.Add(Instruction.Create(OpCodes.Callvirt, dictionaryGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var nextPair = Instruction.Create(OpCodes.Nop);
        var enumerationDone = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var disposalEnd = Instruction.Create(OpCodes.Nop);
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, pair));
        il.Add(Instruction.Create(OpCodes.Ldloca, pair));
        il.Add(Instruction.Create(OpCodes.Call, getValue));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 35f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, nextPair));
        il.Add(Instruction.Create(OpCodes.Ldloc, clients));
        il.Add(Instruction.Create(OpCodes.Ldloca, pair));
        il.Add(Instruction.Create(OpCodes.Call, getKey));
        il.Add(Instruction.Create(OpCodes.Callvirt, clientListAdd));
        il.Add(nextPair);
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, enumerationDone));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
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

        var noChain = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, clients));
        il.Add(Instruction.Create(OpCodes.Callvirt, clientListGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, noChain));
        il.Add(Instruction.Create(OpCodes.Ldloc, clients));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, clients));
        il.Add(Instruction.Create(OpCodes.Callvirt, clientListGetCount));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Callvirt, clientListGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedClient));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(noChain);

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "My booking agent, "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, modelJobField));
        il.Add(Instruction.Create(OpCodes.Ldfld, agencyField));
        il.Add(Instruction.Create(OpCodes.Ldfld, bookingAgentField));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Call, fullName));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Ldstr, ", gave me feedback on my performance during fashion week."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        var noLog = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Brfalse, noLog));
        EmitLifeLogAndRefresh(method, narrativeField);
        il.Add(noLog);
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, challengeTrackerField));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldfld, weeksWalkedField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stfld, weeksWalkedField));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static GenericInstanceType GenericInstance(GenericInstanceType template, params TypeReference[] arguments)
    {
        var instance = new GenericInstanceType(template.ElementType);
        foreach (var argument in arguments) instance.GenericArguments.Add(argument);
        return instance;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name,
        int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.First(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName)
                .SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName,
        int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module,
            module.TypeSystem.Object.Scope);
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var definition = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields)
            .Single(candidate => candidate.Name == name);
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
