public class DataVacationDestination
{
	private string Name__BackingField;

	private string Context__BackingField;

	private bool UseContextInDescription__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	private string CountryName__BackingField;

	private string Continent__BackingField;

	private int CostScore__BackingField;

	private int AppealScore__BackingField;

	private int DangerScore__BackingField;

	private int CommonnessScore__BackingField;

	private bool PixieDustOnly__BackingField;

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

	public string Context
	{
		get
		{
			return Context__BackingField;
		}
		set
		{
			Context__BackingField = value;
		}
	}

	public bool UseContextInDescription
	{
		get
		{
			return UseContextInDescription__BackingField;
		}
		set
		{
			UseContextInDescription__BackingField = value;
		}
	}

	public bool UsesDefiniteArticle
	{
		get
		{
			return UsesDefiniteArticle__BackingField;
		}
		set
		{
			UsesDefiniteArticle__BackingField = value;
		}
	}

	public string CountryName
	{
		get
		{
			return CountryName__BackingField;
		}
		set
		{
			CountryName__BackingField = value;
		}
	}

	public string Continent
	{
		get
		{
			return Continent__BackingField;
		}
		set
		{
			Continent__BackingField = value;
		}
	}

	public int CostScore
	{
		get
		{
			return CostScore__BackingField;
		}
		set
		{
			CostScore__BackingField = value;
		}
	}

	public int AppealScore
	{
		get
		{
			return AppealScore__BackingField;
		}
		set
		{
			AppealScore__BackingField = value;
		}
	}

	public int DangerScore
	{
		get
		{
			return DangerScore__BackingField;
		}
		set
		{
			DangerScore__BackingField = value;
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

	public bool PixieDustOnly
	{
		get
		{
			return PixieDustOnly__BackingField;
		}
		set
		{
			PixieDustOnly__BackingField = value;
		}
	}

	public string GetListTitle()
	{
		return null;
	}

	public string GetDescriptionAndCapitalizeAndUseTrailingCommaIfApplicable(bool capitalize, bool comma)
	{
		return null;
	}

	public float CostOfVacationForClassAndDaysFromCountry(int vacationClass, int days, DataCountry country)
	{
		return 0f;
	}

	public static string VacationClassStringForClass(int theClass)
	{
		return null;
	}
}
