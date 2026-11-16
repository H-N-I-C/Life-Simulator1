using System;
using Game;

[Serializable]
public class SimCasinoAmenity
{
	private string AmenityName__BackingField;

	private string SecondaryTextLine__BackingField;

	private string Category__BackingField;

	private string Theme__BackingField;

	private float Cost__BackingField;

	private float MonthlyExpenses__BackingField;

	private int GamblerHappinessImpact__BackingField;

	private int GamblerAddictionImpact__BackingField;

	private int BasePopularityImpact__BackingField;

	private int MatchPopularityImpact__BackingField;

	private int YearsSinceBuilt__BackingField;

	public string AmenityName
	{
		get
		{
			return AmenityName__BackingField;
		}
		set
		{
			AmenityName__BackingField = value;
		}
	}

	public string SecondaryTextLine
	{
		get
		{
			return SecondaryTextLine__BackingField;
		}
		set
		{
			SecondaryTextLine__BackingField = value;
		}
	}

	public string Category
	{
		get
		{
			return Category__BackingField;
		}
		set
		{
			Category__BackingField = value;
		}
	}

	public string Theme
	{
		get
		{
			return Theme__BackingField;
		}
		set
		{
			Theme__BackingField = value;
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

	public float MonthlyExpenses
	{
		get
		{
			return MonthlyExpenses__BackingField;
		}
		set
		{
			MonthlyExpenses__BackingField = value;
		}
	}

	public int GamblerHappinessImpact
	{
		get
		{
			return GamblerHappinessImpact__BackingField;
		}
		set
		{
			GamblerHappinessImpact__BackingField = value;
		}
	}

	public int GamblerAddictionImpact
	{
		get
		{
			return GamblerAddictionImpact__BackingField;
		}
		set
		{
			GamblerAddictionImpact__BackingField = value;
		}
	}

	public int BasePopularityImpact
	{
		get
		{
			return BasePopularityImpact__BackingField;
		}
		set
		{
			BasePopularityImpact__BackingField = value;
		}
	}

	public int MatchPopularityImpact
	{
		get
		{
			return MatchPopularityImpact__BackingField;
		}
		set
		{
			MatchPopularityImpact__BackingField = value;
		}
	}

	public int YearsSinceBuilt
	{
		get
		{
			return YearsSinceBuilt__BackingField;
		}
		set
		{
			YearsSinceBuilt__BackingField = value;
		}
	}

	public SimCasinoAmenity(DataCasinoAmenity data)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetAnnualCosts()
	{
		return 0f;
	}

	public float GetAnnualRevenue(SimCasino casino)
	{
		return 0f;
	}
}
