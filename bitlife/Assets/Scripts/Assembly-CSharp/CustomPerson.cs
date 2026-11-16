using System;

[Serializable]
public class CustomPerson
{
	private SimPersonName Name__BackingField;

	private int Gender__BackingField;

	public SimPersonName Name
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

	public CustomPerson(string firstName, string lastName, int gender)
	{
	}

	public CustomPerson(bool initHere)
	{
	}
}
