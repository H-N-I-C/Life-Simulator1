using System;
using Game;

[Serializable]
public class SimLuxuryIsland
{
	private string Name__BackingField;

	private string CountryCode__BackingField;

	private double PurchasePrice__BackingField;

	private string Size__BackingField;

	private int YearsOwned__BackingField;

	private string Type__BackingField;

	private SimLuxuryIslandStructure Structure__BackingField;

	private float _condition;

	private int AgeAtLastSellAttempt__BackingField;

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

	public string CountryCode
	{
		get
		{
			return CountryCode__BackingField;
		}
		set
		{
			CountryCode__BackingField = value;
		}
	}

	public double PurchasePrice
	{
		get
		{
			return PurchasePrice__BackingField;
		}
		set
		{
			PurchasePrice__BackingField = value;
		}
	}

	public string Size
	{
		get
		{
			return Size__BackingField;
		}
		set
		{
			Size__BackingField = value;
		}
	}

	public int YearsOwned
	{
		get
		{
			return YearsOwned__BackingField;
		}
		set
		{
			YearsOwned__BackingField = value;
		}
	}

	public string Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public SimLuxuryIslandStructure Structure
	{
		get
		{
			return Structure__BackingField;
		}
		set
		{
			Structure__BackingField = value;
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

	public SimLuxuryIsland(DataLuxuryIslandData data, string code, string size)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetCondition()
	{
		return 0f;
	}

	public void ImpactCondition(int impactScore)
	{
	}

	public double GetMonthlyExpenses()
	{
		return 0.0;
	}

	public double PropertyValue()
	{
		return 0.0;
	}

	public string GetCountryName()
	{
		return null;
	}

	public string GetCountryDemonym()
	{
		return null;
	}
}
