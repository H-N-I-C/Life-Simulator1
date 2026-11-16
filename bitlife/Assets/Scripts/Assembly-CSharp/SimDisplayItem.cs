using System;

[Serializable]
public class SimDisplayItem
{
	private string ListedName__BackingField;

	private bool ListedAuthenticity__BackingField;

	private object DisplayItem__BackingField;

	private int ItemType__BackingField;

	private int YearsOnDisplay__BackingField;

	private int HeroAgeAtStartDisplay__BackingField;

	private int HeroAgeLastPlayWith__BackingField;

	private int HeroAgeAtPoliceConfiscated__BackingField;

	public string ListedName
	{
		get
		{
			return ListedName__BackingField;
		}
		set
		{
			ListedName__BackingField = value;
		}
	}

	public bool ListedAuthenticity
	{
		get
		{
			return ListedAuthenticity__BackingField;
		}
		set
		{
			ListedAuthenticity__BackingField = value;
		}
	}

	public object DisplayItem
	{
		get
		{
			return DisplayItem__BackingField;
		}
		set
		{
			DisplayItem__BackingField = value;
		}
	}

	public int ItemType
	{
		get
		{
			return ItemType__BackingField;
		}
		set
		{
			ItemType__BackingField = value;
		}
	}

	public int YearsOnDisplay
	{
		get
		{
			return YearsOnDisplay__BackingField;
		}
		set
		{
			YearsOnDisplay__BackingField = value;
		}
	}

	public int HeroAgeAtStartDisplay
	{
		get
		{
			return HeroAgeAtStartDisplay__BackingField;
		}
		set
		{
			HeroAgeAtStartDisplay__BackingField = value;
		}
	}

	public int HeroAgeLastPlayWith
	{
		get
		{
			return HeroAgeLastPlayWith__BackingField;
		}
		set
		{
			HeroAgeLastPlayWith__BackingField = value;
		}
	}

	public int HeroAgeAtPoliceConfiscated
	{
		get
		{
			return HeroAgeAtPoliceConfiscated__BackingField;
		}
		set
		{
			HeroAgeAtPoliceConfiscated__BackingField = value;
		}
	}

	public int GetDisplayItemGender()
	{
		return 0;
	}

	public int GetYearsOnDisplay()
	{
		return 0;
	}

	public void IncrementAge()
	{
	}

	public double ItemValue(bool withAppreciation = false)
	{
		return 0.0;
	}

	public int GetAppealScore()
	{
		return 0;
	}

	public bool IsNewAuctionItem()
	{
		return false;
	}

	public string GetDisplayItemEmojiString()
	{
		return null;
	}

	public void ImpactConditionByAmount(int amount)
	{
	}

	public float GetDisplayItemCondition()
	{
		return 0f;
	}

	public string GetItemDisplayName(bool includeTaxidermyPrefix = false)
	{
		return null;
	}

	public string GetItemTypeString()
	{
		return null;
	}

	public void ReturnDisplayItemToHero()
	{
	}

	public double PolishCost()
	{
		return 0.0;
	}

	private double GetDisplayItemValue()
	{
		return 0.0;
	}

	public string GetDisplayItemPossessiveName()
	{
		return null;
	}
}
