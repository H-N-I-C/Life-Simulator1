using System;

[Serializable]
public class SimBond
{
	private double Coupon__BackingField;

	private double MinimumInvestment__BackingField;

	private double Maturity__BackingField;

	private string Entity__BackingField;

	private double Risk__BackingField;

	private int Level__BackingField;

	private string BondID__BackingField;

	private string CountryCode__BackingField;

	private SimStock Stock__BackingField;

	private string Ticker__BackingField;

	private string Location__BackingField;

	private string LevelName__BackingField;

	private bool WillDefaultThisYear__BackingField;

	public double Coupon
	{
		get
		{
			return Coupon__BackingField;
		}
		set
		{
			Coupon__BackingField = value;
		}
	}

	public double MinimumInvestment
	{
		get
		{
			return MinimumInvestment__BackingField;
		}
		set
		{
			MinimumInvestment__BackingField = value;
		}
	}

	public double Maturity
	{
		get
		{
			return Maturity__BackingField;
		}
		set
		{
			Maturity__BackingField = value;
		}
	}

	public string Entity
	{
		get
		{
			return Entity__BackingField;
		}
		set
		{
			Entity__BackingField = value;
		}
	}

	public double Risk
	{
		get
		{
			return Risk__BackingField;
		}
		set
		{
			Risk__BackingField = value;
		}
	}

	public int Level
	{
		get
		{
			return Level__BackingField;
		}
		set
		{
			Level__BackingField = value;
		}
	}

	public string BondID
	{
		get
		{
			return BondID__BackingField;
		}
		set
		{
			BondID__BackingField = value;
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

	public SimStock Stock
	{
		get
		{
			return Stock__BackingField;
		}
		set
		{
			Stock__BackingField = value;
		}
	}

	public string Ticker
	{
		get
		{
			return Ticker__BackingField;
		}
		set
		{
			Ticker__BackingField = value;
		}
	}

	public string Location
	{
		get
		{
			return Location__BackingField;
		}
		set
		{
			Location__BackingField = value;
		}
	}

	public string LevelName
	{
		get
		{
			return LevelName__BackingField;
		}
		set
		{
			LevelName__BackingField = value;
		}
	}

	public bool WillDefaultThisYear
	{
		get
		{
			return WillDefaultThisYear__BackingField;
		}
		set
		{
			WillDefaultThisYear__BackingField = value;
		}
	}

	public SimBond()
	{
	}

	public SimBond(string entity, int level, int maturity)
	{
	}

	public void SetupBond(DataCountry country = null, SimStock stock = null)
	{
	}

	public string BondName()
	{
		return null;
	}

	private bool _003CSetupBond_003Eb__54_0(SimBond x)
	{
		return false;
	}
}
