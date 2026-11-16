using System;

[Serializable]
public class SimZooHabitatFeature
{
	private string Name__BackingField;

	private int Age__BackingField;

	private string HabitatType__BackingField;

	private int HappinessScore__BackingField;

	private int AgeLastFeatureRepaired__BackingField;

	private float _condition;

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

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public string HabitatType
	{
		get
		{
			return HabitatType__BackingField;
		}
		set
		{
			HabitatType__BackingField = value;
		}
	}

	public int HappinessScore
	{
		get
		{
			return HappinessScore__BackingField;
		}
		set
		{
			HappinessScore__BackingField = value;
		}
	}

	public int AgeLastFeatureRepaired
	{
		get
		{
			return AgeLastFeatureRepaired__BackingField;
		}
		set
		{
			AgeLastFeatureRepaired__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public float GetCondition()
	{
		return 0f;
	}

	public void ImpactCondition(int impact)
	{
	}

	public void ImpactConditionByAmount(int amount)
	{
	}

	public float GetRepairCostValue()
	{
		return 0f;
	}

	public float GetRemovalCostValue()
	{
		return 0f;
	}
}
