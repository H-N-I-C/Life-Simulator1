using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Managers;
using Genesis.Entities;
using Genesis.FeatureContainers;
using UnityEngine.Events;

[Serializable]
public class Life
{
	internal sealed class GenesisFeatureContainerTransactionSnapshot
	{
		internal readonly List<string> Keys;

		internal readonly List<GenesisFeatureContainer> Containers;

		internal readonly List<string> SerializedStates;

		internal GenesisFeatureContainerTransactionSnapshot(List<string> keys, List<GenesisFeatureContainer> containers, List<string> serializedStates)
		{
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Scenario, bool> _003C_003E9__1336_0;

		public static Action<Scenario> _003C_003E9__1336_2;

		public static Func<Scenario, bool> _003C_003E9__1336_3;

		public static Func<Scenario, bool> _003C_003E9__1336_4;

		internal bool _003CTiAge_003Eb__1336_0(Scenario x)
		{
			return false;
		}

		internal void _003CTiAge_003Eb__1336_2(Scenario i)
		{
		}

		internal bool _003CTiAge_003Eb__1336_3(Scenario x)
		{
			return false;
		}

		internal bool _003CTiAge_003Eb__1336_4(Scenario x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass1317_0
	{
		public DataCountry aCountry;

		internal bool _003CSetupNewGenerationHeroFromChildInLifeWithOldHeroNetWorth_003Eb__0(SimPlace x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass1317_1
	{
		public SimHouse house;

		internal bool _003CSetupNewGenerationHeroFromChildInLifeWithOldHeroNetWorth_003Eb__1(SimPropertyLeaseContract t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass1336_0
	{
		public ArrayList whiteCollarCrimeArray;

		internal bool _003CTiAge_003Eb__1(SimCrime x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass1336_1
	{
		public Scenario firstScenario;

		internal void _003CTiAge_003Eb__5()
		{
		}
	}

	[NonSerialized]
	public LifeInterface LifeInterface;

	[NonSerialized]
	public List<Scenario> IncrementAgeScenarioQueueArray;

	public static UnityEvent<int> OnYearIncremented;

	private bool IncrementAgeScenariosInProgress__BackingField;

	private int MetaLifeType__BackingField;

	private string MetaGenerationID__BackingField;

	private string MetaHeroID__BackingField;

	private string MetaLastBootupVersion__BackingField;

	private string MetaStartLifeVersion__BackingField;

	private LifeLog LifeLog__BackingField;

	private RandomDeckCollection _Decks;

	private ArrayList ScenarioLogArray__BackingField;

	private ArrayList CustomPersonsNameUsedArray__BackingField;

	public ObituaryGenerator ObituaryGenerator;

	private Apollo16ChallengeTracker ChallengeTracker__BackingField;

	private Dictionary<string, int> _TriggerEventValueDictionary;

	private TriggerManager _TriggerManager;

	private SimHero Hero__BackingField;

	private SimWorld World__BackingField;

	private SimPast Past__BackingField;

	private SimDeath Death__BackingField;

	private SimParent Father__BackingField;

	private int FatherType__BackingField;

	private SimParent Mother__BackingField;

	private bool HeroWasAdopted__BackingField;

	private string HeroBornToImmigrantParentsFromCountryCode__BackingField;

	private bool HeroHasGayParents__BackingField;

	private SimLover Lover__BackingField;

	private ArrayList _ChildArray__BackingField;

	private SimFinances Finances__BackingField;

	private SimPortfolio _Portfolio__BackingField;

	private SimLandlordPortfolio _LandlordPortfolio__BackingField;

	private List<string> _EnemiesTurnedFriendFullNames__BackingField;

	private int NumEnemiesTurnedFriendsKilled__BackingField;

	private List<string> _SketchyUnprotectedLovers__BackingField;

	private int NumSexualEncountersWithoutCondom__BackingField;

	private bool HasGottenSTDFromUnprotectedSex__BackingField;

	private int NumFightsWithoutRetalitation__BackingField;

	private int NumAccidentalMurdersFromFights__BackingField;

	private bool HasRecordedDiamondAlbum__BackingField;

	private bool HasWonABittyAward__BackingField;

	private bool HasBecomeFamousCEO__BackingField;

	private bool HasBecomeFamousMagician__BackingField;

	private bool HasEncounteredVampire__BackingField;

	private int NumPeopleKilledWithPlasticVampireTeeth__BackingField;

	private bool HasPostedSomethingThatIsNotFeetOnOnlyFans__BackingField;

	private SimOccupation Occupation__BackingField;

	private ArrayList PartTimeJobArray__BackingField;

	private ArrayList DiseaseArray__BackingField;

	private ArrayList AddictionArray__BackingField;

	private ArrayList _HouseArray__BackingField;

	private ArrayList _CarArray__BackingField;

	private SimRichSocialMedia RichSocialMedia__BackingField;

	private SimFame Fame__BackingField;

	private SimRoyal Royal__BackingField;

	private SimWill Will__BackingField;

	private List<SimPerson> _SiblingArray__BackingField;

	private ArrayList _PetArray__BackingField;

	private ArrayList _FriendArray__BackingField;

	private int AgeAtLastFriendsRefresh__BackingField;

	private ArrayList AncestorArray__BackingField;

	private SimOrphanage Orphanage__BackingField;

	private ArrayList _AircraftArray__BackingField;

	private ArrayList _WatercraftArray__BackingField;

	private ArrayList _JewelryArray__BackingField;

	private int SpecialTalent__BackingField;

	private float DeterministicCostAdjustmentPercent__BackingField;

	private string LastFailedEmigrationAttemptedCountry__BackingField;

	private ArrayList _RecreationalVehicleArray__BackingField;

	private ArrayList _DeterministicEmigrationPlaceArray__BackingField;

	private bool DrivingLicense__BackingField;

	private int DrivingTestAttempts__BackingField;

	private int AgeWhenLicensed__BackingField;

	private int NumSexualPartners__BackingField;

	private int AgeAtLastThreesomeAttempt__BackingField;

	private int NumAbortions__BackingField;

	private int TimesCheatedOnLovers__BackingField;

	private int TimesSpreadSTD__BackingField;

	private bool OpenToSexBeforeMarriage__BackingField;

	private int LastSelectedMajor__BackingField;

	private int AgeAtLastAcceptanceToUniversity__BackingField;

	private int AgeAtLastAcceptanceToPostUniversity__BackingField;

	private int AgeAtLastParentsRefusalToPayTuition__BackingField;

	private int AgeAtLastScholarshipRejection__BackingField;

	private int AgeAtLastDrugOfferAccept__BackingField;

	private int AgeAtLastStudy__BackingField;

	private int AgeAtLastWorkHarder__BackingField;

	private int AgeAtLastJobRejection__BackingField;

	private int AgeAtLastJobRejectionByEducation__BackingField;

	private int AgeAtLastCompletedRehab__BackingField;

	private int AgeAtLastGymWorkout__BackingField;

	private int AgeAtLastMeditation__BackingField;

	private int AgeAtLastLibraryVisit__BackingField;

	private int AgeAtLastSnubLoverForVacation__BackingField;

	private int AgeAtLastLoverArgument__BackingField;

	private int AgeAtLastTurnDownChildCollegeTuition__BackingField;

	private int AgeAtLastHookupScenario__BackingField;

	private int AgeAtLastHookupFeedback__BackingField;

	private int AgeAtLastWillUpdate__BackingField;

	private int AgeAtLastParentsRejectedPet__BackingField;

	private int AgeAtLastTurnedDownPet__BackingField;

	private int AgeAtLastClubVisit__BackingField;

	private int NumClubVisits__BackingField;

	private int AgeAtLastSocialMediaPost__BackingField;

	private int NumSocialMediaPosts__BackingField;

	private int AgeAtLastWitchRemedy__BackingField;

	private int AgeAtLastAdditionalBid__BackingField;

	private int AgeAtLastCrashOut__BackingField;

	private string _lifeId;

	[NonSerialized]
	public DataWitchRemedy CurrentWitchRemedy;

	private int AttemptsToTreatAddiction__BackingField;

	private int LivesSaved__BackingField;

	private ArrayList PlasticSurgeriesArray__BackingField;

	private int AgeAtLastPlasticSurgery__BackingField;

	private int AgeAtLastSexChange__BackingField;

	private int NumBotchedPlasticSurgeries__BackingField;

	private ArrayList OfferedDrugsArray__BackingField;

	private ArrayList OfferedAlcoholArray__BackingField;

	private ArrayList OfferedFoodArray__BackingField;

	private int NumTimesTurnedDownDrugsOrAlcohol__BackingField;

	private ArrayList WatchedMoviesArray__BackingField;

	private int AgeAtLastNewsEvent__BackingField;

	private ArrayList NewsEventStringArray__BackingField;

	private int AgeAtLastWarEnd__BackingField;

	private float CasinoEarningsLifetime__BackingField;

	private float CasinoLossesLifetime__BackingField;

	private ArrayList BannedCasinosArray__BackingField;

	private ArrayList EscapedCasinosArray__BackingField;

	private ArrayList VisitedCasinosArray__BackingField;

	private int GamblingConsecutiveUrgeResists__BackingField;

	private int AgeAtLastMurder__BackingField;

	private int NumMurdersCommitted__BackingField;

	private int AgeAtLastHorseRaces__BackingField;

	private float HorseRacesEarningsLifetime__BackingField;

	private ArrayList VisitedCountriesArray__BackingField;

	private bool HasHadVasectomy__BackingField;

	private bool HasHadTubalLigation__BackingField;

	private int AgeAtLastSpermDonationGiven__BackingField;

	private SimPerson LastSpermDonor__BackingField;

	private int NumSpermDonations__BackingField;

	private int NumCarsStolen__BackingField;

	private int NumPocketsPicked__BackingField;

	private int NumHousesBurgled__BackingField;

	private int NumItemsShoplifted__BackingField;

	private int NumPorchesPirated__BackingField;

	private int NumTrainsRobbed__BackingField;

	private int NumBanksRobbed__BackingField;

	private int AgeAtLastBankRobbery__BackingField;

	private int NumHitmenHired__BackingField;

	private int AgeAtLastHitmanFound__BackingField;

	private int NumCrimesAttempted__BackingField;

	private int NumUniqueYearsSpentTimeWithKids__BackingField;

	private int AgeAtLastHairCut__BackingField;

	private int AgeAtLastMassage__BackingField;

	private int AgeAtLastMassageHappyEnding__BackingField;

	private int AgeAtLastNailSalon__BackingField;

	private int AgeAtLastTanning__BackingField;

	private int AgeAtLastWaxing__BackingField;

	private int NumTimesUsedTanningBed__BackingField;

	private int NumTimesUsedTanningBedThisYear__BackingField;

	private int NumTimesAskedForMoney__BackingField;

	private int NumTimesGaveMoney__BackingField;

	private int NumTimesAssaultedSomeone__BackingField;

	private int NumTimesInsultedSomeone__BackingField;

	private int NumTimesComplimentedSomeone__BackingField;

	private int NumTimesAbandonedPet__BackingField;

	private int AgeLovedOneLastDied__BackingField;

	private int AgeAtLastBoost__BackingField;

	private int AgeAtLastOfferedBoost__BackingField;

	private bool BlockFullLogAtStart__BackingField;

	private int AgeAtBlockFullLog__BackingField;

	private string MyStuffId__BackingField;

	private string MyStuffExtraIntro__BackingField;

	private bool ReportInfluencerEvents__BackingField;

	private bool ShowBirthCircumstances__BackingField;

	private AGE_VARIANT AgeAtStartLife__BackingField;

	private CASINO_GAME_TYPE CasinoRVGameType__BackingField;

	private int CasinoRVGameCount__BackingField;

	private ArrayList OfferedBoostArray__BackingField;

	[NonSerialized]
	private ArrayList _LawsuitTargetArray;

	private int ConsecutiveYearsLowStress__BackingField;

	private int ConsecutiveYearsHighStress__BackingField;

	private int AgeAtLastExpulsion__BackingField;

	private SimLover CurrentExLoverStalker__BackingField;

	private int CurrentExLoverStalkerYears__BackingField;

	private int AgeAtLastWentOnBirthControl__BackingField;

	private int AgeAtLastWentOffBirthControl__BackingField;

	private Dictionary<string, object> GenesisData__BackingField;

	private ArrayList _HeirloomArray__BackingField;

	private int AgeAtLastPlayedWithHeirloom__BackingField;

	private ArrayList PrisonGangJoinedArray__BackingField;

	private ArrayList PrisonGangLeftArray__BackingField;

	[NonSerialized]
	public DataVacationCruise LastCruise;

	[NonSerialized]
	public DataVacationDestination LastVacationDestination;

	private int HeroAgeAtLastAttack__BackingField;

	private int HeroAgeAtLastGotAttackedByEnemy__BackingField;

	private int AgeAtLastVacationScenario__BackingField;

	private int AgeAtLastVacationDumbScenario__BackingField;

	private int AgeAtLastIntelligenceTest__BackingField;

	private int AgeAtLastJobInterviewFratBypass__BackingField;

	private bool WasResurrected__BackingField;

	private int AgeAtLastParanormalExperience__BackingField;

	private int AgeAtLastLightningStrike__BackingField;

	private bool BoatingLicense__BackingField;

	private int BoatingTestAttempts__BackingField;

	private int AgeWhenEarnedBoatingLicense__BackingField;

	private int PilotHours__BackingField;

	private bool PilotsLicense__BackingField;

	private int PilotTestAttempts__BackingField;

	private int AgeWhenEarnedPilotsLicense__BackingField;

	private int AgeAtLastGarden__BackingField;

	private int AgeAtLastWalk__BackingField;

	private ArrayList MartialArtArray__BackingField;

	private int AgeAtLastParentRejectionForMartialArts__BackingField;

	private int AgeAtLastMartialArtsAttributeBoost__BackingField;

	private int AgeAtLastParentRejectionForModelingLessons__BackingField;

	private int AgeAtLastModelingLessonAttributeBoost__BackingField;

	private int NumGigAuditionsHeroGotAtThisAge__BackingField;

	private int NumGigsCompletedHeroGotAtThisAge__BackingField;

	private SimDiet Diet__BackingField;

	private double MoneyEarnedFromRealEstate__BackingField;

	private int NumHousesSold__BackingField;

	private int GiftsGiven__BackingField;

	private float ValueOfGiftsGiven__BackingField;

	private int AgeAtLastRanForPoliticalOfficeLocal__BackingField;

	private int AgeAtLastRanForPoliticalOfficeCity__BackingField;

	private int AgeAtLastRanForPoliticalOfficeRegional__BackingField;

	private int AgeAtLastRanForPoliticalOfficeNational__BackingField;

	private bool RejectedPoliticalBribe__BackingField;

	private int PoliticalBribesAccepted__BackingField;

	private bool SweepstakesEntered__BackingField;

	private int AgeAtLastNameChange__BackingField;

	private ArrayList ActiveSurrogateSituationArray__BackingField;

	private bool OptedOutOfVaccination__BackingField;

	private bool MetRoyalLoverThisLife__BackingField;

	private SimSportsWorld SportsLeagues__BackingField;

	private int AgeAtLastSteroids__BackingField;

	private int AgeAtLastRefusedDopingTest__BackingField;

	private int AgeAtLastFailedDopingTest__BackingField;

	private ArrayList _InstrumentArray__BackingField;

	private int AgeAtLastParentRejectionForInstrument__BackingField;

	private int AgeAtLastInstrumentAttributeBoost__BackingField;

	private int AgeAtLastBandSearch__BackingField;

	private int NumBandAuditionOffersAtCurrentAge__BackingField;

	private ArrayList MusicianCachedNameSet__BackingField;

	private int NumLogLinkFillers__BackingField;

	private SimActingLessonExperience ActingLessonExperience__BackingField;

	private SimModelLessonExperience ModelLessonExperience__BackingField;

	private SimDealingExperience DealingExperience__BackingField;

	private int AgeAtLastParentRejectionForActingLessons__BackingField;

	private int AgeAtLastActingLessonAttributeBoost__BackingField;

	private int AgeAtLastAgentSearchFailure__BackingField;

	private SimAgent TalentAgent__BackingField;

	private int AgeAtLastActingExtraSearchFailure__BackingField;

	private int AgeAtLastActingExtraRoleOffer__BackingField;

	private int AgeAtLastActingExtraRoleDiscovery__BackingField;

	private int NumExtraRoles__BackingField;

	private int AgeAtLastJewelryPurchase__BackingField;

	private int AgeAtLastManiPedi__BackingField;

	private int AgeAtLastBloodDonation__BackingField;

	private int AgeAtLastPlasmaDonation__BackingField;

	private int AgeAtLastParentRejectionForSpaceCamp__BackingField;

	private int AgeAtLastVisitedSpaceCamp__BackingField;

	private bool HasHeroVisitedSpaceCamp__BackingField;

	private bool HasHeroJoinedAstronomyClub__BackingField;

	private int AgeAtLastMailOrderBrideMarriage__BackingField;

	private SimLover AvailableMailOrderBride__BackingField;

	private ArrayList _PhysicalInstrumentArray__BackingField;

	private int RewardedVideoRandomJobPack__BackingField;

	private bool RewardedVideoRandomJobPackUnlocked__BackingField;

	private bool RewardedVideoBusinessJobPackUnlocked__BackingField;

	private int RewardedVideoExpansionPack__BackingField;

	private bool RewardedVideoRandomExpansionPackUnlocked__BackingField;

	private bool LifeEndedWithChildInheritingBusiness__BackingField;

	private int AgeAtLastBusinessStartupFundingOffer__BackingField;

	private int AgeAtLastEscapedPrisonViaRewardedVideo__BackingField;

	private int AgeAtLastPray__BackingField;

	private int NumTimesPrayUsed__BackingField;

	private bool HasCurseBeenUsed__BackingField;

	private SimSpyAgency SpyAgency__BackingField;

	private SimZoo Zoo__BackingField;

	private SimCasino Casino__BackingField;

	private int NumCasinoSessionsSinceLastNekoAd__BackingField;

	private int NumTimesCasinoNekoAdShown__BackingField;

	private bool HasCleansedInvestmentMarket__BackingField;

	private SimInvestmentMarket _InvestmentMarket;

	private SimLuxury Luxury__BackingField;

	private int NumTimesCommittedTaxEvasion__BackingField;

	private int NumTimesInsiderTraded__BackingField;

	private bool ExpansionPackIneligible__BackingField;

	private double BestEarthDefenseTime__BackingField;

	private int HeroAgeAtLastProbeDesign__BackingField;

	private ArrayList _BlackMarketAuctionItemsArray;

	private SimModelHeadshot _ModelHeadshot__BackingField;

	private ArrayList _ModelGiftArray__BackingField;

	private ArrayList _CasinoCollectibleArray__BackingField;

	private int AgeAtLastRecruiterModelRequest__BackingField;

	private int NumPromoScenariosShownInLife__BackingField;

	private bool WillShowJustForYouJobPacks__BackingField;

	private bool HasInteractedWithRecruiter__BackingField;

	private SimRacing Racing__BackingField;

	private ArrayList _RacingCollectibleArray__BackingField;

	private SimOutdoor Outdoor__BackingField;

	public SimSeniorCenter _seniorCenter;

	private SimSeniorCenter SeniorCenter__BackingField;

	public GenesisEntityContainer _genesisEntities;

	private List<string> _featureContainerKeys;

	private List<GenesisFeatureContainer> _featureContainerValues;

	[NonSerialized]
	private Dictionary<string, int> _featureContainerSchemaReadyGenerations;

	[NonSerialized]
	private Dictionary<string, GenesisFeatureContainer> _featureContainerSchemaReadyInstances;

	[NonSerialized]
	private bool _reportedFeatureContainerPairRepair;

	[NonSerialized]
	private HashSet<string> _reportedFeatureContainerSchemaDiagnostics;

	private ArrayList _OutdoorFishingTreasureArray__BackingField;

	private bool HasBicycleGivenByParent__BackingField;

	private int NumTimesHackSuccessThisYear__BackingField;

	private SimPodcast PodCast__BackingField;

	private SimMusicProfession _MusicProfession;

	private SimFightClub _fightClub;

	public bool IncrementAgeScenariosInProgress
	{
		get
		{
			return IncrementAgeScenariosInProgress__BackingField;
		}
		set
		{
			IncrementAgeScenariosInProgress__BackingField = value;
		}
	}

	public int MetaLifeType
	{
		get
		{
			return MetaLifeType__BackingField;
		}
		set
		{
			MetaLifeType__BackingField = value;
		}
	}

	public string MetaGenerationID
	{
		get
		{
			return MetaGenerationID__BackingField;
		}
		set
		{
			MetaGenerationID__BackingField = value;
		}
	}

	public string MetaHeroID
	{
		get
		{
			return MetaHeroID__BackingField;
		}
		set
		{
			MetaHeroID__BackingField = value;
		}
	}

	public string MetaLastBootupVersion
	{
		get
		{
			return MetaLastBootupVersion__BackingField;
		}
		set
		{
			MetaLastBootupVersion__BackingField = value;
		}
	}

	public string MetaStartLifeVersion
	{
		get
		{
			return MetaStartLifeVersion__BackingField;
		}
		set
		{
			MetaStartLifeVersion__BackingField = value;
		}
	}

	public LifeLog LifeLog
	{
		get
		{
			return LifeLog__BackingField;
		}
		set
		{
			LifeLog__BackingField = value;
		}
	}

	public RandomDeckCollection Decks
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList ScenarioLogArray
	{
		get
		{
			return ScenarioLogArray__BackingField;
		}
		set
		{
			ScenarioLogArray__BackingField = value;
		}
	}

	public ArrayList CustomPersonsNameUsedArray
	{
		get
		{
			return CustomPersonsNameUsedArray__BackingField;
		}
		set
		{
			CustomPersonsNameUsedArray__BackingField = value;
		}
	}

	public Apollo16ChallengeTracker ChallengeTracker
	{
		get
		{
			return ChallengeTracker__BackingField;
		}
		set
		{
			ChallengeTracker__BackingField = value;
		}
	}

	public Dictionary<string, int> TriggerEventValueDictionary
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public TriggerManager TriggerManager
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimHero Hero
	{
		get
		{
			return Hero__BackingField;
		}
		set
		{
			Hero__BackingField = value;
		}
	}

	public SimWorld World
	{
		get
		{
			return World__BackingField;
		}
		set
		{
			World__BackingField = value;
		}
	}

	public SimPast Past
	{
		get
		{
			return Past__BackingField;
		}
		set
		{
			Past__BackingField = value;
		}
	}

	public SimDeath Death
	{
		get
		{
			return Death__BackingField;
		}
		set
		{
			Death__BackingField = value;
		}
	}

	public SimParent Father
	{
		get
		{
			return Father__BackingField;
		}
		set
		{
			Father__BackingField = value;
		}
	}

	public int FatherType
	{
		get
		{
			return FatherType__BackingField;
		}
		set
		{
			FatherType__BackingField = value;
		}
	}

	public SimParent Mother
	{
		get
		{
			return Mother__BackingField;
		}
		set
		{
			Mother__BackingField = value;
		}
	}

	public bool HeroWasAdopted
	{
		get
		{
			return HeroWasAdopted__BackingField;
		}
		set
		{
			HeroWasAdopted__BackingField = value;
		}
	}

	public string HeroBornToImmigrantParentsFromCountryCode
	{
		get
		{
			return HeroBornToImmigrantParentsFromCountryCode__BackingField;
		}
		set
		{
			HeroBornToImmigrantParentsFromCountryCode__BackingField = value;
		}
	}

	public bool HeroHasGayParents
	{
		get
		{
			return HeroHasGayParents__BackingField;
		}
		set
		{
			HeroHasGayParents__BackingField = value;
		}
	}

	public SimLover Lover
	{
		get
		{
			return Lover__BackingField;
		}
		set
		{
			Lover__BackingField = value;
		}
	}

	private ArrayList _ChildArray
	{
		get
		{
			return _ChildArray__BackingField;
		}
		set
		{
			_ChildArray__BackingField = value;
		}
	}

	public ArrayList ChildArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimFinances Finances
	{
		get
		{
			return Finances__BackingField;
		}
		set
		{
			Finances__BackingField = value;
		}
	}

	private SimPortfolio _Portfolio
	{
		get
		{
			return _Portfolio__BackingField;
		}
		set
		{
			_Portfolio__BackingField = value;
		}
	}

	public SimPortfolio Portfolio
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private SimLandlordPortfolio _LandlordPortfolio
	{
		get
		{
			return _LandlordPortfolio__BackingField;
		}
		set
		{
			_LandlordPortfolio__BackingField = value;
		}
	}

	public SimLandlordPortfolio LandlordPortfolio
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<string> _EnemiesTurnedFriendFullNames
	{
		get
		{
			return _EnemiesTurnedFriendFullNames__BackingField;
		}
		set
		{
			_EnemiesTurnedFriendFullNames__BackingField = value;
		}
	}

	public List<string> EnemiesTurnedFriendFullNames
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumEnemiesTurnedFriendsKilled
	{
		get
		{
			return NumEnemiesTurnedFriendsKilled__BackingField;
		}
		set
		{
			NumEnemiesTurnedFriendsKilled__BackingField = value;
		}
	}

	public List<string> _SketchyUnprotectedLovers
	{
		get
		{
			return _SketchyUnprotectedLovers__BackingField;
		}
		set
		{
			_SketchyUnprotectedLovers__BackingField = value;
		}
	}

	public List<string> SketchyUnprotectedLovers
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumSexualEncountersWithoutCondom
	{
		get
		{
			return NumSexualEncountersWithoutCondom__BackingField;
		}
		set
		{
			NumSexualEncountersWithoutCondom__BackingField = value;
		}
	}

	public bool HasGottenSTDFromUnprotectedSex
	{
		get
		{
			return HasGottenSTDFromUnprotectedSex__BackingField;
		}
		set
		{
			HasGottenSTDFromUnprotectedSex__BackingField = value;
		}
	}

	public int NumFightsWithoutRetalitation
	{
		get
		{
			return NumFightsWithoutRetalitation__BackingField;
		}
		set
		{
			NumFightsWithoutRetalitation__BackingField = value;
		}
	}

	public int NumAccidentalMurdersFromFights
	{
		get
		{
			return NumAccidentalMurdersFromFights__BackingField;
		}
		set
		{
			NumAccidentalMurdersFromFights__BackingField = value;
		}
	}

	public bool HasRecordedDiamondAlbum
	{
		get
		{
			return HasRecordedDiamondAlbum__BackingField;
		}
		set
		{
			HasRecordedDiamondAlbum__BackingField = value;
		}
	}

	public bool HasWonABittyAward
	{
		get
		{
			return HasWonABittyAward__BackingField;
		}
		set
		{
			HasWonABittyAward__BackingField = value;
		}
	}

	public bool HasBecomeFamousCEO
	{
		get
		{
			return HasBecomeFamousCEO__BackingField;
		}
		set
		{
			HasBecomeFamousCEO__BackingField = value;
		}
	}

	public bool HasBecomeFamousMagician
	{
		get
		{
			return HasBecomeFamousMagician__BackingField;
		}
		set
		{
			HasBecomeFamousMagician__BackingField = value;
		}
	}

	public bool HasEncounteredVampire
	{
		get
		{
			return HasEncounteredVampire__BackingField;
		}
		set
		{
			HasEncounteredVampire__BackingField = value;
		}
	}

	public int NumPeopleKilledWithPlasticVampireTeeth
	{
		get
		{
			return NumPeopleKilledWithPlasticVampireTeeth__BackingField;
		}
		set
		{
			NumPeopleKilledWithPlasticVampireTeeth__BackingField = value;
		}
	}

	public bool HasPostedSomethingThatIsNotFeetOnOnlyFans
	{
		get
		{
			return HasPostedSomethingThatIsNotFeetOnOnlyFans__BackingField;
		}
		set
		{
			HasPostedSomethingThatIsNotFeetOnOnlyFans__BackingField = value;
		}
	}

	public SimOccupation Occupation
	{
		get
		{
			return Occupation__BackingField;
		}
		set
		{
			Occupation__BackingField = value;
		}
	}

	public ArrayList PartTimeJobArray
	{
		get
		{
			return PartTimeJobArray__BackingField;
		}
		set
		{
			PartTimeJobArray__BackingField = value;
		}
	}

	public ArrayList DiseaseArray
	{
		get
		{
			return DiseaseArray__BackingField;
		}
		set
		{
			DiseaseArray__BackingField = value;
		}
	}

	public ArrayList AddictionArray
	{
		get
		{
			return AddictionArray__BackingField;
		}
		set
		{
			AddictionArray__BackingField = value;
		}
	}

	private ArrayList _HouseArray
	{
		get
		{
			return _HouseArray__BackingField;
		}
		set
		{
			_HouseArray__BackingField = value;
		}
	}

	public ArrayList HouseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _CarArray
	{
		get
		{
			return _CarArray__BackingField;
		}
		set
		{
			_CarArray__BackingField = value;
		}
	}

	public ArrayList CarArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimRichSocialMedia RichSocialMedia
	{
		get
		{
			return RichSocialMedia__BackingField;
		}
		set
		{
			RichSocialMedia__BackingField = value;
		}
	}

	public SimFame Fame
	{
		get
		{
			return Fame__BackingField;
		}
		set
		{
			Fame__BackingField = value;
		}
	}

	public SimRoyal Royal
	{
		get
		{
			return Royal__BackingField;
		}
		set
		{
			Royal__BackingField = value;
		}
	}

	public SimWill Will
	{
		get
		{
			return Will__BackingField;
		}
		set
		{
			Will__BackingField = value;
		}
	}

	private List<SimPerson> _SiblingArray
	{
		get
		{
			return _SiblingArray__BackingField;
		}
		set
		{
			_SiblingArray__BackingField = value;
		}
	}

	public List<SimPerson> SiblingArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _PetArray
	{
		get
		{
			return _PetArray__BackingField;
		}
		set
		{
			_PetArray__BackingField = value;
		}
	}

	public ArrayList PetArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _FriendArray
	{
		get
		{
			return _FriendArray__BackingField;
		}
		set
		{
			_FriendArray__BackingField = value;
		}
	}

	public ArrayList FriendArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastFriendsRefresh
	{
		get
		{
			return AgeAtLastFriendsRefresh__BackingField;
		}
		set
		{
			AgeAtLastFriendsRefresh__BackingField = value;
		}
	}

	public ArrayList AncestorArray
	{
		get
		{
			return AncestorArray__BackingField;
		}
		set
		{
			AncestorArray__BackingField = value;
		}
	}

	public SimOrphanage Orphanage
	{
		get
		{
			return Orphanage__BackingField;
		}
		set
		{
			Orphanage__BackingField = value;
		}
	}

	private ArrayList _AircraftArray
	{
		get
		{
			return _AircraftArray__BackingField;
		}
		set
		{
			_AircraftArray__BackingField = value;
		}
	}

	public ArrayList AircraftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _WatercraftArray
	{
		get
		{
			return _WatercraftArray__BackingField;
		}
		set
		{
			_WatercraftArray__BackingField = value;
		}
	}

	public ArrayList WatercraftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _JewelryArray
	{
		get
		{
			return _JewelryArray__BackingField;
		}
		set
		{
			_JewelryArray__BackingField = value;
		}
	}

	public ArrayList JewelryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int SpecialTalent
	{
		get
		{
			return SpecialTalent__BackingField;
		}
		set
		{
			SpecialTalent__BackingField = value;
		}
	}

	public float DeterministicCostAdjustmentPercent
	{
		get
		{
			return DeterministicCostAdjustmentPercent__BackingField;
		}
		set
		{
			DeterministicCostAdjustmentPercent__BackingField = value;
		}
	}

	public string LastFailedEmigrationAttemptedCountry
	{
		get
		{
			return LastFailedEmigrationAttemptedCountry__BackingField;
		}
		set
		{
			LastFailedEmigrationAttemptedCountry__BackingField = value;
		}
	}

	private ArrayList _RecreationalVehicleArray
	{
		get
		{
			return _RecreationalVehicleArray__BackingField;
		}
		set
		{
			_RecreationalVehicleArray__BackingField = value;
		}
	}

	public ArrayList RecreationalVehicleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _DeterministicEmigrationPlaceArray
	{
		get
		{
			return _DeterministicEmigrationPlaceArray__BackingField;
		}
		set
		{
			_DeterministicEmigrationPlaceArray__BackingField = value;
		}
	}

	public ArrayList DeterministicEmigrationPlaceArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool DrivingLicense
	{
		get
		{
			return DrivingLicense__BackingField;
		}
		set
		{
			DrivingLicense__BackingField = value;
		}
	}

	public int DrivingTestAttempts
	{
		get
		{
			return DrivingTestAttempts__BackingField;
		}
		set
		{
			DrivingTestAttempts__BackingField = value;
		}
	}

	public int AgeWhenLicensed
	{
		get
		{
			return AgeWhenLicensed__BackingField;
		}
		set
		{
			AgeWhenLicensed__BackingField = value;
		}
	}

	public int NumSexualPartners
	{
		get
		{
			return NumSexualPartners__BackingField;
		}
		set
		{
			NumSexualPartners__BackingField = value;
		}
	}

	public int AgeAtLastThreesomeAttempt
	{
		get
		{
			return AgeAtLastThreesomeAttempt__BackingField;
		}
		set
		{
			AgeAtLastThreesomeAttempt__BackingField = value;
		}
	}

	public int NumAbortions
	{
		get
		{
			return NumAbortions__BackingField;
		}
		set
		{
			NumAbortions__BackingField = value;
		}
	}

	public int TimesCheatedOnLovers
	{
		get
		{
			return TimesCheatedOnLovers__BackingField;
		}
		set
		{
			TimesCheatedOnLovers__BackingField = value;
		}
	}

	public int TimesSpreadSTD
	{
		get
		{
			return TimesSpreadSTD__BackingField;
		}
		set
		{
			TimesSpreadSTD__BackingField = value;
		}
	}

	public bool OpenToSexBeforeMarriage
	{
		get
		{
			return OpenToSexBeforeMarriage__BackingField;
		}
		set
		{
			OpenToSexBeforeMarriage__BackingField = value;
		}
	}

	public int LastSelectedMajor
	{
		get
		{
			return LastSelectedMajor__BackingField;
		}
		set
		{
			LastSelectedMajor__BackingField = value;
		}
	}

	public int AgeAtLastAcceptanceToUniversity
	{
		get
		{
			return AgeAtLastAcceptanceToUniversity__BackingField;
		}
		set
		{
			AgeAtLastAcceptanceToUniversity__BackingField = value;
		}
	}

	public int AgeAtLastAcceptanceToPostUniversity
	{
		get
		{
			return AgeAtLastAcceptanceToPostUniversity__BackingField;
		}
		set
		{
			AgeAtLastAcceptanceToPostUniversity__BackingField = value;
		}
	}

	public int AgeAtLastParentsRefusalToPayTuition
	{
		get
		{
			return AgeAtLastParentsRefusalToPayTuition__BackingField;
		}
		set
		{
			AgeAtLastParentsRefusalToPayTuition__BackingField = value;
		}
	}

	public int AgeAtLastScholarshipRejection
	{
		get
		{
			return AgeAtLastScholarshipRejection__BackingField;
		}
		set
		{
			AgeAtLastScholarshipRejection__BackingField = value;
		}
	}

	public int AgeAtLastDrugOfferAccept
	{
		get
		{
			return AgeAtLastDrugOfferAccept__BackingField;
		}
		set
		{
			AgeAtLastDrugOfferAccept__BackingField = value;
		}
	}

	public int AgeAtLastStudy
	{
		get
		{
			return AgeAtLastStudy__BackingField;
		}
		set
		{
			AgeAtLastStudy__BackingField = value;
		}
	}

	public int AgeAtLastWorkHarder
	{
		get
		{
			return AgeAtLastWorkHarder__BackingField;
		}
		set
		{
			AgeAtLastWorkHarder__BackingField = value;
		}
	}

	public int AgeAtLastJobRejection
	{
		get
		{
			return AgeAtLastJobRejection__BackingField;
		}
		set
		{
			AgeAtLastJobRejection__BackingField = value;
		}
	}

	public int AgeAtLastJobRejectionByEducation
	{
		get
		{
			return AgeAtLastJobRejectionByEducation__BackingField;
		}
		set
		{
			AgeAtLastJobRejectionByEducation__BackingField = value;
		}
	}

	public int AgeAtLastCompletedRehab
	{
		get
		{
			return AgeAtLastCompletedRehab__BackingField;
		}
		set
		{
			AgeAtLastCompletedRehab__BackingField = value;
		}
	}

	public int AgeAtLastGymWorkout
	{
		get
		{
			return AgeAtLastGymWorkout__BackingField;
		}
		set
		{
			AgeAtLastGymWorkout__BackingField = value;
		}
	}

	public int AgeAtLastMeditation
	{
		get
		{
			return AgeAtLastMeditation__BackingField;
		}
		set
		{
			AgeAtLastMeditation__BackingField = value;
		}
	}

	public int AgeAtLastLibraryVisit
	{
		get
		{
			return AgeAtLastLibraryVisit__BackingField;
		}
		set
		{
			AgeAtLastLibraryVisit__BackingField = value;
		}
	}

	public int AgeAtLastSnubLoverForVacation
	{
		get
		{
			return AgeAtLastSnubLoverForVacation__BackingField;
		}
		set
		{
			AgeAtLastSnubLoverForVacation__BackingField = value;
		}
	}

	public int AgeAtLastLoverArgument
	{
		get
		{
			return AgeAtLastLoverArgument__BackingField;
		}
		set
		{
			AgeAtLastLoverArgument__BackingField = value;
		}
	}

	public int AgeAtLastTurnDownChildCollegeTuition
	{
		get
		{
			return AgeAtLastTurnDownChildCollegeTuition__BackingField;
		}
		set
		{
			AgeAtLastTurnDownChildCollegeTuition__BackingField = value;
		}
	}

	public int AgeAtLastHookupScenario
	{
		get
		{
			return AgeAtLastHookupScenario__BackingField;
		}
		set
		{
			AgeAtLastHookupScenario__BackingField = value;
		}
	}

	public int AgeAtLastHookupFeedback
	{
		get
		{
			return AgeAtLastHookupFeedback__BackingField;
		}
		set
		{
			AgeAtLastHookupFeedback__BackingField = value;
		}
	}

	public int AgeAtLastWillUpdate
	{
		get
		{
			return AgeAtLastWillUpdate__BackingField;
		}
		set
		{
			AgeAtLastWillUpdate__BackingField = value;
		}
	}

	public int AgeAtLastParentsRejectedPet
	{
		get
		{
			return AgeAtLastParentsRejectedPet__BackingField;
		}
		set
		{
			AgeAtLastParentsRejectedPet__BackingField = value;
		}
	}

	public int AgeAtLastTurnedDownPet
	{
		get
		{
			return AgeAtLastTurnedDownPet__BackingField;
		}
		set
		{
			AgeAtLastTurnedDownPet__BackingField = value;
		}
	}

	public int AgeAtLastClubVisit
	{
		get
		{
			return AgeAtLastClubVisit__BackingField;
		}
		set
		{
			AgeAtLastClubVisit__BackingField = value;
		}
	}

	public int NumClubVisits
	{
		get
		{
			return NumClubVisits__BackingField;
		}
		set
		{
			NumClubVisits__BackingField = value;
		}
	}

	public int AgeAtLastSocialMediaPost
	{
		get
		{
			return AgeAtLastSocialMediaPost__BackingField;
		}
		set
		{
			AgeAtLastSocialMediaPost__BackingField = value;
		}
	}

	public int NumSocialMediaPosts
	{
		get
		{
			return NumSocialMediaPosts__BackingField;
		}
		set
		{
			NumSocialMediaPosts__BackingField = value;
		}
	}

	public int AgeAtLastWitchRemedy
	{
		get
		{
			return AgeAtLastWitchRemedy__BackingField;
		}
		set
		{
			AgeAtLastWitchRemedy__BackingField = value;
		}
	}

	public int AgeAtLastAdditionalBid
	{
		get
		{
			return AgeAtLastAdditionalBid__BackingField;
		}
		set
		{
			AgeAtLastAdditionalBid__BackingField = value;
		}
	}

	public int AgeAtLastCrashOut
	{
		get
		{
			return AgeAtLastCrashOut__BackingField;
		}
		set
		{
			AgeAtLastCrashOut__BackingField = value;
		}
	}

	public string LifeId
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AttemptsToTreatAddiction
	{
		get
		{
			return AttemptsToTreatAddiction__BackingField;
		}
		set
		{
			AttemptsToTreatAddiction__BackingField = value;
		}
	}

	public int LivesSaved
	{
		get
		{
			return LivesSaved__BackingField;
		}
		set
		{
			LivesSaved__BackingField = value;
		}
	}

	public ArrayList PlasticSurgeriesArray
	{
		get
		{
			return PlasticSurgeriesArray__BackingField;
		}
		set
		{
			PlasticSurgeriesArray__BackingField = value;
		}
	}

	public int AgeAtLastPlasticSurgery
	{
		get
		{
			return AgeAtLastPlasticSurgery__BackingField;
		}
		set
		{
			AgeAtLastPlasticSurgery__BackingField = value;
		}
	}

	public int AgeAtLastSexChange
	{
		get
		{
			return AgeAtLastSexChange__BackingField;
		}
		set
		{
			AgeAtLastSexChange__BackingField = value;
		}
	}

	public int NumBotchedPlasticSurgeries
	{
		get
		{
			return NumBotchedPlasticSurgeries__BackingField;
		}
		set
		{
			NumBotchedPlasticSurgeries__BackingField = value;
		}
	}

	public ArrayList OfferedDrugsArray
	{
		get
		{
			return OfferedDrugsArray__BackingField;
		}
		set
		{
			OfferedDrugsArray__BackingField = value;
		}
	}

	public ArrayList OfferedAlcoholArray
	{
		get
		{
			return OfferedAlcoholArray__BackingField;
		}
		set
		{
			OfferedAlcoholArray__BackingField = value;
		}
	}

	public ArrayList OfferedFoodArray
	{
		get
		{
			return OfferedFoodArray__BackingField;
		}
		set
		{
			OfferedFoodArray__BackingField = value;
		}
	}

	public int NumTimesTurnedDownDrugsOrAlcohol
	{
		get
		{
			return NumTimesTurnedDownDrugsOrAlcohol__BackingField;
		}
		set
		{
			NumTimesTurnedDownDrugsOrAlcohol__BackingField = value;
		}
	}

	public ArrayList WatchedMoviesArray
	{
		get
		{
			return WatchedMoviesArray__BackingField;
		}
		set
		{
			WatchedMoviesArray__BackingField = value;
		}
	}

	public int AgeAtLastNewsEvent
	{
		get
		{
			return AgeAtLastNewsEvent__BackingField;
		}
		set
		{
			AgeAtLastNewsEvent__BackingField = value;
		}
	}

	public ArrayList NewsEventStringArray
	{
		get
		{
			return NewsEventStringArray__BackingField;
		}
		set
		{
			NewsEventStringArray__BackingField = value;
		}
	}

	public int AgeAtLastWarEnd
	{
		get
		{
			return AgeAtLastWarEnd__BackingField;
		}
		set
		{
			AgeAtLastWarEnd__BackingField = value;
		}
	}

	public float CasinoEarningsLifetime
	{
		get
		{
			return CasinoEarningsLifetime__BackingField;
		}
		set
		{
			CasinoEarningsLifetime__BackingField = value;
		}
	}

	public float CasinoLossesLifetime
	{
		get
		{
			return CasinoLossesLifetime__BackingField;
		}
		set
		{
			CasinoLossesLifetime__BackingField = value;
		}
	}

	public ArrayList BannedCasinosArray
	{
		get
		{
			return BannedCasinosArray__BackingField;
		}
		set
		{
			BannedCasinosArray__BackingField = value;
		}
	}

	public ArrayList EscapedCasinosArray
	{
		get
		{
			return EscapedCasinosArray__BackingField;
		}
		set
		{
			EscapedCasinosArray__BackingField = value;
		}
	}

	public ArrayList VisitedCasinosArray
	{
		get
		{
			return VisitedCasinosArray__BackingField;
		}
		set
		{
			VisitedCasinosArray__BackingField = value;
		}
	}

	public int GamblingConsecutiveUrgeResists
	{
		get
		{
			return GamblingConsecutiveUrgeResists__BackingField;
		}
		set
		{
			GamblingConsecutiveUrgeResists__BackingField = value;
		}
	}

	public int AgeAtLastMurder
	{
		get
		{
			return AgeAtLastMurder__BackingField;
		}
		set
		{
			AgeAtLastMurder__BackingField = value;
		}
	}

	public int NumMurdersCommitted
	{
		get
		{
			return NumMurdersCommitted__BackingField;
		}
		set
		{
			NumMurdersCommitted__BackingField = value;
		}
	}

	public int AgeAtLastHorseRaces
	{
		get
		{
			return AgeAtLastHorseRaces__BackingField;
		}
		set
		{
			AgeAtLastHorseRaces__BackingField = value;
		}
	}

	public float HorseRacesEarningsLifetime
	{
		get
		{
			return HorseRacesEarningsLifetime__BackingField;
		}
		set
		{
			HorseRacesEarningsLifetime__BackingField = value;
		}
	}

	public ArrayList VisitedCountriesArray
	{
		get
		{
			return VisitedCountriesArray__BackingField;
		}
		set
		{
			VisitedCountriesArray__BackingField = value;
		}
	}

	public bool HasHadVasectomy
	{
		get
		{
			return HasHadVasectomy__BackingField;
		}
		set
		{
			HasHadVasectomy__BackingField = value;
		}
	}

	public bool HasHadTubalLigation
	{
		get
		{
			return HasHadTubalLigation__BackingField;
		}
		set
		{
			HasHadTubalLigation__BackingField = value;
		}
	}

	public int AgeAtLastSpermDonationGiven
	{
		get
		{
			return AgeAtLastSpermDonationGiven__BackingField;
		}
		set
		{
			AgeAtLastSpermDonationGiven__BackingField = value;
		}
	}

	public SimPerson LastSpermDonor
	{
		get
		{
			return LastSpermDonor__BackingField;
		}
		set
		{
			LastSpermDonor__BackingField = value;
		}
	}

	public int NumSpermDonations
	{
		get
		{
			return NumSpermDonations__BackingField;
		}
		set
		{
			NumSpermDonations__BackingField = value;
		}
	}

	public int NumCarsStolen
	{
		get
		{
			return NumCarsStolen__BackingField;
		}
		set
		{
			NumCarsStolen__BackingField = value;
		}
	}

	public int NumPocketsPicked
	{
		get
		{
			return NumPocketsPicked__BackingField;
		}
		set
		{
			NumPocketsPicked__BackingField = value;
		}
	}

	public int NumHousesBurgled
	{
		get
		{
			return NumHousesBurgled__BackingField;
		}
		set
		{
			NumHousesBurgled__BackingField = value;
		}
	}

	public int NumItemsShoplifted
	{
		get
		{
			return NumItemsShoplifted__BackingField;
		}
		set
		{
			NumItemsShoplifted__BackingField = value;
		}
	}

	public int NumPorchesPirated
	{
		get
		{
			return NumPorchesPirated__BackingField;
		}
		set
		{
			NumPorchesPirated__BackingField = value;
		}
	}

	public int NumTrainsRobbed
	{
		get
		{
			return NumTrainsRobbed__BackingField;
		}
		set
		{
			NumTrainsRobbed__BackingField = value;
		}
	}

	public int NumBanksRobbed
	{
		get
		{
			return NumBanksRobbed__BackingField;
		}
		set
		{
			NumBanksRobbed__BackingField = value;
		}
	}

	public int AgeAtLastBankRobbery
	{
		get
		{
			return AgeAtLastBankRobbery__BackingField;
		}
		set
		{
			AgeAtLastBankRobbery__BackingField = value;
		}
	}

	public int NumHitmenHired
	{
		get
		{
			return NumHitmenHired__BackingField;
		}
		set
		{
			NumHitmenHired__BackingField = value;
		}
	}

	public int AgeAtLastHitmanFound
	{
		get
		{
			return AgeAtLastHitmanFound__BackingField;
		}
		set
		{
			AgeAtLastHitmanFound__BackingField = value;
		}
	}

	public int NumCrimesAttempted
	{
		get
		{
			return NumCrimesAttempted__BackingField;
		}
		set
		{
			NumCrimesAttempted__BackingField = value;
		}
	}

	public int NumUniqueYearsSpentTimeWithKids
	{
		get
		{
			return NumUniqueYearsSpentTimeWithKids__BackingField;
		}
		set
		{
			NumUniqueYearsSpentTimeWithKids__BackingField = value;
		}
	}

	public int AgeAtLastHairCut
	{
		get
		{
			return AgeAtLastHairCut__BackingField;
		}
		set
		{
			AgeAtLastHairCut__BackingField = value;
		}
	}

	public int AgeAtLastMassage
	{
		get
		{
			return AgeAtLastMassage__BackingField;
		}
		set
		{
			AgeAtLastMassage__BackingField = value;
		}
	}

	public int AgeAtLastMassageHappyEnding
	{
		get
		{
			return AgeAtLastMassageHappyEnding__BackingField;
		}
		set
		{
			AgeAtLastMassageHappyEnding__BackingField = value;
		}
	}

	public int AgeAtLastNailSalon
	{
		get
		{
			return AgeAtLastNailSalon__BackingField;
		}
		set
		{
			AgeAtLastNailSalon__BackingField = value;
		}
	}

	public int AgeAtLastTanning
	{
		get
		{
			return AgeAtLastTanning__BackingField;
		}
		set
		{
			AgeAtLastTanning__BackingField = value;
		}
	}

	public int AgeAtLastWaxing
	{
		get
		{
			return AgeAtLastWaxing__BackingField;
		}
		set
		{
			AgeAtLastWaxing__BackingField = value;
		}
	}

	public int NumTimesUsedTanningBed
	{
		get
		{
			return NumTimesUsedTanningBed__BackingField;
		}
		set
		{
			NumTimesUsedTanningBed__BackingField = value;
		}
	}

	public int NumTimesUsedTanningBedThisYear
	{
		get
		{
			return NumTimesUsedTanningBedThisYear__BackingField;
		}
		set
		{
			NumTimesUsedTanningBedThisYear__BackingField = value;
		}
	}

	public int NumTimesAskedForMoney
	{
		get
		{
			return NumTimesAskedForMoney__BackingField;
		}
		set
		{
			NumTimesAskedForMoney__BackingField = value;
		}
	}

	public int NumTimesGaveMoney
	{
		get
		{
			return NumTimesGaveMoney__BackingField;
		}
		set
		{
			NumTimesGaveMoney__BackingField = value;
		}
	}

	public int NumTimesAssaultedSomeone
	{
		get
		{
			return NumTimesAssaultedSomeone__BackingField;
		}
		set
		{
			NumTimesAssaultedSomeone__BackingField = value;
		}
	}

	public int NumTimesInsultedSomeone
	{
		get
		{
			return NumTimesInsultedSomeone__BackingField;
		}
		set
		{
			NumTimesInsultedSomeone__BackingField = value;
		}
	}

	public int NumTimesComplimentedSomeone
	{
		get
		{
			return NumTimesComplimentedSomeone__BackingField;
		}
		set
		{
			NumTimesComplimentedSomeone__BackingField = value;
		}
	}

	public int NumTimesAbandonedPet
	{
		get
		{
			return NumTimesAbandonedPet__BackingField;
		}
		set
		{
			NumTimesAbandonedPet__BackingField = value;
		}
	}

	public int AgeLovedOneLastDied
	{
		get
		{
			return AgeLovedOneLastDied__BackingField;
		}
		set
		{
			AgeLovedOneLastDied__BackingField = value;
		}
	}

	public int AgeAtLastBoost
	{
		get
		{
			return AgeAtLastBoost__BackingField;
		}
		set
		{
			AgeAtLastBoost__BackingField = value;
		}
	}

	public int AgeAtLastOfferedBoost
	{
		get
		{
			return AgeAtLastOfferedBoost__BackingField;
		}
		set
		{
			AgeAtLastOfferedBoost__BackingField = value;
		}
	}

	public bool BlockFullLogAtStart
	{
		get
		{
			return BlockFullLogAtStart__BackingField;
		}
		set
		{
			BlockFullLogAtStart__BackingField = value;
		}
	}

	public int AgeAtBlockFullLog
	{
		get
		{
			return AgeAtBlockFullLog__BackingField;
		}
		set
		{
			AgeAtBlockFullLog__BackingField = value;
		}
	}

	public string MyStuffId
	{
		get
		{
			return MyStuffId__BackingField;
		}
		set
		{
			MyStuffId__BackingField = value;
		}
	}

	public string MyStuffExtraIntro
	{
		get
		{
			return MyStuffExtraIntro__BackingField;
		}
		set
		{
			MyStuffExtraIntro__BackingField = value;
		}
	}

	public bool ReportInfluencerEvents
	{
		get
		{
			return ReportInfluencerEvents__BackingField;
		}
		set
		{
			ReportInfluencerEvents__BackingField = value;
		}
	}

	public bool ShowBirthCircumstances
	{
		get
		{
			return ShowBirthCircumstances__BackingField;
		}
		set
		{
			ShowBirthCircumstances__BackingField = value;
		}
	}

	public AGE_VARIANT AgeAtStartLife
	{
		get
		{
			return AgeAtStartLife__BackingField;
		}
		set
		{
			AgeAtStartLife__BackingField = value;
		}
	}

	public CASINO_GAME_TYPE CasinoRVGameType
	{
		get
		{
			return CasinoRVGameType__BackingField;
		}
		set
		{
			CasinoRVGameType__BackingField = value;
		}
	}

	public int CasinoRVGameCount
	{
		get
		{
			return CasinoRVGameCount__BackingField;
		}
		set
		{
			CasinoRVGameCount__BackingField = value;
		}
	}

	public ArrayList OfferedBoostArray
	{
		get
		{
			return OfferedBoostArray__BackingField;
		}
		set
		{
			OfferedBoostArray__BackingField = value;
		}
	}

	public ArrayList LawsuitTargetArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int ConsecutiveYearsLowStress
	{
		get
		{
			return ConsecutiveYearsLowStress__BackingField;
		}
		set
		{
			ConsecutiveYearsLowStress__BackingField = value;
		}
	}

	public int ConsecutiveYearsHighStress
	{
		get
		{
			return ConsecutiveYearsHighStress__BackingField;
		}
		set
		{
			ConsecutiveYearsHighStress__BackingField = value;
		}
	}

	public int AgeAtLastExpulsion
	{
		get
		{
			return AgeAtLastExpulsion__BackingField;
		}
		set
		{
			AgeAtLastExpulsion__BackingField = value;
		}
	}

	public SimLover CurrentExLoverStalker
	{
		get
		{
			return CurrentExLoverStalker__BackingField;
		}
		set
		{
			CurrentExLoverStalker__BackingField = value;
		}
	}

	public int CurrentExLoverStalkerYears
	{
		get
		{
			return CurrentExLoverStalkerYears__BackingField;
		}
		set
		{
			CurrentExLoverStalkerYears__BackingField = value;
		}
	}

	public int AgeAtLastWentOnBirthControl
	{
		get
		{
			return AgeAtLastWentOnBirthControl__BackingField;
		}
		set
		{
			AgeAtLastWentOnBirthControl__BackingField = value;
		}
	}

	public int AgeAtLastWentOffBirthControl
	{
		get
		{
			return AgeAtLastWentOffBirthControl__BackingField;
		}
		set
		{
			AgeAtLastWentOffBirthControl__BackingField = value;
		}
	}

	public Dictionary<string, object> GenesisData
	{
		get
		{
			return GenesisData__BackingField;
		}
		set
		{
			GenesisData__BackingField = value;
		}
	}

	private ArrayList _HeirloomArray
	{
		get
		{
			return _HeirloomArray__BackingField;
		}
		set
		{
			_HeirloomArray__BackingField = value;
		}
	}

	public ArrayList HeirloomArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastPlayedWithHeirloom
	{
		get
		{
			return AgeAtLastPlayedWithHeirloom__BackingField;
		}
		set
		{
			AgeAtLastPlayedWithHeirloom__BackingField = value;
		}
	}

	public ArrayList PrisonGangJoinedArray
	{
		get
		{
			return PrisonGangJoinedArray__BackingField;
		}
		set
		{
			PrisonGangJoinedArray__BackingField = value;
		}
	}

	public ArrayList PrisonGangLeftArray
	{
		get
		{
			return PrisonGangLeftArray__BackingField;
		}
		set
		{
			PrisonGangLeftArray__BackingField = value;
		}
	}

	public int HeroAgeAtLastAttack
	{
		get
		{
			return HeroAgeAtLastAttack__BackingField;
		}
		set
		{
			HeroAgeAtLastAttack__BackingField = value;
		}
	}

	public int HeroAgeAtLastGotAttackedByEnemy
	{
		get
		{
			return HeroAgeAtLastGotAttackedByEnemy__BackingField;
		}
		set
		{
			HeroAgeAtLastGotAttackedByEnemy__BackingField = value;
		}
	}

	public int AgeAtLastVacationScenario
	{
		get
		{
			return AgeAtLastVacationScenario__BackingField;
		}
		set
		{
			AgeAtLastVacationScenario__BackingField = value;
		}
	}

	public int AgeAtLastVacationDumbScenario
	{
		get
		{
			return AgeAtLastVacationDumbScenario__BackingField;
		}
		set
		{
			AgeAtLastVacationDumbScenario__BackingField = value;
		}
	}

	public int AgeAtLastIntelligenceTest
	{
		get
		{
			return AgeAtLastIntelligenceTest__BackingField;
		}
		set
		{
			AgeAtLastIntelligenceTest__BackingField = value;
		}
	}

	public int AgeAtLastJobInterviewFratBypass
	{
		get
		{
			return AgeAtLastJobInterviewFratBypass__BackingField;
		}
		set
		{
			AgeAtLastJobInterviewFratBypass__BackingField = value;
		}
	}

	public bool WasResurrected
	{
		get
		{
			return WasResurrected__BackingField;
		}
		set
		{
			WasResurrected__BackingField = value;
		}
	}

	public int AgeAtLastParanormalExperience
	{
		get
		{
			return AgeAtLastParanormalExperience__BackingField;
		}
		set
		{
			AgeAtLastParanormalExperience__BackingField = value;
		}
	}

	public int AgeAtLastLightningStrike
	{
		get
		{
			return AgeAtLastLightningStrike__BackingField;
		}
		set
		{
			AgeAtLastLightningStrike__BackingField = value;
		}
	}

	public bool BoatingLicense
	{
		get
		{
			return BoatingLicense__BackingField;
		}
		set
		{
			BoatingLicense__BackingField = value;
		}
	}

	public int BoatingTestAttempts
	{
		get
		{
			return BoatingTestAttempts__BackingField;
		}
		set
		{
			BoatingTestAttempts__BackingField = value;
		}
	}

	public int AgeWhenEarnedBoatingLicense
	{
		get
		{
			return AgeWhenEarnedBoatingLicense__BackingField;
		}
		set
		{
			AgeWhenEarnedBoatingLicense__BackingField = value;
		}
	}

	public int PilotHours
	{
		get
		{
			return PilotHours__BackingField;
		}
		set
		{
			PilotHours__BackingField = value;
		}
	}

	public bool PilotsLicense
	{
		get
		{
			return PilotsLicense__BackingField;
		}
		set
		{
			PilotsLicense__BackingField = value;
		}
	}

	public int PilotTestAttempts
	{
		get
		{
			return PilotTestAttempts__BackingField;
		}
		set
		{
			PilotTestAttempts__BackingField = value;
		}
	}

	public int AgeWhenEarnedPilotsLicense
	{
		get
		{
			return AgeWhenEarnedPilotsLicense__BackingField;
		}
		set
		{
			AgeWhenEarnedPilotsLicense__BackingField = value;
		}
	}

	public int AgeAtLastGarden
	{
		get
		{
			return AgeAtLastGarden__BackingField;
		}
		set
		{
			AgeAtLastGarden__BackingField = value;
		}
	}

	public int AgeAtLastWalk
	{
		get
		{
			return AgeAtLastWalk__BackingField;
		}
		set
		{
			AgeAtLastWalk__BackingField = value;
		}
	}

	public ArrayList MartialArtArray
	{
		get
		{
			return MartialArtArray__BackingField;
		}
		set
		{
			MartialArtArray__BackingField = value;
		}
	}

	public int AgeAtLastParentRejectionForMartialArts
	{
		get
		{
			return AgeAtLastParentRejectionForMartialArts__BackingField;
		}
		set
		{
			AgeAtLastParentRejectionForMartialArts__BackingField = value;
		}
	}

	public int AgeAtLastMartialArtsAttributeBoost
	{
		get
		{
			return AgeAtLastMartialArtsAttributeBoost__BackingField;
		}
		set
		{
			AgeAtLastMartialArtsAttributeBoost__BackingField = value;
		}
	}

	public int AgeAtLastParentRejectionForModelingLessons
	{
		get
		{
			return AgeAtLastParentRejectionForModelingLessons__BackingField;
		}
		set
		{
			AgeAtLastParentRejectionForModelingLessons__BackingField = value;
		}
	}

	public int AgeAtLastModelingLessonAttributeBoost
	{
		get
		{
			return AgeAtLastModelingLessonAttributeBoost__BackingField;
		}
		set
		{
			AgeAtLastModelingLessonAttributeBoost__BackingField = value;
		}
	}

	public int NumGigAuditionsHeroGotAtThisAge
	{
		get
		{
			return NumGigAuditionsHeroGotAtThisAge__BackingField;
		}
		set
		{
			NumGigAuditionsHeroGotAtThisAge__BackingField = value;
		}
	}

	public int NumGigsCompletedHeroGotAtThisAge
	{
		get
		{
			return NumGigsCompletedHeroGotAtThisAge__BackingField;
		}
		set
		{
			NumGigsCompletedHeroGotAtThisAge__BackingField = value;
		}
	}

	public SimDiet Diet
	{
		get
		{
			return Diet__BackingField;
		}
		set
		{
			Diet__BackingField = value;
		}
	}

	public double MoneyEarnedFromRealEstate
	{
		get
		{
			return MoneyEarnedFromRealEstate__BackingField;
		}
		set
		{
			MoneyEarnedFromRealEstate__BackingField = value;
		}
	}

	public int NumHousesSold
	{
		get
		{
			return NumHousesSold__BackingField;
		}
		set
		{
			NumHousesSold__BackingField = value;
		}
	}

	public int GiftsGiven
	{
		get
		{
			return GiftsGiven__BackingField;
		}
		set
		{
			GiftsGiven__BackingField = value;
		}
	}

	public float ValueOfGiftsGiven
	{
		get
		{
			return ValueOfGiftsGiven__BackingField;
		}
		set
		{
			ValueOfGiftsGiven__BackingField = value;
		}
	}

	public int AgeAtLastRanForPoliticalOfficeLocal
	{
		get
		{
			return AgeAtLastRanForPoliticalOfficeLocal__BackingField;
		}
		set
		{
			AgeAtLastRanForPoliticalOfficeLocal__BackingField = value;
		}
	}

	public int AgeAtLastRanForPoliticalOfficeCity
	{
		get
		{
			return AgeAtLastRanForPoliticalOfficeCity__BackingField;
		}
		set
		{
			AgeAtLastRanForPoliticalOfficeCity__BackingField = value;
		}
	}

	public int AgeAtLastRanForPoliticalOfficeRegional
	{
		get
		{
			return AgeAtLastRanForPoliticalOfficeRegional__BackingField;
		}
		set
		{
			AgeAtLastRanForPoliticalOfficeRegional__BackingField = value;
		}
	}

	public int AgeAtLastRanForPoliticalOfficeNational
	{
		get
		{
			return AgeAtLastRanForPoliticalOfficeNational__BackingField;
		}
		set
		{
			AgeAtLastRanForPoliticalOfficeNational__BackingField = value;
		}
	}

	public bool RejectedPoliticalBribe
	{
		get
		{
			return RejectedPoliticalBribe__BackingField;
		}
		set
		{
			RejectedPoliticalBribe__BackingField = value;
		}
	}

	public int PoliticalBribesAccepted
	{
		get
		{
			return PoliticalBribesAccepted__BackingField;
		}
		set
		{
			PoliticalBribesAccepted__BackingField = value;
		}
	}

	public bool SweepstakesEntered
	{
		get
		{
			return SweepstakesEntered__BackingField;
		}
		set
		{
			SweepstakesEntered__BackingField = value;
		}
	}

	public int AgeAtLastNameChange
	{
		get
		{
			return AgeAtLastNameChange__BackingField;
		}
		set
		{
			AgeAtLastNameChange__BackingField = value;
		}
	}

	public ArrayList ActiveSurrogateSituationArray
	{
		get
		{
			return ActiveSurrogateSituationArray__BackingField;
		}
		set
		{
			ActiveSurrogateSituationArray__BackingField = value;
		}
	}

	public bool OptedOutOfVaccination
	{
		get
		{
			return OptedOutOfVaccination__BackingField;
		}
		set
		{
			OptedOutOfVaccination__BackingField = value;
		}
	}

	public bool MetRoyalLoverThisLife
	{
		get
		{
			return MetRoyalLoverThisLife__BackingField;
		}
		set
		{
			MetRoyalLoverThisLife__BackingField = value;
		}
	}

	public SimSportsWorld SportsLeagues
	{
		get
		{
			return SportsLeagues__BackingField;
		}
		set
		{
			SportsLeagues__BackingField = value;
		}
	}

	public int AgeAtLastSteroids
	{
		get
		{
			return AgeAtLastSteroids__BackingField;
		}
		set
		{
			AgeAtLastSteroids__BackingField = value;
		}
	}

	public int AgeAtLastRefusedDopingTest
	{
		get
		{
			return AgeAtLastRefusedDopingTest__BackingField;
		}
		set
		{
			AgeAtLastRefusedDopingTest__BackingField = value;
		}
	}

	public int AgeAtLastFailedDopingTest
	{
		get
		{
			return AgeAtLastFailedDopingTest__BackingField;
		}
		set
		{
			AgeAtLastFailedDopingTest__BackingField = value;
		}
	}

	public ArrayList _InstrumentArray
	{
		get
		{
			return _InstrumentArray__BackingField;
		}
		set
		{
			_InstrumentArray__BackingField = value;
		}
	}

	public ArrayList InstrumentArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastParentRejectionForInstrument
	{
		get
		{
			return AgeAtLastParentRejectionForInstrument__BackingField;
		}
		set
		{
			AgeAtLastParentRejectionForInstrument__BackingField = value;
		}
	}

	public int AgeAtLastInstrumentAttributeBoost
	{
		get
		{
			return AgeAtLastInstrumentAttributeBoost__BackingField;
		}
		set
		{
			AgeAtLastInstrumentAttributeBoost__BackingField = value;
		}
	}

	public int AgeAtLastBandSearch
	{
		get
		{
			return AgeAtLastBandSearch__BackingField;
		}
		set
		{
			AgeAtLastBandSearch__BackingField = value;
		}
	}

	public int NumBandAuditionOffersAtCurrentAge
	{
		get
		{
			return NumBandAuditionOffersAtCurrentAge__BackingField;
		}
		set
		{
			NumBandAuditionOffersAtCurrentAge__BackingField = value;
		}
	}

	public ArrayList MusicianCachedNameSet
	{
		get
		{
			return MusicianCachedNameSet__BackingField;
		}
		set
		{
			MusicianCachedNameSet__BackingField = value;
		}
	}

	public int NumLogLinkFillers
	{
		get
		{
			return NumLogLinkFillers__BackingField;
		}
		set
		{
			NumLogLinkFillers__BackingField = value;
		}
	}

	public SimActingLessonExperience ActingLessonExperience
	{
		get
		{
			return ActingLessonExperience__BackingField;
		}
		set
		{
			ActingLessonExperience__BackingField = value;
		}
	}

	public SimModelLessonExperience ModelLessonExperience
	{
		get
		{
			return ModelLessonExperience__BackingField;
		}
		set
		{
			ModelLessonExperience__BackingField = value;
		}
	}

	public SimDealingExperience DealingExperience
	{
		get
		{
			return DealingExperience__BackingField;
		}
		set
		{
			DealingExperience__BackingField = value;
		}
	}

	public int AgeAtLastParentRejectionForActingLessons
	{
		get
		{
			return AgeAtLastParentRejectionForActingLessons__BackingField;
		}
		set
		{
			AgeAtLastParentRejectionForActingLessons__BackingField = value;
		}
	}

	public int AgeAtLastActingLessonAttributeBoost
	{
		get
		{
			return AgeAtLastActingLessonAttributeBoost__BackingField;
		}
		set
		{
			AgeAtLastActingLessonAttributeBoost__BackingField = value;
		}
	}

	public int AgeAtLastAgentSearchFailure
	{
		get
		{
			return AgeAtLastAgentSearchFailure__BackingField;
		}
		set
		{
			AgeAtLastAgentSearchFailure__BackingField = value;
		}
	}

	public SimAgent TalentAgent
	{
		get
		{
			return TalentAgent__BackingField;
		}
		set
		{
			TalentAgent__BackingField = value;
		}
	}

	public int AgeAtLastActingExtraSearchFailure
	{
		get
		{
			return AgeAtLastActingExtraSearchFailure__BackingField;
		}
		set
		{
			AgeAtLastActingExtraSearchFailure__BackingField = value;
		}
	}

	public int AgeAtLastActingExtraRoleOffer
	{
		get
		{
			return AgeAtLastActingExtraRoleOffer__BackingField;
		}
		set
		{
			AgeAtLastActingExtraRoleOffer__BackingField = value;
		}
	}

	public int AgeAtLastActingExtraRoleDiscovery
	{
		get
		{
			return AgeAtLastActingExtraRoleDiscovery__BackingField;
		}
		set
		{
			AgeAtLastActingExtraRoleDiscovery__BackingField = value;
		}
	}

	public int NumExtraRoles
	{
		get
		{
			return NumExtraRoles__BackingField;
		}
		set
		{
			NumExtraRoles__BackingField = value;
		}
	}

	public int AgeAtLastJewelryPurchase
	{
		get
		{
			return AgeAtLastJewelryPurchase__BackingField;
		}
		set
		{
			AgeAtLastJewelryPurchase__BackingField = value;
		}
	}

	public int AgeAtLastManiPedi
	{
		get
		{
			return AgeAtLastManiPedi__BackingField;
		}
		set
		{
			AgeAtLastManiPedi__BackingField = value;
		}
	}

	public int AgeAtLastBloodDonation
	{
		get
		{
			return AgeAtLastBloodDonation__BackingField;
		}
		set
		{
			AgeAtLastBloodDonation__BackingField = value;
		}
	}

	public int AgeAtLastPlasmaDonation
	{
		get
		{
			return AgeAtLastPlasmaDonation__BackingField;
		}
		set
		{
			AgeAtLastPlasmaDonation__BackingField = value;
		}
	}

	public int AgeAtLastParentRejectionForSpaceCamp
	{
		get
		{
			return AgeAtLastParentRejectionForSpaceCamp__BackingField;
		}
		set
		{
			AgeAtLastParentRejectionForSpaceCamp__BackingField = value;
		}
	}

	public int AgeAtLastVisitedSpaceCamp
	{
		get
		{
			return AgeAtLastVisitedSpaceCamp__BackingField;
		}
		set
		{
			AgeAtLastVisitedSpaceCamp__BackingField = value;
		}
	}

	public bool HasHeroVisitedSpaceCamp
	{
		get
		{
			return HasHeroVisitedSpaceCamp__BackingField;
		}
		set
		{
			HasHeroVisitedSpaceCamp__BackingField = value;
		}
	}

	public bool HasHeroJoinedAstronomyClub
	{
		get
		{
			return HasHeroJoinedAstronomyClub__BackingField;
		}
		set
		{
			HasHeroJoinedAstronomyClub__BackingField = value;
		}
	}

	public int AgeAtLastMailOrderBrideMarriage
	{
		get
		{
			return AgeAtLastMailOrderBrideMarriage__BackingField;
		}
		set
		{
			AgeAtLastMailOrderBrideMarriage__BackingField = value;
		}
	}

	public SimLover AvailableMailOrderBride
	{
		get
		{
			return AvailableMailOrderBride__BackingField;
		}
		set
		{
			AvailableMailOrderBride__BackingField = value;
		}
	}

	public ArrayList _PhysicalInstrumentArray
	{
		get
		{
			return _PhysicalInstrumentArray__BackingField;
		}
		set
		{
			_PhysicalInstrumentArray__BackingField = value;
		}
	}

	public ArrayList PhysicalInstrumentArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int RewardedVideoRandomJobPack
	{
		get
		{
			return RewardedVideoRandomJobPack__BackingField;
		}
		set
		{
			RewardedVideoRandomJobPack__BackingField = value;
		}
	}

	public bool RewardedVideoRandomJobPackUnlocked
	{
		get
		{
			return RewardedVideoRandomJobPackUnlocked__BackingField;
		}
		set
		{
			RewardedVideoRandomJobPackUnlocked__BackingField = value;
		}
	}

	public bool RewardedVideoBusinessJobPackUnlocked
	{
		get
		{
			return RewardedVideoBusinessJobPackUnlocked__BackingField;
		}
		set
		{
			RewardedVideoBusinessJobPackUnlocked__BackingField = value;
		}
	}

	public int RewardedVideoExpansionPack
	{
		get
		{
			return RewardedVideoExpansionPack__BackingField;
		}
		set
		{
			RewardedVideoExpansionPack__BackingField = value;
		}
	}

	public bool RewardedVideoRandomExpansionPackUnlocked
	{
		get
		{
			return RewardedVideoRandomExpansionPackUnlocked__BackingField;
		}
		set
		{
			RewardedVideoRandomExpansionPackUnlocked__BackingField = value;
		}
	}

	public bool LifeEndedWithChildInheritingBusiness
	{
		get
		{
			return LifeEndedWithChildInheritingBusiness__BackingField;
		}
		set
		{
			LifeEndedWithChildInheritingBusiness__BackingField = value;
		}
	}

	public int AgeAtLastBusinessStartupFundingOffer
	{
		get
		{
			return AgeAtLastBusinessStartupFundingOffer__BackingField;
		}
		set
		{
			AgeAtLastBusinessStartupFundingOffer__BackingField = value;
		}
	}

	public int AgeAtLastEscapedPrisonViaRewardedVideo
	{
		get
		{
			return AgeAtLastEscapedPrisonViaRewardedVideo__BackingField;
		}
		set
		{
			AgeAtLastEscapedPrisonViaRewardedVideo__BackingField = value;
		}
	}

	public int AgeAtLastPray
	{
		get
		{
			return AgeAtLastPray__BackingField;
		}
		set
		{
			AgeAtLastPray__BackingField = value;
		}
	}

	public int NumTimesPrayUsed
	{
		get
		{
			return NumTimesPrayUsed__BackingField;
		}
		set
		{
			NumTimesPrayUsed__BackingField = value;
		}
	}

	public bool HasCurseBeenUsed
	{
		get
		{
			return HasCurseBeenUsed__BackingField;
		}
		set
		{
			HasCurseBeenUsed__BackingField = value;
		}
	}

	public SimSpyAgency SpyAgency
	{
		get
		{
			return SpyAgency__BackingField;
		}
		set
		{
			SpyAgency__BackingField = value;
		}
	}

	public SimZoo Zoo
	{
		get
		{
			return Zoo__BackingField;
		}
		set
		{
			Zoo__BackingField = value;
		}
	}

	public SimCasino Casino
	{
		get
		{
			return Casino__BackingField;
		}
		set
		{
			Casino__BackingField = value;
		}
	}

	public int NumCasinoSessionsSinceLastNekoAd
	{
		get
		{
			return NumCasinoSessionsSinceLastNekoAd__BackingField;
		}
		set
		{
			NumCasinoSessionsSinceLastNekoAd__BackingField = value;
		}
	}

	public int NumTimesCasinoNekoAdShown
	{
		get
		{
			return NumTimesCasinoNekoAdShown__BackingField;
		}
		set
		{
			NumTimesCasinoNekoAdShown__BackingField = value;
		}
	}

	public bool HasCleansedInvestmentMarket
	{
		get
		{
			return HasCleansedInvestmentMarket__BackingField;
		}
		set
		{
			HasCleansedInvestmentMarket__BackingField = value;
		}
	}

	public SimInvestmentMarket InvestmentMarket
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimLuxury Luxury
	{
		get
		{
			return Luxury__BackingField;
		}
		set
		{
			Luxury__BackingField = value;
		}
	}

	public int NumTimesCommittedTaxEvasion
	{
		get
		{
			return NumTimesCommittedTaxEvasion__BackingField;
		}
		set
		{
			NumTimesCommittedTaxEvasion__BackingField = value;
		}
	}

	public int NumTimesInsiderTraded
	{
		get
		{
			return NumTimesInsiderTraded__BackingField;
		}
		set
		{
			NumTimesInsiderTraded__BackingField = value;
		}
	}

	public bool ExpansionPackIneligible
	{
		get
		{
			return ExpansionPackIneligible__BackingField;
		}
		set
		{
			ExpansionPackIneligible__BackingField = value;
		}
	}

	public double BestEarthDefenseTime
	{
		get
		{
			return BestEarthDefenseTime__BackingField;
		}
		set
		{
			BestEarthDefenseTime__BackingField = value;
		}
	}

	public int HeroAgeAtLastProbeDesign
	{
		get
		{
			return HeroAgeAtLastProbeDesign__BackingField;
		}
		set
		{
			HeroAgeAtLastProbeDesign__BackingField = value;
		}
	}

	public ArrayList BlackMarketAuctionItemsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimModelHeadshot _ModelHeadshot
	{
		get
		{
			return _ModelHeadshot__BackingField;
		}
		set
		{
			_ModelHeadshot__BackingField = value;
		}
	}

	public SimModelHeadshot ModelHeadshot
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ModelGiftArray
	{
		get
		{
			return _ModelGiftArray__BackingField;
		}
		set
		{
			_ModelGiftArray__BackingField = value;
		}
	}

	public ArrayList ModelGiftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _CasinoCollectibleArray
	{
		get
		{
			return _CasinoCollectibleArray__BackingField;
		}
		set
		{
			_CasinoCollectibleArray__BackingField = value;
		}
	}

	public ArrayList CasinoCollectibleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastRecruiterModelRequest
	{
		get
		{
			return AgeAtLastRecruiterModelRequest__BackingField;
		}
		set
		{
			AgeAtLastRecruiterModelRequest__BackingField = value;
		}
	}

	public int NumPromoScenariosShownInLife
	{
		get
		{
			return NumPromoScenariosShownInLife__BackingField;
		}
		set
		{
			NumPromoScenariosShownInLife__BackingField = value;
		}
	}

	public bool WillShowJustForYouJobPacks
	{
		get
		{
			return WillShowJustForYouJobPacks__BackingField;
		}
		set
		{
			WillShowJustForYouJobPacks__BackingField = value;
		}
	}

	public bool HasInteractedWithRecruiter
	{
		get
		{
			return HasInteractedWithRecruiter__BackingField;
		}
		set
		{
			HasInteractedWithRecruiter__BackingField = value;
		}
	}

	public SimRacing Racing
	{
		get
		{
			return Racing__BackingField;
		}
		set
		{
			Racing__BackingField = value;
		}
	}

	private ArrayList _RacingCollectibleArray
	{
		get
		{
			return _RacingCollectibleArray__BackingField;
		}
		set
		{
			_RacingCollectibleArray__BackingField = value;
		}
	}

	public ArrayList RacingCollectibleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimOutdoor Outdoor
	{
		get
		{
			return Outdoor__BackingField;
		}
		set
		{
			Outdoor__BackingField = value;
		}
	}

	public SimSeniorCenter SeniorCenter
	{
		get
		{
			return SeniorCenter__BackingField;
		}
		set
		{
			SeniorCenter__BackingField = value;
		}
	}

	public GenesisEntityContainer GenesisEntities
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _OutdoorFishingTreasureArray
	{
		get
		{
			return _OutdoorFishingTreasureArray__BackingField;
		}
		set
		{
			_OutdoorFishingTreasureArray__BackingField = value;
		}
	}

	public ArrayList OutdoorFishingTreasureArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool HasBicycleGivenByParent
	{
		get
		{
			return HasBicycleGivenByParent__BackingField;
		}
		set
		{
			HasBicycleGivenByParent__BackingField = value;
		}
	}

	public int NumTimesHackSuccessThisYear
	{
		get
		{
			return NumTimesHackSuccessThisYear__BackingField;
		}
		set
		{
			NumTimesHackSuccessThisYear__BackingField = value;
		}
	}

	public SimPodcast PodCast
	{
		get
		{
			return PodCast__BackingField;
		}
		set
		{
			PodCast__BackingField = value;
		}
	}

	public SimMusicProfession MusicProfession
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimFightClub FightClub
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	internal GenesisFeatureContainerTransactionSnapshot CaptureGenesisFeatureContainerTransactionSnapshot()
	{
		return null;
	}

	internal void RestoreGenesisFeatureContainerTransactionSnapshot(GenesisFeatureContainerTransactionSnapshot snapshot)
	{
	}

	public GenesisFeatureContainer GetFeatureContainer(string containerId)
	{
		return null;
	}

	public GenesisFeatureContainer GetOrCreateFeatureContainer(string containerId, int heroAge)
	{
		return null;
	}

	public bool TryGetOrCreateFeatureContainer(string containerId, int heroAge, out GenesisFeatureContainer container)
	{
		container = null;
		return false;
	}

	public bool TryResetFeatureContainer(string containerId, int heroAge, out GenesisFeatureContainer container)
	{
		container = null;
		return false;
	}

	public bool HasFeatureContainer(string containerId)
	{
		return false;
	}

	public List<string> GetActiveFeatureContainerIds()
	{
		return null;
	}

	public GenesisFeatureContainerSchemaApplyResult ApplyCurrentGenesisFeatureContainerSchemas()
	{
		return null;
	}

	private void EnsureFeatureContainerStorage()
	{
	}

	private GenesisFeatureContainerSchemaApplyResult PrepareFeatureContainer(string containerId, GenesisFeatureContainer container)
	{
		return null;
	}

	private void MarkFeatureContainerSchemaReady(string containerId, GenesisFeatureContainer container)
	{
	}

	private void EnsureTriggerInfrastructure()
	{
	}

	public void SetupFromSavedStateWithLifeDelegate(object theDelegate)
	{
	}

	public void SetupNewRandomHero()
	{
	}

	public void SetupNewCustomHeroWithNameAndGenderAndCountryCodeAndCityAndGodModePersonConfig(SimPersonName name, int gender, string countryCode, string city, GodModePersonConfig config)
	{
	}

	public void SetupNewGenerationHeroFromChildInLifeWithOldHeroNetWorth(SimChild child, Life oldLife, double oldHeroNetWorth, double oldHeroPortfolioValue = 0.0)
	{
	}

	public void SetupLifeInfrastructure()
	{
	}

	public void SetupEverythingElseForNewHeroWithCustomEmoji(string customEmoji)
	{
	}

	public void FinishSetupAndLogBirthWithBirthCircumstance(DataBirthCircumstance birthCircumstance)
	{
	}

	public void LogScenarioPresentation(Scenario scenario)
	{
	}

	public void LogPresentation(string presentedItem)
	{
	}

	public int PresentedCount(string presentedItem)
	{
		return 0;
	}

	public int PresentedCountForScenario(string scenarioName)
	{
		return 0;
	}

	public bool HasDumbScenarioBeenPresentedBefore(string fakeClassName)
	{
		return false;
	}

	public int YearsSinceScenario(string scenarioName)
	{
		return 0;
	}

	public int YearsSinceLastDumbScenario()
	{
		return 0;
	}

	public string StringOfScenariosAtCurrentAge()
	{
		return null;
	}

	public bool IsScenarioIncludedInCurrentQueue(Type scenarioClass)
	{
		return false;
	}

	public List<Scenario> GetAgeSpecificPushScenarioArray()
	{
		return null;
	}

	public Scenario GetRandomPushScenario()
	{
		return null;
	}

	public void ValidateAndSetScenario(string aClassString, int amountLoops, ArrayList tierList)
	{
	}

	public void IncrementAge()
	{
	}

	public void IncrementAgeSimulation()
	{
	}

	public void IncrementAgeUI()
	{
	}

	public void TiAge()
	{
	}

	private void CheckForRelativeDeaths()
	{
	}

	private void CheckForRelativeDeathsFather()
	{
	}

	private void CheckForRelativeDeathsMother()
	{
	}

	private void CheckForRelativeDeathsLover()
	{
	}

	private void CheckForRelativeDeathsChildren()
	{
	}

	private void CheckForRelativeDeathsSiblings()
	{
	}

	private void CheckForRelativeDeathsGrandchildren()
	{
	}

	private void CheckForRelativeDeathsNiecesAndNephews()
	{
	}

	private void CheckForRelativeDeathsStepfather()
	{
	}

	private void CheckForRelativeDeathsStepmother()
	{
	}

	private void CheckForRelativeDeathsPets()
	{
	}

	private void CheckForRelativeDeathsFriends()
	{
	}

	private void CheckForRelativeDeathsAgent()
	{
	}

	private void CheckForRelativeDeathsFamiliar()
	{
	}

	public T GetSpecialJob<T>(SPECIAL_JOB_TYPE specialJobType) where T : SimSpecialJob
	{
		return null;
	}

	public void ImpactStressChange(int delta)
	{
	}

	public void ImpactSportSkill(string skill, int impact)
	{
	}

	public void ImpactNuclearFamilyRelationship(int impact)
	{
	}

	private void ImpactPersonIfAlive(SimPerson person, int impact)
	{
	}

	public void ImpactModelSkill(string skill, int impact)
	{
	}

	public void ImpactBlackMarketCondition(string name, int impact)
	{
	}

	public void ImpactAstronautSkill(string skill, int impact)
	{
	}

	public void ImpactMusicianAlbumSuccess(string album, int impact)
	{
	}

	public void ImpactPoliticianIssue(string issue, int impact)
	{
	}

	public void ClearCriminalRecord()
	{
	}

	public void RefreshFriends()
	{
	}

	public SimPerson GetBestFriendAndMustBeLocal(bool local)
	{
		return null;
	}

	public SimPerson GetCrazyFriendAndMustBeLocal(bool local)
	{
		return null;
	}

	public ArrayList GetAllChildrenWithLoverAndMustBeNatural(SimLover aLover, bool natural)
	{
		return null;
	}

	public ArrayList GetAllGrandchildren()
	{
		return null;
	}

	public ArrayList GetAllNiecesAndNephews()
	{
		return null;
	}

	public ArrayList GetAllGhosts()
	{
		return null;
	}

	public int TotalNumberOfRelationshipsBeingLoggedExcludingNiecesNephews(bool excludeNiecesNephews)
	{
		return 0;
	}

	public int TotalNumberOfAlivePets()
	{
		return 0;
	}

	public bool HasAtLeastOneAliveParent()
	{
		return false;
	}

	public string ParentsString()
	{
		return null;
	}

	public GENDER ParentsGender()
	{
		return GENDER.MALE;
	}

	public string ParentAndNameString()
	{
		return null;
	}

	public ArrayList ParentsStringParentsArray()
	{
		return null;
	}

	public string ParentsPossessiveString()
	{
		return null;
	}

	public string ParentsIsAre()
	{
		return null;
	}

	public string ParentsDoNot()
	{
		return null;
	}

	public SimParent GetRandomParentIfAny()
	{
		return null;
	}

	public string ParentsPossessivePronoun()
	{
		return null;
	}

	public string ParentsSubjectPronoun()
	{
		return null;
	}

	public string ParentsObjectPronoun()
	{
		return null;
	}

	public string SiblingsStringAndIncludeStepSiblings(bool includeStepSiblings)
	{
		return null;
	}

	public int NumStepSiblings()
	{
		return 0;
	}

	public void ImpactAllRelationshipStrengthsForCult(int impactScore)
	{
	}

	public void ImpactAllRelationshipStrengths(int impactScore)
	{
	}

	public void ImpactAllRelationshipsForParentsAndSiblingsAndLoverAndChildrenAndGrandchildrenAndNiecesNephews(int impactParents, int impactSiblings, int impactLover, int impactChildren, int impactGrandchildren, int impactNiecesNephews)
	{
	}

	public SimMartialArt GetMartialArtExperienceForMartialArt(DataMartialArt theMartialArtData)
	{
		return null;
	}

	public SimMusicInstrument GetInstrumentExperienceForInstrument(DataMusicInstrument theInstrumentData)
	{
		return null;
	}

	public ArrayList GetHouseArrayAndIncludeSpousesHouses(bool includeSpouse)
	{
		return null;
	}

	public bool IsPrayAvailable()
	{
		return false;
	}

	public void IncrementCrimesAttempted()
	{
	}

	public SimLuxury GetOrCreateLuxuryPack()
	{
		return null;
	}

	public SimRacing GetOrCreateRacingPack()
	{
		return null;
	}

	public void GetOrCreateRaceCarDealership()
	{
	}

	public SimOutdoor GetOrCreateOutdoorPack()
	{
		return null;
	}

	public Dictionary<string, object> GetGenesisMemory()
	{
		return null;
	}

	private bool _003CSetupNewGenerationHeroFromChildInLifeWithOldHeroNetWorth_003Eb__1317_2(SimPropertyLeaseContract x)
	{
		return false;
	}

	private bool _003CSetupNewGenerationHeroFromChildInLifeWithOldHeroNetWorth_003Eb__1317_3(SimPropertyLeaseContract x)
	{
		return false;
	}
}
