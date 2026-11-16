using System;

public class DataSportsLeague : IComparable
{
	private int Index__BackingField;

	private int Sport__BackingField;

	private int Gender__BackingField;

	private string Name__BackingField;

	private string DescriptionString__BackingField;

	private string CountryCode__BackingField;

	private int MoneyScore__BackingField;

	private bool HallOfFame__BackingField;

	public int Index
	{
		get
		{
			return Index__BackingField;
		}
		set
		{
			Index__BackingField = value;
		}
	}

	public int Sport
	{
		get
		{
			return Sport__BackingField;
		}
		set
		{
			Sport__BackingField = value;
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

	public string DescriptionString
	{
		get
		{
			return DescriptionString__BackingField;
		}
		set
		{
			DescriptionString__BackingField = value;
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

	public bool HallOfFame
	{
		get
		{
			return HallOfFame__BackingField;
		}
		set
		{
			HallOfFame__BackingField = value;
		}
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}

	public string ParsedDescriptionString()
	{
		return null;
	}
}
