using System;

[Serializable]
public class SimJewel : IComparable
{
	private int UniqueID__BackingField;

	private int Age__BackingField;

	private float Condition__BackingField;

	private int _DataType;

	private string _MetalString;

	private string _GemName;

	private float Carats__BackingField;

	private int AgeAtLastCleaning__BackingField;

	private bool IsFake__BackingField;

	private bool HeroKnowsAuthenticity__BackingField;

	private float Att_quality__BackingField;

	private bool IsSportsChampionshipRing__BackingField;

	private int ChampionshipSport__BackingField;

	private string SportsTeamName__BackingField;

	private int SportsPrimaryColor__BackingField;

	private int SportsSecondaryColor__BackingField;

	private string ExoticBrand__BackingField;

	private string ExoticModel__BackingField;

	private float ExoticCost__BackingField;

	public int UniqueID
	{
		get
		{
			return UniqueID__BackingField;
		}
		set
		{
			UniqueID__BackingField = value;
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

	public DataJewelryType Type
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public DataJewelryMetal Metal
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public DataJewelryGemstone Gemstone
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float Carats
	{
		get
		{
			return Carats__BackingField;
		}
		set
		{
			Carats__BackingField = value;
		}
	}

	public int AgeAtLastCleaning
	{
		get
		{
			return AgeAtLastCleaning__BackingField;
		}
		set
		{
			AgeAtLastCleaning__BackingField = value;
		}
	}

	public bool IsFake
	{
		get
		{
			return IsFake__BackingField;
		}
		set
		{
			IsFake__BackingField = value;
		}
	}

	public bool HeroKnowsAuthenticity
	{
		get
		{
			return HeroKnowsAuthenticity__BackingField;
		}
		set
		{
			HeroKnowsAuthenticity__BackingField = value;
		}
	}

	public float Att_quality
	{
		get
		{
			return Att_quality__BackingField;
		}
		set
		{
			Att_quality__BackingField = value;
		}
	}

	public bool IsSportsChampionshipRing
	{
		get
		{
			return IsSportsChampionshipRing__BackingField;
		}
		set
		{
			IsSportsChampionshipRing__BackingField = value;
		}
	}

	public int ChampionshipSport
	{
		get
		{
			return ChampionshipSport__BackingField;
		}
		set
		{
			ChampionshipSport__BackingField = value;
		}
	}

	public string SportsTeamName
	{
		get
		{
			return SportsTeamName__BackingField;
		}
		set
		{
			SportsTeamName__BackingField = value;
		}
	}

	public int SportsPrimaryColor
	{
		get
		{
			return SportsPrimaryColor__BackingField;
		}
		set
		{
			SportsPrimaryColor__BackingField = value;
		}
	}

	public int SportsSecondaryColor
	{
		get
		{
			return SportsSecondaryColor__BackingField;
		}
		set
		{
			SportsSecondaryColor__BackingField = value;
		}
	}

	public string ExoticBrand
	{
		get
		{
			return ExoticBrand__BackingField;
		}
		set
		{
			ExoticBrand__BackingField = value;
		}
	}

	public string ExoticModel
	{
		get
		{
			return ExoticModel__BackingField;
		}
		set
		{
			ExoticModel__BackingField = value;
		}
	}

	public float ExoticCost
	{
		get
		{
			return ExoticCost__BackingField;
		}
		set
		{
			ExoticCost__BackingField = value;
		}
	}

	private SimJewel()
	{
	}

	public SimJewel(float minValue, float maxValue, bool isWatch)
	{
	}

	public SimJewel(JEWELRY_TYPE jewelryType, DataJewelryGemstone gemstone, DataJewelryMetal metal, int carats, bool isFake)
	{
	}

	public SimJewel(float minValue, float maxValue, bool isRing, bool isDiamond, bool willBeFake, bool heroKnowsAuthenticity)
	{
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}

	public void SetAsChampionshipRingForTeam(SimSportsTeam team)
	{
	}

	public string GenericDescriptionStringAndCapitalize(bool capitalize)
	{
		return null;
	}

	public virtual string ShortDescription()
	{
		return null;
	}

	public string CaratString()
	{
		return null;
	}

	public string DropDownMenuString()
	{
		return null;
	}

	public virtual string EmojiString()
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public string SignificanceString()
	{
		return null;
	}

	public string GetEngagementSignificanceString()
	{
		return null;
	}

	public string GetGiftSignificanceString()
	{
		return null;
	}

	public string GetSportsChampionshipSignificanceString()
	{
		return null;
	}

	public float CurrentValue()
	{
		return 0f;
	}

	public float EstimatedValue()
	{
		return 0f;
	}

	public float CurrentValueAsReal()
	{
		return 0f;
	}

	public float EstimatedValueAsReal()
	{
		return 0f;
	}

	public float CurrentValueAsFake()
	{
		return 0f;
	}

	private float GetEstimatedAmountFromAmount(float amount)
	{
		return 0f;
	}

	public float DealerPrice()
	{
		return 0f;
	}

	public static string CaratFormattedStringForCarats(float theCarats)
	{
		return null;
	}
}
