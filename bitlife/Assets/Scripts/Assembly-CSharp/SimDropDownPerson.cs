using System.Collections;

public class SimDropDownPerson
{
	private SimPerson Person__BackingField;

	private int PersonType__BackingField;

	private string ListDisplayName__BackingField;

	private string PersonNameScenario__BackingField;

	private string PersonNameNarrative__BackingField;

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

	public int PersonType
	{
		get
		{
			return PersonType__BackingField;
		}
		set
		{
			PersonType__BackingField = value;
		}
	}

	public string ListDisplayName
	{
		get
		{
			return ListDisplayName__BackingField;
		}
		set
		{
			ListDisplayName__BackingField = value;
		}
	}

	public string PersonNameScenario
	{
		get
		{
			return PersonNameScenario__BackingField;
		}
		set
		{
			PersonNameScenario__BackingField = value;
		}
	}

	public string PersonNameNarrative
	{
		get
		{
			return PersonNameNarrative__BackingField;
		}
		set
		{
			PersonNameNarrative__BackingField = value;
		}
	}

	public static ArrayList GetPossibleRecipientArray(int numMaxRecipients, bool canIncludeSpouse)
	{
		return null;
	}
}
