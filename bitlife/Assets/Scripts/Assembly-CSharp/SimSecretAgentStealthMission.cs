using System;
using Game;

[Serializable]
public class SimSecretAgentStealthMission : SimSecretAgentMission
{
	private string TargetName__BackingField;

	private int TargetGender__BackingField;

	private string TargetID__BackingField;

	private string Action__BackingField;

	private string Location__BackingField;

	private int NumEventsToShow__BackingField;

	private int IDPerson__BackingField;

	private int IDLocation__BackingField;

	private bool TargetIsPerson__BackingField;

	public string TargetName
	{
		get
		{
			return TargetName__BackingField;
		}
		set
		{
			TargetName__BackingField = value;
		}
	}

	public int TargetGender
	{
		get
		{
			return TargetGender__BackingField;
		}
		set
		{
			TargetGender__BackingField = value;
		}
	}

	private string TargetID
	{
		get
		{
			return TargetID__BackingField;
		}
		set
		{
			TargetID__BackingField = value;
		}
	}

	public string Action
	{
		get
		{
			return Action__BackingField;
		}
		set
		{
			Action__BackingField = value;
		}
	}

	private string Location
	{
		get
		{
			return Location__BackingField;
		}
		set
		{
			Location__BackingField = value;
		}
	}

	public int NumEventsToShow
	{
		get
		{
			return NumEventsToShow__BackingField;
		}
		set
		{
			NumEventsToShow__BackingField = value;
		}
	}

	public int IDPerson
	{
		get
		{
			return IDPerson__BackingField;
		}
		set
		{
			IDPerson__BackingField = value;
		}
	}

	public int IDLocation
	{
		get
		{
			return IDLocation__BackingField;
		}
		set
		{
			IDLocation__BackingField = value;
		}
	}

	public DataSecretAgentMissionTargetPeople PersonData => null;

	public DataSecretAgentStealthMissionLocation LocationData => null;

	public DataSecretAgentStealthItemTarget ItemData => null;

	public bool TargetIsPerson
	{
		get
		{
			return TargetIsPerson__BackingField;
		}
		set
		{
			TargetIsPerson__BackingField = value;
		}
	}

	public string LocationDescription()
	{
		return null;
	}

	public string TargetDescription(bool definiteVersionOverride = false)
	{
		return null;
	}

	public Scenario GenerateRandomStealthEvent(SimSecretAgentStealthMission mission, Scenario lastEvent = null, int eventIndex = 0)
	{
		return null;
	}
}
