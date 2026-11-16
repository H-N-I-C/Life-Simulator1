using System;
using Game;

[Serializable]
public class SimSecretAgentAssassinationMission : SimSecretAgentMission
{
	private int TargetGender__BackingField;

	private string TargetName__BackingField;

	private string Location__BackingField;

	private string Method__BackingField;

	private string TargetID__BackingField;

	private int IDPerson__BackingField;

	private int IDLocation__BackingField;

	private int AllowedGoodOpportunitiesToIgnore__BackingField;

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

	public string Location
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

	public string Method
	{
		get
		{
			return Method__BackingField;
		}
		set
		{
			Method__BackingField = value;
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

	public int AllowedGoodOpportunitiesToIgnore
	{
		get
		{
			return AllowedGoodOpportunitiesToIgnore__BackingField;
		}
		set
		{
			AllowedGoodOpportunitiesToIgnore__BackingField = value;
		}
	}

	public string TargetDescription()
	{
		return null;
	}

	public string LocationDescription()
	{
		return null;
	}
}
