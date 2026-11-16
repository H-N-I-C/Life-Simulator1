using System.Collections;

public class DataBusinessIndustry
{
	private int Industry__BackingField;

	private ArrayList CountryWhitelist__BackingField;

	private ArrayList CountryBlacklist__BackingField;

	private int ProductLaunchCost__BackingField;

	private string FacilityName__BackingField;

	private string FacilityNamePlural__BackingField;

	private int MaxFacilities__BackingField;

	private int FacilitySetupCost__BackingField;

	private int FacilityAnnualCost__BackingField;

	private int FacilityEmployeeMin__BackingField;

	private int FacilityEmployeeMax__BackingField;

	private int AverageEmployeeSalary__BackingField;

	public int Industry
	{
		get
		{
			return Industry__BackingField;
		}
		set
		{
			Industry__BackingField = value;
		}
	}

	public ArrayList CountryWhitelist
	{
		get
		{
			return CountryWhitelist__BackingField;
		}
		set
		{
			CountryWhitelist__BackingField = value;
		}
	}

	public ArrayList CountryBlacklist
	{
		get
		{
			return CountryBlacklist__BackingField;
		}
		set
		{
			CountryBlacklist__BackingField = value;
		}
	}

	public int ProductLaunchCost
	{
		private get
		{
			return ProductLaunchCost__BackingField;
		}
		set
		{
			ProductLaunchCost__BackingField = value;
		}
	}

	public string FacilityName
	{
		get
		{
			return FacilityName__BackingField;
		}
		set
		{
			FacilityName__BackingField = value;
		}
	}

	public string FacilityNamePlural
	{
		get
		{
			return FacilityNamePlural__BackingField;
		}
		set
		{
			FacilityNamePlural__BackingField = value;
		}
	}

	public int MaxFacilities
	{
		get
		{
			return MaxFacilities__BackingField;
		}
		set
		{
			MaxFacilities__BackingField = value;
		}
	}

	public int FacilitySetupCost
	{
		private get
		{
			return FacilitySetupCost__BackingField;
		}
		set
		{
			FacilitySetupCost__BackingField = value;
		}
	}

	public int FacilityAnnualCost
	{
		private get
		{
			return FacilityAnnualCost__BackingField;
		}
		set
		{
			FacilityAnnualCost__BackingField = value;
		}
	}

	public int FacilityEmployeeMin
	{
		get
		{
			return FacilityEmployeeMin__BackingField;
		}
		set
		{
			FacilityEmployeeMin__BackingField = value;
		}
	}

	public int FacilityEmployeeMax
	{
		get
		{
			return FacilityEmployeeMax__BackingField;
		}
		set
		{
			FacilityEmployeeMax__BackingField = value;
		}
	}

	public int AverageEmployeeSalary
	{
		private get
		{
			return AverageEmployeeSalary__BackingField;
		}
		set
		{
			AverageEmployeeSalary__BackingField = value;
		}
	}

	public int GetProductLaunchCost(DataCountry country)
	{
		return 0;
	}

	public int GetFacilitySetupCost(DataCountry country)
	{
		return 0;
	}

	public int GetFacilityAnnualCost(DataCountry country)
	{
		return 0;
	}

	public int GetAverageEmployeeSalary(DataCountry country)
	{
		return 0;
	}
}
