using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class CrimeMuseumMethodRepairs
{
    public static void FixAccomplice(MethodDefinition method)
    {
        var module = method.Module;
        var accompliceType = method.ReturnType;
        var personType = FindType(module, "SimPerson");
        var siblingType = FindType(module, "SimSibling");
        var gameManager = FindType(module, "GameManager");
        var lifeType = FindType(module, "Life");
        var instField = Field(module, gameManager, "Inst");
        var lifeField = Field(module, gameManager, "Life");
        var getFriends = ImportMethod(module, lifeType, "get_FriendArray", 0, true);
        var getLover = ImportMethod(module, lifeType, "get_Lover", 0, true);
        var getSiblings = ImportMethod(module, lifeType, "get_SiblingArray", 0, true);
        var accompliceConstructor = ImportMethod(module, accompliceType, ".ctor", 0, true);
        var personField = Field(module, accompliceType, "<Person>k__BackingField");
        var personTypeField = Field(module, accompliceType, "<PersonType>k__BackingField");
        var descriptiveNameField = Field(module, accompliceType, "<DescriptiveName>k__BackingField");
        var listDisplayNameField = Field(module, accompliceType, "<ListDisplayName>k__BackingField");
        var aliveField = Field(module, personType, "<Alive>k__BackingField");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var crazinessField = Field(module, personType, "<Att_craziness>k__BackingField");
        var nameField = Field(module, personType, "<Name>k__BackingField");
        var firstNameField = Field(module, nameField.FieldType, "<FirstName>k__BackingField");
        var siblingTypeField = Field(module, siblingType, "<SiblingType>k__BackingField");
        var livesNearby = ImportMethod(module, personType, "LivesInSamePlaceAsHero", 0, true);
        var relationshipStrength = ImportMethod(module, personType, "GetTotalHeroRelationshipStrength", 0, true);
        var friendStatus = ImportMethod(module, personType, "FriendRelationshipStatusString", 0, true);
        var fullName = ImportMethod(module, nameField.FieldType, "FullName", 0, true);
        var capitalize = ImportMethod(module, FindType(module, "GameReference"), "CapitalizeString", 1, false, "System.String");
        var loverStatus = ImportMethod(module, getLover.ReturnType, "RelationshipStatusString", 0, true);
        var siblingStatus = ImportMethod(module, siblingType, "RelationshipStatusStringAndIncludeBigLittle", 1, true, "System.Boolean");
        var effectiveGender = ImportMethod(module, personType, "EffectiveGender", 0, true);
        var format = FrameworkMethod(module, "System.String", "Format", 3, false, "System.String", "System.Object", "System.Object");
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var arrayListGetCount = FrameworkMethod(module, "System.Collections.ArrayList", "get_Count", 0, true);
        var siblingGetItem = ImportMethod(module, getSiblings.ReturnType, "get_Item", 1, true);
        var siblingGetCount = ImportMethod(module, getSiblings.ReturnType, "get_Count", 0, true);

        var body = FreshBody(method);
        var selected = new VariableDefinition(accompliceType);
        var life = new VariableDefinition(lifeType);
        var friends = new VariableDefinition(getFriends.ReturnType);
        var friend = new VariableDefinition(personType);
        var lover = new VariableDefinition(getLover.ReturnType);
        var siblings = new VariableDefinition(getSiblings.ReturnType);
        var sibling = new VariableDefinition(siblingType);
        var prefix = new VariableDefinition(module.TypeSystem.String);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { selected, life, friends, friend, lover, siblings, sibling, prefix, loopIndex }) body.Variables.Add(local);
        var il = body.Instructions;
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Stloc, life));

        void EmitCrazyEnough(VariableDefinition candidate, Instruction skip)
        {
            var eligible = Instruction.Create(OpCodes.Nop);
            il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
            il.Add(Instruction.Create(OpCodes.Ldfld, crazinessField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 70f));
            il.Add(Instruction.Create(OpCodes.Ble_Un, skip));
            il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
            il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, 30));
            il.Add(Instruction.Create(OpCodes.Blt, eligible));
            il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
            il.Add(Instruction.Create(OpCodes.Ldfld, crazinessField));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 95f));
            il.Add(Instruction.Create(OpCodes.Ble_Un, skip));
            il.Add(eligible);
        }

        void EmitShouldReplace(VariableDefinition candidate, Instruction skip)
        {
            var checkCraziness = Instruction.Create(OpCodes.Nop);
            var replace = Instruction.Create(OpCodes.Nop);
            il.Add(Instruction.Create(OpCodes.Ldloc, selected));
            il.Add(Instruction.Create(OpCodes.Brtrue, checkCraziness));
            il.Add(Instruction.Create(OpCodes.Br, replace));
            il.Add(checkCraziness);
            il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
            il.Add(Instruction.Create(OpCodes.Ldfld, crazinessField));
            il.Add(Instruction.Create(OpCodes.Ldloc, selected));
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
            il.Add(Instruction.Create(OpCodes.Ldfld, crazinessField));
            il.Add(Instruction.Create(OpCodes.Blt_Un, skip));
            il.Add(replace);
        }

        void EmitNewSelected(VariableDefinition candidate, int candidateType)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, accompliceConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, selected));
            il.Add(Instruction.Create(OpCodes.Ldloc, selected));
            il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
            il.Add(Instruction.Create(OpCodes.Stfld, personField));
            il.Add(Instruction.Create(OpCodes.Ldloc, selected));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, candidateType));
            il.Add(Instruction.Create(OpCodes.Stfld, personTypeField));
        }

        void EmitFullName(VariableDefinition candidate)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
            il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
            il.Add(Instruction.Create(OpCodes.Call, fullName));
        }

        void EmitFirstName(VariableDefinition candidate)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, candidate));
            il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
            il.Add(Instruction.Create(OpCodes.Ldfld, firstNameField));
        }

        var friendsDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Call, getFriends));
        il.Add(Instruction.Create(OpCodes.Stloc, friends));
        il.Add(Instruction.Create(OpCodes.Ldloc, friends));
        il.Add(Instruction.Create(OpCodes.Call, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, friendsDone));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var friendLoop = Instruction.Create(OpCodes.Nop);
        var friendLoopCheck = Instruction.Create(OpCodes.Nop);
        var nextFriend = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, friendLoopCheck));
        il.Add(friendLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, friends));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, personType));
        il.Add(Instruction.Create(OpCodes.Stloc, friend));
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextFriend));
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Call, livesNearby));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextFriend));
        EmitCrazyEnough(friend, nextFriend);
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Call, relationshipStrength));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 20f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, nextFriend));
        EmitShouldReplace(friend, nextFriend);
        EmitNewSelected(friend, 1);
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldstr, "<pronoun> {0}, {1},"));
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Call, friendStatus));
        EmitFirstName(friend);
        il.Add(Instruction.Create(OpCodes.Call, format));
        il.Add(Instruction.Create(OpCodes.Stfld, descriptiveNameField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} ({1})"));
        EmitFullName(friend);
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Call, friendStatus));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Call, format));
        il.Add(Instruction.Create(OpCodes.Stfld, listDisplayNameField));
        il.Add(nextFriend);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(friendLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, friends));
        il.Add(Instruction.Create(OpCodes.Call, arrayListGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, friendLoop));
        il.Add(friendsDone);

        var loverDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Call, getLover));
        il.Add(Instruction.Create(OpCodes.Stloc, lover));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Brfalse, loverDone));
        EmitCrazyEnough(lover, loverDone);
        EmitShouldReplace(lover, loverDone);
        EmitNewSelected(lover, 3);
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldstr, "<pronoun> {0}, {1},"));
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Call, loverStatus));
        il.Add(Instruction.Create(OpCodes.Call, toLower));
        EmitFirstName(lover);
        il.Add(Instruction.Create(OpCodes.Call, format));
        il.Add(Instruction.Create(OpCodes.Stfld, descriptiveNameField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} ({1})"));
        EmitFullName(lover);
        il.Add(Instruction.Create(OpCodes.Ldloc, lover));
        il.Add(Instruction.Create(OpCodes.Call, loverStatus));
        il.Add(Instruction.Create(OpCodes.Call, format));
        il.Add(Instruction.Create(OpCodes.Stfld, listDisplayNameField));
        il.Add(loverDone);

        var siblingsDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Call, getSiblings));
        il.Add(Instruction.Create(OpCodes.Stloc, siblings));
        il.Add(Instruction.Create(OpCodes.Ldloc, siblings));
        il.Add(Instruction.Create(OpCodes.Call, siblingGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, siblingsDone));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var siblingLoop = Instruction.Create(OpCodes.Nop);
        var siblingLoopCheck = Instruction.Create(OpCodes.Nop);
        var nextSibling = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, siblingLoopCheck));
        il.Add(siblingLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, siblings));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, siblingGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, siblingType));
        il.Add(Instruction.Create(OpCodes.Stloc, sibling));
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextSibling));
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 8));
        il.Add(Instruction.Create(OpCodes.Blt, nextSibling));
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Call, livesNearby));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextSibling));
        EmitCrazyEnough(sibling, nextSibling);
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Call, relationshipStrength));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 20f));
        il.Add(Instruction.Create(OpCodes.Ble_Un, nextSibling));
        EmitShouldReplace(sibling, nextSibling);

        var sister = Instruction.Create(OpCodes.Nop);
        var prefixReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Call, effectiveGender));
        il.Add(Instruction.Create(OpCodes.Brtrue, sister));
        il.Add(Instruction.Create(OpCodes.Ldstr, "brother"));
        il.Add(Instruction.Create(OpCodes.Stloc, prefix));
        il.Add(Instruction.Create(OpCodes.Br, prefixReady));
        il.Add(sister);
        il.Add(Instruction.Create(OpCodes.Ldstr, "sister"));
        il.Add(Instruction.Create(OpCodes.Stloc, prefix));
        il.Add(prefixReady);
        var stepPrefixDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Ldfld, siblingTypeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, -2));
        il.Add(Instruction.Create(OpCodes.And));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
        il.Add(Instruction.Create(OpCodes.Bne_Un, stepPrefixDone));
        var stepSister = Instruction.Create(OpCodes.Nop);
        var stepPrefixReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Call, effectiveGender));
        il.Add(Instruction.Create(OpCodes.Brtrue, stepSister));
        il.Add(Instruction.Create(OpCodes.Ldstr, "stepbrother"));
        il.Add(Instruction.Create(OpCodes.Stloc, prefix));
        il.Add(Instruction.Create(OpCodes.Br, stepPrefixReady));
        il.Add(stepSister);
        il.Add(Instruction.Create(OpCodes.Ldstr, "stepsister"));
        il.Add(Instruction.Create(OpCodes.Stloc, prefix));
        il.Add(stepPrefixReady);
        il.Add(stepPrefixDone);

        EmitNewSelected(sibling, 4);
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldstr, "<pronoun> {0}, {1},"));
        il.Add(Instruction.Create(OpCodes.Ldloc, sibling));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, siblingStatus));
        EmitFirstName(sibling);
        il.Add(Instruction.Create(OpCodes.Call, format));
        il.Add(Instruction.Create(OpCodes.Stfld, descriptiveNameField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} ({1})"));
        EmitFullName(sibling);
        il.Add(Instruction.Create(OpCodes.Ldloc, prefix));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        il.Add(Instruction.Create(OpCodes.Call, format));
        il.Add(Instruction.Create(OpCodes.Stfld, listDisplayNameField));
        il.Add(nextSibling);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(siblingLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, siblings));
        il.Add(Instruction.Create(OpCodes.Call, siblingGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, siblingLoop));
        il.Add(siblingsDone);
        il.Add(Instruction.Create(OpCodes.Ldloc, selected));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixMuseumWing(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var museumParameter = method.Parameters.Single(parameter => parameter.Name == "museum");
        var floorParameter = method.Parameters.Single(parameter => parameter.Name == "floor");
        var wingParameter = method.Parameters.Single(parameter => parameter.Name == "wing");
        var museumType = museumParameter.ParameterType;
        var museumWingType = FindType(module, "SimMuseumWing");
        var displaySlotType = FindType(module, "SimDisplayItem");
        var listItemType = FindType(module, "ListItem");
        var listType = FindType(module, "List");
        var emojiHub = FindType(module, "EmojiHub");
        var gameReference = FindType(module, "GameReference");
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var getMuseumWing = ImportMethod(module, museumType, "GetMuseumWing", 2, true, "System.Int32", "System.Int32");
        var profileConstructor = ImportMethod(module, FindType(module, "ScenarioBlackMarketMuseumWingProfile"), ".ctor", 2, true, "SimMuseum", "SimMuseumWing");
        var cleanAllConstructor = ImportMethod(module, FindType(module, "ScenarioBlackMarketMuseumWingCleanAll"), ".ctor", 1, true, "SimMuseumWing");
        var listItemHeaderConstructor = ImportMethod(module, listItemType, ".ctor", 6, true, "System.String", "System.String", "Scenario", "System.String", "System.String", "System.Boolean");
        var listItemTitleConstructor = ImportMethod(module, listItemType, ".ctor", 1, true, "System.String");
        var listItemRowConstructor = ImportMethod(module, listItemType, ".ctor", 2, true, "System.String", "List");
        var listConstructor = ImportMethod(module, listType, ".ctor", 2, true, "System.String", "System.Collections.ArrayList");
        var wingNameField = Field(module, museumWingType, "<WingName>k__BackingField");
        var wingTypeField = Field(module, museumWingType, "<WingType>k__BackingField");
        var numSlotsField = Field(module, museumType, "<NumExhibitSlots>k__BackingField");
        var displayItemField = Field(module, displaySlotType, "<DisplayItem>k__BackingField");
        var itemTypeField = Field(module, displaySlotType, "<ItemType>k__BackingField");
        var barGraphTitleField = Field(module, listItemType, "BarGraphTitle");
        var barGraphValueField = Field(module, listItemType, "BarGraphValue");
        var secondLineField = Field(module, listItemType, "SecondLine");
        var emojiStringField = Field(module, listItemType, "EmojiString");
        var wingEmoji = ImportMethod(module, emojiHub, "EmojiForBlackMarketMuseumWing", 1, false, "System.Int32");
        var averageAppeal = ImportMethod(module, museumWingType, "GetAverageAppealForWing", 0, true);
        var getDisplayItems = ImportMethod(module, museumWingType, "get_DisplayItems", 0, true);
        var displayGetItem = ImportMethod(module, getDisplayItems.ReturnType, "get_Item", 1, true);
        var displayGetCount = ImportMethod(module, getDisplayItems.ReturnType, "get_Count", 0, true);
        var getAddDisplayItems = ImportMethod(module, owner, "GetMuseumAddDisplayItemList", 3, true, "SimMuseumWing", "SimDisplayItem", "System.Int32");
        var getItemInteractions = ImportMethod(module, owner, "GetMuseumDisplayItemInteraction", 4, true, "SimDisplayItem", "System.Int32", "System.Int32", "System.Int32");
        var getPetInteractions = ImportMethod(module, owner, "GetMuseumDisplayPetInteractionList", 4, true, "SimDisplayItem", "System.Int32", "System.Int32", "System.Int32");
        var capitalize = ImportMethod(module, gameReference, "CapitalizeString", 1, false, "System.String");
        var capitalizeAll = ImportMethod(module, gameReference, "CapitalizeAllMajorWordsInString", 1, false, "System.String");
        var format2 = FrameworkMethod(module, "System.String", "Format", 3, false, "System.String", "System.Object", "System.Object");

        var auctionType = FindType(module, "SimAuctionItem");
        var carType = FindType(module, "SimCar");
        var watercraftType = FindType(module, "SimWatercraft");
        var aircraftType = FindType(module, "SimAircraft");
        var jewelType = FindType(module, "SimJewel");
        var artifactType = FindType(module, "SimArtifact");
        var heirloomType = FindType(module, "SimHeirloom");
        var petType = FindType(module, "SimPet");
        var modelGiftType = FindType(module, "SimModelGift");
        var casinoType = FindType(module, "SimCasinoCollectible");
        var racingType = FindType(module, "SimRacingCollectible");
        var treasureType = FindType(module, "SimOutdoorFishingTreasure");
        var rvType = FindType(module, "SimRecreationalVehicle");

        var body = FreshBody(method);
        var result = new VariableDefinition(method.ReturnType);
        var museumWing = new VariableDefinition(museumWingType);
        var row = new VariableDefinition(listItemType);
        var interactions = new VariableDefinition(listType);
        var remainingSlots = new VariableDefinition(module.TypeSystem.Int32);
        var slotIndex = new VariableDefinition(module.TypeSystem.Int32);
        var emptyCount = new VariableDefinition(module.TypeSystem.Int32);
        var displayItems = new VariableDefinition(getDisplayItems.ReturnType);
        var displaySlot = new VariableDefinition(displaySlotType);
        var displayed = new VariableDefinition(displaySlotType);
        var auction = new VariableDefinition(auctionType);
        var car = new VariableDefinition(carType);
        var watercraft = new VariableDefinition(watercraftType);
        var aircraft = new VariableDefinition(aircraftType);
        var jewel = new VariableDefinition(jewelType);
        var artifact = new VariableDefinition(artifactType);
        var heirloom = new VariableDefinition(heirloomType);
        var pet = new VariableDefinition(petType);
        var modelGift = new VariableDefinition(modelGiftType);
        var casino = new VariableDefinition(casinoType);
        var racing = new VariableDefinition(racingType);
        var treasure = new VariableDefinition(treasureType);
        var rv = new VariableDefinition(rvType);
        foreach (var local in new[] { result, museumWing, row, interactions, remainingSlots, slotIndex, emptyCount, displayItems, displaySlot, displayed, auction, car, watercraft, aircraft, jewel, artifact, heirloom, pet, modelGift, casino, racing, treasure, rv }) body.Variables.Add(local);
        var il = body.Instructions;
        var iterationDone = Instruction.Create(OpCodes.Nop);

        void AddResultRow()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, result));
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
        }

        void EmitEmptyDisplay()
        {
            il.Add(Instruction.Create(OpCodes.Ldstr, "Empty Display"));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Belongings"));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, museumWing));
            il.Add(Instruction.Create(OpCodes.Ldnull));
            il.Add(Instruction.Create(OpCodes.Ldloc, slotIndex));
            il.Add(Instruction.Create(OpCodes.Call, getAddDisplayItems));
            il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
            il.Add(Instruction.Create(OpCodes.Newobj, listItemRowConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, row));
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
            il.Add(Instruction.Create(OpCodes.Stfld, secondLineField));
            AddResultRow();
        }

        void EmitInteractions(bool petSpecific = false)
        {
            il.Add(Instruction.Create(OpCodes.Ldstr, "Exhibit"));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, displaySlot));
            il.Add(Instruction.Create(OpCodes.Ldarg, floorParameter));
            il.Add(Instruction.Create(OpCodes.Ldarg, wingParameter));
            il.Add(Instruction.Create(OpCodes.Ldloc, slotIndex));
            il.Add(Instruction.Create(OpCodes.Call, petSpecific ? getPetInteractions : getItemInteractions));
            il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, interactions));
        }

        void FinishRowConstruction()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, interactions));
            il.Add(Instruction.Create(OpCodes.Newobj, listItemRowConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, row));
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Condition"));
            il.Add(Instruction.Create(OpCodes.Stfld, barGraphTitleField));
        }

        void EmitCondition(TypeReference type)
        {
            EmitGetProperty(module, il, type, "Condition");
        }

        void FinishRowWithEmoji(MethodReference emoji)
        {
            il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
            il.Add(Instruction.Create(OpCodes.Castclass, emoji.Parameters[0].ParameterType));
            il.Add(Instruction.Create(OpCodes.Call, emoji));
            il.Add(Instruction.Create(OpCodes.Stfld, emojiStringField));
            AddResultRow();
            il.Add(Instruction.Create(OpCodes.Br, iterationDone));
        }

        void FinishMuseumConditionRow(VariableDefinition item, MethodReference emoji)
        {
            // The caller has loaded the item for the initial condition test.
            var conditionReady = Instruction.Create(OpCodes.Nop);
            EmitGetProperty(module, il, item.VariableType, "MuseumCondition");
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 0f));
            il.Add(Instruction.Create(OpCodes.Bne_Un, conditionReady));
            il.Add(Instruction.Create(OpCodes.Ldloc, item));
            il.Add(Instruction.Create(OpCodes.Ldloc, item));
            EmitCondition(item.VariableType);
            EmitSetProperty(module, il, item.VariableType, "MuseumCondition");
            il.Add(conditionReady);
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Ldloc, item));
            EmitGetProperty(module, il, item.VariableType, "MuseumCondition");
            FinishRowWithEmoji(emoji);
        }

        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Ldarg, museumParameter));
        il.Add(Instruction.Create(OpCodes.Ldarg, floorParameter));
        il.Add(Instruction.Create(OpCodes.Ldarg, wingParameter));
        il.Add(Instruction.Create(OpCodes.Call, getMuseumWing));
        il.Add(Instruction.Create(OpCodes.Stloc, museumWing));
        il.Add(Instruction.Create(OpCodes.Ldloc, museumWing));
        il.Add(Instruction.Create(OpCodes.Ldfld, wingNameField));
        il.Add(Instruction.Create(OpCodes.Ldloc, museumWing));
        il.Add(Instruction.Create(OpCodes.Ldfld, wingTypeField));
        il.Add(Instruction.Create(OpCodes.Call, wingEmoji));
        il.Add(Instruction.Create(OpCodes.Ldarg, museumParameter));
        il.Add(Instruction.Create(OpCodes.Ldloc, museumWing));
        il.Add(Instruction.Create(OpCodes.Newobj, profileConstructor));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Newobj, listItemHeaderConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Appeal"));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphTitleField));
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, museumWing));
        il.Add(Instruction.Create(OpCodes.Call, averageAppeal));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
        AddResultRow();
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Exhibits"));
        il.Add(Instruction.Create(OpCodes.Newobj, listItemTitleConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldarg, museumParameter));
        il.Add(Instruction.Create(OpCodes.Ldfld, numSlotsField));
        il.Add(Instruction.Create(OpCodes.Stloc, remainingSlots));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, slotIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, emptyCount));
        il.Add(Instruction.Create(OpCodes.Ldloc, museumWing));
        il.Add(Instruction.Create(OpCodes.Call, getDisplayItems));
        il.Add(Instruction.Create(OpCodes.Stloc, displayItems));

        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        body.Variables.Add(loopIndex);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var displayLoop = Instruction.Create(OpCodes.Nop);
        var displayLoopCheck = Instruction.Create(OpCodes.Nop);
        var displaysDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, displayLoopCheck));
        il.Add(displayLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayItems));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, displayGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, displaySlot));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, displayed));
        var displayLoaded = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, displaySlot));
        il.Add(Instruction.Create(OpCodes.Brfalse, displayLoaded));
        il.Add(Instruction.Create(OpCodes.Ldloc, displaySlot));
        il.Add(Instruction.Create(OpCodes.Ldfld, displayItemField));
        il.Add(Instruction.Create(OpCodes.Castclass, displaySlotType));
        il.Add(Instruction.Create(OpCodes.Stloc, displayed));
        il.Add(displayLoaded);
        var emptyCheck = Instruction.Create(OpCodes.Nop);
        var notEmpty = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, displaySlot));
        il.Add(Instruction.Create(OpCodes.Brfalse, emptyCheck));
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Brfalse, emptyCheck));
        il.Add(Instruction.Create(OpCodes.Br, notEmpty));

        var afterEmpty = Instruction.Create(OpCodes.Nop);
        il.Add(emptyCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, remainingSlots));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, afterEmpty));
        EmitEmptyDisplay();
        il.Add(Instruction.Create(OpCodes.Ldloc, emptyCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, emptyCount));
        il.Add(afterEmpty);
        il.Add(Instruction.Create(OpCodes.Br, iterationDone));

        il.Add(notEmpty);
        var auctionDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Isinst, auctionType));
        il.Add(Instruction.Create(OpCodes.Stloc, auction));
        il.Add(Instruction.Create(OpCodes.Ldloc, auction));
        il.Add(Instruction.Create(OpCodes.Brfalse, auctionDone));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, auction));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, auctionType, "GetItemShortName", 0, true)));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, auction));
        EmitCondition(auctionType);
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, auction));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, auctionType, "GetEmojiString", 0, true)));
        il.Add(Instruction.Create(OpCodes.Stfld, emojiStringField));
        AddResultRow();
        il.Add(Instruction.Create(OpCodes.Br, iterationDone));
        il.Add(auctionDone);

        var case11 = Instruction.Create(OpCodes.Nop);
        var case12 = Instruction.Create(OpCodes.Nop);
        var case13 = Instruction.Create(OpCodes.Nop);
        var case14 = Instruction.Create(OpCodes.Nop);
        var case15 = Instruction.Create(OpCodes.Nop);
        var case16 = Instruction.Create(OpCodes.Nop);
        var case17 = Instruction.Create(OpCodes.Nop);
        var case19 = Instruction.Create(OpCodes.Nop);
        var case20 = Instruction.Create(OpCodes.Nop);
        var case21 = Instruction.Create(OpCodes.Nop);
        var case22 = Instruction.Create(OpCodes.Nop);
        var case23 = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Ldfld, itemTypeField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 11));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Switch, new[] { case11, case12, case13, case14, case15, case16, case17, iterationDone, case19, case20, case21, case22, case23 }));
        il.Add(Instruction.Create(OpCodes.Br, iterationDone));

        il.Add(case11);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, carType));
        il.Add(Instruction.Create(OpCodes.Stloc, car));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} {1}"));
        var getCarModel = ImportMethod(module, carType, "get_Model", 0, true);
        il.Add(Instruction.Create(OpCodes.Ldloc, car));
        il.Add(Instruction.Create(OpCodes.Call, getCarModel));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, getCarModel.ReturnType, "<Make>k__BackingField")));
        il.Add(Instruction.Create(OpCodes.Ldloc, car));
        il.Add(Instruction.Create(OpCodes.Call, getCarModel));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, getCarModel.ReturnType, "<Model>k__BackingField")));
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, car));
        FinishMuseumConditionRow(car, ImportMethod(module, emojiHub, "EmojiForCar", 1, false, "SimCar"));

        il.Add(case12);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, watercraftType));
        il.Add(Instruction.Create(OpCodes.Stloc, watercraft));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, watercraft));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, watercraftType, "GenericDescriptionStringAndCapitalize", 1, true, "System.Boolean")));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, watercraft));
        FinishMuseumConditionRow(watercraft, ImportMethod(module, emojiHub, "EmojiForWatercraft", 1, false, "SimWatercraft"));

        il.Add(case13);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, aircraftType));
        il.Add(Instruction.Create(OpCodes.Stloc, aircraft));
        EmitInteractions();
        var getAircraftData = ImportMethod(module, aircraftType, "get_Data", 0, true);
        il.Add(Instruction.Create(OpCodes.Ldloc, aircraft));
        il.Add(Instruction.Create(OpCodes.Call, getAircraftData));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, getAircraftData.ReturnType, "GetMakeAndModelStringAndTrimIfNecessary", 1, true, "System.Boolean")));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, aircraft));
        FinishMuseumConditionRow(aircraft, ImportMethod(module, emojiHub, "EmojiForAircraft", 1, false, "SimAircraft"));

        il.Add(case14);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, jewelType));
        il.Add(Instruction.Create(OpCodes.Stloc, jewel));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, jewelType, "ShortDescription", 0, true)));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
        EmitCondition(jewelType);
        FinishRowWithEmoji(ImportMethod(module, emojiHub, "EmojiForJewel", 1, false, "SimJewel"));

        il.Add(case15);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, artifactType));
        il.Add(Instruction.Create(OpCodes.Stloc, artifact));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, artifactType, "<ArtifactName>k__BackingField")));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        EmitCondition(artifactType);
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, artifactType, "EmojiString", 0, true)));
        il.Add(Instruction.Create(OpCodes.Stfld, emojiStringField));
        AddResultRow();
        il.Add(Instruction.Create(OpCodes.Br, iterationDone));

        il.Add(case16);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, heirloomType));
        il.Add(Instruction.Create(OpCodes.Stloc, heirloom));
        EmitInteractions();
        var getHeirloom = ImportMethod(module, heirloomType, "get_Heirloom", 0, true);
        il.Add(Instruction.Create(OpCodes.Ldloc, heirloom));
        il.Add(Instruction.Create(OpCodes.Call, getHeirloom));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, getHeirloom.ReturnType, "GetShortHandNameAndCapitalize", 1, true, "System.Boolean")));
        il.Add(Instruction.Create(OpCodes.Call, capitalize));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, heirloom));
        EmitGetProperty(module, il, heirloomType, "CurrentCondition");
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, heirloom));
        il.Add(Instruction.Create(OpCodes.Call, getHeirloom));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, emojiHub, "EmojiForHeirloom", 1, false, "DataHeirloom")));
        il.Add(Instruction.Create(OpCodes.Stfld, emojiStringField));
        AddResultRow();
        il.Add(Instruction.Create(OpCodes.Br, iterationDone));

        il.Add(case17);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, petType));
        il.Add(Instruction.Create(OpCodes.Stloc, pet));
        EmitInteractions(true);
        il.Add(Instruction.Create(OpCodes.Ldloc, pet));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, petType, "<Name>k__BackingField")));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, pet));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, petType, "<Att_health>k__BackingField")));
        FinishRowWithEmoji(ImportMethod(module, emojiHub, "EmojiForPet", 1, false, "SimPet"));

        il.Add(case19);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, modelGiftType));
        il.Add(Instruction.Create(OpCodes.Stloc, modelGift));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, modelGift));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, modelGiftType, "<Name>k__BackingField")));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, modelGift));
        EmitCondition(modelGiftType);
        FinishRowWithEmoji(ImportMethod(module, emojiHub, "EmojiForModelGift", 1, false, "SimModelGift"));

        il.Add(case20);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, casinoType));
        il.Add(Instruction.Create(OpCodes.Stloc, casino));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, casino));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, casinoType, "<ItemName>k__BackingField")));
        il.Add(Instruction.Create(OpCodes.Call, capitalizeAll));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, casino));
        EmitCondition(casinoType);
        FinishRowWithEmoji(ImportMethod(module, emojiHub, "EmojiForCasinoCollectible", 1, false, "SimCasinoCollectible"));

        il.Add(case21);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, racingType));
        il.Add(Instruction.Create(OpCodes.Stloc, racing));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, racing));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, racingType, "<ShortName>k__BackingField")));
        il.Add(Instruction.Create(OpCodes.Call, capitalizeAll));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, racing));
        EmitCondition(racingType);
        FinishRowWithEmoji(ImportMethod(module, emojiHub, "EmojiForRacingCollectible", 1, false, "SimRacingCollectible"));

        il.Add(case22);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, treasureType));
        il.Add(Instruction.Create(OpCodes.Stloc, treasure));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, treasure));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, treasureType, "<ShortName>k__BackingField")));
        il.Add(Instruction.Create(OpCodes.Call, capitalizeAll));
        FinishRowConstruction();
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, treasure));
        EmitCondition(treasureType);
        FinishRowWithEmoji(ImportMethod(module, emojiHub, "EmojiForOutdoorFishingTreasure", 1, false, "SimOutdoorFishingTreasure"));

        il.Add(case23);
        il.Add(Instruction.Create(OpCodes.Ldloc, displayed));
        il.Add(Instruction.Create(OpCodes.Castclass, rvType));
        il.Add(Instruction.Create(OpCodes.Stloc, rv));
        EmitInteractions();
        il.Add(Instruction.Create(OpCodes.Ldloc, rv));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, rvType, "GenericDescriptionString", 0, true)));
        il.Add(Instruction.Create(OpCodes.Call, capitalizeAll));
        FinishRowConstruction();
        var rvConditionReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, rv));
        EmitGetProperty(module, il, rvType, "MuseumCondition");
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0f));
        il.Add(Instruction.Create(OpCodes.Bne_Un, rvConditionReady));
        il.Add(Instruction.Create(OpCodes.Ldloc, rv));
        il.Add(Instruction.Create(OpCodes.Ldloc, rv));
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, rvType, "_condition")));
        EmitSetProperty(module, il, rvType, "MuseumCondition");
        il.Add(rvConditionReady);
        il.Add(Instruction.Create(OpCodes.Ldloc, row));
        il.Add(Instruction.Create(OpCodes.Ldloc, rv));
        EmitGetProperty(module, il, rvType, "MuseumCondition");
        FinishRowWithEmoji(ImportMethod(module, emojiHub, "EmojiForRecreationalVehicle", 1, false, "SimRecreationalVehicle"));

        il.Add(iterationDone);
        il.Add(Instruction.Create(OpCodes.Ldloc, remainingSlots));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Stloc, remainingSlots));
        il.Add(Instruction.Create(OpCodes.Ldloc, slotIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, slotIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(displayLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, displayItems));
        il.Add(Instruction.Create(OpCodes.Callvirt, displayGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, displayLoop));
        il.Add(displaysDone);

        var trailingDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, remainingSlots));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, trailingDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, emptyCount));
        il.Add(Instruction.Create(OpCodes.Ldloc, remainingSlots));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, emptyCount));
        var trailingLoop = Instruction.Create(OpCodes.Nop);
        var trailingCheck = Instruction.Create(OpCodes.Nop);
        il.Add(trailingLoop);
        EmitEmptyDisplay();
        il.Add(Instruction.Create(OpCodes.Ldloc, slotIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, slotIndex));
        il.Add(trailingCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, slotIndex));
        il.Add(Instruction.Create(OpCodes.Ldarg, museumParameter));
        il.Add(Instruction.Create(OpCodes.Ldfld, numSlotsField));
        il.Add(Instruction.Create(OpCodes.Bne_Un, trailingLoop));
        il.Add(trailingDone);

        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, emptyCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Bgt, done));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Clean All"));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Ldloc, museumWing));
        il.Add(Instruction.Create(OpCodes.Newobj, cleanAllConstructor));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, listItemHeaderConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static void EmitGetProperty(ModuleDefinition module, Mono.Collections.Generic.Collection<Instruction> il, TypeReference declaringType, string name)
    {
        var getter = FindMethod(declaringType, "get_" + name, 0, true);
        if (getter is not null)
        {
            il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, declaringType, getter)));
            return;
        }
        il.Add(Instruction.Create(OpCodes.Ldfld, Field(module, declaringType, "<" + name + ">k__BackingField")));
    }

    private static void EmitSetProperty(ModuleDefinition module, Mono.Collections.Generic.Collection<Instruction> il, TypeReference declaringType, string name)
    {
        var setter = FindMethod(declaringType, "set_" + name, 1, true);
        if (setter is not null)
        {
            il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, declaringType, setter)));
            return;
        }
        il.Add(Instruction.Create(OpCodes.Stfld, Field(module, declaringType, "<" + name + ">k__BackingField")));
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var definition = FindMethod(declaringType, name, parameterCount, instance, parameterTypes) ?? throw new InvalidOperationException($"Method not found: {declaringType.FullName}::{name}/{parameterCount}");
        return ImportMethod(module, declaringType, definition);
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, MethodDefinition definition)
    {
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
