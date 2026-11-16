using System.Collections;

public class DataDumbScenarioBusiness : DataDumbScenario
{
	private ArrayList IndustryWhitelist__BackingField;

	private ArrayList IndustryBlacklist__BackingField;

	private bool HeroHasLover__BackingField;

	private int LoverGender__BackingField;

	private bool RequiresSingleFacility__BackingField;

	private bool RequiresMultipleFacilities__BackingField;

	private int BusinessSize__BackingField;

	public ArrayList IndustryWhitelist
	{
		get
		{
			return IndustryWhitelist__BackingField;
		}
		set
		{
			IndustryWhitelist__BackingField = value;
		}
	}

	public ArrayList IndustryBlacklist
	{
		get
		{
			return IndustryBlacklist__BackingField;
		}
		set
		{
			IndustryBlacklist__BackingField = value;
		}
	}

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

	public bool RequiresSingleFacility
	{
		get
		{
			return RequiresSingleFacility__BackingField;
		}
		set
		{
			RequiresSingleFacility__BackingField = value;
		}
	}

	public bool RequiresMultipleFacilities
	{
		get
		{
			return RequiresMultipleFacilities__BackingField;
		}
		set
		{
			RequiresMultipleFacilities__BackingField = value;
		}
	}

	public int BusinessSize
	{
		get
		{
			return BusinessSize__BackingField;
		}
		set
		{
			BusinessSize__BackingField = value;
		}
	}
}
