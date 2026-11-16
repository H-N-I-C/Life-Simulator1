using System;

[Serializable]
public class SimPersonName
{
	private string firstName;

	private string FirstName__BackingField;

	private string LastName__BackingField;

	private bool ReversesNames__BackingField;

	private bool IsCustom__BackingField;

	private string RoyalTitle__BackingField;

	private bool HasDoctorate__BackingField;

	public string FirstName
	{
		get
		{
			return FirstName__BackingField;
		}
		set
		{
			FirstName__BackingField = value;
		}
	}

	public string LastName
	{
		get
		{
			return LastName__BackingField;
		}
		set
		{
			LastName__BackingField = value;
		}
	}

	public bool ReversesNames
	{
		get
		{
			return ReversesNames__BackingField;
		}
		set
		{
			ReversesNames__BackingField = value;
		}
	}

	public bool IsCustom
	{
		get
		{
			return IsCustom__BackingField;
		}
		set
		{
			IsCustom__BackingField = value;
		}
	}

	public string RoyalTitle
	{
		get
		{
			return RoyalTitle__BackingField;
		}
		set
		{
			RoyalTitle__BackingField = value;
		}
	}

	public bool HasDoctorate
	{
		get
		{
			return HasDoctorate__BackingField;
		}
		set
		{
			HasDoctorate__BackingField = value;
		}
	}

	public string Description()
	{
		return null;
	}

	public SimPersonName(string first, string last, bool reversesNames)
	{
	}

	public string FirstNamePossessive()
	{
		return null;
	}

	public string FullName()
	{
		return null;
	}

	public string PureFirstName()
	{
		return null;
	}

	public string PureLastName()
	{
		return null;
	}

	public string PureFullName()
	{
		return null;
	}

	public SimPersonName GetDuplicatePersonName()
	{
		return null;
	}
}
