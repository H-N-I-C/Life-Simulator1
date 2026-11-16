using System.Collections;

public class DataDumbScenarioLandlord : DataDumbScenario
{
	private bool HeroHasLover__BackingField;

	private int LoverGender__BackingField;

	private bool HeroTenantSameCity__BackingField;

	private ArrayList HouseTypeWhitelist__BackingField;

	private ArrayList HouseTypeBlacklist__BackingField;

	private int MinSatisfaction__BackingField;

	private int MaxSatisfaction__BackingField;

	private int MinCraziness__BackingField;

	private int MaxCraziness__BackingField;

	public bool HeroHasLover
	{
		get
		{
			return HeroHasLover__BackingField;
		}
		set
		{
			HeroHasLover__BackingField = value;
		}
	}

	public int LoverGender
	{
		get
		{
			return LoverGender__BackingField;
		}
		set
		{
			LoverGender__BackingField = value;
		}
	}

	public bool HeroTenantSameCity
	{
		get
		{
			return HeroTenantSameCity__BackingField;
		}
		set
		{
			HeroTenantSameCity__BackingField = value;
		}
	}

	public ArrayList HouseTypeWhitelist
	{
		get
		{
			return HouseTypeWhitelist__BackingField;
		}
		set
		{
			HouseTypeWhitelist__BackingField = value;
		}
	}

	public ArrayList HouseTypeBlacklist
	{
		get
		{
			return HouseTypeBlacklist__BackingField;
		}
		set
		{
			HouseTypeBlacklist__BackingField = value;
		}
	}

	public int MinSatisfaction
	{
		get
		{
			return MinSatisfaction__BackingField;
		}
		set
		{
			MinSatisfaction__BackingField = value;
		}
	}

	public int MaxSatisfaction
	{
		get
		{
			return MaxSatisfaction__BackingField;
		}
		set
		{
			MaxSatisfaction__BackingField = value;
		}
	}

	public int MinCraziness
	{
		get
		{
			return MinCraziness__BackingField;
		}
		set
		{
			MinCraziness__BackingField = value;
		}
	}

	public int MaxCraziness
	{
		get
		{
			return MaxCraziness__BackingField;
		}
		set
		{
			MaxCraziness__BackingField = value;
		}
	}
}
