using System;
using System.Collections;

[Serializable]
public class SimWorkplace
{
	public static int kNumCoworkersSmallTeam;

	public static int kNumCoworkers;

	private int Type__BackingField;

	private int ManagementStrictness__BackingField;

	private ArrayList CoworkerArray__BackingField;

	private int AgeAtLastCoworkerRefresh__BackingField;

	private int TroublePoints__BackingField;

	private int TimesReportedCoworker__BackingField;

	private int AgeAtLastHookedUpWithSupervisor__BackingField;

	public int Type
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

	public int ManagementStrictness
	{
		get
		{
			return ManagementStrictness__BackingField;
		}
		set
		{
			ManagementStrictness__BackingField = value;
		}
	}

	public ArrayList CoworkerArray
	{
		get
		{
			return CoworkerArray__BackingField;
		}
		set
		{
			CoworkerArray__BackingField = value;
		}
	}

	public int AgeAtLastCoworkerRefresh
	{
		get
		{
			return AgeAtLastCoworkerRefresh__BackingField;
		}
		set
		{
			AgeAtLastCoworkerRefresh__BackingField = value;
		}
	}

	public int TroublePoints
	{
		get
		{
			return TroublePoints__BackingField;
		}
		set
		{
			TroublePoints__BackingField = value;
		}
	}

	public int TimesReportedCoworker
	{
		get
		{
			return TimesReportedCoworker__BackingField;
		}
		set
		{
			TimesReportedCoworker__BackingField = value;
		}
	}

	public int AgeAtLastHookedUpWithSupervisor
	{
		get
		{
			return AgeAtLastHookedUpWithSupervisor__BackingField;
		}
		set
		{
			AgeAtLastHookedUpWithSupervisor__BackingField = value;
		}
	}

	public SimWorkplace(int theType)
	{
	}

	public void IncrementYearsInWorkplace()
	{
	}

	public void RefreshCoworkers()
	{
	}

	public int NumCoworkersOnTeam()
	{
		return 0;
	}

	public bool TeamNeedsASupervisor()
	{
		return false;
	}

	public SimCoworker GetSupervisor()
	{
		return null;
	}

	public float GetAverageRelationshipStrengthWithCoworkers()
	{
		return 0f;
	}

	public void FireSupervisor()
	{
	}

	public void FireCoworker(SimCoworker coworker)
	{
	}

	public bool DoesSupervisorWantToHookUpWithHero()
	{
		return false;
	}

	public bool WillSupervisorAcceptHeroAttemptToSeduce()
	{
		return false;
	}

	public void ImpactRelationshipWithAllCoworkers(int impactScore)
	{
	}

	public void ImpactWorkplacePerformance(int impactScore)
	{
	}
}
