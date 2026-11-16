using System.Collections.Generic;

public class DataCarModel
{
	private string Make__BackingField;

	private string Model__BackingField;

	private int CarType__BackingField;

	private float Cost__BackingField;

	private int DepreciationScore__BackingField;

	private int ReliabilityScore__BackingField;

	private int LongevityScore__BackingField;

	private int CoolScore__BackingField;

	private int EaseOfTheftScore__BackingField;

	private bool Unavailable__BackingField;

	private bool Luxury__BackingField;

	private bool RaceCarDealershipOnly__BackingField;

	private List<RACING_TRACK_CATEGORY> RaceTypeWhitelist__BackingField;

	private string RacingClass__BackingField;

	private int DurabilityScore__BackingField;

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

	public int DepreciationScore
	{
		get
		{
			return DepreciationScore__BackingField;
		}
		set
		{
			DepreciationScore__BackingField = value;
		}
	}

	public int ReliabilityScore
	{
		get
		{
			return ReliabilityScore__BackingField;
		}
		set
		{
			ReliabilityScore__BackingField = value;
		}
	}

	public int LongevityScore
	{
		get
		{
			return LongevityScore__BackingField;
		}
		set
		{
			LongevityScore__BackingField = value;
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

	public bool Unavailable
	{
		get
		{
			return Unavailable__BackingField;
		}
		set
		{
			Unavailable__BackingField = value;
		}
	}

	public bool Luxury
	{
		get
		{
			return Luxury__BackingField;
		}
		set
		{
			Luxury__BackingField = value;
		}
	}

	public bool RaceCarDealershipOnly
	{
		get
		{
			return RaceCarDealershipOnly__BackingField;
		}
		set
		{
			RaceCarDealershipOnly__BackingField = value;
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

	public int LifespanYears()
	{
		return 0;
	}

	public float AccidentPronenessScore()
	{
		return 0f;
	}
}
