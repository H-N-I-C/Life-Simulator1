using System;

[Serializable]
public class SimOccupationMilitary : SimOccupation
{
	private int YearsCompleted__BackingField;

	private int Promotions__BackingField;

	private int MilitaryBranch__BackingField;

	private int MilitaryStatus__BackingField;

	private int MilitaryDischargeType__BackingField;

	private int Rank__BackingField;

	private string RankString__BackingField;

	private float LastRaisePercentage__BackingField;

	private int RehabsAttended__BackingField;

	private int NumDeployments__BackingField;

	private int AgeAtLastDeployment__BackingField;

	private int AgeAtLastSuccessfulDeployment__BackingField;

	private int AgeAtLastRefusedDeployment__BackingField;

	private int AgeAtLastPushRetirementScenario__BackingField;

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

	public int Promotions
	{
		get
		{
			return Promotions__BackingField;
		}
		set
		{
			Promotions__BackingField = value;
		}
	}

	public int MilitaryBranch
	{
		get
		{
			return MilitaryBranch__BackingField;
		}
		set
		{
			MilitaryBranch__BackingField = value;
		}
	}

	public int MilitaryStatus
	{
		get
		{
			return MilitaryStatus__BackingField;
		}
		set
		{
			MilitaryStatus__BackingField = value;
		}
	}

	public int MilitaryDischargeType
	{
		get
		{
			return MilitaryDischargeType__BackingField;
		}
		set
		{
			MilitaryDischargeType__BackingField = value;
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

	public string RankString
	{
		get
		{
			return RankString__BackingField;
		}
		set
		{
			RankString__BackingField = value;
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

	public int RehabsAttended
	{
		get
		{
			return RehabsAttended__BackingField;
		}
		set
		{
			RehabsAttended__BackingField = value;
		}
	}

	public int NumDeployments
	{
		get
		{
			return NumDeployments__BackingField;
		}
		set
		{
			NumDeployments__BackingField = value;
		}
	}

	public int AgeAtLastDeployment
	{
		get
		{
			return AgeAtLastDeployment__BackingField;
		}
		set
		{
			AgeAtLastDeployment__BackingField = value;
		}
	}

	public int AgeAtLastSuccessfulDeployment
	{
		get
		{
			return AgeAtLastSuccessfulDeployment__BackingField;
		}
		set
		{
			AgeAtLastSuccessfulDeployment__BackingField = value;
		}
	}

	public int AgeAtLastRefusedDeployment
	{
		get
		{
			return AgeAtLastRefusedDeployment__BackingField;
		}
		set
		{
			AgeAtLastRefusedDeployment__BackingField = value;
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

	private SimOccupationMilitary()
	{
	}

	public SimOccupationMilitary(bool initHere)
	{
	}

	public bool CheckForPromotion()
	{
		return false;
	}

	public void AdjustAndSetSalaryForCountry(float theSalary, DataCountry theCountry)
	{
	}

	public static int MinimumYearsForRetirement()
	{
		return 0;
	}

	public static float GetMilitaryPensionAmountForSalaryAndYearsOfService(float theSalary, int yearsOfService)
	{
		return 0f;
	}

	public void ImpactPerformance(int impactScore)
	{
	}

	public void SetRank(int rank)
	{
	}

	private void PromoteToRank(int rank)
	{
	}
}
