using System.Collections;

public class DataLandlordHomeDefaultFeature
{
	private string FeatureString__BackingField;

	private ArrayList HouseTypeWhitelist__BackingField;

	private ArrayList HouseTypeBlacklist__BackingField;

	private int EnjoymentScore__BackingField;

	private bool RequiresHauntedHouse__BackingField;

	private int MinPropertyCondition__BackingField;

	private int MaxPropertyCondition__BackingField;

	private FeatureType FeatureType__BackingField;

	public string FeatureString
	{
		get
		{
			return FeatureString__BackingField;
		}
		set
		{
			FeatureString__BackingField = value;
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

	public int EnjoymentScore
	{
		get
		{
			return EnjoymentScore__BackingField;
		}
		set
		{
			EnjoymentScore__BackingField = value;
		}
	}

	public bool RequiresHauntedHouse
	{
		get
		{
			return RequiresHauntedHouse__BackingField;
		}
		set
		{
			RequiresHauntedHouse__BackingField = value;
		}
	}

	public int MinPropertyCondition
	{
		get
		{
			return MinPropertyCondition__BackingField;
		}
		set
		{
			MinPropertyCondition__BackingField = value;
		}
	}

	public int MaxPropertyCondition
	{
		get
		{
			return MaxPropertyCondition__BackingField;
		}
		set
		{
			MaxPropertyCondition__BackingField = value;
		}
	}

	public FeatureType FeatureType
	{
		get
		{
			return FeatureType__BackingField;
		}
		set
		{
			FeatureType__BackingField = value;
		}
	}
}
