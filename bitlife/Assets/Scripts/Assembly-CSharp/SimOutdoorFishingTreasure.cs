using System;
using Game;

[Serializable]
public class SimOutdoorFishingTreasure
{
	private string ItemName__BackingField;

	private string ShortName__BackingField;

	private int Age__BackingField;

	private int StartingCondition__BackingField;

	private float Condition__BackingField;

	private bool Appreciates__BackingField;

	private float StartingValue__BackingField;

	private float CurrentValue__BackingField;

	private int HeroAgeAcquiredItem__BackingField;

	private int AgeAtLastRefurbish__BackingField;

	private bool EverBeenRefurbished__BackingField;

	private int HeroAgeAtLastSellAttempt__BackingField;

	private int HeroAgeAtLastPlayedWith__BackingField;

	public string ItemName
	{
		get
		{
			return ItemName__BackingField;
		}
		set
		{
			ItemName__BackingField = value;
		}
	}

	public string ShortName
	{
		get
		{
			return ShortName__BackingField;
		}
		set
		{
			ShortName__BackingField = value;
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

	public bool Appreciates
	{
		get
		{
			return Appreciates__BackingField;
		}
		set
		{
			Appreciates__BackingField = value;
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

	public DataOutdoorFishingTreasure Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimOutdoorFishingTreasure(DataOutdoorFishingTreasure fishingTreasureData)
	{
	}

	public string DescriptiveName()
	{
		return null;
	}

	public bool IncrementAge()
	{
		return false;
	}

	public void ImpactConditionByAmount(int amount)
	{
	}

	public void UpdateCurrentValueForNonAppreciatingItem()
	{
	}

	public string EmojiString()
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
}
