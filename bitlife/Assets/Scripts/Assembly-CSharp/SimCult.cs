using System;
using System.Collections.Generic;

[Serializable]
public class SimCult
{
	public static int ExtraScenarioPercent;

	public static int DumbScenarioPercent;

	private string Name__BackingField;

	private string LeaderHonorificName__BackingField;

	private CULT_PHILOSOPHY Philosophy__BackingField;

	private int YearsActive__BackingField;

	private int YearsAsLeader__BackingField;

	private int Followers__BackingField;

	private List<string> DoctrineList__BackingField;

	private double CurrentAnnualRevenue__BackingField;

	private double LastYearRevenue__BackingField;

	private double InitialMembershipFee__BackingField;

	private double AnnualMembershipFee__BackingField;

	private double ManualEarningsThisYear__BackingField;

	private int NumRecruitmentAttemptsCurrentYear__BackingField;

	private int NumSelectionApplicationsReviewedCurrentYear__BackingField;

	private int NumUnsolvedDevotionRelatedDeaths__BackingField;

	private bool DevotionDeathInvestigationStarted__BackingField;

	private int HeroAgeAtLastPullInteractionWithCult__BackingField;

	private int HeroAgeAtLastTeachInPrison__BackingField;

	private int HeroAgeAtLastTeach__BackingField;

	private int NumCommunalFastingCurrentYear__BackingField;

	private int HeroAgeAtLastStructureUse__BackingField;

	private int HeroAgeAtLastChangeMembership__BackingField;

	private SimCultPlot Plot__BackingField;

	private SimTotem Totem__BackingField;

	private int TattooStyle__BackingField;

	private CULT_TATTOO_LOCATION TattooLocation__BackingField;

	private int FollowersGainedThisYear__BackingField;

	private int FollowersLostThisYear__BackingField;

	private int NumWorkshopsMadeCurrentYear__BackingField;

	private int HeroLastOfferingItemType__BackingField;

	private int NumOutreachCurrentYear__BackingField;

	private int YearsWithSameTotem__BackingField;

	[NonSerialized]
	public List<Scenario> _CultIncrementAgeScenarioQueueArray;

	private float Reputation__BackingField;

	private List<string> _ListHookupCommunards__BackingField;

	private int NumBabiesHadInCult__BackingField;

	private int NumCommunardsTaxidermyInCult__BackingField;

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

	public string LeaderHonorificName
	{
		get
		{
			return LeaderHonorificName__BackingField;
		}
		set
		{
			LeaderHonorificName__BackingField = value;
		}
	}

	public CULT_PHILOSOPHY Philosophy
	{
		get
		{
			return Philosophy__BackingField;
		}
		set
		{
			Philosophy__BackingField = value;
		}
	}

	public int YearsActive
	{
		get
		{
			return YearsActive__BackingField;
		}
		set
		{
			YearsActive__BackingField = value;
		}
	}

	public int YearsAsLeader
	{
		get
		{
			return YearsAsLeader__BackingField;
		}
		set
		{
			YearsAsLeader__BackingField = value;
		}
	}

	public int Followers
	{
		get
		{
			return Followers__BackingField;
		}
		set
		{
			Followers__BackingField = value;
		}
	}

	public List<string> DoctrineList
	{
		get
		{
			return DoctrineList__BackingField;
		}
		set
		{
			DoctrineList__BackingField = value;
		}
	}

	public double CurrentAnnualRevenue
	{
		get
		{
			return CurrentAnnualRevenue__BackingField;
		}
		set
		{
			CurrentAnnualRevenue__BackingField = value;
		}
	}

	public double LastYearRevenue
	{
		get
		{
			return LastYearRevenue__BackingField;
		}
		set
		{
			LastYearRevenue__BackingField = value;
		}
	}

	public double InitialMembershipFee
	{
		get
		{
			return InitialMembershipFee__BackingField;
		}
		set
		{
			InitialMembershipFee__BackingField = value;
		}
	}

	public double AnnualMembershipFee
	{
		get
		{
			return AnnualMembershipFee__BackingField;
		}
		set
		{
			AnnualMembershipFee__BackingField = value;
		}
	}

	public double ManualEarningsThisYear
	{
		get
		{
			return ManualEarningsThisYear__BackingField;
		}
		set
		{
			ManualEarningsThisYear__BackingField = value;
		}
	}

	public int NumRecruitmentAttemptsCurrentYear
	{
		get
		{
			return NumRecruitmentAttemptsCurrentYear__BackingField;
		}
		set
		{
			NumRecruitmentAttemptsCurrentYear__BackingField = value;
		}
	}

	public int NumSelectionApplicationsReviewedCurrentYear
	{
		get
		{
			return NumSelectionApplicationsReviewedCurrentYear__BackingField;
		}
		set
		{
			NumSelectionApplicationsReviewedCurrentYear__BackingField = value;
		}
	}

	public int NumUnsolvedDevotionRelatedDeaths
	{
		get
		{
			return NumUnsolvedDevotionRelatedDeaths__BackingField;
		}
		set
		{
			NumUnsolvedDevotionRelatedDeaths__BackingField = value;
		}
	}

	public bool DevotionDeathInvestigationStarted
	{
		get
		{
			return DevotionDeathInvestigationStarted__BackingField;
		}
		set
		{
			DevotionDeathInvestigationStarted__BackingField = value;
		}
	}

	public int HeroAgeAtLastPullInteractionWithCult
	{
		get
		{
			return HeroAgeAtLastPullInteractionWithCult__BackingField;
		}
		set
		{
			HeroAgeAtLastPullInteractionWithCult__BackingField = value;
		}
	}

	public int HeroAgeAtLastTeachInPrison
	{
		get
		{
			return HeroAgeAtLastTeachInPrison__BackingField;
		}
		set
		{
			HeroAgeAtLastTeachInPrison__BackingField = value;
		}
	}

	public int HeroAgeAtLastTeach
	{
		get
		{
			return HeroAgeAtLastTeach__BackingField;
		}
		set
		{
			HeroAgeAtLastTeach__BackingField = value;
		}
	}

	public int NumCommunalFastingCurrentYear
	{
		get
		{
			return NumCommunalFastingCurrentYear__BackingField;
		}
		set
		{
			NumCommunalFastingCurrentYear__BackingField = value;
		}
	}

	public int HeroAgeAtLastStructureUse
	{
		get
		{
			return HeroAgeAtLastStructureUse__BackingField;
		}
		set
		{
			HeroAgeAtLastStructureUse__BackingField = value;
		}
	}

	public int HeroAgeAtLastChangeMembership
	{
		get
		{
			return HeroAgeAtLastChangeMembership__BackingField;
		}
		set
		{
			HeroAgeAtLastChangeMembership__BackingField = value;
		}
	}

	public SimCultPlot Plot
	{
		get
		{
			return Plot__BackingField;
		}
		set
		{
			Plot__BackingField = value;
		}
	}

	public SimTotem Totem
	{
		get
		{
			return Totem__BackingField;
		}
		set
		{
			Totem__BackingField = value;
		}
	}

	public int TattooStyle
	{
		get
		{
			return TattooStyle__BackingField;
		}
		set
		{
			TattooStyle__BackingField = value;
		}
	}

	public CULT_TATTOO_LOCATION TattooLocation
	{
		get
		{
			return TattooLocation__BackingField;
		}
		set
		{
			TattooLocation__BackingField = value;
		}
	}

	public int FollowersGainedThisYear
	{
		get
		{
			return FollowersGainedThisYear__BackingField;
		}
		set
		{
			FollowersGainedThisYear__BackingField = value;
		}
	}

	public int FollowersLostThisYear
	{
		get
		{
			return FollowersLostThisYear__BackingField;
		}
		set
		{
			FollowersLostThisYear__BackingField = value;
		}
	}

	public int NumWorkshopsMadeCurrentYear
	{
		get
		{
			return NumWorkshopsMadeCurrentYear__BackingField;
		}
		set
		{
			NumWorkshopsMadeCurrentYear__BackingField = value;
		}
	}

	public int HeroLastOfferingItemType
	{
		get
		{
			return HeroLastOfferingItemType__BackingField;
		}
		set
		{
			HeroLastOfferingItemType__BackingField = value;
		}
	}

	public int NumOutreachCurrentYear
	{
		get
		{
			return NumOutreachCurrentYear__BackingField;
		}
		set
		{
			NumOutreachCurrentYear__BackingField = value;
		}
	}

	public int YearsWithSameTotem
	{
		get
		{
			return YearsWithSameTotem__BackingField;
		}
		set
		{
			YearsWithSameTotem__BackingField = value;
		}
	}

	public List<Scenario> CultIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float Reputation
	{
		get
		{
			return Reputation__BackingField;
		}
		set
		{
			Reputation__BackingField = value;
		}
	}

	private List<string> _ListHookupCommunards
	{
		get
		{
			return _ListHookupCommunards__BackingField;
		}
		set
		{
			_ListHookupCommunards__BackingField = value;
		}
	}

	public List<string> ListHookupCommunards
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumBabiesHadInCult
	{
		get
		{
			return NumBabiesHadInCult__BackingField;
		}
		set
		{
			NumBabiesHadInCult__BackingField = value;
		}
	}

	public int NumCommunardsTaxidermyInCult
	{
		get
		{
			return NumCommunardsTaxidermyInCult__BackingField;
		}
		set
		{
			NumCommunardsTaxidermyInCult__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public int GetAnnualFollowers()
	{
		return 0;
	}

	public void AdjustFollowers(int followers, bool annualFollowers = false)
	{
	}

	public double CalculateAnnualRevenue()
	{
		return 0.0;
	}

	public void AdjustAnnualReputation()
	{
	}

	public void ImpactReputation(int impactScore)
	{
	}

	public string GetStringForPhilosophy(CULT_PHILOSOPHY philosophy, bool capitalize = false)
	{
		return null;
	}

	public CULT_PHILOSOPHY GetPhilosophyForName(string name)
	{
		return CULT_PHILOSOPHY.NONE;
	}

	public int GetAppealScore()
	{
		return 0;
	}

	public int MaxHighRankCommunardsForType()
	{
		return 0;
	}

	public int MaxCommunardsForType()
	{
		return 0;
	}

	public bool CultWillSupportHero()
	{
		return false;
	}

	public double NetEarnings()
	{
		return 0.0;
	}

	public void CancelAllCultScenarios()
	{
	}

	public static void CloseCommune()
	{
	}
}
