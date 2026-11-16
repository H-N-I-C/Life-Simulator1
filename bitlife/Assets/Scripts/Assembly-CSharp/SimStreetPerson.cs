using System;

[Serializable]
public class SimStreetPerson
{
	private bool IsRegular__BackingField;

	private SimPerson Person__BackingField;

	private string StreetPersonDataLookup__BackingField;

	private int StreetPersonType__BackingField;

	private float Att_professionalism__BackingField;

	private string DescriptionString__BackingField;

	private string PoliceHonorificName__BackingField;

	public bool IsRegular
	{
		get
		{
			return IsRegular__BackingField;
		}
		set
		{
			IsRegular__BackingField = value;
		}
	}

	public SimPerson Person
	{
		get
		{
			return Person__BackingField;
		}
		set
		{
			Person__BackingField = value;
		}
	}

	public DataStreetPerson StreetPersonData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private string StreetPersonDataLookup
	{
		get
		{
			return StreetPersonDataLookup__BackingField;
		}
		set
		{
			StreetPersonDataLookup__BackingField = value;
		}
	}

	public int StreetPersonType
	{
		get
		{
			return StreetPersonType__BackingField;
		}
		set
		{
			StreetPersonType__BackingField = value;
		}
	}

	public float Att_professionalism
	{
		get
		{
			return Att_professionalism__BackingField;
		}
		set
		{
			Att_professionalism__BackingField = value;
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

	public string PoliceHonorificName
	{
		get
		{
			return PoliceHonorificName__BackingField;
		}
		set
		{
			PoliceHonorificName__BackingField = value;
		}
	}

	public string HonorificName()
	{
		return null;
	}

	public SimStreetPerson()
	{
	}

	public SimStreetPerson(DataStreetPerson streetPersonData, int specificGender = 2)
	{
	}

	public void SetupMoneyAttribute()
	{
	}

	public float GetDamagesBaseline()
	{
		return 0f;
	}

	public bool IsInNeed()
	{
		return false;
	}
}
