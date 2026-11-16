using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSpecialJobActor : SimSpecialJob
{
	private SimActorFilmRole FilmRole__BackingField;

	private SimActorShowRole ShowRole__BackingField;

	private double TotalEarnings__BackingField;

	private ArrayList _FilmographyArray__BackingField;

	private ArrayList CostarArray__BackingField;

	[NonSerialized]
	public List<Scenario> _ActorIncrementAgeScenarioQueueArray;

	private int AgeAtStartRole__BackingField;

	private int AgeAtLastPractice__BackingField;

	private int NumPracticesAtCurrentAge__BackingField;

	private int AgeAtLastDevelopCharacter__BackingField;

	private int NumDevelopCharactersAtCurrentAge__BackingField;

	private int AgeAtLastPublicityStunt__BackingField;

	private float Att_reputation__BackingField;

	private float Att_performance__BackingField;

	public SimActorFilmRole FilmRole
	{
		get
		{
			return FilmRole__BackingField;
		}
		set
		{
			FilmRole__BackingField = value;
		}
	}

	public SimActorShowRole ShowRole
	{
		get
		{
			return ShowRole__BackingField;
		}
		set
		{
			ShowRole__BackingField = value;
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

	private ArrayList _FilmographyArray
	{
		get
		{
			return _FilmographyArray__BackingField;
		}
		set
		{
			_FilmographyArray__BackingField = value;
		}
	}

	public ArrayList FilmographyArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList CostarArray
	{
		get
		{
			return CostarArray__BackingField;
		}
		set
		{
			CostarArray__BackingField = value;
		}
	}

	public List<Scenario> ActorIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtStartRole
	{
		get
		{
			return AgeAtStartRole__BackingField;
		}
		set
		{
			AgeAtStartRole__BackingField = value;
		}
	}

	public int AgeAtLastPractice
	{
		get
		{
			return AgeAtLastPractice__BackingField;
		}
		set
		{
			AgeAtLastPractice__BackingField = value;
		}
	}

	public int NumPracticesAtCurrentAge
	{
		get
		{
			return NumPracticesAtCurrentAge__BackingField;
		}
		set
		{
			NumPracticesAtCurrentAge__BackingField = value;
		}
	}

	public int AgeAtLastDevelopCharacter
	{
		get
		{
			return AgeAtLastDevelopCharacter__BackingField;
		}
		set
		{
			AgeAtLastDevelopCharacter__BackingField = value;
		}
	}

	public int NumDevelopCharactersAtCurrentAge
	{
		get
		{
			return NumDevelopCharactersAtCurrentAge__BackingField;
		}
		set
		{
			NumDevelopCharactersAtCurrentAge__BackingField = value;
		}
	}

	public int AgeAtLastPublicityStunt
	{
		get
		{
			return AgeAtLastPublicityStunt__BackingField;
		}
		set
		{
			AgeAtLastPublicityStunt__BackingField = value;
		}
	}

	public float Att_reputation
	{
		get
		{
			return Att_reputation__BackingField;
		}
		set
		{
			Att_reputation__BackingField = value;
		}
	}

	public float Att_performance
	{
		get
		{
			return Att_performance__BackingField;
		}
		set
		{
			Att_performance__BackingField = value;
		}
	}

	public SimSpecialJobActor(SimActorFilmRole theFilmRole)
	{
	}

	public SimSpecialJobActor(SimActorShowRole theShowRole)
	{
	}

	public void SetupEverythingElseForNewSpecialJobActor()
	{
	}

	public override void IncrementAge()
	{
	}

	public void RefreshCostars()
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

	public int GetActorType()
	{
		return 0;
	}

	public static string GetActorFameTitle()
	{
		return null;
	}

	public static SimSpecialJobActor GetCurrentSpecialJobActor()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsAsActor()
	{
		return null;
	}

	public static SimSpecialJobActor GetMostRecentSpecialJobActor()
	{
		return null;
	}

	public static int GetYearsSinceLastRole()
	{
		return 0;
	}

	public static ArrayList GetCareerFilmography()
	{
		return null;
	}

	public static float GetCurrentReputation()
	{
		return 0f;
	}

	public float GetStartingPerformance()
	{
		return 0f;
	}

	public static bool HasEverShunnedAwardType(int awardType)
	{
		return false;
	}

	public void RecordStats()
	{
	}

	public static int PropensityScoreForHeroToPlayFilmRole(SimActorFilmRole role)
	{
		return 0;
	}

	public static int PropensityScoreForHeroToPlayShowRole(SimActorShowRole role)
	{
		return 0;
	}

	public static float GetCompensationForFilmRole(SimActorFilmRole role)
	{
		return 0f;
	}

	public static float GetBoxOfficeBonusPercentForFilmRole(SimActorFilmRole role)
	{
		return 0f;
	}

	public static float GetCompensationPerEpisodeForShowRole(SimActorShowRole role)
	{
		return 0f;
	}

	public static float GetRoundedCompensation(float compensation)
	{
		return 0f;
	}

	public float GetAverageRelationshipStrengthWithCostars()
	{
		return 0f;
	}

	public static SimActorDirectorMeta GetDirectorMetaForFilmGenre(int genre)
	{
		return null;
	}

	public static SimCostar GetDirectorForFilm(SimActorFilm film)
	{
		return null;
	}

	public SimCostar GetCurrentFilmDirector()
	{
		return null;
	}

	public ArrayList GetCostarArrayFilteredByNormalActors()
	{
		return null;
	}

	public string GetCurrentProductionTitleWithQuotes()
	{
		return null;
	}

	public static SimActorFilmRole GetBestAvailableFilmRoleForHero()
	{
		return null;
	}

	public static SimActorShowRole GetBestAvailableShowRoleForHero()
	{
		return null;
	}

	public string GetOccupationString()
	{
		return null;
	}

	public void SetupLawsuitTargetAgainstStudioForDisease(SimDisease disease)
	{
	}

	public void ClearAnyPendingActorScenarios()
	{
	}

	public bool IsEligibleForMajorScenes()
	{
		return false;
	}

	public ArrayList GetScenarioStringBankArray()
	{
		return null;
	}

	public ArrayList GetScenarioArrayForPostStartRole()
	{
		return null;
	}

	public static void ImpactReputation(int impactScore)
	{
	}

	public static void ImpactPerformance(int impactScore)
	{
	}

	public static bool DevForceRoleOffers()
	{
		return false;
	}
}
