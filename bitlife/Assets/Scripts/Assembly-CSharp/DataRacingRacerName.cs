using System.Collections.Generic;

public class DataRacingRacerName
{
	private string FirstName__BackingField;

	private string LastName__BackingField;

	private int Gender__BackingField;

	private List<string> RacerType__BackingField;

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

	public List<string> RacerType
	{
		get
		{
			return RacerType__BackingField;
		}
		set
		{
			RacerType__BackingField = value;
		}
	}
}
