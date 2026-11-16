using System.Collections;

public class DataCountryPoliticalSystem
{
	private string CountryCode__BackingField;

	private bool NationalOfficeAvailable__BackingField;

	private bool RegionalOfficeAvailable__BackingField;

	private bool CityOfficeAvailable__BackingField;

	private bool LocalOfficeAvailable__BackingField;

	private string TitleNational__BackingField;

	private string TitleNationalAdjective__BackingField;

	private int NationalOfficeTermLimit__BackingField;

	private int NationalOfficeTermYears__BackingField;

	private string TitleRegional__BackingField;

	private string TitleRegionalAdjective__BackingField;

	private string TitleRegionalSpecific__BackingField;

	private string RegionJurisdiction__BackingField;

	private string RegionJurisdictionAdjective__BackingField;

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

	public bool NationalOfficeAvailable
	{
		get
		{
			return NationalOfficeAvailable__BackingField;
		}
		set
		{
			NationalOfficeAvailable__BackingField = value;
		}
	}

	public bool RegionalOfficeAvailable
	{
		get
		{
			return RegionalOfficeAvailable__BackingField;
		}
		set
		{
			RegionalOfficeAvailable__BackingField = value;
		}
	}

	public bool CityOfficeAvailable
	{
		get
		{
			return CityOfficeAvailable__BackingField;
		}
		set
		{
			CityOfficeAvailable__BackingField = value;
		}
	}

	public bool LocalOfficeAvailable
	{
		get
		{
			return LocalOfficeAvailable__BackingField;
		}
		set
		{
			LocalOfficeAvailable__BackingField = value;
		}
	}

	public string TitleNational
	{
		get
		{
			return TitleNational__BackingField;
		}
		set
		{
			TitleNational__BackingField = value;
		}
	}

	public string TitleNationalAdjective
	{
		get
		{
			return TitleNationalAdjective__BackingField;
		}
		set
		{
			TitleNationalAdjective__BackingField = value;
		}
	}

	public int NationalOfficeTermLimit
	{
		get
		{
			return NationalOfficeTermLimit__BackingField;
		}
		set
		{
			NationalOfficeTermLimit__BackingField = value;
		}
	}

	public int NationalOfficeTermYears
	{
		get
		{
			return NationalOfficeTermYears__BackingField;
		}
		set
		{
			NationalOfficeTermYears__BackingField = value;
		}
	}

	public string TitleRegional
	{
		get
		{
			return TitleRegional__BackingField;
		}
		set
		{
			TitleRegional__BackingField = value;
		}
	}

	public string TitleRegionalAdjective
	{
		get
		{
			return TitleRegionalAdjective__BackingField;
		}
		set
		{
			TitleRegionalAdjective__BackingField = value;
		}
	}

	public string TitleRegionalSpecific
	{
		get
		{
			return TitleRegionalSpecific__BackingField;
		}
		set
		{
			TitleRegionalSpecific__BackingField = value;
		}
	}

	public string RegionJurisdiction
	{
		get
		{
			return RegionJurisdiction__BackingField;
		}
		set
		{
			RegionJurisdiction__BackingField = value;
		}
	}

	public string RegionJurisdictionAdjective
	{
		get
		{
			return RegionJurisdictionAdjective__BackingField;
		}
		set
		{
			RegionJurisdictionAdjective__BackingField = value;
		}
	}

	public bool IsPoliticalOfficeAvailable(int office)
	{
		return false;
	}

	public string GetTitleForOfficeAndPlaceAndIsSpecificAndIsCapitalized(int office, SimPlace place, bool isSpecific, bool isCapitalized)
	{
		return null;
	}

	public int GetTermYearsForOffice(int office)
	{
		return 0;
	}

	public int GetTermLimitForOffice(int office)
	{
		return 0;
	}

	public int GetMinAgeForOffice(int office)
	{
		return 0;
	}

	public bool GetBirthRequirementForOffice(int office)
	{
		return false;
	}

	public ArrayList GetPoliticalIssueArrayForOffice(int office)
	{
		return null;
	}

	public string ReplaceBracketsInStringForOffice(string s, int office)
	{
		return null;
	}

	public static string GetTitleStringForPoliticalIssue(int issue)
	{
		return null;
	}

	public static int GetPoliticalIssueEnumForTitleString(string s)
	{
		return 0;
	}

	public static string GetDescriptiveStringForPoliticalIssue(int issue)
	{
		return null;
	}
}
