using System.Collections.Generic;

public class DataBlackMarketAutomobiles : DataBlackMarketBase
{
	private string Make__BackingField;

	private string Model__BackingField;

	private int CarType__BackingField;

	private bool Appreciates__BackingField;

	private int CoolScore__BackingField;

	private int EaseOfTheftScore__BackingField;

	private bool StorageUnitOnly__BackingField;

	private List<RACING_TRACK_CATEGORY> RaceTypeWhitelist__BackingField;

	private string RacingClass__BackingField;

	private int DurabilityScore__BackingField;

	private string Color__BackingField;

	public string Make
	{
		get
		{
			return Make__BackingField;
		}
		set
		{
			Make__BackingField = value;
		}
	}

	public string Model
	{
		get
		{
			return Model__BackingField;
		}
		set
		{
			Model__BackingField = value;
		}
	}

	public int CarType
	{
		get
		{
			return CarType__BackingField;
		}
		set
		{
			CarType__BackingField = value;
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

	public int CoolScore
	{
		get
		{
			return CoolScore__BackingField;
		}
		set
		{
			CoolScore__BackingField = value;
		}
	}

	public int EaseOfTheftScore
	{
		get
		{
			return EaseOfTheftScore__BackingField;
		}
		set
		{
			EaseOfTheftScore__BackingField = value;
		}
	}

	public bool StorageUnitOnly
	{
		get
		{
			return StorageUnitOnly__BackingField;
		}
		set
		{
			StorageUnitOnly__BackingField = value;
		}
	}

	public List<RACING_TRACK_CATEGORY> RaceTypeWhitelist
	{
		get
		{
			return RaceTypeWhitelist__BackingField;
		}
		set
		{
			RaceTypeWhitelist__BackingField = value;
		}
	}

	public string RacingClass
	{
		get
		{
			return RacingClass__BackingField;
		}
		set
		{
			RacingClass__BackingField = value;
		}
	}

	public int DurabilityScore
	{
		get
		{
			return DurabilityScore__BackingField;
		}
		set
		{
			DurabilityScore__BackingField = value;
		}
	}

	public string Color
	{
		get
		{
			return Color__BackingField;
		}
		set
		{
			Color__BackingField = value;
		}
	}
}
