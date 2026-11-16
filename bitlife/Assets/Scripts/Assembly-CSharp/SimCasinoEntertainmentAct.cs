using System;

[Serializable]
public class SimCasinoEntertainmentAct
{
	private string Theme__BackingField;

	private string Category__BackingField;

	private string Name__BackingField;

	private float Salary__BackingField;

	private float LastYearRevenue__BackingField;

	private int YearAtCasino__BackingField;

	private int HeroAgeAtLastInteractedWithHero__BackingField;

	private float _popularity;

	private float _competence;

	public string Theme
	{
		get
		{
			return Theme__BackingField;
		}
		set
		{
			Theme__BackingField = value;
		}
	}

	public string Category
	{
		get
		{
			return Category__BackingField;
		}
		set
		{
			Category__BackingField = value;
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

	public float Salary
	{
		get
		{
			return Salary__BackingField;
		}
		set
		{
			Salary__BackingField = value;
		}
	}

	public float LastYearRevenue
	{
		get
		{
			return LastYearRevenue__BackingField;
		}
		set
		{
			LastYearRevenue__BackingField = value;
		}
	}

	public int YearAtCasino
	{
		get
		{
			return YearAtCasino__BackingField;
		}
		set
		{
			YearAtCasino__BackingField = value;
		}
	}

	public int HeroAgeAtLastInteractedWithHero
	{
		get
		{
			return HeroAgeAtLastInteractedWithHero__BackingField;
		}
		set
		{
			HeroAgeAtLastInteractedWithHero__BackingField = value;
		}
	}

	public SimCasinoEntertainmentAct(string theme, string category, float salary)
	{
	}

	public void IncrementAge(SimCasino casino)
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public float GetCompetence()
	{
		return 0f;
	}

	public double GetLastYearRevenue()
	{
		return 0.0;
	}

	public float GetAnnualCosts()
	{
		return 0f;
	}

	private float CalculateAnnualRevenue(SimCasino casino)
	{
		return 0f;
	}

	public float GetAnnualRevenue()
	{
		return 0f;
	}

	public void SetPopularity(int impression)
	{
	}
}
