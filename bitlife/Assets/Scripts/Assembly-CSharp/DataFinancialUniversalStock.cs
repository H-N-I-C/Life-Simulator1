public class DataFinancialUniversalStock
{
	private string Symbol__BackingField;

	private string Name__BackingField;

	private int Sector__BackingField;

	private int MinPrice__BackingField;

	private int MaxPrice__BackingField;

	private int WinnerScore__BackingField;

	private int HeroOwnershipCap__BackingField;

	public string Symbol
	{
		get
		{
			return Symbol__BackingField;
		}
		set
		{
			Symbol__BackingField = value;
		}
	}

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

	public int Sector
	{
		get
		{
			return Sector__BackingField;
		}
		set
		{
			Sector__BackingField = value;
		}
	}

	public int MinPrice
	{
		get
		{
			return MinPrice__BackingField;
		}
		set
		{
			MinPrice__BackingField = value;
		}
	}

	public int MaxPrice
	{
		get
		{
			return MaxPrice__BackingField;
		}
		set
		{
			MaxPrice__BackingField = value;
		}
	}

	public int WinnerScore
	{
		get
		{
			return WinnerScore__BackingField;
		}
		set
		{
			WinnerScore__BackingField = value;
		}
	}

	public int HeroOwnershipCap
	{
		get
		{
			return HeroOwnershipCap__BackingField;
		}
		set
		{
			HeroOwnershipCap__BackingField = value;
		}
	}
}
