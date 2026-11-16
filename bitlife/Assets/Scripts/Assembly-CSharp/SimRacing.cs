using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimRacing
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRacingTrack, int> _003C_003E9__90_0;

		public static Func<SimRacingCup, string> _003C_003E9__101_0;

		public static Func<SimRaceCarDriver, float> _003C_003E9__110_1;

		public static Func<SimRaceCarDriver, float> _003C_003E9__114_1;

		public static Func<SimRaceCarDriver, string> _003C_003E9__115_0;

		public static Func<SimRaceCarDriver, string> _003C_003E9__115_1;

		public static Func<SimRacingTrack, int> _003C_003E9__141_0;

		public static Func<SimRacingTrack, int> _003C_003E9__142_0;

		public static Func<SimRacingCup, float> _003C_003E9__143_0;

		public static Func<SimRacingTrack, double> _003C_003E9__143_1;

		public static Func<SimRacingTrack, int> _003C_003E9__144_0;

		public static Func<SimRacingTrack, int> _003C_003E9__145_0;

		internal int _003CEligibleForFanInteraction_003Eb__90_0(SimRacingTrack t)
		{
			return 0;
		}

		internal string _003CAddCupForType_003Eb__101_0(SimRacingCup item)
		{
			return null;
		}

		internal float _003CGetActiveDriversForCategoryOrNull_003Eb__110_1(SimRaceCarDriver x)
		{
			return 0f;
		}

		internal float _003CMoveInjuredDriverToActiveList_003Eb__114_1(SimRaceCarDriver x)
		{
			return 0f;
		}

		internal string _003CGenerateNewDriver_003Eb__115_0(SimRaceCarDriver item)
		{
			return null;
		}

		internal string _003CGenerateNewDriver_003Eb__115_1(SimRaceCarDriver item)
		{
			return null;
		}

		internal int _003CGetTotalRacesWon_003Eb__141_0(SimRacingTrack t)
		{
			return 0;
		}

		internal int _003CGetTotalRacesLost_003Eb__142_0(SimRacingTrack t)
		{
			return 0;
		}

		internal float _003CGetTotalWinnings_003Eb__143_0(SimRacingCup t)
		{
			return 0f;
		}

		internal double _003CGetTotalWinnings_003Eb__143_1(SimRacingTrack t)
		{
			return 0.0;
		}

		internal int _003CGetTotalCupsWon_003Eb__144_0(SimRacingTrack t)
		{
			return 0;
		}

		internal int _003CGetTotalCupsLost_003Eb__145_0(SimRacingTrack t)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass100_0
	{
		public RACING_TRACK_CATEGORY category;

		public SimRacing _003C_003E4__this;

		internal bool _003CUpdateCupsForCategory_003Eb__0(SimRacingCup item)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass102_0
	{
		public RACING_TRACK_CATEGORY trackCategory;

		public RACING_LICENSE license;

		internal bool _003CCreateCup_003Eb__0(SimRacingTrack x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass108_0
	{
		public SimCar car;

		internal bool _003CRebuildCarsForSale_003Eb__0(SimCar t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass110_0
	{
		public RACING_TRACK_CATEGORY category;

		internal bool _003CGetActiveDriversForCategoryOrNull_003Eb__0(SimRaceCarDriver d)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass114_0
	{
		public SimRaceCarDriver driver;

		internal bool _003CMoveInjuredDriverToActiveList_003Eb__0(SimRaceCarDriver t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass120_0
	{
		public string crewName;

		internal bool _003CRefreshPitCrewCandidates_003Eb__0(string t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass127_0
	{
		public RACING_TRACK_CATEGORY category;

		public RACING_LICENSE licenseType;

		internal bool _003CIsLicenseUpgradeAvailable_003Eb__0(SimRacingCup t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass132_0
	{
		public SimRacing _003C_003E4__this;

		public RACING_TRACK_CATEGORY category;

		internal bool _003CGetAvailableTracksForCategory_003Eb__0(SimRacingTrack t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass134_0
	{
		public string winningDriverName;

		internal bool _003CGetDriverByName_003Eb__0(SimRaceCarDriver t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass146_0
	{
		public RACING_TRACK_CATEGORY circuit;

		internal bool _003CGetCupsRaced_003Eb__0(SimRacingCup t)
		{
			return false;
		}
	}

	private SimRaceCarGarage Garage__BackingField;

	private SimRacingPitCrew PitCrew__BackingField;

	private List<SimRacingPitCrew> PitCrewCandidates__BackingField;

	private List<SimRacingTrack> Tracks__BackingField;

	private List<SimRacingCup> Cups__BackingField;

	private List<SimRacingCup> CupsHeroHasParticipatedIn__BackingField;

	private int CupsWon__BackingField;

	private int CupsLost__BackingField;

	private List<RACING_TRACK_CATEGORY> TrackTypesRacedOn__BackingField;

	private Dictionary<RACING_TRACK_CATEGORY, RACING_LICENSE> RacingLicense__BackingField;

	private static Scenario PaywallScenario__BackingField;

	private bool HasReceivedBankruptcyWarning__BackingField;

	private int NumRacesParticipatedThisYear__BackingField;

	private int NumCupRacesParticipatedThisYear__BackingField;

	private int NumUntilFanInteraction__BackingField;

	private List<SimCar> _carsForSale;

	private List<SimRaceCarDriver> _activeDrivers;

	private List<SimRaceCarDriver> _injuredDrivers;

	[NonSerialized]
	private ArrayList _racingIncrementAgeScenarioQueueArray;

	private bool HasRejectedFanInteraction__BackingField;

	private bool WonQualifyingRaceThisYear__BackingField;

	private int NumYearsToStealNextCar__BackingField;

	private int LastAgeHadRaceInteraction__BackingField;

	public SimRaceCarGarage Garage
	{
		get
		{
			return Garage__BackingField;
		}
		set
		{
			Garage__BackingField = value;
		}
	}

	public SimRacingPitCrew PitCrew
	{
		get
		{
			return PitCrew__BackingField;
		}
		set
		{
			PitCrew__BackingField = value;
		}
	}

	public List<SimRacingPitCrew> PitCrewCandidates
	{
		get
		{
			return PitCrewCandidates__BackingField;
		}
		set
		{
			PitCrewCandidates__BackingField = value;
		}
	}

	public List<SimRacingTrack> Tracks
	{
		get
		{
			return Tracks__BackingField;
		}
		set
		{
			Tracks__BackingField = value;
		}
	}

	public List<SimRacingCup> Cups
	{
		get
		{
			return Cups__BackingField;
		}
		set
		{
			Cups__BackingField = value;
		}
	}

	public List<SimRacingCup> CupsHeroHasParticipatedIn
	{
		get
		{
			return CupsHeroHasParticipatedIn__BackingField;
		}
		set
		{
			CupsHeroHasParticipatedIn__BackingField = value;
		}
	}

	public int CupsWon
	{
		get
		{
			return CupsWon__BackingField;
		}
		set
		{
			CupsWon__BackingField = value;
		}
	}

	public int CupsLost
	{
		get
		{
			return CupsLost__BackingField;
		}
		set
		{
			CupsLost__BackingField = value;
		}
	}

	public List<RACING_TRACK_CATEGORY> TrackTypesRacedOn
	{
		get
		{
			return TrackTypesRacedOn__BackingField;
		}
		set
		{
			TrackTypesRacedOn__BackingField = value;
		}
	}

	public Dictionary<RACING_TRACK_CATEGORY, RACING_LICENSE> RacingLicense
	{
		get
		{
			return RacingLicense__BackingField;
		}
		set
		{
			RacingLicense__BackingField = value;
		}
	}

	public static Scenario PaywallScenario
	{
		get
		{
			return PaywallScenario__BackingField;
		}
		set
		{
			PaywallScenario__BackingField = value;
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

	public int NumRacesParticipatedThisYear
	{
		get
		{
			return NumRacesParticipatedThisYear__BackingField;
		}
		set
		{
			NumRacesParticipatedThisYear__BackingField = value;
		}
	}

	public int NumCupRacesParticipatedThisYear
	{
		get
		{
			return NumCupRacesParticipatedThisYear__BackingField;
		}
		set
		{
			NumCupRacesParticipatedThisYear__BackingField = value;
		}
	}

	public int NumUntilFanInteraction
	{
		get
		{
			return NumUntilFanInteraction__BackingField;
		}
		set
		{
			NumUntilFanInteraction__BackingField = value;
		}
	}

	public bool HasRejectedFanInteraction
	{
		get
		{
			return HasRejectedFanInteraction__BackingField;
		}
		set
		{
			HasRejectedFanInteraction__BackingField = value;
		}
	}

	public bool WonQualifyingRaceThisYear
	{
		get
		{
			return WonQualifyingRaceThisYear__BackingField;
		}
		set
		{
			WonQualifyingRaceThisYear__BackingField = value;
		}
	}

	public int NumYearsToStealNextCar
	{
		get
		{
			return NumYearsToStealNextCar__BackingField;
		}
		set
		{
			NumYearsToStealNextCar__BackingField = value;
		}
	}

	public int LastAgeHadRaceInteraction
	{
		get
		{
			return LastAgeHadRaceInteraction__BackingField;
		}
		set
		{
			LastAgeHadRaceInteraction__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	private void AgeUpPortfolioScenario()
	{
	}

	private void CheckForDriverDeaths()
	{
	}

	public static bool IsCarNPCOwned(SimRaceCar raceCar)
	{
		return false;
	}

	private void AgeUpScenarios()
	{
	}

	private void CheckForBrokenCars()
	{
	}

	private void RacingCups()
	{
	}

	private bool EligibleForRivalChallenge(SimRaceCarDriver rivalDriver)
	{
		return false;
	}

	private bool EligibleForRivalRacerSabotage(SimRaceCarDriver rivalDriver)
	{
		return false;
	}

	private bool EligibleForFanInteraction()
	{
		return false;
	}

	private SimLover GetLoveInterest()
	{
		return null;
	}

	private bool EligibleForLoveInterest()
	{
		return false;
	}

	private bool EligibleForGoldFanEncouragement()
	{
		return false;
	}

	private bool HasCarInCategory(RACING_TRACK_CATEGORY rivalDriverCategory)
	{
		return false;
	}

	private RACING_LICENSE HighestLicenseInAnyType()
	{
		return RACING_LICENSE.NONE;
	}

	public List<string> GetUnRacedTracksList()
	{
		return null;
	}

	private void CancelAllRacingScenarios()
	{
	}

	private bool ProcessLoan(SimLoan loan)
	{
		return false;
	}

	public List<SimRaceCarDriver> GetCompetitorsForCircuitType(RACING_TRACK_CATEGORY circuitType)
	{
		return null;
	}

	private void UpdateCupsForCategory(RACING_TRACK_CATEGORY category, bool blockRacesThisAgeup)
	{
	}

	private void AddCupForType(RACING_TRACK_CATEGORY category)
	{
	}

	private SimRacingCup CreateCup(string cupName, RACING_TRACK_CATEGORY trackCategory, RACING_LICENSE license)
	{
		return null;
	}

	public List<SimRacingCup> GetRacingCupsForType(RACING_TRACK_CATEGORY category, RACING_LICENSE racingLicense)
	{
		return null;
	}

	public void CreateNewTracks()
	{
	}

	public string GetLicenseTitleForStatusForCategory(RACING_TRACK_CATEGORY category)
	{
		return null;
	}

	private void CalculateCosts()
	{
	}

	public List<SimCar> GetCarsForSale()
	{
		return null;
	}

	public void RebuildCarsForSale()
	{
	}

	public void RemoveCarFromDealership(SimCar car)
	{
	}

	public List<SimRaceCarDriver> GetActiveDriversForCategoryOrNull(RACING_TRACK_CATEGORY category)
	{
		return null;
	}

	public void AddDriver(SimRaceCarDriver driver)
	{
	}

	public void RemoveDriver(SimRaceCarDriver driver)
	{
	}

	public void MoveDriverToInjuredList(SimRaceCarDriver driver)
	{
	}

	public void MoveInjuredDriverToActiveList(SimRaceCarDriver driver)
	{
	}

	public SimRaceCarDriver GenerateNewDriver(RACING_TRACK_CATEGORY category, RACING_LICENSE license)
	{
		return null;
	}

	public SimRaceCarDriver GetRivalDriverOrNull()
	{
		return null;
	}

	public void RefreshActiveDrivers()
	{
	}

	private void PopulateDriversForCategory(RACING_TRACK_CATEGORY category, int requireCount = 5)
	{
	}

	public List<SimRaceCarDriver> GetRandomDriversForCategory(RACING_TRACK_CATEGORY category, int numNeeded)
	{
		return null;
	}

	public void RefreshPitCrewCandidates()
	{
	}

	private void ReplenishRaceDrivers()
	{
	}

	public static float GetMinimumCashRaceBetAmount()
	{
		return 0f;
	}

	public static float GetMaximumCashRaceBetAmount()
	{
		return 0f;
	}

	public bool ActiveDriversAvailable()
	{
		return false;
	}

	public int GetNumberOfNPCDriversForRaceCategory(RACING_TRACK_CATEGORY category)
	{
		return 0;
	}

	public string GetLicenseString(RACING_TRACK_CATEGORY category, RACING_LICENSE license)
	{
		return null;
	}

	public bool IsLicenseUpgradeAvailable(RACING_TRACK_CATEGORY category)
	{
		return false;
	}

	public void UpgradeLicense(RACING_TRACK_CATEGORY category)
	{
	}

	public static string GetTrackCategoryString(RACING_TRACK_CATEGORY category)
	{
		return null;
	}

	public int CompareCarClasses(string car1, string car2)
	{
		return 0;
	}

	public float GetMonthlyCosts()
	{
		return 0f;
	}

	public List<SimRacingTrack> GetAvailableTracksForCategory(RACING_TRACK_CATEGORY category)
	{
		return null;
	}

	public SimRacingTrack GetRandomAvailableTrackForCategory(RACING_TRACK_CATEGORY category)
	{
		return null;
	}

	public SimRaceCarDriver GetDriverByName(string winningDriverName)
	{
		return null;
	}

	public static string GetPlacementString(int placement)
	{
		return null;
	}

	public static string GetCarStatString(RACING_CAR_STAT stat)
	{
		return null;
	}

	public static int GetRaceWarningsBeforeCrash()
	{
		return 0;
	}

	public static DataAccessory GetAccessoryRewardOrNull()
	{
		return null;
	}

	public static SimRacingCollectible GetCollectibleRewardOrNull(bool mustGive)
	{
		return null;
	}

	public void AddTrackTypeRacedOn(RACING_TRACK_CATEGORY trackDataTrackType)
	{
	}

	public int GetTotalRacesWon()
	{
		return 0;
	}

	public int GetTotalRacesLost()
	{
		return 0;
	}

	public double GetTotalWinnings()
	{
		return 0.0;
	}

	public int GetTotalCupsWon()
	{
		return 0;
	}

	public int GetTotalCupsLost()
	{
		return 0;
	}

	public List<SimRacingCup> GetCupsRaced(RACING_TRACK_CATEGORY circuit)
	{
		return null;
	}

	public static string GetRaceTrackTerm(RACING_TRACK_CATEGORY trackType)
	{
		return null;
	}

	public static string GetRaceTrackTerm(string trackName)
	{
		return null;
	}

	public static string GetTrophyString(int placement)
	{
		return null;
	}

	public static string GetEmojiForCup(int cupPlacement)
	{
		return null;
	}

	public SimRaceCar GetRandomGarageCar()
	{
		return null;
	}

	public void CloseGarage()
	{
	}

	public static SimCar GenerateFreeCar()
	{
		return null;
	}

	public static string GetTrackingTrackString(RACING_TRACK_CATEGORY trackDataTrackType)
	{
		return null;
	}

	public void ResetValuesAfterPrison()
	{
	}
}
