using System;

[Serializable]
public class SimGhost : IComparable
{
	private string Name__BackingField;

	public string GhostName;

	private int Type__BackingField;

	private int Age__BackingField;

	private int Gender__BackingField;

	private PersonEmojiScript EmojiScript__BackingField;

	private float Att_friendliness__BackingField;

	private float Att_activeness__BackingField;

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

	public DataGhost GhostData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
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

	public PersonEmojiScript EmojiScript
	{
		get
		{
			return EmojiScript__BackingField;
		}
		set
		{
			EmojiScript__BackingField = value;
		}
	}

	public float Att_friendliness
	{
		get
		{
			return Att_friendliness__BackingField;
		}
		set
		{
			Att_friendliness__BackingField = value;
		}
	}

	public float Att_activeness
	{
		get
		{
			return Att_activeness__BackingField;
		}
		set
		{
			Att_activeness__BackingField = value;
		}
	}

	public SimGhost(SimHouse theHouse)
	{
	}

	public void SetupWithGhostFromGrave(Grave theGrave)
	{
	}

	public void SetupWithGhostFromRelative(SimPerson thePerson)
	{
	}

	public void SetupWithGhostFromDataWithName(DataGhost theData, string theName)
	{
	}

	public void SetupEmojiScriptAndModelAfterPerson(SimPerson person)
	{
	}

	public static Grave GetPossibleGraveForCountryCode(string countryCode)
	{
		return null;
	}

	public string DescriptiveGhostName()
	{
		return null;
	}

	public string GetGhostName()
	{
		return null;
	}

	public string GhostTypeString()
	{
		return null;
	}

	public SimHouse GetHouseOfResidence()
	{
		return null;
	}

	public string EmojiString()
	{
		return null;
	}

	public void AnnualAttributeAdjustment()
	{
	}

	public void ImpactFriendliness(int impactScore)
	{
	}

	public void ImpactActiveness(int impactScore)
	{
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
