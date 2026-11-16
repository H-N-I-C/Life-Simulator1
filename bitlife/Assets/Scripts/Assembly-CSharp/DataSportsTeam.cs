using System;
using System.Collections;
using UnityEngine;

public class DataSportsTeam : IComparable
{
	private int Index__BackingField;

	private int Sport__BackingField;

	private int Gender__BackingField;

	private int LeagueIndex__BackingField;

	private string Name__BackingField;

	private string NearestCity__BackingField;

	private string CountryCode__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	private int ColorPrimary__BackingField;

	private int ColorSecondary__BackingField;

	private int LegacyScore__BackingField;

	private int MoneyScore__BackingField;

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

	public int LeagueIndex
	{
		get
		{
			return LeagueIndex__BackingField;
		}
		set
		{
			LeagueIndex__BackingField = value;
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

	public string NearestCity
	{
		get
		{
			return NearestCity__BackingField;
		}
		set
		{
			NearestCity__BackingField = value;
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

	public int ColorPrimary
	{
		get
		{
			return ColorPrimary__BackingField;
		}
		set
		{
			ColorPrimary__BackingField = value;
		}
	}

	public int ColorSecondary
	{
		get
		{
			return ColorSecondary__BackingField;
		}
		set
		{
			ColorSecondary__BackingField = value;
		}
	}

	public int LegacyScore
	{
		get
		{
			return LegacyScore__BackingField;
		}
		set
		{
			LegacyScore__BackingField = value;
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

	public int CompareTo(object otherObject)
	{
		return 0;
	}

	public string GetDefaultName()
	{
		return null;
	}

	public static Color GetColorForColorEnum(int colorEnum)
	{
		return default;
	}

	public static ArrayList GetAllSportsColorsStringArray()
	{
		return null;
	}

	public static int GetColorEnumForString(string s)
	{
		return 0;
	}

	public static string GetStringForColorEnum(int colorEnum)
	{
		return null;
	}
}
