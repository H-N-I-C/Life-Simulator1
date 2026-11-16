using System;

[Serializable]
public class SimDiscovery
{
	private bool IsFullyDiscovered__BackingField;

	private string Name__BackingField;

	private double Diameter__BackingField;

	private int DiscoveryType__BackingField;

	private SimProbe DiscoveryProbe__BackingField;

	private int HeroAgeAtDiscovery__BackingField;

	private string Color__BackingField;

	private double Period__BackingField;

	private double TailLength__BackingField;

	private string Classification__BackingField;

	private double NumberOfStars__BackingField;

	private double Mass__BackingField;

	private double EventHorizon__BackingField;

	private int PlanetsInOrbit__BackingField;

	private string PrimaryColorHexString__BackingField;

	private string SecondaryColorHexString__BackingField;

	private bool IsPublished__BackingField;

	public bool IsFullyDiscovered
	{
		get
		{
			return IsFullyDiscovered__BackingField;
		}
		set
		{
			IsFullyDiscovered__BackingField = value;
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

	public double Diameter
	{
		get
		{
			return Diameter__BackingField;
		}
		set
		{
			Diameter__BackingField = value;
		}
	}

	public int DiscoveryType
	{
		get
		{
			return DiscoveryType__BackingField;
		}
		set
		{
			DiscoveryType__BackingField = value;
		}
	}

	public SimProbe DiscoveryProbe
	{
		get
		{
			return DiscoveryProbe__BackingField;
		}
		set
		{
			DiscoveryProbe__BackingField = value;
		}
	}

	public int HeroAgeAtDiscovery
	{
		get
		{
			return HeroAgeAtDiscovery__BackingField;
		}
		set
		{
			HeroAgeAtDiscovery__BackingField = value;
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

	public double Period
	{
		get
		{
			return Period__BackingField;
		}
		set
		{
			Period__BackingField = value;
		}
	}

	public double TailLength
	{
		get
		{
			return TailLength__BackingField;
		}
		set
		{
			TailLength__BackingField = value;
		}
	}

	public string Classification
	{
		get
		{
			return Classification__BackingField;
		}
		set
		{
			Classification__BackingField = value;
		}
	}

	public double NumberOfStars
	{
		get
		{
			return NumberOfStars__BackingField;
		}
		set
		{
			NumberOfStars__BackingField = value;
		}
	}

	public double Mass
	{
		get
		{
			return Mass__BackingField;
		}
		set
		{
			Mass__BackingField = value;
		}
	}

	public double EventHorizon
	{
		get
		{
			return EventHorizon__BackingField;
		}
		set
		{
			EventHorizon__BackingField = value;
		}
	}

	public int PlanetsInOrbit
	{
		get
		{
			return PlanetsInOrbit__BackingField;
		}
		set
		{
			PlanetsInOrbit__BackingField = value;
		}
	}

	public string PrimaryColorHexString
	{
		get
		{
			return PrimaryColorHexString__BackingField;
		}
		set
		{
			PrimaryColorHexString__BackingField = value;
		}
	}

	public string SecondaryColorHexString
	{
		get
		{
			return SecondaryColorHexString__BackingField;
		}
		set
		{
			SecondaryColorHexString__BackingField = value;
		}
	}

	public bool IsPublished
	{
		get
		{
			return IsPublished__BackingField;
		}
		set
		{
			IsPublished__BackingField = value;
		}
	}

	public int GetYearsSinceDiscovery()
	{
		return 0;
	}

	public virtual bool IncrementAge()
	{
		return false;
	}

	public virtual string EmojiString()
	{
		return null;
	}
}
