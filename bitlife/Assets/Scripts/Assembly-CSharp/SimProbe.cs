using System;

[Serializable]
public class SimProbe
{
	private string ProbeName__BackingField;

	private int ActiveStatus__BackingField;

	private float Att_Materials__BackingField;

	private float Att_Software__BackingField;

	private float Att_Equipment__BackingField;

	private float Budget__BackingField;

	private int HeroAgeWhenLaunched__BackingField;

	private int YearsBeforeDiscovery__BackingField;

	private int YearsActive__BackingField;

	private long DistanceTraveled__BackingField;

	private int DiscoveriesLogged__BackingField;

	private bool IsDataProbe__BackingField;

	public string ProbeName
	{
		get
		{
			return ProbeName__BackingField;
		}
		set
		{
			ProbeName__BackingField = value;
		}
	}

	public int ActiveStatus
	{
		get
		{
			return ActiveStatus__BackingField;
		}
		set
		{
			ActiveStatus__BackingField = value;
		}
	}

	public float Att_Materials
	{
		get
		{
			return Att_Materials__BackingField;
		}
		set
		{
			Att_Materials__BackingField = value;
		}
	}

	public float Att_Software
	{
		get
		{
			return Att_Software__BackingField;
		}
		set
		{
			Att_Software__BackingField = value;
		}
	}

	public float Att_Equipment
	{
		get
		{
			return Att_Equipment__BackingField;
		}
		set
		{
			Att_Equipment__BackingField = value;
		}
	}

	public float Budget
	{
		get
		{
			return Budget__BackingField;
		}
		set
		{
			Budget__BackingField = value;
		}
	}

	public int HeroAgeWhenLaunched
	{
		get
		{
			return HeroAgeWhenLaunched__BackingField;
		}
		set
		{
			HeroAgeWhenLaunched__BackingField = value;
		}
	}

	public int YearsBeforeDiscovery
	{
		get
		{
			return YearsBeforeDiscovery__BackingField;
		}
		set
		{
			YearsBeforeDiscovery__BackingField = value;
		}
	}

	public int YearsActive
	{
		get
		{
			return YearsActive__BackingField;
		}
		set
		{
			YearsActive__BackingField = value;
		}
	}

	public long DistanceTraveled
	{
		get
		{
			return DistanceTraveled__BackingField;
		}
		set
		{
			DistanceTraveled__BackingField = value;
		}
	}

	public int DiscoveriesLogged
	{
		get
		{
			return DiscoveriesLogged__BackingField;
		}
		set
		{
			DiscoveriesLogged__BackingField = value;
		}
	}

	public bool IsDataProbe
	{
		get
		{
			return IsDataProbe__BackingField;
		}
		set
		{
			IsDataProbe__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	private Scenario GenerateNewDiscovery(SimProbe simProbe)
	{
		return null;
	}

	private float GenerateRandomBudget()
	{
		return 0f;
	}

	public string GetActiveStatusString()
	{
		return null;
	}
}
