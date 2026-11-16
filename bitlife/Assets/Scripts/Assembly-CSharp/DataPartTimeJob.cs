using System;

public class DataPartTimeJob : IComparable
{
	private string Name__BackingField;

	private int CompanyType__BackingField;

	private bool IsFreelance__BackingField;

	private int MinHourlyRate__BackingField;

	private int MaxHourlyRate__BackingField;

	private int MinWeeklyHours__BackingField;

	private int MaxWeeklyHours__BackingField;

	private int CommonnessScore__BackingField;

	private int TiringScore__BackingField;

	private int Gender__BackingField;

	private int MinAge__BackingField;

	private int MaxAge__BackingField;

	private bool RequiresCleanCriminalRecord__BackingField;

	private bool RequiresDrivingLicense__BackingField;

	private bool RequiresVehicle__BackingField;

	private bool MustBeStudent__BackingField;

	private int MinIntelligence__BackingField;

	private int MinAppearance__BackingField;

	private int MinHealth__BackingField;

	private int MinHappiness__BackingField;

	private int SuccessDefiningAttribute__BackingField;

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

	public int CompanyType
	{
		get
		{
			return CompanyType__BackingField;
		}
		set
		{
			CompanyType__BackingField = value;
		}
	}

	public bool IsFreelance
	{
		get
		{
			return IsFreelance__BackingField;
		}
		set
		{
			IsFreelance__BackingField = value;
		}
	}

	public int MinHourlyRate
	{
		get
		{
			return MinHourlyRate__BackingField;
		}
		set
		{
			MinHourlyRate__BackingField = value;
		}
	}

	public int MaxHourlyRate
	{
		get
		{
			return MaxHourlyRate__BackingField;
		}
		set
		{
			MaxHourlyRate__BackingField = value;
		}
	}

	public int MinWeeklyHours
	{
		get
		{
			return MinWeeklyHours__BackingField;
		}
		set
		{
			MinWeeklyHours__BackingField = value;
		}
	}

	public int MaxWeeklyHours
	{
		get
		{
			return MaxWeeklyHours__BackingField;
		}
		set
		{
			MaxWeeklyHours__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public int TiringScore
	{
		get
		{
			return TiringScore__BackingField;
		}
		set
		{
			TiringScore__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public int MinAge
	{
		get
		{
			return MinAge__BackingField;
		}
		set
		{
			MinAge__BackingField = value;
		}
	}

	public int MaxAge
	{
		get
		{
			return MaxAge__BackingField;
		}
		set
		{
			MaxAge__BackingField = value;
		}
	}

	public bool RequiresCleanCriminalRecord
	{
		get
		{
			return RequiresCleanCriminalRecord__BackingField;
		}
		set
		{
			RequiresCleanCriminalRecord__BackingField = value;
		}
	}

	public bool RequiresDrivingLicense
	{
		get
		{
			return RequiresDrivingLicense__BackingField;
		}
		set
		{
			RequiresDrivingLicense__BackingField = value;
		}
	}

	public bool RequiresVehicle
	{
		get
		{
			return RequiresVehicle__BackingField;
		}
		set
		{
			RequiresVehicle__BackingField = value;
		}
	}

	public bool MustBeStudent
	{
		get
		{
			return MustBeStudent__BackingField;
		}
		set
		{
			MustBeStudent__BackingField = value;
		}
	}

	public int MinIntelligence
	{
		get
		{
			return MinIntelligence__BackingField;
		}
		set
		{
			MinIntelligence__BackingField = value;
		}
	}

	public int MinAppearance
	{
		get
		{
			return MinAppearance__BackingField;
		}
		set
		{
			MinAppearance__BackingField = value;
		}
	}

	public int MinHealth
	{
		get
		{
			return MinHealth__BackingField;
		}
		set
		{
			MinHealth__BackingField = value;
		}
	}

	public int MinHappiness
	{
		get
		{
			return MinHappiness__BackingField;
		}
		set
		{
			MinHappiness__BackingField = value;
		}
	}

	public int SuccessDefiningAttribute
	{
		get
		{
			return SuccessDefiningAttribute__BackingField;
		}
		set
		{
			SuccessDefiningAttribute__BackingField = value;
		}
	}

	public int CompareTo(object obj)
	{
		return 0;
	}

	public int GetMinHourlyRateAdjustedForCostOfLiving()
	{
		return 0;
	}

	public int GetMaxHourlyRateAdjustedForCostOfLiving()
	{
		return 0;
	}
}
