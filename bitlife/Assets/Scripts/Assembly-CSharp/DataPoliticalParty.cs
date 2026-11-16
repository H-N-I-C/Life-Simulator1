using System;

public class DataPoliticalParty : IComparable
{
	private string Name__BackingField;

	private string NameUS__BackingField;

	private string NameUK__BackingField;

	private string NameAU__BackingField;

	private bool National__BackingField;

	private bool Regional__BackingField;

	private bool City__BackingField;

	private bool Local__BackingField;

	private int AppealScore__BackingField;

	private int CommonnessScore__BackingField;

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

	public string NameUS
	{
		get
		{
			return NameUS__BackingField;
		}
		set
		{
			NameUS__BackingField = value;
		}
	}

	public string NameUK
	{
		get
		{
			return NameUK__BackingField;
		}
		set
		{
			NameUK__BackingField = value;
		}
	}

	public string NameAU
	{
		get
		{
			return NameAU__BackingField;
		}
		set
		{
			NameAU__BackingField = value;
		}
	}

	public bool National
	{
		get
		{
			return National__BackingField;
		}
		set
		{
			National__BackingField = value;
		}
	}

	public bool Regional
	{
		get
		{
			return Regional__BackingField;
		}
		set
		{
			Regional__BackingField = value;
		}
	}

	public bool City
	{
		get
		{
			return City__BackingField;
		}
		set
		{
			City__BackingField = value;
		}
	}

	public bool Local
	{
		get
		{
			return Local__BackingField;
		}
		set
		{
			Local__BackingField = value;
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

	public string GetNameForCountryAndCapitalizeAndIncludeParty(DataCountry country, bool capitalize, bool includeParty)
	{
		return null;
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
