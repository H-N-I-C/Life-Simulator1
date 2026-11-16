using System;
using System.Collections;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimCasino
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoEntertainmentAct, float> _003C_003E9__117_2;

		public static Func<SimCasinoGambler, double> _003C_003E9__118_0;

		public static Func<SimCasinoGambler, bool> _003C_003E9__141_0;

		public static Func<SimCasinoGameRoom, CASINO_GAME_TYPE> _003C_003E9__168_0;

		public static Func<SimCasinoGameRoom, string> _003C_003E9__168_1;

		public static Func<SimCasinoGambler, bool> _003C_003E9__194_0;

		public static Func<SimCasinoGameRoom, float> _003C_003E9__195_0;

		public static Func<SimCasinoGambler, double> _003C_003E9__196_0;

		public static Func<SimCasinoEntertainmentAct, float> _003C_003E9__197_0;

		public static Func<SimCasinoGameRoom, float> _003C_003E9__201_0;

		public static Func<SimCasinoGameRoom, float> _003C_003E9__202_0;

		internal float _003CCheckForAgeUpImpacts_003Eb__117_2(SimCasinoEntertainmentAct t)
		{
			return 0f;
		}

		internal double _003CGamblersAreWinning_003Eb__118_0(SimCasinoGambler t)
		{
			return 0.0;
		}

		internal bool _003CGetGamblers_003Eb__141_0(SimCasinoGambler t)
		{
			return false;
		}

		internal CASINO_GAME_TYPE _003CGetInstalledGameTypesStringList_003Eb__168_0(SimCasinoGameRoom x)
		{
			return CASINO_GAME_TYPE.NONE;
		}

		internal string _003CGetInstalledGameTypesStringList_003Eb__168_1(SimCasinoGameRoom t)
		{
			return null;
		}

		internal bool _003CRemoveMafiaMembers_003Eb__194_0(SimCasinoGambler t)
		{
			return false;
		}

		internal float _003CGetEmojiForGameRooms_003Eb__195_0(SimCasinoGameRoom t)
		{
			return 0f;
		}

		internal double _003CGetEmojiForGamblers_003Eb__196_0(SimCasinoGambler t)
		{
			return 0.0;
		}

		internal float _003CGetEmojiForEntertainment_003Eb__197_0(SimCasinoEntertainmentAct t)
		{
			return 0f;
		}

		internal float _003CGetMostPopularRoom_003Eb__201_0(SimCasinoGameRoom t)
		{
			return 0f;
		}

		internal float _003CGetLeastPopularRoom_003Eb__202_0(SimCasinoGameRoom t)
		{
			return 0f;
		}
	}

	private sealed class _003C_003Ec__DisplayClass183_0
	{
		public string fullName;

		internal bool _003CGetGamblerByName_003Eb__0(SimCasinoGambler t)
		{
			return false;
		}
	}

	private string Name__BackingField;

	private CASINO_TYPE Type__BackingField;

	private string Address__BackingField;

	private double BankBalance__BackingField;

	private double TotalInvestment__BackingField;

	private string Theme__BackingField;

	private int HeroAgeAtAcquisition__BackingField;

	private SimLoan Mortgage__BackingField;

	private bool HasReceivedBankruptcyWarning__BackingField;

	private double LastYearProfits__BackingField;

	private float LastYearGrossRevenue__BackingField;

	private float LastYearTaxes__BackingField;

	private double LastPreviousLastYearProfits__BackingField;

	private float LastYearExpenses__BackingField;

	private float LastYearPayouts__BackingField;

	private float LastYearWagersPlaced__BackingField;

	private float LastYearGamblingRevenue__BackingField;

	private float LastYearEntertainmentRevenue__BackingField;

	private float LastYearAmenityRevenue__BackingField;

	private int NumYearsNegativeBalance__BackingField;

	private int NumTimesWentMeetAndGreet__BackingField;

	private int AgeHeroAgeLastYearCelebrityUpdated__BackingField;

	private int CasinoAccessoriesEarned__BackingField;

	private bool CelebrityToHookupViaHollywoodStar__BackingField;

	private bool HeroHasUsedMafiaMemberToRoughUpGambler__BackingField;

	[NonSerialized]
	private ArrayList _casinoIncrementAgeScenarioQueueArray;

	private float _popularity;

	private List<SimCasinoAmenity> _amenities;

	private List<SimCasinoEntertainmentAct> _entertainmentActs;

	private List<SimCasinoGameRoom> _gameRooms;

	private List<SimCasinoGambler> _gamblers;

	private List<SimPerson> _dealers;

	private List<SimCasinoCelebrity> _potentialCelebrities;

	private SimCasinoCelebrity _celebrity;

	private SimCasinoCelebrity _celebrityToAcquireViaHollywoodStar;

	private float _celebrityToAcquireViaHollywoodStarSalary;

	private int _celebrityToAcquireViaHollywoodStarContractLength;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public CASINO_TYPE Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public string Address
	{
		get
		{
			return Address__BackingField;
		}
		set
		{
			Address__BackingField = value;
		}
	}

	public double BankBalance
	{
		get
		{
			return BankBalance__BackingField;
		}
		set
		{
			BankBalance__BackingField = value;
		}
	}

	public double TotalInvestment
	{
		get
		{
			return TotalInvestment__BackingField;
		}
		set
		{
			TotalInvestment__BackingField = value;
		}
	}

	public string Theme
	{
		get
		{
			return Theme__BackingField;
		}
		set
		{
			Theme__BackingField = value;
		}
	}

	public int HeroAgeAtAcquisition
	{
		get
		{
			return HeroAgeAtAcquisition__BackingField;
		}
		set
		{
			HeroAgeAtAcquisition__BackingField = value;
		}
	}

	public SimLoan Mortgage
	{
		get
		{
			return Mortgage__BackingField;
		}
		set
		{
			Mortgage__BackingField = value;
		}
	}

	public bool HasReceivedBankruptcyWarning
	{
		get
		{
			return HasReceivedBankruptcyWarning__BackingField;
		}
		set
		{
			HasReceivedBankruptcyWarning__BackingField = value;
		}
	}

	public double LastYearProfits
	{
		get
		{
			return LastYearProfits__BackingField;
		}
		set
		{
			LastYearProfits__BackingField = value;
		}
	}

	public float LastYearGrossRevenue
	{
		get
		{
			return LastYearGrossRevenue__BackingField;
		}
		set
		{
			LastYearGrossRevenue__BackingField = value;
		}
	}

	public float LastYearTaxes
	{
		get
		{
			return LastYearTaxes__BackingField;
		}
		set
		{
			LastYearTaxes__BackingField = value;
		}
	}

	public double LastPreviousLastYearProfits
	{
		get
		{
			return LastPreviousLastYearProfits__BackingField;
		}
		set
		{
			LastPreviousLastYearProfits__BackingField = value;
		}
	}

	public float LastYearExpenses
	{
		get
		{
			return LastYearExpenses__BackingField;
		}
		set
		{
			LastYearExpenses__BackingField = value;
		}
	}

	public float LastYearPayouts
	{
		get
		{
			return LastYearPayouts__BackingField;
		}
		set
		{
			LastYearPayouts__BackingField = value;
		}
	}

	public float LastYearWagersPlaced
	{
		get
		{
			return LastYearWagersPlaced__BackingField;
		}
		set
		{
			LastYearWagersPlaced__BackingField = value;
		}
	}

	public float LastYearGamblingRevenue
	{
		get
		{
			return LastYearGamblingRevenue__BackingField;
		}
		set
		{
			LastYearGamblingRevenue__BackingField = value;
		}
	}

	public float LastYearEntertainmentRevenue
	{
		get
		{
			return LastYearEntertainmentRevenue__BackingField;
		}
		set
		{
			LastYearEntertainmentRevenue__BackingField = value;
		}
	}

	public float LastYearAmenityRevenue
	{
		get
		{
			return LastYearAmenityRevenue__BackingField;
		}
		set
		{
			LastYearAmenityRevenue__BackingField = value;
		}
	}

	public int NumYearsNegativeBalance
	{
		get
		{
			return NumYearsNegativeBalance__BackingField;
		}
		set
		{
			NumYearsNegativeBalance__BackingField = value;
		}
	}

	public int NumTimesWentMeetAndGreet
	{
		get
		{
			return NumTimesWentMeetAndGreet__BackingField;
		}
		set
		{
			NumTimesWentMeetAndGreet__BackingField = value;
		}
	}

	public int AgeHeroAgeLastYearCelebrityUpdated
	{
		get
		{
			return AgeHeroAgeLastYearCelebrityUpdated__BackingField;
		}
		set
		{
			AgeHeroAgeLastYearCelebrityUpdated__BackingField = value;
		}
	}

	public int CasinoAccessoriesEarned
	{
		get
		{
			return CasinoAccessoriesEarned__BackingField;
		}
		set
		{
			CasinoAccessoriesEarned__BackingField = value;
		}
	}

	public bool CelebrityToHookupViaHollywoodStar
	{
		get
		{
			return CelebrityToHookupViaHollywoodStar__BackingField;
		}
		set
		{
			CelebrityToHookupViaHollywoodStar__BackingField = value;
		}
	}

	public bool HeroHasUsedMafiaMemberToRoughUpGambler
	{
		get
		{
			return HeroHasUsedMafiaMemberToRoughUpGambler__BackingField;
		}
		set
		{
			HeroHasUsedMafiaMemberToRoughUpGambler__BackingField = value;
		}
	}

	public DataCasinoStructure CasinoData => null;

	public SimCasino(CASINO_TYPE type)
	{
	}

	public void IncrementAge()
	{
	}

	private float FirstYearGamblerProfits()
	{
		return 0f;
	}

	private void CheckForAgeUpImpacts()
	{
	}

	private bool GamblersAreWinning()
	{
		return false;
	}

	private (float, float) GetGameRoomRevenue()
	{
		return default;
	}

	private (float, float) GetWagersPlaced()
	{
		return default;
	}

	private void CheckForDeadGamblers()
	{
	}

	public void CreateDealers()
	{
	}

	public void CreateGamblers()
	{
	}

	public void CreatePotentialCelebrities(bool force = false)
	{
	}

	public List<SimCasinoCelebrity> GetPotentialCelebrityList()
	{
		return null;
	}

	public SimCasinoGambler GetRandomNonMafiaGambler()
	{
		return null;
	}

	public SimCasinoGambler GetRandomGambler()
	{
		return null;
	}

	public SimCasinoMafiaMember GetRandomMafiaMember()
	{
		return null;
	}

	public SimPerson GetRandomDealer()
	{
		return null;
	}

	public List<SimCasinoAmenity> GetAmenities()
	{
		return null;
	}

	public SimCasinoAmenity GetRandomAmenity()
	{
		return null;
	}

	public SimCasinoCelebrity GetCelebrity()
	{
		return null;
	}

	public void HireCelebrity(SimCasinoCelebrity celebrity, float salary, int contractLength, bool acquiredWithHollywoodStar)
	{
	}

	public void RemoveCelebrity()
	{
	}

	public List<SimCasinoEntertainmentAct> GetEntertainmentActs()
	{
		return null;
	}

	public SimCasinoEntertainmentAct GetRandomEntertainmentAct()
	{
		return null;
	}

	public void HireEntertainmentAct(SimCasinoEntertainmentAct act)
	{
	}

	public void RemoveEntertainmentAct(SimCasinoEntertainmentAct act)
	{
	}

	public List<SimCasinoGameRoom> GetGameRooms(CASINO_GAME_TYPE desiredType = CASINO_GAME_TYPE.NONE)
	{
		return null;
	}

	public SimCasinoGameRoom GetRandomGameRoom(CASINO_GAME_TYPE desiredType = CASINO_GAME_TYPE.NONE)
	{
		return null;
	}

	public List<SimCasinoGambler> GetGamblers(bool excludeMafiaMembers = false)
	{
		return null;
	}

	public void ImpactGamblersAddiction(int impact)
	{
	}

	public void ImpactGamblersHappiness(int impact)
	{
	}

	public List<SimCasinoGambler> GetHighRollers(bool excludeMafiaMembers = false)
	{
		return null;
	}

	public List<SimCasinoGambler> GetCasualGamblers(bool excludeMafiaMembers = false)
	{
		return null;
	}

	public List<SimCasinoMafiaMember> GetMafiaMembers()
	{
		return null;
	}

	public List<CASINO_GAME_TYPE> GetInstalledGameTypes()
	{
		return null;
	}

	public List<string> GetInstalledGameNames(bool capitalize = false)
	{
		return null;
	}

	public void AddInvestment(double amount)
	{
	}

	public void AdjustBankBalance(double amount)
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public void UpgradeToType(CASINO_TYPE type)
	{
	}

	public static string GetTypeString(CASINO_TYPE type, bool capitalize)
	{
		return null;
	}

	public double GetPrice(CASINO_TYPE theType = CASINO_TYPE.NONE)
	{
		return 0.0;
	}

	public double GetUpgradePriceForType(CASINO_TYPE upgradeType)
	{
		return 0.0;
	}

	public int GetAmenityCapacity()
	{
		return 0;
	}

	public int GetEntertainmentActCapacity()
	{
		return 0;
	}

	public int GetNumberOfGameRoomsCapacity()
	{
		return 0;
	}

	public double GetMonthlyExpenses()
	{
		return 0.0;
	}

	public void AddGameRoom(SimCasinoGameRoom gameRoom)
	{
	}

	public void RemoveGameRoom(SimCasinoGameRoom gameRoom)
	{
	}

	public void RemoveGambler(SimCasinoGambler gambler)
	{
	}

	public SimCasinoAmenity GetAmenityWithName(string name)
	{
		return null;
	}

	public void AddAmenity(SimCasinoAmenity amenity)
	{
	}

	public void RemoveAmenity(SimCasinoAmenity amenity)
	{
	}

	public int GetYearsOwned()
	{
		return 0;
	}

	public string GetInstalledGameTypesStringList()
	{
		return null;
	}

	public List<CASINO_GAME_TYPE> GetTournamentGameTypes()
	{
		return null;
	}

	public List<CASINO_GAME_TYPE> GetInstalledGamesThatQualifyForTournamentPlay()
	{
		return null;
	}

	public void RemoveNonMafiaCasualGambler()
	{
	}

	public void AddGambler(SimCasinoGambler gambler)
	{
	}

	public static double MinimumTournamentPrize()
	{
		return 0.0;
	}

	public LOAN_REJECTION_REASON MortgageRejectionReason()
	{
		return LOAN_REJECTION_REASON.NONE;
	}

	public string CasinoDescriptionWithDescriptorAndAddressAndIsCapitalized(bool descriptor, bool address, bool capitalized)
	{
		return null;
	}

	public void OriginateMortgage()
	{
	}

	public void ShutDownCasino()
	{
	}

	private void CancelAllCasinoScenarios()
	{
	}

	private void CancelAllCasinoMafiaScenarios()
	{
	}

	private void CancelAllCasinoCelebrityScenarios()
	{
	}

	public static float MinimumEntertainmentActSalary()
	{
		return 0f;
	}

	public static float MinimumCelebritySalary()
	{
		return 0f;
	}

	public SimCasinoGambler GetGamblerByName(string fullName)
	{
		return null;
	}

	public static ArrayList GetThemeDropDownList(string excludeTheme = null)
	{
		return null;
	}

	public void SetPendingCelebrities(SimCasinoCelebrity celebrity, float salary, int contractLength)
	{
	}

	public void ProcessPendingCelebrityActions()
	{
	}

	public static DataAccessory GetAccessoryReward(CASINO_GAME_WIN_TYPE winType)
	{
		return null;
	}

	public float GetAnnualRevenue()
	{
		return 0f;
	}

	public float GetEntertainmentRevenue()
	{
		return 0f;
	}

	public float GetAnnualAmenitiesRevenue()
	{
		return 0f;
	}

	public float GetAnnualExpenses()
	{
		return 0f;
	}

	public float GetAnnualProfit()
	{
		return 0f;
	}

	private bool ProcessLoan(SimLoan loan)
	{
		return false;
	}

	public void RemoveMafiaMembers()
	{
	}

	public string GetEmojiForGameRooms()
	{
		return null;
	}

	public string GetEmojiForGamblers()
	{
		return null;
	}

	public string GetEmojiForEntertainment()
	{
		return null;
	}

	public string GetEmojiForAmenity()
	{
		return null;
	}

	public int GamblersRequiredForCasino()
	{
		return 0;
	}

	private void AddAnyAdditionalNeededGamblers()
	{
	}

	public string GetMostPopularRoom()
	{
		return null;
	}

	public string GetLeastPopularRoom()
	{
		return null;
	}

	private bool _003CCheckForAgeUpImpacts_003Eb__117_0(SimCasinoAmenity t)
	{
		return false;
	}

	private bool _003CCheckForAgeUpImpacts_003Eb__117_1(SimCasinoAmenity t)
	{
		return false;
	}
}
