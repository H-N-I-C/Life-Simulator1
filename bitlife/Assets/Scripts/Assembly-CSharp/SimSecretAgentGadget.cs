using System;
using System.Collections.Generic;

[Serializable]
public class SimSecretAgentGadget
{
	private string Name__BackingField;

	private SECRET_AGENT_SPECIALIZATION Specialization__BackingField;

	private string Description__BackingField;

	private float Cost__BackingField;

	private bool IsPlural__BackingField;

	private int Level__BackingField;

	private int Age__BackingField;

	private List<string> Uses__BackingField;

	private float Condition__BackingField;

	private int StartingCondition__BackingField;

	private int HeroAgeAcquiredItem__BackingField;

	private int AgeAtLastRefurbish__BackingField;

	private int HeroAgeAtLastSellAttempt__BackingField;

	private bool EverBeenRefurbished__BackingField;

	private float StartingValue__BackingField;

	private float CurrentValue__BackingField;

	private IAP_TYPE IAPType__BackingField;

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

	public SECRET_AGENT_SPECIALIZATION Specialization
	{
		get
		{
			return Specialization__BackingField;
		}
		set
		{
			Specialization__BackingField = value;
		}
	}

	public string Description
	{
		get
		{
			return Description__BackingField;
		}
		set
		{
			Description__BackingField = value;
		}
	}

	public float Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public bool IsPlural
	{
		get
		{
			return IsPlural__BackingField;
		}
		set
		{
			IsPlural__BackingField = value;
		}
	}

	public int Level
	{
		get
		{
			return Level__BackingField;
		}
		set
		{
			Level__BackingField = value;
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

	public List<string> Uses
	{
		get
		{
			return Uses__BackingField;
		}
		set
		{
			Uses__BackingField = value;
		}
	}

	public float Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public int StartingCondition
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

	public int HeroAgeAcquiredItem
	{
		get
		{
			return HeroAgeAcquiredItem__BackingField;
		}
		set
		{
			HeroAgeAcquiredItem__BackingField = value;
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

	public int HeroAgeAtLastSellAttempt
	{
		get
		{
			return HeroAgeAtLastSellAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastSellAttempt__BackingField = value;
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

	public IAP_TYPE IAPType
	{
		get
		{
			return IAPType__BackingField;
		}
		set
		{
			IAPType__BackingField = value;
		}
	}

	public SimSecretAgentGadget()
	{
	}

	public SimSecretAgentGadget(IAP_TYPE type)
	{
	}

	public void IncrementAge()
	{
	}

	public string GetUsesString()
	{
		return null;
	}

	public double RefurbishCost()
	{
		return 0.0;
	}

	public void RefurbishItem()
	{
	}

	public void ImpactConditionByAmount(int amount)
	{
	}
}
