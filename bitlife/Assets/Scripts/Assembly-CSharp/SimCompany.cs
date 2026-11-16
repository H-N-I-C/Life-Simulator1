using System;

[Serializable]
public class SimCompany
{
	private int CompanyType__BackingField;

	private string CompanyName__BackingField;

	private float CompensationCoef__BackingField;

	private float ProfitMarginCoef__BackingField;

	private int ReputationScore__BackingField;

	private int AgeAtLastHeroOpportunity__BackingField;

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

	public string CompanyName
	{
		get
		{
			return CompanyName__BackingField;
		}
		set
		{
			CompanyName__BackingField = value;
		}
	}

	public float CompensationCoef
	{
		get
		{
			return CompensationCoef__BackingField;
		}
		set
		{
			CompensationCoef__BackingField = value;
		}
	}

	public float ProfitMarginCoef
	{
		get
		{
			return ProfitMarginCoef__BackingField;
		}
		set
		{
			ProfitMarginCoef__BackingField = value;
		}
	}

	public int ReputationScore
	{
		get
		{
			return ReputationScore__BackingField;
		}
		set
		{
			ReputationScore__BackingField = value;
		}
	}

	public int AgeAtLastHeroOpportunity
	{
		get
		{
			return AgeAtLastHeroOpportunity__BackingField;
		}
		set
		{
			AgeAtLastHeroOpportunity__BackingField = value;
		}
	}

	public SimCompany()
	{
	}

	public SimCompany(bool initHere)
	{
	}

	public string Description()
	{
		return null;
	}

	public int CommercialReach()
	{
		return 0;
	}

	public int MinimumYearsForPension()
	{
		return 0;
	}

	public float GetPensionAmountForCurrentSalaryAndYearsAtCompany(float salary, int yearsAtCompany)
	{
		return 0f;
	}
}
