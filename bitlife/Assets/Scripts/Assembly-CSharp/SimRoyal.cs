using System;
using System.Collections;

[Serializable]
public class SimRoyal
{
	private string _CountryCode;

	private int Rank__BackingField;

	private string Seat__BackingField;

	private bool RoyalByBirth__BackingField;

	private bool RoyalDowager__BackingField;

	private int YearsRoyal__BackingField;

	private int YearsInCurrentRank__BackingField;

	private SimHouse RoyalHouse__BackingField;

	private string Honorific__BackingField;

	private int TerminationType__BackingField;

	private int CurrentLawIndex__BackingField;

	private ArrayList _LawIndexHistoryArray__BackingField;

	private int NumTimesActedCrazy__BackingField;

	private int NumPeopleExecuted__BackingField;

	private int AgeAtLastPublicOutrage__BackingField;

	private int AgeAtLastPublicService__BackingField;

	private int AgeAtTerminatedRoyalty__BackingField;

	private ArrayList _RoyalSeatsUsedArray__BackingField;

	private ArrayList _OtherRoyalArray__BackingField;

	private int CumulativeRespectImpactDelta__BackingField;

	private float Att_respect__BackingField;

	private bool HasExecutedThisYear__BackingField;

	public DataCountryRoyalSystem System
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int Rank
	{
		get
		{
			return Rank__BackingField;
		}
		set
		{
			Rank__BackingField = value;
		}
	}

	public string Seat
	{
		get
		{
			return Seat__BackingField;
		}
		set
		{
			Seat__BackingField = value;
		}
	}

	public bool RoyalByBirth
	{
		get
		{
			return RoyalByBirth__BackingField;
		}
		set
		{
			RoyalByBirth__BackingField = value;
		}
	}

	public bool RoyalDowager
	{
		get
		{
			return RoyalDowager__BackingField;
		}
		set
		{
			RoyalDowager__BackingField = value;
		}
	}

	public int YearsRoyal
	{
		get
		{
			return YearsRoyal__BackingField;
		}
		set
		{
			YearsRoyal__BackingField = value;
		}
	}

	public int YearsInCurrentRank
	{
		get
		{
			return YearsInCurrentRank__BackingField;
		}
		set
		{
			YearsInCurrentRank__BackingField = value;
		}
	}

	public SimHouse RoyalHouse
	{
		get
		{
			return RoyalHouse__BackingField;
		}
		set
		{
			RoyalHouse__BackingField = value;
		}
	}

	public string Honorific
	{
		get
		{
			return Honorific__BackingField;
		}
		set
		{
			Honorific__BackingField = value;
		}
	}

	public int TerminationType
	{
		get
		{
			return TerminationType__BackingField;
		}
		set
		{
			TerminationType__BackingField = value;
		}
	}

	public int CurrentLawIndex
	{
		get
		{
			return CurrentLawIndex__BackingField;
		}
		set
		{
			CurrentLawIndex__BackingField = value;
		}
	}

	private ArrayList _LawIndexHistoryArray
	{
		get
		{
			return _LawIndexHistoryArray__BackingField;
		}
		set
		{
			_LawIndexHistoryArray__BackingField = value;
		}
	}

	public ArrayList LawIndexHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumTimesActedCrazy
	{
		get
		{
			return NumTimesActedCrazy__BackingField;
		}
		set
		{
			NumTimesActedCrazy__BackingField = value;
		}
	}

	public int NumPeopleExecuted
	{
		get
		{
			return NumPeopleExecuted__BackingField;
		}
		set
		{
			NumPeopleExecuted__BackingField = value;
		}
	}

	public int AgeAtLastPublicOutrage
	{
		get
		{
			return AgeAtLastPublicOutrage__BackingField;
		}
		set
		{
			AgeAtLastPublicOutrage__BackingField = value;
		}
	}

	public int AgeAtLastPublicService
	{
		get
		{
			return AgeAtLastPublicService__BackingField;
		}
		set
		{
			AgeAtLastPublicService__BackingField = value;
		}
	}

	public int AgeAtTerminatedRoyalty
	{
		get
		{
			return AgeAtTerminatedRoyalty__BackingField;
		}
		set
		{
			AgeAtTerminatedRoyalty__BackingField = value;
		}
	}

	private ArrayList _RoyalSeatsUsedArray
	{
		get
		{
			return _RoyalSeatsUsedArray__BackingField;
		}
		set
		{
			_RoyalSeatsUsedArray__BackingField = value;
		}
	}

	public ArrayList RoyalSeatsUsedArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _OtherRoyalArray
	{
		get
		{
			return _OtherRoyalArray__BackingField;
		}
		set
		{
			_OtherRoyalArray__BackingField = value;
		}
	}

	public ArrayList OtherRoyalArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int CumulativeRespectImpactDelta
	{
		get
		{
			return CumulativeRespectImpactDelta__BackingField;
		}
		set
		{
			CumulativeRespectImpactDelta__BackingField = value;
		}
	}

	public float Att_respect
	{
		get
		{
			return Att_respect__BackingField;
		}
		set
		{
			Att_respect__BackingField = value;
		}
	}

	public bool HasExecutedThisYear
	{
		get
		{
			return HasExecutedThisYear__BackingField;
		}
		set
		{
			HasExecutedThisYear__BackingField = value;
		}
	}

	private SimRoyal()
	{
	}

	public SimRoyal(int theRank, string theSeat, DataCountry theCountry, bool isByBirth)
	{
	}

	public SimRoyal(DataCountry theCountry)
	{
	}

	public float GetBirthFortune()
	{
		return 0f;
	}

	public void SetupEverythingElse()
	{
	}

	public void IncrementYearsAsRoyal()
	{
	}

	public void TerminateWithType(int theTerminationType)
	{
	}

	public void UpdatePersonNameTitle()
	{
	}

	public void UpdateHonorific()
	{
	}

	public string GetTitleWithName()
	{
		return null;
	}

	public string GetShortTitleWithArticle(bool article)
	{
		return null;
	}

	public string GetLongTitleWithArticle(bool article)
	{
		return null;
	}

	public bool IsHighestRankingRoyal()
	{
		return false;
	}

	public string GetRoyalHouseName()
	{
		return null;
	}

	public bool IsEligibleForRevolt()
	{
		return false;
	}

	public int GetMaxFame()
	{
		return 0;
	}

	public SimRoyalPerson GetHighestRoyalPerson()
	{
		return null;
	}

	public SimRoyalPerson GetOtherRoyalPerson()
	{
		return null;
	}

	public void RefreshCurrentLaw()
	{
	}

	public void ImpactRespect(int impactScore)
	{
	}

	public static SimPerson GetHeirAfterDeathOfMonarch(SimPerson oldMonarch)
	{
		return null;
	}

	public static SimHouse GenerateRoyalHouseInPlaceWithRank(SimPlace thePlace, int rank)
	{
		return null;
	}

	public static string GetShortTitleInCountryForRankAndGenderAndIsRoyalByBirthAndIsRoyalDowagerAndIsForPersonName(DataCountry theCountry, int rank, int gender, bool royalByBirth, bool royalDowager, bool isForPersonName)
	{
		return null;
	}

	public static string GetLongTitleInCountryForRankAndGenderAndSeatAndIsRoyalByBirthAndIsRoyalDowager(DataCountry theCountry, int rank, int gender, string seat, bool royalByBirth, bool royalDowager)
	{
		return null;
	}

	public static ArrayList GetRoyalRankTitleArrayInCountryCodeForGender(string theCountryCode, int gender)
	{
		return null;
	}

	public static string GetRandomSeatInCountry(DataCountry theCountry)
	{
		return null;
	}

	public static float GetAnnualStipendInCountryForRankAndAge(DataCountry theCountry, int rank, int age)
	{
		return 0f;
	}
}
