using Mono.Cecil;
using Mono.Cecil.Cil;

if (args.Length < 1 || !Directory.Exists(args[0]))
{
    Console.Error.WriteLine("Usage: RecoveredMethodFixer <directory-containing-dlls> [extra-search-directory ...]");
    return 2;
}
var resolver = new DefaultAssemblyResolver();
foreach (var directory in args.Where(Directory.Exists)) resolver.AddSearchDirectory(directory);
var repaired = new HashSet<string>(StringComparer.Ordinal);
foreach (var path in Directory.EnumerateFiles(args[0], "*.dll", SearchOption.AllDirectories))
{
    var fileName = Path.GetFileName(path);
    if (fileName != "Assembly-CSharp.dll" && fileName != "Assembly-CSharp-firstpass.dll") continue;
    using var assembly = AssemblyDefinition.ReadAssembly(path, new ReaderParameters { InMemory = true, AssemblyResolver = resolver });
    var changed = false;
    foreach (var type in assembly.MainModule.Types.SelectMany(Flatten))
    foreach (var method in type.Methods)
    {
        if (!method.HasBody) continue;
        switch (method.FullName)
        {
            case "System.Boolean ChallengeMetadataBeachVibesOnly::MeetRequirement4()":
            case "System.Boolean ChallengeMetadataJudyTheJudge::MeetsCrimeShowRequirement()":
            case "System.Boolean ChallengeMetadataHotWetSummer::MeetRequirementWaterSlideTester()":
            case "System.Boolean ChallengeMetadataHausOfWhacks::MeetRequirementFashionDesigner()":
            case "System.Boolean ChallengeMetadataFullTimePartTime::MeetsHighSchoolPartTimeRequirement()":
            case "System.Boolean ChallengeMetadataShibUya::MeetRequirementFashionDesigner()":
            case "System.Boolean ChallengeMetadataSerialDater::MeetRequirementPoliceOfficer()":
            case "System.Boolean ChallengeMetadataOz::MeetsEmigrationRequirement()":
            case "System.Boolean ChallengeMetadataNerd::MeetRequirement5()":
            case "System.Boolean ChallengeMetadataLordUnderworld::HasSoldProductWorth()":
            case "System.Boolean ChallengeMetadataKahloMyWorld::MeetRequirementBecomePainter()":
            case "System.Boolean ChallengeMetadataHollywoodHustler::MeetsRequirementWon2PlusBittyAwards()":
            case "System.Boolean ChallengeMetadataSwiftie::MeetsRecordRequirement()":
            case "System.Boolean ChallengeMetadataTitanic::MeetsNudeArtModelRequirement()":
            case "System.Boolean ChallengeMetadataSimpleLife::MeetRequirementEmigrateBolivia()":
            case "System.Boolean ChallengeMetadataLoveBug::MeetRequirement2()":
            case "System.Boolean ChallengeMetadataTrailblazingSuperstar::MeetRequirement5()":
            case "System.Boolean ChallengeManager/<UploadCompletedChallenge>d__25::MoveNext()":
            case "System.Boolean ChallengeManager/<UploadQueuedOfflineChallenges>d__26::MoveNext()":
            case "System.Boolean IAPManager::IsProductAvailableToRestore(System.String)":
                ReturnConstant(method, false); break;
            case "T CandyBusiness.Confidence.ConfidenceSnapshot/LocalState::GetPropertyValue(System.String,T)":
                PropertyRemodelMethodRepairs.FixPropertyValue(method); break;
            case "System.Decimal IAPManager::LocalizedPrice(System.Int32)":
            case "System.String DataHub::GetBlackMarketChemistProductFeedbackColor(System.Int32,System.String)":
            case "Game.DataFightErTrainingString DataHub::GetFightClubRandomTrainingString(FIGHT_CLUB_ATTRIBUTE,GENDER)":
            case "DataBoost BoostInterface::GetAvailableBoost()":
            case "System.DateTime GraveyardManager::GetEarliestEarnedDateForBadge(System.Int32)":
            case "System.String EmojiSportsTeamsRings::EmojiStringForTypePrimaryColorSecondaryColorMetalColorString(System.Int32,UnityEngine.Color,UnityEngine.Color,System.String)":
            case "UnityEngine.Sprite EmojiAccessoryCacheManager::CreateCompositeImageForTypeIndex(System.Int32,System.Int32)":
            case "UnityEngine.Sprite EmojiGlovedFighterCacheManager::CreateCompositeImage(System.String)":
            case "UnityEngine.Sprite EmojiCelestialBodyCacheManager::CreateCompositeImageForTypePrimaryColorSecondaryColor(System.String)":
            case "UnityEngine.Sprite EmojiCryptoCacheManager::CreateCompositeImageForTextString(System.String)":
            case "UnityEngine.Sprite EmojiSportsTeamsCacheManager::CreateCompositeImageForTypePrimaryColorSecondaryColor(System.Int32,UnityEngine.Color,UnityEngine.Color)":
            case "UnityEngine.Sprite EmojiBondCacheManager::CreateCompositeImageForFlagEmojiString(System.String)":
                ReturnDefault(method); break;
            case "UnityEngine.Purchasing.Security.IPurchaseReceipt[] IAPManager::GetAllAndroidReceipts()":
            case "UnityEngine.Purchasing.Security.IPurchaseReceipt[] IAPManager::GetAllAppleReceipts()":
            case "UnityEngine.Color32[] EmojiCelestialBodyCacheManager::CreatePlanetComposite(System.String,System.Int32,System.Collections.Generic.List`1<UnityEngine.Color>,System.Boolean)":
            case "UnityEngine.Color32[] EmojiGlovedFighterCacheManager::CreateCompositeFaceAndGloves(System.String)":
            case "UnityEngine.Color32[] EmojiCelestialBodyCacheManager::CreateCometComposite(System.String,UnityEngine.Color)":
            case "UnityEngine.Color32[] EmojiCelestialBodyCacheManager::CreateNebulaComposite(System.String,UnityEngine.Color)":
                ReturnEmptyArray(method); break;
            case "System.Collections.ArrayList DataHub::GetRandomRemodelFeatureForHouseType(SimHouse,System.Int32,System.Boolean)":
                PropertyRemodelMethodRepairs.FixRemodelFeature(method); break;
            case "System.Collections.ArrayList LifeInterface::AvailableStreetPersonArray()":
                GameplayListMethodRepairs.FixStreetPeople(method); break;
            case "List LifeInterface::GetZooAnimalsList()":
                GameplayListMethodRepairs.FixZooAnimals(method); break;
            case "List LifeInterface::GetLuxuryUpscaleJewelersList()":
                GameplayListMethodRepairs.FixJewelers(method); break;
            case "System.Void BitLife.UI.Managers.UIDarklyManager/<>c__DisplayClass137_0::<CreateNewDarkly>g__Create|0(System.String,System.Action`1<T>,System.Boolean,System.Boolean)":
            case "System.Void DeclaredAgeRangeIOS::OnNativeCallback(System.Int32,System.Int32,System.Int32,System.Int32,System.IntPtr,System.IntPtr)":
            case "System.Void MoreMountains.NiceVibrations.MMVibrationManager::iOSTriggerHaptics(MoreMountains.NiceVibrations.HapticTypes)":
            case "System.Void DiagnosticsEmail/<Send>d__1::MoveNext()":
            case "System.Void CareerCollectibleManager::LoadCompletedCareers()":
            case "System.Void ChallengeManager::TranslateWebResponse(System.String)":
            case "System.Void HolidayCollectionManager::<TryGetScavengerHuntList>g__AddItem|49_0(System.String,System.String,HOLIDAY_COLLECTIBLE_TYPE,System.String,HolidayCollectionManager/<>c__DisplayClass49_0&)":
            case "System.Void ScenarioAstronautTransmissionData::RefreshScenario()":
            case "System.Void ScenarioAstronautTransmissionPublicationProfile::RefreshScenario()":
            case "System.Void ScenarioAstronautTransmissionPublicationReview::RefreshScenario()":
            case "System.Void EmojiAccessoryCacheManager::CombineTextures(UnityEngine.Color32[],UnityEngine.Color32[],UnityEngine.Vector2,UnityEngine.Vector2)":
            case "System.Void EmojiCryptoCacheManager::CombineTextures(UnityEngine.Color32[],UnityEngine.Color32[],UnityEngine.Vector2,UnityEngine.Vector2)":
            case "System.Void EmojiGlovedFighterCacheManager::CombineTextures(UnityEngine.Color32[],UnityEngine.Color32[],UnityEngine.Vector2,UnityEngine.Vector2)":
            case "System.Void EmojiBondCacheManager::CombineTextures(UnityEngine.Color32[],UnityEngine.Color32[],UnityEngine.Vector2,UnityEngine.Vector2)":
                ReturnVoid(method); break;
            case "System.Boolean ScenarioAstronautJobApplication::CanAchieveRank(DataAstronautCareerRank,System.Single)":
            case "System.Boolean LocalNotification::IsHeroEligibleForNotificationVoice(DataNotificationVoice)":
            case "System.Boolean LifeInterface/<>c__DisplayClass189_0::<GetTotemsList>b__20(SimAuctionItem)":
            case "System.Boolean LifeInterface/<>c__DisplayClass214_1::<GetHackingMissionDatabaseFilesList>b__1(System.ValueTuple`2<System.String,System.String>)":
                ReturnConstant(method, false); break;
            case "System.Void Life::FinishSetupAndLogBirthWithBirthCircumstance(DataBirthCircumstance)":
                PatchBirthDateComparison(method); break;
            case "System.Boolean RandomDeckCollection::Draw(DECK_ID,System.Single)":
                FixRandomDeckDraw(method); break;
            case "HousePartyOutcome HousePartyActivity::CalculatePartyOutcome()":
                FixHousePartyOutcome(method); break;
            case "System.Void ScenarioEmployeeHireInterviewQuestion::ChoiceSelectedAtIndex(System.Int32)":
                FixInterviewChoice(method); break;
            case "System.Void ScenarioDealerStreetPersonSellDrugs::ChoiceSelectedAtIndex(System.Int32,System.Collections.ArrayList,System.Collections.ArrayList,System.Collections.ArrayList)":
                DealerSellDrugsMethodRepair.Fix(method); break;
            case "System.Boolean ScenarioDealerStreetPersonSellDrugs/<>c__DisplayClass7_0::<ChoiceSelectedAtIndex>b__0(System.Collections.Generic.KeyValuePair`2<System.String,System.Single>)":
                DealerDrugLambdaMethodRepair.Fix(method); break;
            case "System.Void ScenarioDealerStreetPersonAttack::ChoiceSelectedAtIndex(System.Int32,System.Collections.ArrayList,System.Collections.ArrayList,System.Collections.ArrayList)":
                DealerMethodRepairs.FixAttackChoice(method); break;
            case "System.Void ScenarioDealerDealItem::ChoiceSelectedAtIndex(System.Int32,System.Collections.ArrayList,System.Collections.ArrayList,System.Collections.ArrayList)":
                DealerMethodRepairs.FixDealChoice(method); break;
            case "System.Void ScenarioDealerCartelMemberScapegoat::RefreshScenario()":
                DealerMethodRepairs.FixScapegoat(method); break;
            case "System.Void ScenarioDealerAgeUpTurfWar::RefreshScenario()":
                DealerMethodRepairs.FixTurfWar(method); break;
            case "SimSibling ScenarioSiblingSquabble::GetSquabbleSibling()":
                SiblingWildlifeMethodRepairs.FixSquabbleSibling(method); break;
            case "System.String LifeInterface::<GetWildlifeHandbookList>g__GetCountString|303_0(OUTDOOR_HANDBOOK_ENTRY_TYPE,LifeInterface/<>c__DisplayClass303_0&)":
                SiblingWildlifeMethodRepairs.FixWildlifeCountString(method); break;
            case "Game.DataFightIngStyleIdentityColor DataHub::GetFightClubStyleColorByStyleType(FIGHT_CLUB_FIGHTING_STYLE)":
                FixFightClubStyleColor(method); break;
            case "System.Void ScenarioLandlordRemodelOpportunity::ChoiceSelectedAtIndex(System.Int32)":
                LandlordMethodRepair.Fix(method); break;
            case "System.Void LifeInterface::<GetRacingTrackRecordList>g__AddTracksToList|318_0(RACING_TRACK_CATEGORY,LifeInterface/<>c__DisplayClass318_0&)":
                RacingTrackMethodRepair.Fix(method); break;
            case "System.String FightClub_NewFighterActivity::GetStyleColorString()":
                FighterTextureMethodRepairs.FixFighterColor(method); break;
            case "UnityEngine.Color32[] EmojiCelestialBodyCacheManager::RotateTexture(UnityEngine.Color32[],System.Single)":
                FighterTextureMethodRepairs.FixRotateTexture(method); break;
            case "System.Void EmojiCelestialBodyCacheManager::FlipTexture(UnityEngine.Color32[])":
                FlipTextureMethodRepair.Fix(method); break;
            case "System.Void LifeInterface::<GetRacingTrophyCaseList>g__CreateListItems|319_0(System.Collections.Generic.List`1<SimRacingCup>,System.String,LifeInterface/<>c__DisplayClass319_0&)":
                RacingTrophyMethodRepair.Fix(method); break;
            case "System.Boolean ScenarioItemPromiscuityPotionSideEffectsPushScenario::ScenarioIsEligibleForRandomPush()":
                NullableGameplayMethodRepairs.FixPotionEligibility(method); break;
            case "System.Void ScenarioHustleEarnings::RefreshScenario()":
                NullableGameplayMethodRepairs.FixHustleRefresh(method); break;
            case "System.Void ScenarioFriendStatusOutbound::ProcessFriendship(System.Collections.ArrayList)":
                NullableGameplayMethodRepairs.FixFriendshipProcess(method); break;
            case "System.Void ScenarioFriendHookup::ChoiceSelectedAtIndex(System.Int32)":
                NullableGameplayMethodRepairs.FixHookupChoice(method); break;
            case "System.Collections.ArrayList ScenarioStreetPersonSerenade::BuildPlayableInstrumentArray()":
                SerenadeMethodRepairs.FixBuild(method); break;
            case "System.Void ScenarioStreetPersonSerenade::ChoiceSelectedAtIndex(System.Int32,System.Collections.ArrayList,System.Collections.ArrayList,System.Collections.ArrayList)":
                SerenadeMethodRepairs.FixChoice(method); break;
            case "System.Void ScenarioMilitaryTermination::ChoiceSelectedAtIndex(System.Int32)":
                MilitaryMethodRepair.Fix(method); break;
            case "System.Void ScenarioMafiaWitnessProtectionProgram::ChoiceSelectedAtIndex(System.Int32)":
                MafiaMusicMethodRepairs.FixMafiaChoice(method); break;
            case "System.String DataHub::GetMusicTalentScoutVenueName(System.String)":
                MafiaMusicMethodRepairs.FixMusicVenue(method); break;
            case "System.Int32 OutdoorCaveSpelunkActivityData::GetButtonIndex(System.String)":
                OutdoorMethodRepairs.FixCaveButtonIndex(method); break;
            case "Game.DataOutdoorAnimal DataHub::GetRandomOutdoorAnimal(System.String)":
                OutdoorMethodRepairs.FixRandomOutdoorAnimal(method); break;
            case "Game.DataOutdoorAnimalTreat DataHub::GetRandomOutdoorAnimalTreatForDangerScore(System.Int32)":
                OutdoorAnimalTreatMethodRepair.FixRandomOutdoorAnimalTreat(method); break;
            case "System.Void ScenarioMediaOnlyFansCrossPromote::ChoiceSelectedAtIndex(System.Int32,System.Collections.ArrayList,System.Collections.ArrayList,System.Collections.ArrayList)":
                MediaFashionMethodRepairs.FixOnlyFansChoice(method); break;
            case "System.Void ScenarioModelFashionWeekSummary::ChoiceSelectedAtIndex(System.Int32)":
                MediaFashionMethodRepairs.FixFashionChoice(method); break;
            case "System.Void ScenarioParanormalEncounter::ChoiceSelectedAtIndex(System.Int32)":
                ParanormalMethodRepairs.FixParanormal(method); break;
            case "System.Void ScenarioGhostEncounter::ChoiceSelectedAtIndex(System.Int32)":
                ParanormalMethodRepairs.FixGhost(method); break;
            case "System.Collections.ArrayList ScenarioBusinessSellCompany::SellPriceArrayForBusiness()":
                FixBusinessSellPrices(method); break;
            case "System.Void ScenarioBusinessProductProfile::RefreshScenario()":
                PatchProductProfileKeyValuePairs(method); break;
            case "System.Void MiniGameCardFront::InitWithParameters(GGS.Util.Either`2<MiniGameCard/NormalCard,MiniGameCard/Joker>)":
                CardMethodRepair.FixCardFront(method); break;
            case "System.Void ScenarioCasinoBet::.ctor(SimCasinoSession)":
                CasinoMethodRepairs.PatchBet(method); break;
            case "System.Void ScenarioCasinoCelebrityMeetAndGreetPickGameRoom::ChoiceSelectedAtIndex(System.Int32,System.Collections.ArrayList,System.Collections.ArrayList,System.Collections.ArrayList)":
                CasinoMethodRepairs.FixMeetChoice(method); break;
            case "System.Void ScenarioCasinoCelebrityPartyPickGameRoom::RefreshScenario()":
                CasinoMethodRepairs.FixPartyRoom(method); break;
            case "System.Void ScenarioCasinoAgeUpGamblerLost::RefreshScenario()":
                CasinoMethodRepairs.FixAgeUpGamblerLost(method); break;
            case "SimDelinquentAccomplice ScenarioCrimeDelinquencyPush::GetAccomplice()":
                CrimeMuseumMethodRepairs.FixAccomplice(method); break;
            case "System.Collections.ArrayList LifeInterface::GetBlackMarketMuseumWingList(SimMuseum,System.Int32,System.Int32)":
                CrimeMuseumMethodRepairs.FixMuseumWing(method); break;
            case "System.Collections.ArrayList LifeInterface::GetBelongingsArtifactItemsToAddDisplay(SimMuseumWing,SimDisplayItem,System.Int32)":
                MuseumArtifactMethodRepair.Fix(method); break;
            case "UnityEngine.Color32[] EmojiCelestialBodyCacheManager::CreateStarComposite(System.String,UnityEngine.Color)":
                StarMethodRepair.Fix(method); break;
            case "System.Void EmojiCelestialBodyCacheManager::CombineTextures(UnityEngine.Color32[],UnityEngine.Color32[],UnityEngine.Vector2,UnityEngine.Vector2,UnityEngine.Color32,System.Boolean,System.Boolean)":
                StarCombineMethodRepair.Fix(method); break;
            case "System.ValueTuple`2<SimPerson,System.String> ScenarioCultRelationshipBegsHeroToLeave::GetPersonFromHeroLife()":
                CultMethodRepairs.FixRelationshipPerson(method); break;
            case "System.Boolean ScenarioCultRelationshipBegsHeroToLeave::ScenarioIsEligibleForRandomPush()":
                CultMethodRepairs.FixRelationshipEligibility(method); break;
            case "System.Boolean ScenarioPetFind::ScenarioIsEligibleForRandomPush()":
                EligibilityHandleMethodRepairs.FixPetFindEligibility(method); break;
            case "System.Boolean ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush()":
                EligibilityHandleMethodRepairs.FixSexualityEligibility(method); break;
            case "System.Void ScenarioCultDoctrineSetup::RefreshScenario()":
                CultMethodRepairs.FixDoctrineSetup(method); break;
            case "System.Void ScenarioCultCommunePrisonTeach::ChoiceSelectedAtIndex(System.Int32,System.Collections.ArrayList,System.Collections.ArrayList,System.Collections.ArrayList)":
                CultMethodRepairs.FixPrisonTeach(method); break;
            case "T OPS.Serialization.IO.Serializer::DeSerialize(System.Byte[],System.Boolean,System.Boolean,System.String)":
            case "T OPS.Serialization.IO.Serializer::DeSerializeFromStream(System.IO.Stream,System.Boolean,System.Boolean,System.String)":
                FixGenericDeserialize(method); break;
            case "T BitLife.UI.Managers.UIManager::CreateSupportObject(BitLife.UI.Interfaces.IguiOptions,UnityEngine.Transform)":
                FixCreateSupportObject(method); break;
            case "System.Void Game.GameVars::LoadOrDefault(System.String,System.Collections.Generic.Dictionary`2<System.String,System.Object>,T,System.Action`1<T>)":
                FixLoadOrDefault(method); break;
            case "T ExList::GetOrDefault(System.Collections.Generic.IList`1<T>,System.Int32,T)":
                FixGetOrDefault(method); break;
            case "System.Void CandyBusiness.Confidence.ConfidenceUtilities::SafeInvoke(System.Action`1<T>,T,System.String)":
            case "System.Void CandyBusiness.Confidence.ConfidenceUtilities::SafeInvoke(System.Action`2<T1,T2>,T1,T2,System.String)":
                FixSafeInvoke(method); break;
            case "T2 Genesis.Services.GenesisFilterProcessing::CheckCollectionSize(T1,Game.Genesis.Components.GenesisFilterModel,System.Boolean&)":
                FixCheckCollectionSize(method); break;
            case "T2 Genesis.Services.GenesisFilterProcessing::CheckIfExistsAndWithDataInArray(T1,Game.Genesis.Components.GenesisFilterModel)":
                FixCheckArray(method); break;
            case "TResult Genesis.SerializableLists.GenesisUtils::CallStaticMethod(Genesis.SerializableLists.StaticCall)":
                FixCallStaticMethod(method); break;
            case "System.Void DG.Tweening.DOTweenModuleUtils/Physics::SetOrientationOnPath(DG.Tweening.Plugins.Options.PathOptions,DG.Tweening.Tween,UnityEngine.Quaternion,UnityEngine.Transform)":
                FixSetOrientationOnPath(method); break;
            case "T ArrayExtension::GetRandomElement(System.Collections.Generic.List`1<T>)":
                FixGetRandomElement(method); break;
            case "System.Void ScenarioProfileGrandchild::RefreshScenario()":
            case "System.Void ScenarioProfileNieceNephew::RefreshScenario()":
            case "System.Void ScenarioProfileLover::RefreshScenario()":
            case "System.Void ScenarioProfileSibling::RefreshScenario()":
            case "DataActorReal DataHub::GetRandomRealActor(System.String,System.Int32)":
            case "System.String VoltronManager::GetGenderSwapDataForData(System.String)":
                StructComparisonMethodRepairs.FixStructComparison(method); break;
            case "SimRaceCarDriver ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)":
                RacingCupMethodRepairs.FixGetNpcWinningDriver(method); break;
            case "System.Void ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario()":
                RacingCupScenarioMethodRepairs.FixStreetRefreshScenario(method); break;
            case "System.Void ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()":
                RacingCupScenarioMethodRepairs.FixCircuitRefreshScenario(method); break;
            case "System.Void ScenarioRacingCupSeriesDemolitionRaceResults::RefreshScenario()":
                RacingCupScenarioMethodRepairs.FixDemolitionRefreshScenario(method); break;
            case "System.Int32 RacingCupSeriesActiveRace::GetPlayerTotalPoints()":
                RacingCupActiveRaceMethodRepairs.FixGetPlayerTotalPoints(method); break;
            case "System.TimeSpan RacingCupSeriesActiveRace::GetPlayerTotalTime()":
                RacingCupActiveRaceMethodRepairs.FixGetPlayerTotalTime(method); break;
            case "System.Int32 RacingCupSeriesActiveRace::GetRacerTotalPoints(SimRaceCarDriver)":
                RacingCupActiveRaceMethodRepairs.FixGetRacerTotalPoints(method); break;
            case "System.TimeSpan RacingCupSeriesActiveRace::GetRacerTotalTime(SimRaceCarDriver)":
                RacingCupActiveRaceMethodRepairs.FixGetRacerTotalTime(method); break;
            case "System.Int32 RacingCupSeriesActiveRace::GetCurrentPlacement()":
                RacingCupActiveRaceMethodRepairs.FixGetCurrentPlacement(method); break;
            case "System.Boolean MoreMountains.NiceVibrations.MMVibrationManager::HapticsSupported()":
                ReturnConstant(method, false); break;
            default: continue;
        }
        var invalid = ValidateBody(method);
        if (invalid != null)
        {
            Console.Error.WriteLine($"INVALID BODY {method.FullName}: {invalid}");
            Console.Error.WriteLine("Refusing to write the assembly (this body would crash IL2CPP).");
            Environment.Exit(3);
        }
        repaired.Add(method.FullName);
        changed = true;
        Console.WriteLine($"Reconstructed {method.FullName}");
    }
    if (!changed) continue;
    var temporary = path + ".method-fix.tmp";
    assembly.Write(temporary);
    File.Move(temporary, path, true);
}
Console.WriteLine($"Reconstructed {repaired.Count} methods");
return repaired.Count == 142 ? 0 : 1;

static MethodBody FreshBody(MethodDefinition method)
{
    var body = new MethodBody(method);
    method.Body = body;
    return body;
}

static string? ValidateBody(MethodDefinition method)
{
    var body = method.Body;
    if (body == null) return null;
    var instructions = body.Instructions;
    var known = new HashSet<Instruction>(instructions);
    var argumentLimit = method.HasThis ? method.Parameters.Count + 1 : method.Parameters.Count;
    var localCount = body.Variables.Count;
    foreach (var instruction in instructions)
    {
        var code = instruction.OpCode.Code;
        var argumentIndex = -1;
        switch (code)
        {
            case Code.Ldarg_0: argumentIndex = 0; break;
            case Code.Ldarg_1: argumentIndex = 1; break;
            case Code.Ldarg_2: argumentIndex = 2; break;
            case Code.Ldarg_3: argumentIndex = 3; break;
            case Code.Ldarg:
            case Code.Ldarga:
            case Code.Starg:
            case Code.Ldarg_S:
            case Code.Ldarga_S:
            case Code.Starg_S:
                argumentIndex = instruction.Operand is ParameterDefinition parameter ? parameter.Index
                    : instruction.Operand is int rawArgument ? rawArgument : -2;
                break;
        }
        if (argumentIndex >= argumentLimit)
            return $"{instruction} reads argument {argumentIndex} but the method only has {argumentLimit}";
        if (argumentIndex == -2)
            return $"{instruction} has an argument operand that is not a parameter";

        var localIndex = -1;
        switch (code)
        {
            case Code.Ldloc_0: localIndex = 0; break;
            case Code.Ldloc_1: localIndex = 1; break;
            case Code.Ldloc_2: localIndex = 2; break;
            case Code.Ldloc_3: localIndex = 3; break;
            case Code.Ldloc:
            case Code.Ldloca:
            case Code.Stloc:
            case Code.Ldloc_S:
            case Code.Ldloca_S:
            case Code.Stloc_S:
                localIndex = instruction.Operand is VariableDefinition variable ? variable.Index
                    : instruction.Operand is int rawLocal ? rawLocal : -2;
                break;
        }
        if (localIndex >= localCount)
            return $"{instruction} reads local {localIndex} but the body only declares {localCount}";
        if (localIndex == -2)
            return $"{instruction} has a local operand that is not a variable";

        switch (instruction.OpCode.OperandType)
        {
            case OperandType.InlineBrTarget:
            case OperandType.ShortInlineBrTarget:
                if (instruction.Operand is not Instruction branchTarget || !known.Contains(branchTarget))
                    return $"{instruction} branches outside the body";
                break;
            case OperandType.InlineSwitch:
                if (instruction.Operand is Instruction[] switchTargets)
                    foreach (var switchTarget in switchTargets)
                        if (!known.Contains(switchTarget))
                            return $"{instruction} has a switch target outside the body";
                break;
        }
    }
    foreach (var handler in body.ExceptionHandlers)
    {
        foreach (var boundary in new[] { handler.TryStart, handler.TryEnd, handler.HandlerStart, handler.HandlerEnd })
            if (boundary != null && !known.Contains(boundary))
                return $"exception handler boundary {boundary} is outside the body";
    }
    return null;
}

static void ReturnVoid(MethodDefinition method)
{
    FreshBody(method).Instructions.Add(Instruction.Create(OpCodes.Ret));
}

static void ReturnConstant(MethodDefinition method, bool value)
{
    var il = FreshBody(method).Instructions;
    il.Add(Instruction.Create(value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void ReturnArgument(MethodDefinition method, int index)
{
    var il = FreshBody(method).Instructions;
    il.Add(Instruction.Create(OpCodes.Ldarg, method.Parameters[method.HasThis ? index - 1 : index]));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void ReturnDefault(MethodDefinition method)
{
    var body = FreshBody(method);
    body.InitLocals = true;
    var local = new VariableDefinition(method.ReturnType);
    body.Variables.Add(local);
    var il = body.Instructions;
    il.Add(Instruction.Create(OpCodes.Ldloca_S, local));
    il.Add(Instruction.Create(OpCodes.Initobj, method.ReturnType));
    il.Add(Instruction.Create(OpCodes.Ldloc, local));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void ReturnNewObject(MethodDefinition method)
{
    var il = FreshBody(method).Instructions;
    var constructors = method.ReturnType.Resolve().Methods.Where(candidate => candidate.Name == ".ctor").ToArray();
    var constructor = constructors.SingleOrDefault(candidate => candidate.Parameters.Count == 0);
    if (constructor is null)
    {
        constructor = constructors.Single(candidate => candidate.Parameters.Count == 2 && candidate.Parameters[0].ParameterType.FullName == "System.String" && candidate.Parameters[1].ParameterType.FullName == "System.Collections.ArrayList");
        il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
        il.Add(Instruction.Create(OpCodes.Newobj, ImportFrameworkMethod(method.Module, "System.Collections.ArrayList", ".ctor", 0, true)));
    }
    il.Add(Instruction.Create(OpCodes.Newobj, method.Module.ImportReference(constructor)));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void ReturnEmptyArray(MethodDefinition method)
{
    var il = FreshBody(method).Instructions;
    var arrayType = (ArrayType)method.ReturnType;
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Newarr, arrayType.ElementType));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixGetPropertyValue(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    var value = new VariableDefinition(module.TypeSystem.Object);
    var converted = new VariableDefinition(method.GenericParameters[0]);
    body.Variables.Add(value);
    body.Variables.Add(converted);
    var il = body.Instructions;
    var done = Instruction.Create(OpCodes.Nop);
    var flags = method.DeclaringType.Fields.Single(field => field.Name == "<flags>k__BackingField");
    var tryGet = ImportMethod(module, (GenericInstanceType)flags.FieldType, "TryGetValue", 2);
    var tryConvertDefinition = method.DeclaringType.Methods.Single(candidate => candidate.Name == "TryConvertCachedValue" && candidate.HasGenericParameters);
    var tryConvert = new GenericInstanceMethod(module.ImportReference(tryConvertDefinition));
    tryConvert.GenericArguments.Add(method.GenericParameters[0]);
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Stloc, converted));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, flags));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, flags));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, value));
    il.Add(Instruction.Create(OpCodes.Callvirt, tryGet));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldloc, value));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, converted));
    il.Add(Instruction.Create(OpCodes.Call, tryConvert));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ldloc, converted));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixGenericDeserialize(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    var il = body.Instructions;
    var genericParameter = method.GenericParameters[0];
    var deserialize = method.DeclaringType.Methods.Single(candidate => candidate.Name == "DeSerialize" && !candidate.HasGenericParameters && candidate.Parameters.Count == 5 && candidate.Parameters[0].ParameterType.FullName == "System.Type");
    il.Add(Instruction.Create(OpCodes.Ldtoken, genericParameter));
    il.Add(Instruction.Create(OpCodes.Call, ImportFrameworkMethod(module, "System.Type", "GetTypeFromHandle", 1)));
    var firstArgument = 0;
    if (method.Name == "DeSerializeFromStream")
    {
        var memoryStreamConstructor = ImportFrameworkMethod(module, "System.IO.MemoryStream", ".ctor", 0, true);
        var memoryStream = new VariableDefinition(memoryStreamConstructor.DeclaringType);
        body.Variables.Add(memoryStream);
        il.Add(Instruction.Create(OpCodes.Newobj, memoryStreamConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, memoryStream));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, memoryStream));
        il.Add(Instruction.Create(OpCodes.Callvirt, ImportFrameworkMethod(module, "System.IO.Stream", "CopyTo", 1, true)));
        il.Add(Instruction.Create(OpCodes.Ldloc, memoryStream));
        il.Add(Instruction.Create(OpCodes.Callvirt, ImportFrameworkMethod(module, "System.IO.MemoryStream", "ToArray", 0, true)));
        firstArgument = 1;
    }
    for (var index = firstArgument; index < 4; index++) il.Add(Instruction.Create(OpCodes.Ldarg, method.Parameters[index]));
    il.Add(Instruction.Create(OpCodes.Call, deserialize));
    il.Add(Instruction.Create(OpCodes.Unbox_Any, genericParameter));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixCreateSupportObject(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    var index = new VariableDefinition(module.TypeSystem.Int32);
    body.Variables.Add(index);
    var il = body.Instructions;
    var table = method.DeclaringType.Fields.Single(field => field.Name == "supportObjectTable");
    var prefabs = method.DeclaringType.Fields.Single(field => field.Name == "supportObjectPrefabs");
    var getItem = ImportMethod(module, table.FieldType, "get_Item", 1);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, table));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Call, ImportFrameworkMethod(module, "System.Object", "GetType", 0, true)));
    il.Add(Instruction.Create(OpCodes.Callvirt, getItem));
    il.Add(Instruction.Create(OpCodes.Unbox_Any, module.TypeSystem.Int32));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, prefabs));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    var gameObjectType = module.ImportReference(prefabs.FieldType.GetElementType());
    il.Add(Instruction.Create(OpCodes.Ldelem_Any, gameObjectType));
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    var unityObject = module.ImportReference(gameObjectType.Resolve().BaseType);
    var instantiateDefinition = unityObject.Resolve().Methods.Single(candidate => candidate.Name == "Instantiate" && !candidate.HasGenericParameters && candidate.Parameters.Count == 2 && candidate.Parameters[1].ParameterType.FullName == "UnityEngine.Transform");
    il.Add(Instruction.Create(OpCodes.Call, module.ImportReference(instantiateDefinition)));
    il.Add(Instruction.Create(OpCodes.Castclass, gameObjectType));
    var getComponentDefinition = gameObjectType.Resolve().Methods.Single(candidate => candidate.Name == "GetComponent" && candidate.HasGenericParameters && candidate.Parameters.Count == 0);
    var getComponent = new GenericInstanceMethod(module.ImportReference(getComponentDefinition));
    getComponent.GenericArguments.Add(method.GenericParameters[0]);
    il.Add(Instruction.Create(OpCodes.Call, getComponent));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixLoadOrDefault(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    var value = new VariableDefinition(module.TypeSystem.Object);
    body.Variables.Add(value);
    var il = body.Instructions;
    var useDefault = Instruction.Create(OpCodes.Nop);
    var done = Instruction.Create(OpCodes.Nop);
    var dictionaryType = (GenericInstanceType)method.Parameters[1].ParameterType;
    var actionType = (GenericInstanceType)method.Parameters[3].ParameterType;
    var tryGet = ImportMethod(module, dictionaryType, "TryGetValue", 2);
    var invoke = ImportMethod(module, actionType, "Invoke", 1);
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Brfalse, useDefault));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, value));
    il.Add(Instruction.Create(OpCodes.Callvirt, tryGet));
    il.Add(Instruction.Create(OpCodes.Brfalse, useDefault));
    il.Add(Instruction.Create(OpCodes.Ldarg_3));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_3));
    il.Add(Instruction.Create(OpCodes.Ldloc, value));
    il.Add(Instruction.Create(OpCodes.Unbox_Any, method.GenericParameters[0]));
    il.Add(Instruction.Create(OpCodes.Callvirt, invoke));
    il.Add(Instruction.Create(OpCodes.Br, done));
    il.Add(useDefault);
    il.Add(Instruction.Create(OpCodes.Ldarg_3));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_3));
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Callvirt, invoke));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixGetOrDefault(MethodDefinition method)
{
    var module = method.Module;
    var il = FreshBody(method).Instructions;
    var useDefault = Instruction.Create(OpCodes.Nop);
    var listType = (GenericInstanceType)method.Parameters[0].ParameterType;
    var getCount = ImportMethod(module, listType, "get_Count", 0);
    var getItem = ImportMethod(module, listType, "get_Item", 1);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Brfalse, useDefault));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Blt, useDefault));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
    il.Add(Instruction.Create(OpCodes.Bge, useDefault));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Callvirt, getItem));
    il.Add(Instruction.Create(OpCodes.Ret));
    il.Add(useDefault);
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixSafeInvoke(MethodDefinition method)
{
    var module = method.Module;
    var il = FreshBody(method).Instructions;
    var done = Instruction.Create(OpCodes.Nop);
    var valueParameterCount = method.Parameters.Count - 2;
    var invoke = ImportMethod(module, (GenericInstanceType)method.Parameters[0].ParameterType, "Invoke", valueParameterCount);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    for (var index = 1; index <= valueParameterCount; index++) il.Add(Instruction.Create(OpCodes.Ldarg, method.Parameters[index]));
    il.Add(Instruction.Create(OpCodes.Callvirt, invoke));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixCheckCollectionSize(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var listType = module.ImportReference(new TypeReference("System.Collections", "IList", module, module.TypeSystem.Object.Scope));
    var list = new VariableDefinition(listType);
    var size = new VariableDefinition(module.TypeSystem.Int32);
    var result = new VariableDefinition(method.GenericParameters[1]);
    body.Variables.Add(list);
    body.Variables.Add(size);
    body.Variables.Add(result);
    var il = body.Instructions;
    var checkParsedSize = Instruction.Create(OpCodes.Nop);
    var returnRandom = Instruction.Create(OpCodes.Nop);
    var done = Instruction.Create(OpCodes.Nop);
    var data = method.Parameters[1].ParameterType.Resolve().Fields.Single(field => field.Name == "<Data>k__BackingField");
    var getCount = ImportMethod(module, listType, "get_Count", 0);
    var tryParse = module.ImportReference(module.TypeSystem.Int32.Resolve().Methods.Single(candidate => candidate.Name == "TryParse" && candidate.IsStatic && candidate.Parameters.Count == 2 && candidate.Parameters[0].ParameterType.FullName == "System.String" && candidate.Parameters[1].ParameterType.FullName == "System.Int32&"));
    var randomDefinition = module.Types.Single(type => type.Name == "ExList").Methods.Single(candidate => candidate.Name == "GetRandom" && candidate.HasGenericParameters && candidate.Parameters.Count == 1 && candidate.Parameters[0].ParameterType.FullName == "System.Collections.IList");
    var random = new GenericInstanceMethod(module.ImportReference(randomDefinition));
    random.GenericArguments.Add(method.GenericParameters[1]);
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Stind_I1));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, result));
    il.Add(Instruction.Create(OpCodes.Initobj, method.GenericParameters[1]));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Box, method.GenericParameters[0]));
    il.Add(Instruction.Create(OpCodes.Castclass, listType));
    il.Add(Instruction.Create(OpCodes.Stloc, list));
    il.Add(Instruction.Create(OpCodes.Ldloc, list));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldfld, data));
    il.Add(Instruction.Create(OpCodes.Call, ImportFrameworkMethod(module, "System.String", "IsNullOrWhiteSpace", 1)));
    il.Add(Instruction.Create(OpCodes.Brfalse, checkParsedSize));
    il.Add(Instruction.Create(OpCodes.Ldloc, list));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Blt, done));
    il.Add(Instruction.Create(OpCodes.Br, returnRandom));
    il.Add(checkParsedSize);
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldfld, data));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, size));
    il.Add(Instruction.Create(OpCodes.Call, tryParse));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldloc, list));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
    il.Add(Instruction.Create(OpCodes.Ldloc, size));
    il.Add(Instruction.Create(OpCodes.Ceq));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(returnRandom);
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Stind_I1));
    il.Add(Instruction.Create(OpCodes.Ldloc, list));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Stloc, result));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ldloc, result));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixCallStaticMethod(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var typeType = module.ImportReference(new TypeReference("System", "Type", module, module.TypeSystem.Object.Scope));
    var methodInfoType = module.ImportReference(new TypeReference("System.Reflection", "MethodInfo", module, module.TypeSystem.Object.Scope));
    var targetType = new VariableDefinition(typeType);
    var targetMethod = new VariableDefinition(methodInfoType);
    var arguments = new VariableDefinition(new ArrayType(module.TypeSystem.Object));
    var index = new VariableDefinition(module.TypeSystem.Int32);
    var result = new VariableDefinition(method.GenericParameters[0]);
    body.Variables.Add(targetType);
    body.Variables.Add(targetMethod);
    body.Variables.Add(arguments);
    body.Variables.Add(index);
    body.Variables.Add(result);
    var il = body.Instructions;
    var invoke = Instruction.Create(OpCodes.Nop);
    var loop = Instruction.Create(OpCodes.Nop);
    var checkLoop = Instruction.Create(OpCodes.Nop);
    var done = Instruction.Create(OpCodes.Nop);
    var callType = method.Parameters[0].ParameterType.Resolve();
    var className = callType.Fields.Single(field => field.Name == "ClassName");
    var methodName = callType.Fields.Single(field => field.Name == "MethodName");
    var parameters = callType.Fields.Single(field => field.Name == "Parameters");
    var parameterType = parameters.FieldType.GetElementType();
    var getValue = ImportMethod(module, parameterType, "GetParamValue", 0);
    var getType = ImportFrameworkMethod(module, "System.Type", "GetType", 1);
    var getMethod = module.ImportReference(typeType.Resolve().Methods.Single(candidate => candidate.Name == "GetMethod" && candidate.Parameters.Count == 2 && candidate.Parameters[0].ParameterType.FullName == "System.String" && candidate.Parameters[1].ParameterType.FullName == "System.Reflection.BindingFlags"));
    var invokeMethod = ImportFrameworkMethod(module, "System.Reflection.MethodBase", "Invoke", 2, true);
    il.Add(Instruction.Create(OpCodes.Ldloca_S, result));
    il.Add(Instruction.Create(OpCodes.Initobj, method.GenericParameters[0]));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, className));
    il.Add(Instruction.Create(OpCodes.Call, getType));
    il.Add(Instruction.Create(OpCodes.Stloc, targetType));
    il.Add(Instruction.Create(OpCodes.Ldloc, targetType));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldloc, targetType));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, methodName));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 24));
    il.Add(Instruction.Create(OpCodes.Callvirt, getMethod));
    il.Add(Instruction.Create(OpCodes.Stloc, targetMethod));
    il.Add(Instruction.Create(OpCodes.Ldloc, targetMethod));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, parameters));
    il.Add(Instruction.Create(OpCodes.Brfalse, invoke));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, parameters));
    il.Add(Instruction.Create(OpCodes.Ldlen));
    il.Add(Instruction.Create(OpCodes.Conv_I4));
    il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.Object));
    il.Add(Instruction.Create(OpCodes.Stloc, arguments));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(Instruction.Create(OpCodes.Br, checkLoop));
    il.Add(loop);
    il.Add(Instruction.Create(OpCodes.Ldloc, arguments));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, parameters));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldelem_Ref));
    il.Add(Instruction.Create(OpCodes.Callvirt, getValue));
    il.Add(Instruction.Create(OpCodes.Stelem_Ref));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(checkLoop);
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, parameters));
    il.Add(Instruction.Create(OpCodes.Ldlen));
    il.Add(Instruction.Create(OpCodes.Conv_I4));
    il.Add(Instruction.Create(OpCodes.Blt, loop));
    il.Add(invoke);
    il.Add(Instruction.Create(OpCodes.Ldloc, targetMethod));
    il.Add(Instruction.Create(OpCodes.Ldnull));
    il.Add(Instruction.Create(OpCodes.Ldloc, arguments));
    il.Add(Instruction.Create(OpCodes.Callvirt, invokeMethod));
    il.Add(Instruction.Create(OpCodes.Unbox_Any, method.GenericParameters[0]));
    il.Add(Instruction.Create(OpCodes.Stloc, result));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ldloc, result));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixCheckArray(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var listType = module.ImportReference(new TypeReference("System.Collections", "IList", module, module.TypeSystem.Object.Scope));
    var list = new VariableDefinition(listType);
    var wasNumberOrEmpty = new VariableDefinition(module.TypeSystem.Boolean);
    var result = new VariableDefinition(method.GenericParameters[1]);
    var arrayListConstructor = ImportFrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
    var matches = new VariableDefinition(arrayListConstructor.DeclaringType);
    var index = new VariableDefinition(module.TypeSystem.Int32);
    var item = new VariableDefinition(module.TypeSystem.Object);
    body.Variables.Add(list);
    body.Variables.Add(wasNumberOrEmpty);
    body.Variables.Add(result);
    body.Variables.Add(matches);
    body.Variables.Add(index);
    body.Variables.Add(item);
    var il = body.Instructions;
    var checkWasNumberOrEmpty = Instruction.Create(OpCodes.Nop);
    var loop = Instruction.Create(OpCodes.Nop);
    var next = Instruction.Create(OpCodes.Nop);
    var checkLoop = Instruction.Create(OpCodes.Nop);
    var done = Instruction.Create(OpCodes.Nop);
    var getItem = ImportMethod(module, listType, "get_Item", 1);
    var getCount = ImportMethod(module, listType, "get_Count", 0);
    var add = ImportFrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
    var matchesCount = ImportMethod(module, arrayListConstructor.DeclaringType, "get_Count", 0);
    var checkSize = new GenericInstanceMethod(module.ImportReference(method.DeclaringType.Methods.Single(candidate => candidate.Name == "CheckCollectionSize" && candidate.HasGenericParameters)));
    checkSize.GenericArguments.Add(method.GenericParameters[0]);
    checkSize.GenericArguments.Add(method.GenericParameters[1]);
    var checkExists = method.DeclaringType.Methods.Single(candidate => candidate.Name == "CheckIfExistsAndWithData" && candidate.Parameters.Count == 2 && candidate.Parameters[0].ParameterType.FullName == "System.Object");
    var randomDefinition = module.Types.Single(type => type.Name == "ExList").Methods.Single(candidate => candidate.Name == "GetRandom" && candidate.HasGenericParameters && candidate.Parameters.Count == 1 && candidate.Parameters[0].ParameterType.FullName == "System.Collections.IList");
    var random = new GenericInstanceMethod(module.ImportReference(randomDefinition));
    random.GenericArguments.Add(method.GenericParameters[1]);
    il.Add(Instruction.Create(OpCodes.Ldloca_S, result));
    il.Add(Instruction.Create(OpCodes.Initobj, method.GenericParameters[1]));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Box, method.GenericParameters[0]));
    il.Add(Instruction.Create(OpCodes.Castclass, listType));
    il.Add(Instruction.Create(OpCodes.Stloc, list));
    il.Add(Instruction.Create(OpCodes.Ldloc, list));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, wasNumberOrEmpty));
    il.Add(Instruction.Create(OpCodes.Call, checkSize));
    il.Add(Instruction.Create(OpCodes.Stloc, result));
    il.Add(Instruction.Create(OpCodes.Ldloc, result));
    il.Add(Instruction.Create(OpCodes.Box, method.GenericParameters[1]));
    il.Add(Instruction.Create(OpCodes.Brfalse, checkWasNumberOrEmpty));
    il.Add(Instruction.Create(OpCodes.Br, done));
    il.Add(checkWasNumberOrEmpty);
    il.Add(Instruction.Create(OpCodes.Ldloc, wasNumberOrEmpty));
    il.Add(Instruction.Create(OpCodes.Brtrue, done));
    il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, matches));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(Instruction.Create(OpCodes.Br, checkLoop));
    il.Add(loop);
    il.Add(Instruction.Create(OpCodes.Ldloc, list));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Callvirt, getItem));
    il.Add(Instruction.Create(OpCodes.Stloc, item));
    il.Add(Instruction.Create(OpCodes.Ldloc, item));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Call, checkExists));
    il.Add(Instruction.Create(OpCodes.Brfalse, next));
    il.Add(Instruction.Create(OpCodes.Ldloc, matches));
    il.Add(Instruction.Create(OpCodes.Ldloc, item));
    il.Add(Instruction.Create(OpCodes.Callvirt, add));
    il.Add(Instruction.Create(OpCodes.Pop));
    il.Add(next);
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(checkLoop);
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldloc, list));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
    il.Add(Instruction.Create(OpCodes.Blt, loop));
    il.Add(Instruction.Create(OpCodes.Ldloc, matches));
    il.Add(Instruction.Create(OpCodes.Callvirt, matchesCount));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldloc, matches));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Stloc, result));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ldloc, result));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixSetOrientationOnPath(MethodDefinition method)
{
    var module = method.Module;
    var rigidbodyType = method.Body.Variables.Single(variable => variable.VariableType.FullName == "UnityEngine.Rigidbody").VariableType;
    var body = FreshBody(method);
    var rigidbody = new VariableDefinition(rigidbodyType);
    body.Variables.Add(rigidbody);
    var il = body.Instructions;
    var useTransform = Instruction.Create(OpCodes.Nop);
    var done = Instruction.Create(OpCodes.Nop);
    var isRigidbody = module.ImportReference(method.Parameters[0].ParameterType.Resolve().Fields.Single(field => field.Name == "isRigidbody"));
    var target = module.ImportReference(method.Parameters[1].ParameterType.Resolve().Fields.Single(field => field.Name == "target"));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, isRigidbody));
    il.Add(Instruction.Create(OpCodes.Brfalse, useTransform));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldfld, target));
    il.Add(Instruction.Create(OpCodes.Castclass, rigidbodyType));
    il.Add(Instruction.Create(OpCodes.Stloc, rigidbody));
    il.Add(Instruction.Create(OpCodes.Ldloc, rigidbody));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldloc, rigidbody));
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Callvirt, ImportMethod(module, rigidbodyType, "set_rotation", 1)));
    il.Add(Instruction.Create(OpCodes.Br, done));
    il.Add(useTransform);
    il.Add(Instruction.Create(OpCodes.Ldarg_3));
    il.Add(Instruction.Create(OpCodes.Brfalse, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_3));
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Callvirt, ImportMethod(module, method.Parameters[3].ParameterType, "set_rotation", 1)));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixGetRandomElement(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    var defaultValue = new VariableDefinition(method.GenericParameters[0]);
    body.Variables.Add(defaultValue);
    body.InitLocals = true;
    var il = body.Instructions;
    var useDefault = Instruction.Create(OpCodes.Nop);
    var listType = (GenericInstanceType)method.Parameters[0].ParameterType;
    var getCount = ImportMethod(module, listType, "get_Count", 0);
    var getItem = ImportMethod(module, listType, "get_Item", 1);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Brfalse, useDefault));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
    il.Add(Instruction.Create(OpCodes.Brfalse, useDefault));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
    var unityScope = method.Module.AssemblyReferences.Single(reference => reference.Name == "UnityEngine.CoreModule");
    var randomType = new TypeReference("UnityEngine", "Random", module, unityScope);
    var rangeDefinition = randomType.Resolve().Methods.Single(candidate => candidate.Name == "Range" && candidate.Parameters.Count == 2 && candidate.Parameters[0].ParameterType.FullName == "System.Int32");
    var range = module.ImportReference(rangeDefinition);
    il.Add(Instruction.Create(OpCodes.Call, range));
    il.Add(Instruction.Create(OpCodes.Callvirt, getItem));
    il.Add(Instruction.Create(OpCodes.Ret));
    il.Add(useDefault);
    il.Add(Instruction.Create(OpCodes.Ldloca_S, defaultValue));
    il.Add(Instruction.Create(OpCodes.Initobj, method.GenericParameters[0]));
    il.Add(Instruction.Create(OpCodes.Ldloc, defaultValue));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void PatchBirthDateComparison(MethodDefinition method)
{
    var instructions = method.Body.Instructions;
    var comparison = instructions.SingleOrDefault(instruction => instruction.Offset == 0x4b51 && instruction.OpCode == OpCodes.Ceq);
    if (comparison is null) return;
    var index = instructions.IndexOf(comparison);
    comparison.OpCode = OpCodes.Pop;
    comparison.Operand = null;
    instructions.Insert(index + 1, Instruction.Create(OpCodes.Pop));
    instructions.Insert(index + 2, Instruction.Create(OpCodes.Ldc_I4_0));
}

static void FixRandomDeckDraw(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var decks = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "_decks"));
    var deckType = ((GenericInstanceType)decks.FieldType).GenericArguments[1];
    var deckDefinition = deckType.Resolve();
    var compute = deckDefinition.Methods.Single(candidate => candidate.Name == "ComputeDeckParameters");
    var constructor = module.ImportReference(deckDefinition.Methods.Single(candidate => candidate.Name == ".ctor" && candidate.Parameters.Count == 2));
    var shuffle = module.ImportReference(deckDefinition.Methods.Single(candidate => candidate.Name == "ShuffleAll" && candidate.Parameters.Count == 0));
    var indexField = module.ImportReference(deckDefinition.Fields.Single(field => field.Name == "_index"));
    var deckSizeField = module.ImportReference(deckDefinition.Fields.Single(field => field.Name == "_deckSize"));
    var cardsField = module.ImportReference(deckDefinition.Fields.Single(field => field.Name == "_cards"));
    var cardsType = (GenericInstanceType)cardsField.FieldType;
    var tupleType = (GenericInstanceType)compute.ReturnType;
    var item1 = module.ImportReference(tupleType.Resolve().Fields.Single(field => field.Name == "Item1"));
    item1.DeclaringType = tupleType;
    var item2 = module.ImportReference(tupleType.Resolve().Fields.Single(field => field.Name == "Item2"));
    item2.DeclaringType = tupleType;
    var tryGet = ImportMethod(module, (GenericInstanceType)decks.FieldType, "TryGetValue", 2);
    var setItem = ImportMethod(module, (GenericInstanceType)decks.FieldType, "set_Item", 2);
    var getCard = ImportMethod(module, cardsType, "get_Item", 1);
    var deck = new VariableDefinition(deckType);
    var parameters = new VariableDefinition(tupleType);
    var result = new VariableDefinition(module.TypeSystem.Boolean);
    body.Variables.Add(deck);
    body.Variables.Add(parameters);
    body.Variables.Add(result);
    var il = body.Instructions;
    var initialized = Instruction.Create(OpCodes.Nop);
    var shuffleCards = Instruction.Create(OpCodes.Nop);
    var draw = Instruction.Create(OpCodes.Nop);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, decks));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, deck));
    il.Add(Instruction.Create(OpCodes.Callvirt, tryGet));
    il.Add(Instruction.Create(OpCodes.Brtrue, initialized));
    il.Add(Instruction.Create(OpCodes.Ldarg_2));
    il.Add(Instruction.Create(OpCodes.Call, module.ImportReference(compute)));
    il.Add(Instruction.Create(OpCodes.Stloc, parameters));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, parameters));
    il.Add(Instruction.Create(OpCodes.Ldfld, item1));
    il.Add(Instruction.Create(OpCodes.Ldloca_S, parameters));
    il.Add(Instruction.Create(OpCodes.Ldfld, item2));
    il.Add(Instruction.Create(OpCodes.Newobj, constructor));
    il.Add(Instruction.Create(OpCodes.Stloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, decks));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Callvirt, setItem));
    il.Add(initialized);
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldfld, cardsField));
    il.Add(Instruction.Create(OpCodes.Brfalse, shuffleCards));
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldfld, indexField));
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldfld, deckSizeField));
    il.Add(Instruction.Create(OpCodes.Blt, draw));
    il.Add(shuffleCards);
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Callvirt, shuffle));
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Stfld, indexField));
    il.Add(draw);
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldfld, cardsField));
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldfld, indexField));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCard));
    il.Add(Instruction.Create(OpCodes.Stloc, result));
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldloc, deck));
    il.Add(Instruction.Create(OpCodes.Ldfld, indexField));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stfld, indexField));
    il.Add(Instruction.Create(OpCodes.Ldloc, result));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixHousePartyOutcome(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var il = body.Instructions;
    var outcomeType = method.ReturnType;
    var outcomeDefinition = outcomeType.Resolve();
    var permission = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "<PermissionStatus>k__BackingField"));
    var calculateScore = module.ImportReference(method.DeclaringType.Methods.Single(candidate => candidate.Name == "CalculateEnjoymentScore"));
    var score = new VariableDefinition(module.TypeSystem.Int32);
    var status = new VariableDefinition(module.TypeSystem.Int32);
    var hookup = new VariableDefinition(module.TypeSystem.Int32);
    var friend = new VariableDefinition(module.TypeSystem.Int32);
    var arrest = new VariableDefinition(module.TypeSystem.Int32);
    var failedArrest = new VariableDefinition(module.TypeSystem.Int32);
    var parentArgument = new VariableDefinition(module.TypeSystem.Int32);
    var outcome = new VariableDefinition(outcomeType);
    foreach (var local in new[] { score, status, hookup, friend, arrest, failedArrest, parentArgument, outcome }) body.Variables.Add(local);
    VariableDefinition MakeArray(int[] values)
    {
        var array = new VariableDefinition(new ArrayType(module.TypeSystem.Int32));
        body.Variables.Add(array);
        il.Add(Instruction.Create(OpCodes.Ldc_I4, values.Length));
        il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.Int32));
        il.Add(Instruction.Create(OpCodes.Stloc, array));
        for (var index = 0; index < values.Length; index++)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, array));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, index));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, values[index]));
            il.Add(Instruction.Create(OpCodes.Stelem_I4));
        }
        return array;
    }
    var commonHookup = MakeArray(new[] { 5, 7, 10, 15, 20 });
    var deniedHookup = MakeArray(new[] { 5, 7, 10, 15, 21 });
    var commonFriend = MakeArray(new[] { 33, 28, 25, 20, 15 });
    var deniedFriend = MakeArray(new[] { 33, 28, 25, 20, 12 });
    var arrestChances = MakeArray(new[] { 2, 5, 7, 10, 12 });
    var failedArrestChances = MakeArray(new[] { 5, 7, 10, 12, 15 });
    var notAsked = Instruction.Create(OpCodes.Nop);
    var denied = Instruction.Create(OpCodes.Nop);
    var assignArrest = Instruction.Create(OpCodes.Nop);
    var done = Instruction.Create(OpCodes.Nop);
    il.Add(Instruction.Create(OpCodes.Ldloca_S, outcome));
    il.Add(Instruction.Create(OpCodes.Initobj, outcomeType));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Call, calculateScore));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
    il.Add(Instruction.Create(OpCodes.Sub));
    il.Add(Instruction.Create(OpCodes.Stloc, score));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
    il.Add(Instruction.Create(OpCodes.Bgt_Un, done));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, permission));
    il.Add(Instruction.Create(OpCodes.Stloc, status));
    il.Add(Instruction.Create(OpCodes.Ldloc, status));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
    il.Add(Instruction.Create(OpCodes.Beq, denied));
    il.Add(Instruction.Create(OpCodes.Ldloc, status));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
    il.Add(Instruction.Create(OpCodes.Beq, notAsked));
    il.Add(Instruction.Create(OpCodes.Ldloc, status));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Bne_Un, done));
    il.Add(Instruction.Create(OpCodes.Ldloc, commonHookup));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, hookup));
    il.Add(Instruction.Create(OpCodes.Ldloc, commonFriend));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, friend));
    il.Add(Instruction.Create(OpCodes.Br, assignArrest));
    il.Add(notAsked);
    il.Add(Instruction.Create(OpCodes.Ldloc, commonHookup));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, hookup));
    il.Add(Instruction.Create(OpCodes.Ldloc, commonFriend));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, friend));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 20));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
    il.Add(Instruction.Create(OpCodes.Shl));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stloc, parentArgument));
    il.Add(Instruction.Create(OpCodes.Br, assignArrest));
    il.Add(denied);
    il.Add(Instruction.Create(OpCodes.Ldloc, deniedHookup));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, hookup));
    il.Add(Instruction.Create(OpCodes.Ldloc, deniedFriend));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, friend));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 24));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
    il.Add(Instruction.Create(OpCodes.Shl));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stloc, parentArgument));
    il.Add(assignArrest);
    il.Add(Instruction.Create(OpCodes.Ldloc, arrestChances));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, arrest));
    il.Add(Instruction.Create(OpCodes.Ldloc, failedArrestChances));
    il.Add(Instruction.Create(OpCodes.Ldloc, score));
    il.Add(Instruction.Create(OpCodes.Ldelem_I4));
    il.Add(Instruction.Create(OpCodes.Stloc, failedArrest));
    void SetOutcome(string fieldName, VariableDefinition value)
    {
        var field = module.ImportReference(outcomeDefinition.Fields.Single(candidate => candidate.Name == fieldName));
        il.Add(Instruction.Create(OpCodes.Ldloca_S, outcome));
        il.Add(Instruction.Create(OpCodes.Ldloc, value));
        il.Add(Instruction.Create(OpCodes.Stfld, field));
    }
    SetOutcome("PercentChanceOfHookup", hookup);
    SetOutcome("PercentChanceMakeAFriend", friend);
    SetOutcome("PercentChanceOfArrest", arrest);
    SetOutcome("PercentChanceFailedArrest", failedArrest);
    SetOutcome("PercentChanceOfParentArgument", parentArgument);
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ldloc, outcome));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixInterviewChoice(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var il = body.Instructions;
    var scenarioType = method.DeclaringType.BaseType.Resolve();
    var choiceArray = module.ImportReference(scenarioType.Fields.Single(field => field.Name == "ChoiceArray"));
    var colorScheme = module.ImportReference(scenarioType.Fields.Single(field => field.Name == "ColorScheme"));
    var nextScenario = module.ImportReference(scenarioType.Fields.Single(field => field.Name == "<NextScenario>k__BackingField"));
    var candidateField = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "_theCandidate"));
    var candidateType = candidateField.FieldType.Resolve();
    var initialImpression = module.ImportReference(candidateType.Fields.Single(field => field.Name == "<InitialImpression>k__BackingField"));
    var competence = module.ImportReference(candidateType.Fields.Single(field => field.Name == "<Att_competence>k__BackingField"));
    var morale = module.ImportReference(candidateType.Fields.Single(field => field.Name == "<Att_morale>k__BackingField"));
    var choiceType = module.Types.Single(type => type.Name == "ScenarioChoice");
    var getChoiceTitle = ImportMethod(module, choiceType, "get_Title", 0);
    var dataHub = module.Types.Single(type => type.Name == "DataHub");
    var getQuestion = module.ImportReference(dataHub.Methods.Single(candidate => candidate.Name == "GetEmployeeInterviewQuestion" && candidate.Parameters.Count == 1));
    var questionType = getQuestion.ReturnType.Resolve();
    var gameReference = module.Types.Single(type => type.Name == "GameReference");
    var random = module.ImportReference(gameReference.Methods.Single(candidate => candidate.Name == "GetRandomNumber" && candidate.Parameters.Count == 2));
    var dictionaryDefinition = new TypeReference("System.Collections.Generic", "Dictionary`2", module, module.TypeSystem.Object.Scope).Resolve();
    var dictionaryType = new GenericInstanceType(module.ImportReference(dictionaryDefinition));
    dictionaryType.GenericArguments.Add(module.TypeSystem.Int32);
    dictionaryType.GenericArguments.Add(module.TypeSystem.Object);
    var dictionaryConstructor = ImportMethod(module, dictionaryType, ".ctor", 0);
    var setAnswer = ImportMethod(module, dictionaryType, "set_Item", 2);
    var getAnswer = ImportMethod(module, dictionaryType, "get_Item", 1);
    var containsKey = ImportMethod(module, dictionaryType, "ContainsKey", 1);
    var getAnswerCount = ImportMethod(module, dictionaryType, "get_Count", 0);
    var getKeys = ImportMethod(module, dictionaryType, "get_Keys", 0);
    var listDefinition = new TypeReference("System.Collections.Generic", "List`1", module, module.TypeSystem.Object.Scope).Resolve();
    var keysType = new GenericInstanceType(module.ImportReference(listDefinition));
    keysType.GenericArguments.Add(module.TypeSystem.Int32);
    var keysConstructorDefinition = listDefinition.Methods.Single(candidate => candidate.Name == ".ctor" && candidate.Parameters.Count == 1 && candidate.Parameters[0].ParameterType.FullName.StartsWith("System.Collections.Generic.IEnumerable`1"));
    var keysConstructor = module.ImportReference(keysConstructorDefinition);
    keysConstructor.DeclaringType = keysType;
    var getKey = ImportMethod(module, keysType, "get_Item", 1);
    var answerType = module.Types.Single(type => type.Name == "ScenarioEmployeeHireInterviewAnswer");
    var answerConstructor = module.ImportReference(answerType.Methods.Single(candidate => candidate.Name == ".ctor" && candidate.Parameters.Count == 3));
    var questionText = module.ImportReference(questionType.Fields.Single(field => field.Name == "<Question>k__BackingField"));
    var question = new VariableDefinition(getQuestion.ReturnType);
    var answers = new VariableDefinition(dictionaryType);
    var keys = new VariableDefinition(keysType);
    var selectedKey = new VariableDefinition(module.TypeSystem.Int32);
    var selectedAnswer = new VariableDefinition(module.TypeSystem.Object);
    var next = new VariableDefinition(answerType);
    foreach (var local in new[] { question, answers, keys, selectedKey, selectedAnswer, next }) body.Variables.Add(local);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, choiceArray));
    il.Add(Instruction.Create(OpCodes.Ldarg_1));
    il.Add(Instruction.Create(OpCodes.Callvirt, ImportFrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true)));
    il.Add(Instruction.Create(OpCodes.Castclass, choiceType));
    il.Add(Instruction.Create(OpCodes.Callvirt, getChoiceTitle));
    il.Add(Instruction.Create(OpCodes.Call, getQuestion));
    il.Add(Instruction.Create(OpCodes.Stloc, question));
    il.Add(Instruction.Create(OpCodes.Newobj, dictionaryConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, answers));
    for (var index = 1; index <= 5; index++)
    {
        var scoreField = module.ImportReference(questionType.Fields.Single(field => field.Name == $"<Choice{index}Score>k__BackingField"));
        var answerField = module.ImportReference(questionType.Fields.Single(field => field.Name == $"<Choice{index}>k__BackingField"));
        il.Add(Instruction.Create(OpCodes.Ldloc, answers));
        il.Add(Instruction.Create(OpCodes.Ldloc, question));
        il.Add(Instruction.Create(OpCodes.Ldfld, scoreField));
        il.Add(Instruction.Create(OpCodes.Ldloc, question));
        il.Add(Instruction.Create(OpCodes.Ldfld, answerField));
        il.Add(Instruction.Create(OpCodes.Callvirt, setAnswer));
    }
    il.Add(Instruction.Create(OpCodes.Ldloc, answers));
    il.Add(Instruction.Create(OpCodes.Callvirt, getKeys));
    il.Add(Instruction.Create(OpCodes.Newobj, keysConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, keys));
    var impression = Instruction.Create(OpCodes.Nop);
    var selectionDone = Instruction.Create(OpCodes.Nop);
    var incrementScore = Instruction.Create(OpCodes.Nop);
    var adjustedScore = Instruction.Create(OpCodes.Nop);
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Brfalse, impression));
    il.Add(Instruction.Create(OpCodes.Ldloc, keys));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Ldloc, answers));
    il.Add(Instruction.Create(OpCodes.Callvirt, getAnswerCount));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Callvirt, getKey));
    il.Add(Instruction.Create(OpCodes.Stloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Br, selectionDone));
    il.Add(impression);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, candidateField));
    il.Add(Instruction.Create(OpCodes.Ldfld, initialImpression));
    il.Add(Instruction.Create(OpCodes.Conv_I4));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 20));
    il.Add(Instruction.Create(OpCodes.Div));
    il.Add(Instruction.Create(OpCodes.Stloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Ldloc, answers));
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Callvirt, containsKey));
    il.Add(Instruction.Create(OpCodes.Brtrue, selectionDone));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Brtrue, incrementScore));
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Sub));
    il.Add(Instruction.Create(OpCodes.Stloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Br, adjustedScore));
    il.Add(incrementScore);
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stloc, selectedKey));
    il.Add(adjustedScore);
    il.Add(Instruction.Create(OpCodes.Ldloc, answers));
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Callvirt, containsKey));
    il.Add(Instruction.Create(OpCodes.Brtrue, selectionDone));
    il.Add(Instruction.Create(OpCodes.Ldloc, keys));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Callvirt, getKey));
    il.Add(Instruction.Create(OpCodes.Stloc, selectedKey));
    il.Add(selectionDone);
    il.Add(Instruction.Create(OpCodes.Ldloc, answers));
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Callvirt, getAnswer));
    il.Add(Instruction.Create(OpCodes.Stloc, selectedAnswer));
    var weightedAttributes = Instruction.Create(OpCodes.Nop);
    var createAnswer = Instruction.Create(OpCodes.Nop);
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Brtrue, weightedAttributes));
    foreach (var attribute in new[] { competence, morale })
    {
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, candidateField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Stfld, attribute));
    }
    il.Add(Instruction.Create(OpCodes.Br, createAnswer));
    il.Add(weightedAttributes);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, candidateField));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 15));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 21));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Mul));
    il.Add(Instruction.Create(OpCodes.Conv_R4));
    il.Add(Instruction.Create(OpCodes.Stfld, competence));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, candidateField));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 10));
    il.Add(Instruction.Create(OpCodes.Ldc_I4, 21));
    il.Add(Instruction.Create(OpCodes.Call, random));
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedKey));
    il.Add(Instruction.Create(OpCodes.Mul));
    il.Add(Instruction.Create(OpCodes.Conv_R4));
    il.Add(Instruction.Create(OpCodes.Stfld, morale));
    il.Add(createAnswer);
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, candidateField));
    il.Add(Instruction.Create(OpCodes.Ldloc, question));
    il.Add(Instruction.Create(OpCodes.Ldfld, questionText));
    il.Add(Instruction.Create(OpCodes.Ldloc, selectedAnswer));
    il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
    il.Add(Instruction.Create(OpCodes.Newobj, answerConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, next));
    il.Add(Instruction.Create(OpCodes.Ldloc, next));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
    il.Add(Instruction.Create(OpCodes.Stfld, colorScheme));
    il.Add(Instruction.Create(OpCodes.Ldloc, next));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, nextScenario));
    il.Add(Instruction.Create(OpCodes.Stfld, nextScenario));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldloc, next));
    il.Add(Instruction.Create(OpCodes.Stfld, nextScenario));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixFightClubStyleColor(MethodDefinition method)
{
    method.IsStatic = true;
    method.HasThis = false;
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var il = body.Instructions;
    var colorType = method.ReturnType;
    var colorDefinition = colorType.Resolve();
    var colorSetField = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "_fightIngStyleIdentityColorSet"));
    var colorSetType = (GenericInstanceType)colorSetField.FieldType;
    var loadColors = module.ImportReference(method.DeclaringType.Methods.Single(candidate => candidate.Name == "LoadFightIngStyleIdentityColorSet"));
    var weightedListDefinition = module.Types.Single(type => type.Name == "WeightedList`1");
    var weightedListType = new GenericInstanceType(module.ImportReference(weightedListDefinition));
    weightedListType.GenericArguments.Add(colorType);
    var weightedEntryDefinition = module.Types.Single(type => type.Name == "WeightedEntry`1");
    var weightedEntryType = new GenericInstanceType(module.ImportReference(weightedEntryDefinition));
    weightedEntryType.GenericArguments.Add(colorType);
    MethodReference GenericMethod(TypeDefinition type, GenericInstanceType instance, string name, int parameterCount)
    {
        var definition = type.Methods.Single(candidate => candidate.Name == name && candidate.Parameters.Count == parameterCount);
        var reference = new MethodReference(definition.Name, module.ImportReference(definition.ReturnType), instance) { HasThis = definition.HasThis, CallingConvention = definition.CallingConvention };
        foreach (var parameter in definition.Parameters) reference.Parameters.Add(new ParameterDefinition(module.ImportReference(parameter.ParameterType)));
        return reference;
    }
    var choicesConstructor = GenericMethod(weightedListDefinition, weightedListType, ".ctor", 0);
    var addChoice = GenericMethod(weightedListDefinition, weightedListType, "Add", 1);
    var getRandomChoice = GenericMethod(weightedListDefinition, weightedListType, "GetRandomWeightedEntry", 0);
    var entryConstructor = GenericMethod(weightedEntryDefinition, weightedEntryType, ".ctor", 2);
    var entryValueDefinition = weightedEntryDefinition.Fields.Single(field => field.Name == "<Value>k__BackingField");
    var entryValue = new FieldReference(entryValueDefinition.Name, module.ImportReference(entryValueDefinition.FieldType), weightedEntryType);
    var getColorCount = ImportMethod(module, colorSetType, "get_Count", 0);
    var getColor = ImportMethod(module, colorSetType, "get_Item", 1);
    var fightingStyle = module.ImportReference(colorDefinition.Fields.Single(field => field.Name == "<FightingStyle>k__BackingField"));
    var commonness = module.ImportReference(colorDefinition.Fields.Single(field => field.Name == "<CommonnessScore>k__BackingField"));
    var text = module.ImportReference(colorDefinition.Fields.Single(field => field.Name == "<String>k__BackingField"));
    var colorConstructor = ImportMethod(module, colorType, ".ctor", 0);
    var stringEquals = ImportFrameworkMethod(module, "System.String", "Equals", 3);
    var style = new VariableDefinition(module.TypeSystem.String);
    var choices = new VariableDefinition(weightedListType);
    var index = new VariableDefinition(module.TypeSystem.Int32);
    var entry = new VariableDefinition(colorType);
    var selected = new VariableDefinition(weightedEntryType);
    var result = new VariableDefinition(colorType);
    foreach (var local in new[] { style, choices, index, entry, selected, result }) body.Variables.Add(local);
    var loaded = Instruction.Create(OpCodes.Nop);
    var striker = Instruction.Create(OpCodes.Nop);
    var submission = Instruction.Create(OpCodes.Nop);
    var wrestler = Instruction.Create(OpCodes.Nop);
    var styleDone = Instruction.Create(OpCodes.Nop);
    var loop = Instruction.Create(OpCodes.Nop);
    var next = Instruction.Create(OpCodes.Nop);
    var checkLoop = Instruction.Create(OpCodes.Nop);
    var fallback = Instruction.Create(OpCodes.Nop);
    il.Add(Instruction.Create(OpCodes.Ldsfld, colorSetField));
    il.Add(Instruction.Create(OpCodes.Brtrue, loaded));
    il.Add(Instruction.Create(OpCodes.Call, loadColors));
    il.Add(loaded);
    il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
    il.Add(Instruction.Create(OpCodes.Stloc, style));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Beq, striker));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
    il.Add(Instruction.Create(OpCodes.Beq, submission));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
    il.Add(Instruction.Create(OpCodes.Beq, wrestler));
    il.Add(Instruction.Create(OpCodes.Br, styleDone));
    il.Add(striker);
    il.Add(Instruction.Create(OpCodes.Ldstr, "striker"));
    il.Add(Instruction.Create(OpCodes.Stloc, style));
    il.Add(Instruction.Create(OpCodes.Br, styleDone));
    il.Add(submission);
    il.Add(Instruction.Create(OpCodes.Ldstr, "submission artist"));
    il.Add(Instruction.Create(OpCodes.Stloc, style));
    il.Add(Instruction.Create(OpCodes.Br, styleDone));
    il.Add(wrestler);
    il.Add(Instruction.Create(OpCodes.Ldstr, "wrestler"));
    il.Add(Instruction.Create(OpCodes.Stloc, style));
    il.Add(styleDone);
    il.Add(Instruction.Create(OpCodes.Newobj, choicesConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, choices));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(Instruction.Create(OpCodes.Br, checkLoop));
    il.Add(loop);
    il.Add(Instruction.Create(OpCodes.Ldsfld, colorSetField));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Callvirt, getColor));
    il.Add(Instruction.Create(OpCodes.Stloc, entry));
    il.Add(Instruction.Create(OpCodes.Ldloc, entry));
    il.Add(Instruction.Create(OpCodes.Ldfld, fightingStyle));
    il.Add(Instruction.Create(OpCodes.Ldloc, style));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
    il.Add(Instruction.Create(OpCodes.Call, stringEquals));
    il.Add(Instruction.Create(OpCodes.Brfalse, next));
    il.Add(Instruction.Create(OpCodes.Ldloc, choices));
    il.Add(Instruction.Create(OpCodes.Ldloc, entry));
    il.Add(Instruction.Create(OpCodes.Ldloc, entry));
    il.Add(Instruction.Create(OpCodes.Ldfld, commonness));
    il.Add(Instruction.Create(OpCodes.Conv_R4));
    il.Add(Instruction.Create(OpCodes.Newobj, entryConstructor));
    il.Add(Instruction.Create(OpCodes.Callvirt, addChoice));
    il.Add(next);
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(checkLoop);
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldsfld, colorSetField));
    il.Add(Instruction.Create(OpCodes.Callvirt, getColorCount));
    il.Add(Instruction.Create(OpCodes.Blt, loop));
    il.Add(Instruction.Create(OpCodes.Ldloc, choices));
    il.Add(Instruction.Create(OpCodes.Callvirt, getRandomChoice));
    il.Add(Instruction.Create(OpCodes.Stloc, selected));
    var done = Instruction.Create(OpCodes.Nop);
    il.Add(Instruction.Create(OpCodes.Ldloc, selected));
    il.Add(Instruction.Create(OpCodes.Brfalse, fallback));
    il.Add(Instruction.Create(OpCodes.Ldloc, selected));
    il.Add(Instruction.Create(OpCodes.Ldfld, entryValue));
    il.Add(Instruction.Create(OpCodes.Brfalse, fallback));
    il.Add(Instruction.Create(OpCodes.Ldloc, selected));
    il.Add(Instruction.Create(OpCodes.Ldfld, entryValue));
    il.Add(Instruction.Create(OpCodes.Stloc, result));
    il.Add(Instruction.Create(OpCodes.Br, done));
    il.Add(fallback);
    il.Add(Instruction.Create(OpCodes.Newobj, colorConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, entry));
    il.Add(Instruction.Create(OpCodes.Ldloc, entry));
    il.Add(Instruction.Create(OpCodes.Ldstr, "Fight on!"));
    il.Add(Instruction.Create(OpCodes.Stfld, text));
    il.Add(Instruction.Create(OpCodes.Ldloc, entry));
    il.Add(Instruction.Create(OpCodes.Ldloc, style));
    il.Add(Instruction.Create(OpCodes.Stfld, fightingStyle));
    il.Add(Instruction.Create(OpCodes.Ldloc, entry));
    il.Add(Instruction.Create(OpCodes.Stloc, result));
    il.Add(done);
    il.Add(Instruction.Create(OpCodes.Ldloc, result));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void FixBusinessSellPrices(MethodDefinition method)
{
    var module = method.Module;
    var body = FreshBody(method);
    body.InitLocals = true;
    var il = body.Instructions;
    var businessField = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "_theBusiness"));
    var optimalSellPrice = ImportMethod(module, businessField.FieldType, "OptimalSellPrice", 1);
    var arrayListType = module.ImportReference(new TypeReference("System.Collections", "ArrayList", module, module.TypeSystem.Object.Scope));
    var arrayListDefinition = arrayListType.Resolve();
    var arrayListConstructor = ImportFrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
    var add = ImportFrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
    var getCount = ImportFrameworkMethod(module, "System.Collections.ArrayList", "get_Count", 0, true);
    var getItem = ImportFrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
    var copyConstructor = module.ImportReference(arrayListDefinition.Methods.Single(candidate => candidate.Name == ".ctor" && candidate.Parameters.Count == 1 && candidate.Parameters[0].ParameterType.FullName == "System.Collections.ICollection"));
    var gameReference = module.Types.Single(type => type.Name == "GameReference");
    var round = module.ImportReference(gameReference.Methods.Single(candidate => candidate.Name == "GetRoundedNumber" && candidate.Parameters.Count == 2));
    var format = module.ImportReference(gameReference.Methods.Single(candidate => candidate.Name == "CurrencyStringForAmountWithBigNumberShorthand" && candidate.Parameters.Count == 2));
    var values = new VariableDefinition(arrayListType);
    var formatted = new VariableDefinition(arrayListType);
    var optimal = new VariableDefinition(module.TypeSystem.Double);
    var lower = new VariableDefinition(module.TypeSystem.Double);
    var lowerStep = new VariableDefinition(module.TypeSystem.Double);
    var upper = new VariableDefinition(module.TypeSystem.Double);
    var upperStep = new VariableDefinition(module.TypeSystem.Double);
    var value = new VariableDefinition(module.TypeSystem.Double);
    var index = new VariableDefinition(module.TypeSystem.Int32);
    foreach (var local in new[] { values, formatted, optimal, lower, lowerStep, upper, upperStep, value, index }) body.Variables.Add(local);
    il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, values));
    il.Add(Instruction.Create(OpCodes.Ldarg_0));
    il.Add(Instruction.Create(OpCodes.Ldfld, businessField));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Callvirt, optimalSellPrice));
    il.Add(Instruction.Create(OpCodes.Stloc, optimal));
    il.Add(Instruction.Create(OpCodes.Ldloc, optimal));
    il.Add(Instruction.Create(OpCodes.Ldc_R8, 0.699999988079071));
    il.Add(Instruction.Create(OpCodes.Mul));
    il.Add(Instruction.Create(OpCodes.Stloc, lower));
    void AddValue(Action emitValue)
    {
        il.Add(Instruction.Create(OpCodes.Ldloc, values));
        emitValue();
        il.Add(Instruction.Create(OpCodes.Box, module.TypeSystem.Double));
        il.Add(Instruction.Create(OpCodes.Callvirt, add));
        il.Add(Instruction.Create(OpCodes.Pop));
    }
    AddValue(() => il.Add(Instruction.Create(OpCodes.Ldloc, lower)));
    il.Add(Instruction.Create(OpCodes.Ldloc, optimal));
    il.Add(Instruction.Create(OpCodes.Ldloc, lower));
    il.Add(Instruction.Create(OpCodes.Sub));
    il.Add(Instruction.Create(OpCodes.Ldc_R8, 0.25));
    il.Add(Instruction.Create(OpCodes.Mul));
    il.Add(Instruction.Create(OpCodes.Stloc, lowerStep));
    foreach (var multiplier in new[] { 1.0, 2.0, 3.0, 4.0 })
    {
        AddValue(() =>
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, lower));
            il.Add(Instruction.Create(OpCodes.Ldloc, lowerStep));
            il.Add(Instruction.Create(OpCodes.Ldc_R8, multiplier));
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Add));
        });
    }
    il.Add(Instruction.Create(OpCodes.Ldloc, optimal));
    il.Add(Instruction.Create(OpCodes.Ldc_R8, 1.2999999523162842));
    il.Add(Instruction.Create(OpCodes.Mul));
    il.Add(Instruction.Create(OpCodes.Stloc, upper));
    il.Add(Instruction.Create(OpCodes.Ldloc, upper));
    il.Add(Instruction.Create(OpCodes.Ldloc, optimal));
    il.Add(Instruction.Create(OpCodes.Sub));
    il.Add(Instruction.Create(OpCodes.Ldc_R8, 0.25));
    il.Add(Instruction.Create(OpCodes.Mul));
    il.Add(Instruction.Create(OpCodes.Stloc, upperStep));
    foreach (var multiplier in new[] { 1.0, 2.0, 3.0, 4.0 })
    {
        AddValue(() =>
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, optimal));
            il.Add(Instruction.Create(OpCodes.Ldloc, upperStep));
            il.Add(Instruction.Create(OpCodes.Ldc_R8, multiplier));
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Add));
        });
    }
    il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
    il.Add(Instruction.Create(OpCodes.Stloc, formatted));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    var loop = Instruction.Create(OpCodes.Nop);
    var checkLoop = Instruction.Create(OpCodes.Nop);
    il.Add(Instruction.Create(OpCodes.Br, checkLoop));
    il.Add(loop);
    il.Add(Instruction.Create(OpCodes.Ldloc, values));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Callvirt, getItem));
    il.Add(Instruction.Create(OpCodes.Unbox_Any, module.TypeSystem.Double));
    il.Add(Instruction.Create(OpCodes.Stloc, value));
    il.Add(Instruction.Create(OpCodes.Ldloc, formatted));
    il.Add(Instruction.Create(OpCodes.Ldloc, value));
    il.Add(Instruction.Create(OpCodes.Ldc_R8, -1.0));
    il.Add(Instruction.Create(OpCodes.Call, round));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
    il.Add(Instruction.Create(OpCodes.Call, format));
    il.Add(Instruction.Create(OpCodes.Callvirt, add));
    il.Add(Instruction.Create(OpCodes.Pop));
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
    il.Add(Instruction.Create(OpCodes.Add));
    il.Add(Instruction.Create(OpCodes.Stloc, index));
    il.Add(checkLoop);
    il.Add(Instruction.Create(OpCodes.Ldloc, index));
    il.Add(Instruction.Create(OpCodes.Ldloc, values));
    il.Add(Instruction.Create(OpCodes.Callvirt, getCount));
    il.Add(Instruction.Create(OpCodes.Blt, loop));
    il.Add(Instruction.Create(OpCodes.Ldloc, formatted));
    il.Add(Instruction.Create(OpCodes.Newobj, copyConstructor));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static void PatchProductProfileKeyValuePairs(MethodDefinition method)
{
    var sites = new[]
    {
        (Offset: 0x1ec5, Local: 757), (Offset: 0x210f, Local: 757), (Offset: 0x2129, Local: 757),
        (Offset: 0x48c2, Local: 757), (Offset: 0x48dc, Local: 757), (Offset: 0x4a77, Local: 757),
        (Offset: 0x4a91, Local: 757), (Offset: 0x4e97, Local: 757), (Offset: 0x4eb1, Local: 757),
        (Offset: 0x690a, Local: 757), (Offset: 0x6924, Local: 757), (Offset: 0x6dfa, Local: 757),
        (Offset: 0x6e14, Local: 757), (Offset: 0x7388, Local: 757), (Offset: 0x73a2, Local: 757),
        (Offset: 0x7471, Local: 757), (Offset: 0x748b, Local: 757), (Offset: 0x7798, Local: 757),
        (Offset: 0x77b2, Local: 757), (Offset: 0x314f, Local: 756), (Offset: 0x3204, Local: 756),
        (Offset: 0x55ef, Local: 756), (Offset: 0x6bf0, Local: 756), (Offset: 0x4764, Local: 938),
        (Offset: 0x4775, Local: 938), (Offset: 0x4846, Local: 913), (Offset: 0x59f1, Local: 1057),
        (Offset: 0x55f3, Local: 1073), (Offset: 0x6bf4, Local: 1073), (Offset: 0x8de4, Local: 1073),
        (Offset: 0x565c, Local: 1089), (Offset: 0x6c6e, Local: 1093)
    };
    var instructions = method.Body.Instructions;
    var loads = sites.Select(site => instructions.SingleOrDefault(instruction => instruction.Offset == site.Offset && (instruction.OpCode == OpCodes.Ldloc || instruction.OpCode == OpCodes.Ldloc_S) && instruction.Operand is VariableDefinition variable && variable.Index == site.Local)).ToArray();
    if (loads.All(load => load is null)) return;
    if (loads.Any(load => load is null)) throw new InvalidOperationException($"Product-profile repair matched {loads.Count(load => load is not null)} of {sites.Length} sites");
    for (var index = 0; index < sites.Length; index++)
    {
        var load = loads[index]!;
        var variable = (VariableDefinition)load.Operand;
        var pairType = (GenericInstanceType)variable.VariableType;
        var getValue = new MethodReference("get_Value", pairType.Resolve().GenericParameters[1], pairType) { HasThis = true };
        load.OpCode = OpCodes.Ldloca;
        instructions.Insert(instructions.IndexOf(load) + 1, Instruction.Create(OpCodes.Call, method.Module.ImportReference(getValue)));
    }
}

static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount)
{
    var typeDefinition = declaringType.Resolve();
    var definition = typeDefinition.Methods.SingleOrDefault(candidate => candidate.Name == name && candidate.Parameters.Count == parameterCount && !candidate.HasGenericParameters) ??
        typeDefinition.Interfaces.SelectMany(face => face.InterfaceType.Resolve().Methods).Single(candidate => candidate.Name == name && candidate.Parameters.Count == parameterCount && !candidate.HasGenericParameters);
    var reference = module.ImportReference(definition);
    if (declaringType is GenericInstanceType generic)
        reference.DeclaringType = generic;
    return reference;
}

static MethodReference ImportFrameworkMethod(ModuleDefinition module, string typeName, string methodName, int parameterCount, bool instance = false)
{
    var scope = module.TypeSystem.Object.Scope;
    var separator = typeName.LastIndexOf('.');
    var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module, scope);
    var definition = type.Resolve().Methods.Single(candidate => candidate.Name == methodName && candidate.Parameters.Count == parameterCount && candidate.IsStatic != instance);
    return module.ImportReference(definition);
}

static IEnumerable<TypeDefinition> Flatten(TypeDefinition type)
{
    yield return type;
    foreach (var nested in type.NestedTypes)
    foreach (var descendant in Flatten(nested)) yield return descendant;
}
