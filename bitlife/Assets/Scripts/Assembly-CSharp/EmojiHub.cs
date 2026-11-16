using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class EmojiHub
{
	public enum ALTERNATIVE_DOCTOR
	{
		ACUPUNCTURE = 1,
		CHIROPRACTOR = 2,
		HOMEOPATHIC_DOCTOR = 3,
		COLON_HYDROTHERAPIST = 4
	}

	private static Dictionary<string, string> _scenarioNameDictionary;

	private static Dictionary<string, string> _listItemNameDictionary;

	public static string EmojiForImportContacts()
	{
		return null;
	}

	public static string EmojiForPerson(SimPerson person)
	{
		return null;
	}

	public static string EmojiForPersonAndShowSkullAfterDeath(SimPerson person, bool showSkull = false)
	{
		return null;
	}

	public static string EmojiForPetAndShowSkullAfterDeath(SimPet pet, bool showSkull)
	{
		return null;
	}

	public static string EmojiForFriendStatus(int friendStatus)
	{
		return null;
	}

	public static string EmojiForPlasticSurgery(DataPlasticSurgery plasticSurgery)
	{
		return null;
	}

	public static string EmojiForMilitaryBranch(int militaryBranch)
	{
		return null;
	}

	public static string EmojiForSchoolType(int schoolType)
	{
		return null;
	}

	public static string EmojiForAlternativeDoctorType(int type)
	{
		return null;
	}

	public static string EmojiForVacationAndIsCruise(bool isCruise)
	{
		return null;
	}

	public static string EmojiForFood(DataFood food)
	{
		return null;
	}

	public static string EmojiForAnimal(DataAnimal animal)
	{
		return null;
	}

	public static string EmojiForFightErAnimal(DataFightErAnimal fightAnimal)
	{
		return null;
	}

	public static string EmojiForDiseaseType(DataDiseaseType diseaseType)
	{
		return null;
	}

	public static string EmojiForSymptomScenario(DataDiseaseSymptomScenario symptomScenario)
	{
		return null;
	}

	public static string EmojiForCareer(DataCareer career)
	{
		return null;
	}

	public static string EmojiForCareerString(string careerString)
	{
		return null;
	}

	public static string EmojiForPartTimeJob(DataPartTimeJob job)
	{
		return null;
	}

	public static string EmojiForCompany(SimCompany company)
	{
		return null;
	}

	public static string EmojiForCountryCode(string countryCode)
	{
		return null;
	}

	public static string EmojiForCar(SimCar car)
	{
		return null;
	}

	public static string EmojiForHouse(SimHouse house)
	{
		return null;
	}

	public static string EmojiForDrug(DataDrug drug)
	{
		return null;
	}

	public static string EmojiForAlcohol(DataAlcohol alcohol)
	{
		return null;
	}

	public static string EmojiForOccupationString(string s)
	{
		return null;
	}

	public static string EmojiForFertilityActivity(int activity)
	{
		return null;
	}

	public static string EmojiForCompletedAchievement()
	{
		return null;
	}

	public static string EmojiForMovieGenre(int genre)
	{
		return null;
	}

	public static string EmojiForBurgleItem(SimBurgleItem item)
	{
		return null;
	}

	public static string EmojiForStreetPersonWithSkinColor(DataStreetPerson streetPerson, int skinColor)
	{
		return null;
	}

	public static string EmojiForPet(SimPet pet)
	{
		return null;
	}

	public static string EmojiForCustomLifeGender(int gender)
	{
		return null;
	}

	public static string EmojiForHeirloom(DataHeirloom heirloom)
	{
		return null;
	}

	public static string EmojiForPrisonJob(DataPrisonJob prisonJob)
	{
		return null;
	}

	public static string EmojiForPrisonGangAndGender(DataPrisonGang gang, int gender)
	{
		return null;
	}

	public static string EmojiForPrisonGuardOfGender(int gender)
	{
		return null;
	}

	public static string EmojiForSchoolActivity(DataSchoolActivity activity)
	{
		return null;
	}

	public static string EmojiForSchoolClique(DataSchoolClique clique)
	{
		return null;
	}

	public static string EmojiForSchoolFrat(DataSchoolFrat frat)
	{
		return null;
	}

	public static string EmojiForCoworkerOfGender(int gender)
	{
		return null;
	}

	public static string EmojiForSaveSlotMissingEmojiScript()
	{
		return null;
	}

	public static string EmojiForWalkAndIsWithPet(bool withPet)
	{
		return null;
	}

	public static string EmojiForMartialArt(DataMartialArt martialArt)
	{
		return null;
	}

	public static string EmojiForBabyOfGender(int gender)
	{
		return null;
	}

	public static string EmojiForDeath()
	{
		return null;
	}

	public static string EmojiForOrphanage()
	{
		return null;
	}

	public static string EmojiForAircraft(SimAircraft aircraft)
	{
		return null;
	}

	public static string EmojiForAircraftDealer()
	{
		return null;
	}

	public static string EmojiForSpecialtyAircraftDealer()
	{
		return null;
	}

	public static string EmojiForWatercraft(SimWatercraft watercraft)
	{
		return null;
	}

	public static string EmojiForRecreationalVehicle(SimRecreationalVehicle recreationalVehicle)
	{
		return null;
	}

	public static string EmojiForBoatRide()
	{
		return null;
	}

	public static string EmojiForWatercraftDealer()
	{
		return null;
	}

	public static string EmojiForJewel(SimJewel jewel)
	{
		return null;
	}

	public static string EmojiForJeweler()
	{
		return null;
	}

	public static string EmojiForFakeJeweler()
	{
		return null;
	}

	public static string EmojiForPoliticalOfficeInCountryCode(int offfice, string countryCode)
	{
		return null;
	}

	public static string EmojiForPoliticalIssue(int issue)
	{
		return null;
	}

	public static string EmojiForElectionResultAndDidWin(bool won)
	{
		return null;
	}

	public static string EmojiForSocialMediaChannel(int channel)
	{
		return null;
	}

	public static string EmojiForSocialMediaVerified()
	{
		return null;
	}

	public static string EmojiForRoyalOfRank(int rank)
	{
		return null;
	}

	public static string EmojiForCommoner()
	{
		return null;
	}

	public static string EmojiForSport(int sport)
	{
		return null;
	}

	public static string EmojiForSportsTeam(SimSportsTeam team)
	{
		return null;
	}

	public static string EmojiForSportsTeamInSportWithPrimaryColorAndSecondaryColor(int sport, Color primaryColor, Color secondaryColor)
	{
		return null;
	}

	public static string EmojiForSportsInjury(DataSportsInjury injury)
	{
		return null;
	}

	public static string EmojiForSportsChampionship()
	{
		return null;
	}

	public static string EmojiForFanAndIsPositive(bool positive)
	{
		return null;
	}

	public static string EmojiForMusicRole(int role)
	{
		return null;
	}

	public static string EmojiForMusicGenre(int genre)
	{
		return null;
	}

	public static string EmojiForMusicBand()
	{
		return null;
	}

	public static string EmojiForMusicSolo()
	{
		return null;
	}

	public static string EmojiForMusicInstrument(DataMusicInstrument instrument)
	{
		return null;
	}

	public static string EmojiForMusicStore()
	{
		return null;
	}

	public static string EmojiForPhysicalMusicInstrument(SimMusicInstrumentPhysical instrument)
	{
		return null;
	}

	public static string EmojiForMusicAlbumType(int albumType)
	{
		return null;
	}

	public static string EmojiForMusicAward(int awardType)
	{
		return null;
	}

	public static string EmojiForMusicConcert()
	{
		return null;
	}

	public static string EmojiForMafiaRank(int rankNI)
	{
		return null;
	}

	public static string EmojiForMafiaMob(DataMafiaMob mob)
	{
		return null;
	}

	public static string EmojiForMafiaFamilyGeneric()
	{
		return null;
	}

	public static string EmojiForMafiaInformant()
	{
		return null;
	}

	public static string EmojiForMafiaExtortionAction(int action)
	{
		return null;
	}

	public static string EmojiForFilmGenre(int genre)
	{
		return null;
	}

	public static string EmojiForShowGenre(int genre)
	{
		return null;
	}

	public static string EmojiForActorType(int type)
	{
		return null;
	}

	public static string EmojiForActorShowRenewal()
	{
		return null;
	}

	public static string EmojiForActorShowCancel()
	{
		return null;
	}

	public static string EmojiForActorAwardType(int type)
	{
		return null;
	}

	public static string EmojiForActorPractice()
	{
		return null;
	}

	public static string EmojiForSpecialTalent(int specialTalent)
	{
		return null;
	}

	public static string EmojiForHustleType(int type)
	{
		return null;
	}

	public static string EmojiForHustleActType(int type)
	{
		return null;
	}

	public static string EmojiForScamType(int type)
	{
		return null;
	}

	public static string EmojiForStreetBasedOnTraffic(int traffic)
	{
		return null;
	}

	public static string EmojiForPanhandleItem(DataHustlerPanhandleItem item)
	{
		return null;
	}

	public static string EmojiForPanhandleFood(DataHustlerPanhandleFood food)
	{
		return null;
	}

	public static string EmojiForBusinessIndustry(int industry)
	{
		return null;
	}

	public static string EmojiForBusinessIndustryProducts(int industry, string product)
	{
		return null;
	}

	public static string EmojiForLandlordAmenity(string project)
	{
		return null;
	}

	public static string EmojiForTeamBuildingActivity(DataBusinessTeamBuildingActivity activity)
	{
		return null;
	}

	public static string EmojiForBusinessDemand(float demand)
	{
		return null;
	}

	public static string EmojiForStockForSector(int sector)
	{
		return null;
	}

	public static string EmojiForPennyStockForSector(int sector)
	{
		return null;
	}

	public static string EmojiForFinancialAdvice(bool willGiveAdvice)
	{
		return null;
	}

	public static string EmojiForFund(SimFund fund)
	{
		return null;
	}

	public static string EmojiForFundSector(int sector)
	{
		return null;
	}

	public static string EmojiForLocalBonds(SimBond bond)
	{
		return null;
	}

	public static string EmojiForGovernmentBonds(SimBond bond)
	{
		return null;
	}

	public static string EmojiForCrypto(SimCrypto crypto)
	{
		return null;
	}

	public static string EmojiForSpecialJob(int specialJob)
	{
		return null;
	}

	public static string EmojiForOnboardingStep(int step)
	{
		return null;
	}

	public static string EmojiForAccessoryCompositeWithType(int pType, int pIndex)
	{
		return null;
	}

	private static string DynamicEmojiStringForListItemTitle(string s)
	{
		return null;
	}

	private static void LoadListItemTitleDictionary()
	{
	}

	public static string EmojiForListItemTitle(string s)
	{
		return null;
	}

	public static string EmojiForMusicPlayInstrument()
	{
		return null;
	}

	public static string EmojiForRealEstateBrokerListItem()
	{
		return null;
	}

	public static string EmojiForCarDealerListItem()
	{
		return null;
	}

	public static string EmojiForMenuListItemTitle(string s)
	{
		return null;
	}

	public static string EmojiForSocialPlatform(string s)
	{
		return null;
	}

	public static string EmojiForAstronautPerformance(float att_value)
	{
		return null;
	}

	public static string EmojiForAstronautRank(int rank)
	{
		return null;
	}

	public static string EmojiForAstronautCelestialBody(SimDiscovery discovery)
	{
		return null;
	}

	public static string EmojiForCustomPersonGender(int gender)
	{
		return null;
	}

	public static string EmojiForSettingsListItemTitle(string s)
	{
		return null;
	}

	public static string EmojiForBadge(int badge)
	{
		return null;
	}

	public static string EmojiForObfuscatedBadge()
	{
		return null;
	}

	public static string EmojiForLegacy()
	{
		return null;
	}

	private static string ScenarioClassNameDynamicString(string s)
	{
		return null;
	}

	private static void LoadEmojiForScenarioClassNameDict()
	{
	}

	public static string EmojiForScenarioClassName(string s)
	{
		return null;
	}

	public static string EmojiForDumbScenario(DataDumbScenario dumbScenario)
	{
		return null;
	}

	public static string EmojiForHappiness(float att_value)
	{
		return null;
	}

	public static string EmojiForHealth(float att_value)
	{
		return null;
	}

	public static string EmojiForVampireEssence(float att_value)
	{
		return null;
	}

	public static string EmojiForIntelligence(float att_value)
	{
		return null;
	}

	public static string EmojiForAppearance(float att_value)
	{
		return null;
	}

	public static string EmojiForFame(float att_value)
	{
		return null;
	}

	public static string EmojiForCult(float att_value)
	{
		return null;
	}

	public static string EmojiForPoliticalApproval(float att_value)
	{
		return null;
	}

	public static string EmojiForAttribute(int attribute)
	{
		return null;
	}

	public static string EmojiForReputation(float att_value)
	{
		return null;
	}

	public static string EmojiForStreetSmarts(float att_value)
	{
		return null;
	}

	public static string EmojiForInfamy()
	{
		return null;
	}

	public static string EmojiForBusinessPerformance(float att_value)
	{
		return null;
	}

	public static string EmojiForBusinessAnnualReport(bool isPush, double cookedIncome)
	{
		return null;
	}

	public static string EmojiForMiniGameBlackjackWin()
	{
		return null;
	}

	public static string EmojiForMiniGameBlackjackLose()
	{
		return null;
	}

	public static string EmojiForMiniGameBlackjackTie()
	{
		return null;
	}

	public static string EmojiForMiniGameTheseusGuard()
	{
		return null;
	}

	public static string EmojiForMiniGameHorseRacingBet()
	{
		return null;
	}

	public static string EmojiForMiniGameHorseRacingFirstPlace()
	{
		return null;
	}

	public static string EmojiForMiniGameHorseRacingWin()
	{
		return null;
	}

	public static string EmojiForMiniGameHorseRacingLose()
	{
		return null;
	}

	public static ArrayList EmojiStringArrayForRacehorses()
	{
		return null;
	}

	public static string EmojiForMiniGameBurglaryDog()
	{
		return null;
	}

	public static string EmojiForMiniGameBurglaryCop()
	{
		return null;
	}

	public static string EmojiForMiniGamePrisonRiotGuardOfGender(int gender)
	{
		return null;
	}

	public static string EmojiForMiniGamePorchPiratePackage()
	{
		return null;
	}

	public static string EmojiMedalForChallenge(int challengeID)
	{
		return null;
	}

	public static string EmojiForChallenge(int challengeID)
	{
		return null;
	}

	public static string EmojiForCompletedChallengeRequirement()
	{
		return null;
	}

	public static string EmojiForNewLifeView()
	{
		return null;
	}

	public static string EmojiForGodModeEditViews()
	{
		return null;
	}

	public static string EmojiForInfoButton()
	{
		return null;
	}

	public static string EmojiForCheckMark()
	{
		return null;
	}

	public static string EmojiForDogLifeAndIsSticker(bool isSticker)
	{
		return null;
	}

	public static string EmojiForCatLife()
	{
		return null;
	}

	public static ArrayList EmojiPrecachingArray()
	{
		return null;
	}

	public static Texture2D Texture2DForImageString(string imageString)
	{
		return null;
	}

	public static Sprite ImageForEmojiString(string s)
	{
		return null;
	}

	public static string ActualEmojiForNotificationType(int notificationType)
	{
		return null;
	}

	public static string ActualEmojiForNotificationBaitIndex(int index)
	{
		return null;
	}

	public static string ActualEmojiForNotificationHeirloomIndex(int index)
	{
		return null;
	}

	public static string EmojiForAstronautArtifact(int id)
	{
		return null;
	}

	public static string EmojiForAstronautMoonMars(bool isMoon)
	{
		return null;
	}

	public static string EmojiForAstronautPublication(string discoveryType, bool hasWonNobelPrize)
	{
		return null;
	}

	public static string EmojiForBlackMarketAntique(int antiqueID)
	{
		return null;
	}

	public static string EmojiForBlackMarketHistoricalArtifact(int historicalArtifactID)
	{
		return null;
	}

	public static string EmojiForBlackMarketFashion(int fashionID)
	{
		return null;
	}

	public static string EmojiForBlackMarketArt(int artID)
	{
		return null;
	}

	public static string EmojiForBlackMarketSportsMemorabilia(int sportID)
	{
		return null;
	}

	public static string EmojiForBlackMarketWeapon(int weaponID)
	{
		return null;
	}

	public static string EmojiForBlackMarketGemstone(int gemstoneID)
	{
		return null;
	}

	public static string EmojiForBlackMarketCar(int carID)
	{
		return null;
	}

	public static string EmojiForBlackMarketMuseumWing(int wing)
	{
		return null;
	}

	public static string GetEmojiStringForDefaultTotem(string totemName)
	{
		return null;
	}

	public static string GetEmojiForCult(CULT_PHILOSOPHY cult)
	{
		return null;
	}

	public static string EmojiForCultCommuneStructure(string projectName)
	{
		return null;
	}

	public static string EmojiForCultPlotType(CULT_PROPERTY_TYPE propertyType)
	{
		return null;
	}

	public static string EmojiForModelType(MODEL_CAREER_TYPE modelType)
	{
		return null;
	}

	public static string EmojiForModelGigType(MODEL_GIG_TYPE gigType)
	{
		return null;
	}

	public static string EmojiForModelCampaignType(MODEL_CAMPAIGN_TYPE modelCampaign)
	{
		return null;
	}

	public static string EmojiForModelFashionCategory(string itemType)
	{
		return null;
	}

	public static string EmojiForModelHeadshot()
	{
		return null;
	}

	public static string EmojiForModelGift(SimModelGift gift)
	{
		return null;
	}

	public static string EmojiForCasinoCollectible(SimCasinoCollectible collectible)
	{
		return null;
	}

	public static string EmojiForModelIndustryType(MODEL_INDUSTRY clientClientIndustry)
	{
		return null;
	}

	public static string EmojiForSecretAgentFrontType(SECRET_AGENT_AGENCY_FRONT_TYPE frontType)
	{
		return null;
	}

	public static string EmojiForSecretAgentGadget(SimSecretAgentGadget gadget)
	{
		return null;
	}

	public static string EmojiForSecretAgentSpecialization(SECRET_AGENT_SPECIALIZATION specialization)
	{
		return null;
	}

	public static string EmojiForSecretAgentSecurityFeature(string securityFeature)
	{
		return null;
	}

	public static string EmojiForSecretAgentHackingFileType(SECRET_AGENT_HACKING_MISSION_FILE_TYPE fileType = SECRET_AGENT_HACKING_MISSION_FILE_TYPE.NONE)
	{
		return null;
	}

	public static string GetEmojiForZooAnimalBreed(DataZooAnimal data, GENDER gender, int age)
	{
		return null;
	}

	public static string GetEmojiForZooHabitat(string habitat)
	{
		return null;
	}

	public static string GetEmojiForZooHabitatFeature(string feature)
	{
		return null;
	}

	public static string GetEmojiForZooAttraction(string attractionType)
	{
		return null;
	}

	public static string EmojiForZooGiftShopMerch(string product)
	{
		return null;
	}

	public static string EmojiForDealerRank(DEALER_RANK rank)
	{
		return null;
	}

	public static string EmojiForDealerDrug(string drug)
	{
		return null;
	}

	public static string EmojiForDealerCutDrug(string cutIngredient)
	{
		return null;
	}

	public static string EmojiForCasinoType(CASINO_TYPE casinoType, string theme = "default")
	{
		return null;
	}

	public static string EmojiForCasinoGame(CASINO_GAME_TYPE gameType)
	{
		return null;
	}

	public static string EmojiForCasinoAmenity(string amenityCategory)
	{
		return null;
	}

	public static string EmojiForCasinoEntertainment(string category)
	{
		return null;
	}

	public static string EmojiForLuxurySpaType(LUXURY_SPA_TYPE spaType)
	{
		return null;
	}

	public static string EmojiForLuxuryIslandType(string islandType, string islandSize)
	{
		return null;
	}

	public static string EmojiForLuxurySecretSociety(DataLuxurySecretSocietie society, bool isBadge = false)
	{
		return null;
	}

	public static string EmojiForLuxuryIslandStructure(LUXURY_ISLAND_STRUCTURE_TYPE structureType)
	{
		return null;
	}

	public static string EmojiForRacingTrack(SimRacingTrack track)
	{
		return null;
	}

	public static string EmojiForRacingCollectible(SimRacingCollectible collectible)
	{
		return null;
	}

	public static string EmojiForOutdoorFishingTreasure(SimOutdoorFishingTreasure fishingTreasure)
	{
		return null;
	}

	public static string EmojiForOutdoorPlant(SimOutdoorPlant plant)
	{
		return null;
	}

	public static string EmojiForOutdoorAnimal(SimOutdoorAnimal animal)
	{
		return null;
	}

	public static string EmojiForOutdoorBird(SimOutdoorBird animal)
	{
		return null;
	}

	public static string EmojiForOutdoorFish(SimOutdoorFishEntry fish)
	{
		return null;
	}

	public static string EmojiForOutdoorMarineLife(SimOutdoorMarineLife animal)
	{
		return null;
	}

	public static string EmojiForGlovedFighter(SimFightClubFighter fighter)
	{
		return null;
	}
}
