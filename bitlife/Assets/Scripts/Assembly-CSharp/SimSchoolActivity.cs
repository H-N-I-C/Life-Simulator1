using System;

[Serializable]
public class SimSchoolActivity
{
	public string ActivityName;

	private int Status__BackingField;

	private int YearsInActivity__BackingField;

	private int AgeAtLastRejection__BackingField;

	private int AgeAtLastLeftTeam__BackingField;

	private int AgeAtLastPracticeHarder__BackingField;

	private string LeadershipStatus__BackingField;

	private float Att_performance__BackingField;

	public DataSchoolActivity ActivityData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int Status
	{
		get
		{
			return Status__BackingField;
		}
		set
		{
			Status__BackingField = value;
		}
	}

	public int YearsInActivity
	{
		get
		{
			return YearsInActivity__BackingField;
		}
		set
		{
			YearsInActivity__BackingField = value;
		}
	}

	public int AgeAtLastRejection
	{
		get
		{
			return AgeAtLastRejection__BackingField;
		}
		set
		{
			AgeAtLastRejection__BackingField = value;
		}
	}

	public int AgeAtLastLeftTeam
	{
		get
		{
			return AgeAtLastLeftTeam__BackingField;
		}
		set
		{
			AgeAtLastLeftTeam__BackingField = value;
		}
	}

	public int AgeAtLastPracticeHarder
	{
		get
		{
			return AgeAtLastPracticeHarder__BackingField;
		}
		set
		{
			AgeAtLastPracticeHarder__BackingField = value;
		}
	}

	public string LeadershipStatus
	{
		get
		{
			return LeadershipStatus__BackingField;
		}
		set
		{
			LeadershipStatus__BackingField = value;
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

	public SimSchoolActivity(DataSchoolActivity theActivityData)
	{
	}

	public string GetLeadershipString()
	{
		return null;
	}

	public bool InTopLeadershipRole()
	{
		return false;
	}

	public void ImpactRelationshipWithOtherClassmatesOnJoin()
	{
	}

	public void ImpactRelationshipWithOtherClassmatesOnLeave()
	{
	}

	public float GetInitialPerformanceAttribute()
	{
		return 0f;
	}

	public void ImpactPerformance(int impactScore)
	{
	}
}
