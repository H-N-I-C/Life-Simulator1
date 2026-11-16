using System;

public class DataMafiaMob : IComparable
{
	private string Name__BackingField;

	private string AssociatedCountryCode__BackingField;

	private bool OperatesInAllCountries__BackingField;

	private string SpecificHostCountryCode__BackingField;

	private int NotorietyScore__BackingField;

	private int MoneyScore__BackingField;

	private string Rank1Name__BackingField;

	private string Rank2Name__BackingField;

	private string Rank3Name__BackingField;

	private string Rank4Name__BackingField;

	private string Rank5NameMale__BackingField;

	private string Rank5NameFemale__BackingField;

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

	public string AssociatedCountryCode
	{
		get
		{
			return AssociatedCountryCode__BackingField;
		}
		set
		{
			AssociatedCountryCode__BackingField = value;
		}
	}

	public bool OperatesInAllCountries
	{
		get
		{
			return OperatesInAllCountries__BackingField;
		}
		set
		{
			OperatesInAllCountries__BackingField = value;
		}
	}

	public string SpecificHostCountryCode
	{
		get
		{
			return SpecificHostCountryCode__BackingField;
		}
		set
		{
			SpecificHostCountryCode__BackingField = value;
		}
	}

	public int NotorietyScore
	{
		get
		{
			return NotorietyScore__BackingField;
		}
		set
		{
			NotorietyScore__BackingField = value;
		}
	}

	public int MoneyScore
	{
		get
		{
			return MoneyScore__BackingField;
		}
		set
		{
			MoneyScore__BackingField = value;
		}
	}

	public string Rank1Name
	{
		get
		{
			return Rank1Name__BackingField;
		}
		set
		{
			Rank1Name__BackingField = value;
		}
	}

	public string Rank2Name
	{
		get
		{
			return Rank2Name__BackingField;
		}
		set
		{
			Rank2Name__BackingField = value;
		}
	}

	public string Rank3Name
	{
		get
		{
			return Rank3Name__BackingField;
		}
		set
		{
			Rank3Name__BackingField = value;
		}
	}

	public string Rank4Name
	{
		get
		{
			return Rank4Name__BackingField;
		}
		set
		{
			Rank4Name__BackingField = value;
		}
	}

	public string Rank5NameMale
	{
		get
		{
			return Rank5NameMale__BackingField;
		}
		set
		{
			Rank5NameMale__BackingField = value;
		}
	}

	public string Rank5NameFemale
	{
		get
		{
			return Rank5NameFemale__BackingField;
		}
		set
		{
			Rank5NameFemale__BackingField = value;
		}
	}

	public string GetRankNameForRankAndGender(int rankNI, int gender)
	{
		return null;
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
