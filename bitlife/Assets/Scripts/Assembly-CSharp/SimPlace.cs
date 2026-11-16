using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimPlace
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<SimJob> _003C_003E9__367_0;

		public static Func<SimOccupationStudent, bool> _003C_003E9__368_1;

		public static Comparison<SimJob> _003C_003E9__368_0;

		public static Func<SimCar, float> _003C_003E9__371_0;

		public static Func<SimCar, float> _003C_003E9__371_1;

		public static Func<SimCar, float> _003C_003E9__371_2;

		public static Func<SimHouse, float> _003C_003E9__372_0;

		public static Func<SimHouse, float> _003C_003E9__372_1;

		public static Func<SimAircraft, float> _003C_003E9__393_0;

		public static Func<SimWatercraft, double> _003C_003E9__394_0;

		public static Func<SimJewel, float> _003C_003E9__395_0;

		public static Func<SimMusicInstrumentPhysical, float> _003C_003E9__399_0;

		public static Func<SimHustleStreet, int> _003C_003E9__400_0;

		public static Func<SimSpyAgencyFront, float> _003C_003E9__401_0;

		public static Func<SimZoo, bool> _003C_003E9__410_0;

		public static Func<SimZoo, bool> _003C_003E9__410_1;

		public static Func<SimRecreationalVehicle, float> _003C_003E9__415_0;

		public static Comparison<SimMusicProducerRecordLabel> _003C_003E9__417_0;

		internal int _003CRefreshAvailableJobs_003Eb__367_0(SimJob x, SimJob y)
		{
			return 0;
		}

		internal bool _003CRefreshAvailableGoldenDiplomaJobs_003Eb__368_1(SimOccupationStudent t)
		{
			return false;
		}

		internal int _003CRefreshAvailableGoldenDiplomaJobs_003Eb__368_0(SimJob x, SimJob y)
		{
			return 0;
		}

		internal float _003CRefreshAvailableCars_003Eb__371_0(SimCar x)
		{
			return 0f;
		}

		internal float _003CRefreshAvailableCars_003Eb__371_1(SimCar x)
		{
			return 0f;
		}

		internal float _003CRefreshAvailableCars_003Eb__371_2(SimCar x)
		{
			return 0f;
		}

		internal float _003CRefreshAvailableHouses_003Eb__372_0(SimHouse x)
		{
			return 0f;
		}

		internal float _003CRefreshAvailableHouses_003Eb__372_1(SimHouse x)
		{
			return 0f;
		}

		internal float _003CRefreshAvailableAircraft_003Eb__393_0(SimAircraft x)
		{
			return 0f;
		}

		internal double _003CRefreshAvailableWatercraft_003Eb__394_0(SimWatercraft x)
		{
			return 0.0;
		}

		internal float _003CRefreshAvailableJewelry_003Eb__395_0(SimJewel x)
		{
			return 0f;
		}

		internal float _003CRefreshAvailableInstruments_003Eb__399_0(SimMusicInstrumentPhysical x)
		{
			return 0f;
		}

		internal int _003CCreateHustleStreetsIfApplicable_003Eb__400_0(SimHustleStreet x)
		{
			return 0;
		}

		internal float _003CRefreshAvailableSpyAgencyFronts_003Eb__401_0(SimSpyAgencyFront t)
		{
			return 0f;
		}

		internal bool _003CRefreshAvailableZoos_003Eb__410_0(SimZoo t)
		{
			return false;
		}

		internal bool _003CRefreshAvailableZoos_003Eb__410_1(SimZoo t)
		{
			return false;
		}

		internal float _003CGetBikeShopList_003Eb__415_0(SimRecreationalVehicle x)
		{
			return 0f;
		}

		internal int _003CBuildAvailableMusicProducerRecordLabels_003Eb__417_0(SimMusicProducerRecordLabel left, SimMusicProducerRecordLabel right)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass399_0
	{
		public SimMusicInstrumentPhysical anInstrument;

		internal bool _003CRefreshAvailableInstruments_003Eb__1(SimMusicInstrumentPhysical x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass399_1
	{
		public SimMusicInstrumentPhysical anInstrument;

		internal bool _003CRefreshAvailableInstruments_003Eb__2(SimMusicInstrumentPhysical x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass399_2
	{
		public SimMusicInstrumentPhysical anInstrument;

		internal bool _003CRefreshAvailableInstruments_003Eb__3(SimMusicInstrumentPhysical x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass409_0
	{
		public SimCompany randomCompany;

		internal bool _003CGeneratePennyStocks_003Eb__0(SimStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass410_0
	{
		public SimStreetName streetName;

		internal bool _003CRefreshAvailableZoos_003Eb__2(SimZoo t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass411_0
	{
		public SimSpecialJobDealer specialJob;

		internal bool _003CBuildAvailableDealerCartels_003Eb__0(SimDealerCartel t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass411_1
	{
		public string name;

		internal bool _003CBuildAvailableDealerCartels_003Eb__1(SimDealerCartel t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass415_0
	{
		public SimRecreationalVehicle recreationalVehicle;

		internal bool _003CGetBikeShopList_003Eb__1(SimRecreationalVehicle t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass417_0
	{
		public string employerName;

		internal bool _003CBuildAvailableMusicProducerRecordLabels_003Eb__1(SimMusicProducerRecordLabel x)
		{
			return false;
		}
	}

	[NonSerialized]
	public int AgeAtLastCompanyPersonsRefresh;

	[NonSerialized]
	public int AgeAtLastJobsRefresh;

	[NonSerialized]
	public int AgeAtLastGoldenResumeJobsRefresh;

	[NonSerialized]
	public int AgeAtLastPartTimeJobsRefresh;

	[NonSerialized]
	public int AgeAtLastCarsRefresh;

	[NonSerialized]
	public int AgeAtLastHousesRefresh;

	[NonSerialized]
	public int AgeAtLastLotteryRefresh;

	[NonSerialized]
	public int AgeAtLastSecretAgencyFrontRefresh;

	public float LotteryJackpot;

	[NonSerialized]
	public int AgeAtLastLoversRefresh;

	public int LoverIndexZI;

	[NonSerialized]
	public int AgeAtLastAdopteeCandidateRefresh;

	[NonSerialized]
	public int AgeAtLastEscortsRefresh;

	public int _escortIndexZI;

	[NonSerialized]
	public int AgeAtLastSpermDonorRefresh;

	[NonSerialized]
	public int AgeAtLastCarsToStealRefresh;

	[NonSerialized]
	public int AgeAtLastMovieRefresh;

	[NonSerialized]
	public int AgeAtLastHomesToBurgleRefresh;

	[NonSerialized]
	public int AgeAtLastPetsRefresh;

	[NonSerialized]
	public int AgeAtLastCollegeMajorRefresh;

	[NonSerialized]
	public int AgeAtLastFameOpportunityRefresh;

	[NonSerialized]
	public int AgeAtLastAircraftRefresh;

	[NonSerialized]
	public int AgeAtLastWatercraftRefresh;

	[NonSerialized]
	public int AgeAtLastJewelryRefresh;

	[NonSerialized]
	public int AgeAtLastInstrumentRefresh;

	[NonSerialized]
	public int AgeAtLastSurrogateRefresh;

	[NonSerialized]
	public static int kLoversPerYear;

	[NonSerialized]
	public static int kMailOrderBrideCandidatesPerYear;

	[NonSerialized]
	public int AgeAtLastMailOrderBrideCandidateRefresh;

	public int MailOrderBrideIndexZI;

	[NonSerialized]
	public int AgeAtLastCultPlotsRefresh;

	private string City__BackingField;

	private string CountryCode__BackingField;

	private int AgeAtLastBikeshopRefresh__BackingField;

	[NonSerialized]
	public DataCountry _Country;

	private SimCompany RaceCarDealership__BackingField;

	private ArrayList CompanyArray__BackingField;

	private ArrayList _MafiaFamilyArray__BackingField;

	private ArrayList _CurrentDoctorArray__BackingField;

	private ArrayList _CurrentPlasticSurgeonArray__BackingField;

	[NonSerialized]
	private ArrayList _CurrentCasinoDealerArray;

	[NonSerialized]
	private ArrayList _CurrentRaceCarMechanicArray;

	[NonSerialized]
	private ArrayList _AvailableJobArray;

	[NonSerialized]
	private ArrayList _AvailableGoldenResumeJobArray;

	[NonSerialized]
	private ArrayList _AvailablePartTimeJobArray;

	[NonSerialized]
	private ArrayList _AvailableCarsDealer1Array;

	[NonSerialized]
	private ArrayList _AvailableCarsDealer2Array;

	[NonSerialized]
	private ArrayList _AvailableCarsDealer3Array;

	[NonSerialized]
	private ArrayList _AvailableHousesBroker1Array;

	[NonSerialized]
	private ArrayList _AvailableHousesBroker2Array;

	private bool LastEmigrationPermission__BackingField;

	[NonSerialized]
	public int AgeAtLastEmigrationPermission;

	[NonSerialized]
	private ArrayList _AvailableEmigrationPlaceArray;

	[NonSerialized]
	public int AgeAtLastVacationView;

	[NonSerialized]
	private ArrayList _AvailableVacationArray;

	[NonSerialized]
	private ArrayList _AvailableCruiseArray;

	[NonSerialized]
	public int AgeAtLastWeddingVenueView;

	[NonSerialized]
	private ArrayList _WeddingVenueArray;

	[NonSerialized]
	public DataMovie AvailableMovie;

	private string AvailableMovieTitle__BackingField;

	[NonSerialized]
	public DataMovie AvailableChildrensMovie;

	private string AvailableChildrensMovieTitle__BackingField;

	[NonSerialized]
	public DataMovie AvailableComedyMovie;

	private string AvailableComedyMovieTitle__BackingField;

	[NonSerialized]
	public DataMovie AvailableDocumentaryMovie;

	private string AvailableDocumentaryMovieTitle__BackingField;

	[NonSerialized]
	public int AgeAtLastMurderShortlistBuild;

	[NonSerialized]
	private ArrayList _MurderShortlistArray;

	[NonSerialized]
	public int AgeAtLastHorseRefresh;

	[NonSerialized]
	private ArrayList _RaceTrackHorseArray;

	[NonSerialized]
	public SimPerson AvailableSpermDonor1Person;

	[NonSerialized]
	public SimPerson AvailableSpermDonor2Person;

	[NonSerialized]
	private ArrayList _AvailableCarsToStealArray;

	[NonSerialized]
	public ArrayList _AvailableHomesToBurgleArray;

	[NonSerialized]
	private ArrayList _AvailablePetsShelterArray;

	[NonSerialized]
	private ArrayList _AvailablePetsStoreArray;

	[NonSerialized]
	private ArrayList _AvailablePetsCatBreederArray;

	[NonSerialized]
	private ArrayList _AvailablePetsDogBreederArray;

	[NonSerialized]
	private ArrayList _AvailablePetsHorseRanchArray;

	private ArrayList AvailablePetsLlamaFarmArray__BackingField;

	private SimPet AvailableExoticPet__BackingField;

	[NonSerialized]
	private ArrayList _AvailableCollegeMajorArray;

	private SimCommercial AvailableFameOpportunityCommercial__BackingField;

	private SimMagazineOpportunity AvailableFameMagazineOpportunity__BackingField;

	private SimBookOpportunity AvailableFameBookOpportunity__BackingField;

	private SimTalkShowOpportunity AvailableTalkShowOpportunity__BackingField;

	private ArrayList _StreetNameArray__BackingField;

	private string AvailableConcert__BackingField;

	[NonSerialized]
	public int AgeAtLastConcertRefresh;

	[NonSerialized]
	private ArrayList _AvailableAircraftArray;

	[NonSerialized]
	private ArrayList _AvailableWatercraftArray;

	[NonSerialized]
	private ArrayList _AvailableRecreationalVehicleArray;

	[NonSerialized]
	private ArrayList _AvailableJewelryArray;

	[NonSerialized]
	private ArrayList _AvailableAdoptionCandidateArray;

	[NonSerialized]
	private ArrayList _AvailableSurrogateArray;

	public int AgeAtLastActingRoleRefresh;

	private ArrayList _AvailableFilmArray;

	private ArrayList _AvailableFilmRoleArray;

	private ArrayList _AvailableShowArray;

	private ArrayList _AvailableShowRoleArray;

	private bool IsBlackBalledFromBloodBlank__BackingField;

	[NonSerialized]
	private ArrayList _AvailableInstrumentArray;

	private ArrayList _AvailableHustleStreetArray;

	[NonSerialized]
	private ArrayList _AvailableCultPlotsArray;

	private Dictionary<int, float> _BusinessIndustryDemand;

	private Dictionary<int, int> _BusinessIndustrySupply;

	private ArrayList _AvailableBusinessesForSaleArray;

	public int AgeAtLastBusinessForSaleRefresh;

	private Dictionary<SimProduct, List<SimSupplier>> _AvailableBusinessProductSupplierDict;

	private Dictionary<int, SimBusiness> _StartupBusinessPlaceholderList;

	public List<SimPerson> ListOfPossibleEmployees;

	private string FinancialNewspaperEditorName__BackingField;

	private SimCult Cult__BackingField;

	private SimMarket _Market__BackingField;

	private ArrayList _AvailableSpyAgencyFrontsArray;

	private ArrayList _availableZoosArray;

	[NonSerialized]
	public int AgeAtLastZooCompetitorsRefresh;

	private ArrayList _availableDealerCartelsArray;

	private List<SimMusicProducerRecordLabel> _availableMusicProducerRecordLabelsArray;

	private SimCompany _bikeShopDealership;

	[NonSerialized]
	private List<SimRecreationalVehicle> _recreationalVehicles;

	public string City
	{
		get
		{
			return City__BackingField;
		}
		set
		{
			City__BackingField = value;
		}
	}

	public string CountryCode
	{
		get
		{
			return CountryCode__BackingField;
		}
		set
		{
			CountryCode__BackingField = value;
		}
	}

	public int AgeAtLastBikeshopRefresh
	{
		get
		{
			return AgeAtLastBikeshopRefresh__BackingField;
		}
		set
		{
			AgeAtLastBikeshopRefresh__BackingField = value;
		}
	}

	public SimCompany RaceCarDealership
	{
		get
		{
			return RaceCarDealership__BackingField;
		}
		set
		{
			RaceCarDealership__BackingField = value;
		}
	}

	public DataCountry Country
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList CompanyArray
	{
		get
		{
			return CompanyArray__BackingField;
		}
		set
		{
			CompanyArray__BackingField = value;
		}
	}

	private ArrayList _MafiaFamilyArray
	{
		get
		{
			return _MafiaFamilyArray__BackingField;
		}
		set
		{
			_MafiaFamilyArray__BackingField = value;
		}
	}

	public ArrayList MafiaFamilyArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _CurrentDoctorArray
	{
		get
		{
			return _CurrentDoctorArray__BackingField;
		}
		set
		{
			_CurrentDoctorArray__BackingField = value;
		}
	}

	public ArrayList CurrentDoctorArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _CurrentPlasticSurgeonArray
	{
		get
		{
			return _CurrentPlasticSurgeonArray__BackingField;
		}
		set
		{
			_CurrentPlasticSurgeonArray__BackingField = value;
		}
	}

	public ArrayList CurrentPlasticSurgeonArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList CurrentCasinoDealerArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList CurrentRaceCarMechanicArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableJobArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableGoldenResumeJobArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailablePartTimeJobArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableCarsDealer1Array
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableCarsDealer2Array
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableCarsDealer3Array
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableHousesBroker1Array
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableHousesBroker2Array
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool LastEmigrationPermission
	{
		get
		{
			return LastEmigrationPermission__BackingField;
		}
		set
		{
			LastEmigrationPermission__BackingField = value;
		}
	}

	public ArrayList AvailableEmigrationPlaceArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableVacationArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableCruiseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList WeddingVenueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string AvailableMovieTitle
	{
		get
		{
			return AvailableMovieTitle__BackingField;
		}
		set
		{
			AvailableMovieTitle__BackingField = value;
		}
	}

	public string AvailableChildrensMovieTitle
	{
		get
		{
			return AvailableChildrensMovieTitle__BackingField;
		}
		set
		{
			AvailableChildrensMovieTitle__BackingField = value;
		}
	}

	public string AvailableComedyMovieTitle
	{
		get
		{
			return AvailableComedyMovieTitle__BackingField;
		}
		set
		{
			AvailableComedyMovieTitle__BackingField = value;
		}
	}

	public string AvailableDocumentaryMovieTitle
	{
		get
		{
			return AvailableDocumentaryMovieTitle__BackingField;
		}
		set
		{
			AvailableDocumentaryMovieTitle__BackingField = value;
		}
	}

	public ArrayList MurderShortlistArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList RaceTrackHorseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableCarsToStealArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableHomesToBurgleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailablePetsShelterArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailablePetsStoreArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailablePetsCatBreederArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailablePetsDogBreederArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailablePetsHorseRanchArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailablePetsLlamaFarmArray
	{
		get
		{
			return AvailablePetsLlamaFarmArray__BackingField;
		}
		set
		{
			AvailablePetsLlamaFarmArray__BackingField = value;
		}
	}

	public SimPet AvailableExoticPet
	{
		get
		{
			return AvailableExoticPet__BackingField;
		}
		set
		{
			AvailableExoticPet__BackingField = value;
		}
	}

	public ArrayList AvailableCollegeMajorArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimCommercial AvailableFameOpportunityCommercial
	{
		get
		{
			return AvailableFameOpportunityCommercial__BackingField;
		}
		set
		{
			AvailableFameOpportunityCommercial__BackingField = value;
		}
	}

	public SimMagazineOpportunity AvailableFameMagazineOpportunity
	{
		get
		{
			return AvailableFameMagazineOpportunity__BackingField;
		}
		set
		{
			AvailableFameMagazineOpportunity__BackingField = value;
		}
	}

	public SimBookOpportunity AvailableFameBookOpportunity
	{
		get
		{
			return AvailableFameBookOpportunity__BackingField;
		}
		set
		{
			AvailableFameBookOpportunity__BackingField = value;
		}
	}

	public SimTalkShowOpportunity AvailableTalkShowOpportunity
	{
		get
		{
			return AvailableTalkShowOpportunity__BackingField;
		}
		set
		{
			AvailableTalkShowOpportunity__BackingField = value;
		}
	}

	private ArrayList _StreetNameArray
	{
		get
		{
			return _StreetNameArray__BackingField;
		}
		set
		{
			_StreetNameArray__BackingField = value;
		}
	}

	public ArrayList StreetNameArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string AvailableConcert
	{
		get
		{
			return AvailableConcert__BackingField;
		}
		set
		{
			AvailableConcert__BackingField = value;
		}
	}

	public ArrayList AvailableAircraftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableWatercraftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableRecreationalVehicleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableJewelryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableAdoptionCandidateArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableSurrogateArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableFilmArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableFilmRoleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableShowArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableShowRoleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool IsBlackBalledFromBloodBlank
	{
		get
		{
			return IsBlackBalledFromBloodBlank__BackingField;
		}
		set
		{
			IsBlackBalledFromBloodBlank__BackingField = value;
		}
	}

	public ArrayList AvailableInstrumentArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableHustleStreetArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableCultPlotsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public Dictionary<int, float> BusinessIndustryDemandDict
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public Dictionary<int, int> BusinessIndustrySupplyDict
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableBusinessesForSaleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public Dictionary<SimProduct, List<SimSupplier>> AvailableBusinessProductSupplierDict
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public Dictionary<int, SimBusiness> StartupBusinessPlaceholderList
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string FinancialNewspaperEditorName
	{
		get
		{
			return FinancialNewspaperEditorName__BackingField;
		}
		set
		{
			FinancialNewspaperEditorName__BackingField = value;
		}
	}

	public SimCult Cult
	{
		get
		{
			return Cult__BackingField;
		}
		set
		{
			Cult__BackingField = value;
		}
	}

	private SimMarket _Market
	{
		get
		{
			return _Market__BackingField;
		}
		set
		{
			_Market__BackingField = value;
		}
	}

	public SimMarket Market
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableSpyAgencyFrontsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableZoosArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList AvailableDealerCartelsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimMusicProducerRecordLabel> AvailableMusicProducerRecordLabelsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string Description()
	{
		return null;
	}

	private SimPlace()
	{
	}

	public SimPlace(bool initHere)
	{
	}

	public SimPlace(string city, DataCountry country)
	{
	}

	public void PickRandomCity()
	{
	}

	public void SetupCompanies()
	{
	}

	public bool WillPermitHeroToEmigrate()
	{
		return false;
	}

	public float CostToEmigrate()
	{
		return 0f;
	}

	public float CostOfVacationForClassAndDays(int vacationClass, int days)
	{
		return 0f;
	}

	public ArrayList GetCompanyArrayForCompanyType(int companyType)
	{
		return null;
	}

	public ArrayList GetCarDealerArray()
	{
		return null;
	}

	public ArrayList GetRealEstateCompanyArray()
	{
		return null;
	}

	public ArrayList GetNightclubArray()
	{
		return null;
	}

	public ArrayList GetLawFirmArray()
	{
		return null;
	}

	public SimMafiaFamily GetMafiaFamilyForMob(DataMafiaMob theMob)
	{
		return null;
	}

	public void RefreshAvailableBusinessesForSale()
	{
	}

	public void RefreshCompanyPersons()
	{
	}

	public void RefreshAvailableJobs(bool overrideRefresh = false)
	{
	}

	public void RefreshAvailableGoldenDiplomaJobs(bool overrideRefresh = false)
	{
	}

	public void RefreshAvailablePartTimeJobs()
	{
	}

	public SimCompany GetRandomCompanyForCompanyType(int companyType)
	{
		return null;
	}

	public void RefreshAvailableCars(bool forceRefresh = false)
	{
	}

	public void RefreshAvailableHouses(bool forceRefresh = false)
	{
	}

	public SimHouse GenerateHouseOfApproximateValue(float value)
	{
		return null;
	}

	public void CreateStreetAddressesIfApplicable()
	{
	}

	public string GetStreetAddressForHouse(SimHouse house)
	{
		return null;
	}

	public void RefreshAvailableWeddingVenues()
	{
	}

	public void RefreshAvailableMovies()
	{
	}

	public void RefreshAvailableSpermDonors()
	{
	}

	public void RefreshAvailableCarsToSteal()
	{
	}

	public void RefreshAvailableHomesToBurgle()
	{
	}

	public void RefreshAvailableVacations()
	{
	}

	public void AddAnotherVacationDestination()
	{
	}

	public float GetLotteryJackpot()
	{
		return 0f;
	}

	public void HeroWonLottery()
	{
	}

	public void RefreshMurderShortlistAndForce(bool forceRefresh)
	{
	}

	public void RefreshRaceTrackHorsesAndForce(bool forceRefresh)
	{
	}

	public string GetRaceTrackName()
	{
		return null;
	}

	public void RefreshPets()
	{
	}

	public void RefreshCollegeMajors()
	{
	}

	public void RefreshAvailableEmigrationPlaces()
	{
	}

	public void RefreshAvailableFameOpportunities()
	{
	}

	public void RefreshConcert()
	{
	}

	public void RefreshAvailableAircraft()
	{
	}

	public void RefreshAvailableWatercraft()
	{
	}

	public void RefreshAvailableJewelry()
	{
	}

	public void RefreshAvailableAdoptionCandidateArray()
	{
	}

	public void RefreshAvailableSurrogateArray()
	{
	}

	public void RefreshAvailableActorRoles()
	{
	}

	public void RefreshAvailableInstruments()
	{
	}

	public void CreateHustleStreetsIfApplicable()
	{
	}

	public void RefreshAvailableSpyAgencyFronts(bool forceRefresh = false, SimSpyAgencyFront existingFront = null)
	{
	}

	public SimLover GetLoverCandidateAndIsHookup(bool isHookup)
	{
		return null;
	}

	public bool AreMoreLoverCandidatesAvailable()
	{
		return false;
	}

	public void RefreshLovers()
	{
	}

	public SimLover GetMailOrderBride()
	{
		return null;
	}

	public bool AreMoreMailOrderBrideCandidatesAvailable()
	{
		return false;
	}

	public void RefreshAvailableMailOrderBrides()
	{
	}

	public void RefreshAvailableCultPlots()
	{
	}

	public void GeneratePennyStocks(int numberStocksToCreate)
	{
	}

	public void RefreshAvailableZoos(bool forceRefresh = false)
	{
	}

	public void BuildAvailableDealerCartels()
	{
	}

	public SimCompany GetBikeShopDealership()
	{
		return null;
	}

	public List<SimRecreationalVehicle> GetBikeShopList(bool forceRefresh = false)
	{
		return null;
	}

	public void RemoveRecreationalVehicleFromDealer(SimRecreationalVehicle recreationalVehicle)
	{
	}

	public void BuildAvailableMusicProducerRecordLabels()
	{
	}
}
