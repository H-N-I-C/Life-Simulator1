using System;

[Serializable]
public class SimOccupationEmployee : SimOccupation
{
	private int YearsCompleted__BackingField;

	private int AgeAtLastPromotion__BackingField;

	private int AgeAtLastRaise__BackingField;

	private float LastRaisePercentage__BackingField;

	private int RequestsForRaiseOrPromotion__BackingField;

	private int RequestsToAttendRehab__BackingField;

	private bool TeamBuildingOffered__BackingField;

	private bool SupervisorAskedToSocialize__BackingField;

	private bool InheritedFromThirdPersonScript__BackingField;

	private SimJob Job__BackingField;

	private int TerminationType__BackingField;

	private int AgeAtLastPushRetirementScenario__BackingField;

	private int EditedWeeklyHours__BackingField;

	private float Att_performance__BackingField;

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

	public int AgeAtLastPromotion
	{
		get
		{
			return AgeAtLastPromotion__BackingField;
		}
		set
		{
			AgeAtLastPromotion__BackingField = value;
		}
	}

	public int AgeAtLastRaise
	{
		get
		{
			return AgeAtLastRaise__BackingField;
		}
		set
		{
			AgeAtLastRaise__BackingField = value;
		}
	}

	public float LastRaisePercentage
	{
		get
		{
			return LastRaisePercentage__BackingField;
		}
		set
		{
			LastRaisePercentage__BackingField = value;
		}
	}

	public int RequestsForRaiseOrPromotion
	{
		get
		{
			return RequestsForRaiseOrPromotion__BackingField;
		}
		set
		{
			RequestsForRaiseOrPromotion__BackingField = value;
		}
	}

	public int RequestsToAttendRehab
	{
		get
		{
			return RequestsToAttendRehab__BackingField;
		}
		set
		{
			RequestsToAttendRehab__BackingField = value;
		}
	}

	public bool TeamBuildingOffered
	{
		get
		{
			return TeamBuildingOffered__BackingField;
		}
		set
		{
			TeamBuildingOffered__BackingField = value;
		}
	}

	public bool SupervisorAskedToSocialize
	{
		get
		{
			return SupervisorAskedToSocialize__BackingField;
		}
		set
		{
			SupervisorAskedToSocialize__BackingField = value;
		}
	}

	public bool InheritedFromThirdPersonScript
	{
		get
		{
			return InheritedFromThirdPersonScript__BackingField;
		}
		set
		{
			InheritedFromThirdPersonScript__BackingField = value;
		}
	}

	public SimJob Job
	{
		get
		{
			return Job__BackingField;
		}
		set
		{
			Job__BackingField = value;
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

	public int AgeAtLastPushRetirementScenario
	{
		get
		{
			return AgeAtLastPushRetirementScenario__BackingField;
		}
		set
		{
			AgeAtLastPushRetirementScenario__BackingField = value;
		}
	}

	public int EditedWeeklyHours
	{
		get
		{
			return EditedWeeklyHours__BackingField;
		}
		set
		{
			EditedWeeklyHours__BackingField = value;
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

	private SimOccupationEmployee()
	{
	}

	public SimOccupationEmployee(bool initHere)
	{
	}

	public string JobTitle()
	{
		return null;
	}

	public int CheckForPromotionOrRaise()
	{
		return 0;
	}

	public int GetWeeklyHours()
	{
		return 0;
	}

	public void ImpactPerformance(int impactScore)
	{
	}

	public void PromoteSupervisor()
	{
	}
}
