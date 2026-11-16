using System;
using System.Collections;

[Serializable]
public class SimSpecialJobAthlete : SimSpecialJob
{
	[NonSerialized]
	public DataSport _Sport;

	private int SportsIndex__BackingField;

	private SimSportsTeam Team__BackingField;

	[NonSerialized]
	public DataSportsPosition _Position;

	private int SportsPosition__BackingField;

	private int YearsCompleted__BackingField;

	private int YearsLeft__BackingField;

	private int ContractYears__BackingField;

	private double ContractValue__BackingField;

	private double TotalEarnings__BackingField;

	private SimAthleteSkills Skills__BackingField;

	private ArrayList _SeasonStatsArray__BackingField;

	private ArrayList _InjuryArray__BackingField;

	public DataSport Sport
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int SportsIndex
	{
		get
		{
			return SportsIndex__BackingField;
		}
		set
		{
			SportsIndex__BackingField = value;
		}
	}

	public SimSportsTeam Team
	{
		get
		{
			return Team__BackingField;
		}
		set
		{
			Team__BackingField = value;
		}
	}

	public DataSportsPosition Position
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int SportsPosition
	{
		get
		{
			return SportsPosition__BackingField;
		}
		set
		{
			SportsPosition__BackingField = value;
		}
	}

	public int YearsCompleted
	{
		get
		{
			return YearsCompleted__BackingField;
		}
		set
		{
			YearsCompleted__BackingField = value;
		}
	}

	public int YearsLeft
	{
		get
		{
			return YearsLeft__BackingField;
		}
		set
		{
			YearsLeft__BackingField = value;
		}
	}

	public int ContractYears
	{
		get
		{
			return ContractYears__BackingField;
		}
		set
		{
			ContractYears__BackingField = value;
		}
	}

	public double ContractValue
	{
		get
		{
			return ContractValue__BackingField;
		}
		set
		{
			ContractValue__BackingField = value;
		}
	}

	public double TotalEarnings
	{
		get
		{
			return TotalEarnings__BackingField;
		}
		set
		{
			TotalEarnings__BackingField = value;
		}
	}

	public SimAthleteSkills Skills
	{
		get
		{
			return Skills__BackingField;
		}
		set
		{
			Skills__BackingField = value;
		}
	}

	private ArrayList _SeasonStatsArray
	{
		get
		{
			return _SeasonStatsArray__BackingField;
		}
		set
		{
			_SeasonStatsArray__BackingField = value;
		}
	}

	public ArrayList SeasonStatsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _InjuryArray
	{
		get
		{
			return _InjuryArray__BackingField;
		}
		set
		{
			_InjuryArray__BackingField = value;
		}
	}

	public ArrayList InjuryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimSpecialJobAthlete(SimSportsTeam theTeam, DataSportsPosition thePosition, float theContractValue, int theContractYears)
	{
	}

	public void SignNewContractWithValueAndYears(float theContractValue, int theContractYears)
	{
	}

	public override void IncrementAge()
	{
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public override bool ShouldBecomeFamous()
	{
		return false;
	}

	public override int GetMaxFame()
	{
		return 0;
	}

	public override int GetFameType()
	{
		return 0;
	}

	public override bool CanAttendRehab()
	{
		return false;
	}

	public bool HeroHasDebilitatingInjury()
	{
		return false;
	}

	public SimSportsInjury GetDebilitatingInjury()
	{
		return null;
	}

	public ArrayList GetCurrentInjuries()
	{
		return null;
	}

	public SimSportsInjury SustainNewInjuryWithOrigin(int origin)
	{
		return null;
	}

	public int GetInjuryMissedGamesThisSeason()
	{
		return 0;
	}

	public void AnnualAttributeAdjustment()
	{
	}

	public string ContractString()
	{
		return null;
	}

	public void RecordNewEndOfSeasonStats()
	{
	}

	public string FormatValue(float theValue, int fractionData, int integerDigits)
	{
		return null;
	}

	public int GetNumSeasonsPlayingThisSport()
	{
		return 0;
	}

	public SimSportsTeam GetRandomOpponent()
	{
		return null;
	}

	public string ThirdPersonJobTitle()
	{
		return null;
	}

	public int PropensityForInjuryScore()
	{
		return 0;
	}

	public int GetNumChampionshipsWonPlayingThisSport()
	{
		return 0;
	}

	public double GetCareerEarningsPlayingThisSport()
	{
		return 0.0;
	}

	public string GetCareerLengthString()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsForSport(int sport)
	{
		return null;
	}

	public static ArrayList GetAllSeasonStatsPlayingSport(int sport)
	{
		return null;
	}

	public static int PropensityScoreForHeroToPlayProSportAndPositionAndIgnoreMinAge(int sport, DataSportsPosition position, bool ignoreMinAge)
	{
		return 0;
	}

	public static SimSportsContract GetContractToPlayProSportForTeamAndPositionAndForceContractAndOverrideYears(int sport, SimSportsTeam team, DataSportsPosition position, bool force, int overrideYears)
	{
		return null;
	}

	public static int GetSportForHeroToPlayAfterGraduationFromSchoolType(int schoolType)
	{
		return 0;
	}

	public static int GetSportForHeroToGetScholarshipOffer()
	{
		return 0;
	}

	public static bool DoesDedicatedLeagueExistForHeroGenderInSport(int sport)
	{
		return false;
	}

	public static int YearBanForDopingRefusal()
	{
		return 0;
	}

	public static int YearBanForDopingFail()
	{
		return 0;
	}

	public static bool IsHeroServingDopingBan()
	{
		return false;
	}
}
