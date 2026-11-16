using System;

[Serializable]
public class SimHeirloom : IComparable
{
	private int Age__BackingField;

	private float StartingCondition__BackingField;

	private float CurrentCondition__BackingField;

	private float StartingValue__BackingField;

	private float CurrentValue__BackingField;

	private int AgeAtLastRefurbish__BackingField;

	private bool EverBeenRefurbished__BackingField;

	private int AgeAtLastSellAttempt__BackingField;

	public int HeirloomIndex;

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

	public float StartingCondition
	{
		get
		{
			return StartingCondition__BackingField;
		}
		set
		{
			StartingCondition__BackingField = value;
		}
	}

	public float CurrentCondition
	{
		get
		{
			return CurrentCondition__BackingField;
		}
		set
		{
			CurrentCondition__BackingField = value;
		}
	}

	public float StartingValue
	{
		get
		{
			return StartingValue__BackingField;
		}
		set
		{
			StartingValue__BackingField = value;
		}
	}

	public float CurrentValue
	{
		get
		{
			return CurrentValue__BackingField;
		}
		set
		{
			CurrentValue__BackingField = value;
		}
	}

	public int AgeAtLastRefurbish
	{
		get
		{
			return AgeAtLastRefurbish__BackingField;
		}
		set
		{
			AgeAtLastRefurbish__BackingField = value;
		}
	}

	public bool EverBeenRefurbished
	{
		get
		{
			return EverBeenRefurbished__BackingField;
		}
		set
		{
			EverBeenRefurbished__BackingField = value;
		}
	}

	public int AgeAtLastSellAttempt
	{
		get
		{
			return AgeAtLastSellAttempt__BackingField;
		}
		set
		{
			AgeAtLastSellAttempt__BackingField = value;
		}
	}

	public DataHeirloom Heirloom
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private SimHeirloom()
	{
	}

	public SimHeirloom(DataHeirloom theHeirloom, bool forcePerfectCondition = false)
	{
	}

	public string Description()
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public void RefurbishHeirloom()
	{
	}

	public void ImpactConditionByAmount(int amount)
	{
	}

	public void UpdateCurrentValueForNonAppreciatingHeirloom()
	{
	}

	public string SortName()
	{
		return null;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
