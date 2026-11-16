using System;

[Serializable]
public class SimOutdoorMountedFish
{
	private string Name__BackingField;

	private string Species__BackingField;

	private float Weight__BackingField;

	private int Age__BackingField;

	private int StartingCondition__BackingField;

	private float Condition__BackingField;

	private float StartingValue__BackingField;

	private float CurrentValue__BackingField;

	private int HeroAgeAcquiredItem__BackingField;

	private int HeroAgeAtLastSellAttempt__BackingField;

	private int HeroAgeAtLastPlayedWith__BackingField;

	private int AgeAtLastRefurbish__BackingField;

	private bool EverBeenRefurbished__BackingField;

	private string EmojiString__BackingField;

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

	public string Species
	{
		get
		{
			return Species__BackingField;
		}
		set
		{
			Species__BackingField = value;
		}
	}

	public float Weight
	{
		get
		{
			return Weight__BackingField;
		}
		set
		{
			Weight__BackingField = value;
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

	public int HeroAgeAtLastPlayedWith
	{
		get
		{
			return HeroAgeAtLastPlayedWith__BackingField;
		}
		set
		{
			HeroAgeAtLastPlayedWith__BackingField = value;
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

	public string EmojiString
	{
		get
		{
			return EmojiString__BackingField;
		}
		set
		{
			EmojiString__BackingField = value;
		}
	}

	public SimOutdoorMountedFish(SimOutdoorCatchableFish fish)
	{
	}

	public string GetDescription()
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public void ImpactConditionByAmount(int amount)
	{
	}

	public void UpdateCurrentValueForNonAppreciatingItem()
	{
	}

	public void Refurbish()
	{
	}
}
