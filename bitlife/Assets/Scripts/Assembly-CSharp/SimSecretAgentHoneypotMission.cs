using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimSecretAgentHoneypotMission : SimSecretAgentMission
{
	private string TargetName__BackingField;

	private int TargetGender__BackingField;

	private int TargetSexuality__BackingField;

	private string Disguise__BackingField;

	private List<string> TargetTurnOns__BackingField;

	private List<string> TargetTurnOffs__BackingField;

	private string TargetHobby__BackingField;

	private string Location__BackingField;

	private string TargetID__BackingField;

	private int IDPerson__BackingField;

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

	public int TargetSexuality
	{
		get
		{
			return TargetSexuality__BackingField;
		}
		set
		{
			TargetSexuality__BackingField = value;
		}
	}

	public string Disguise
	{
		get
		{
			return Disguise__BackingField;
		}
		set
		{
			Disguise__BackingField = value;
		}
	}

	public List<string> TargetTurnOns
	{
		get
		{
			return TargetTurnOns__BackingField;
		}
		set
		{
			TargetTurnOns__BackingField = value;
		}
	}

	public List<string> TargetTurnOffs
	{
		get
		{
			return TargetTurnOffs__BackingField;
		}
		set
		{
			TargetTurnOffs__BackingField = value;
		}
	}

	public string TargetHobby
	{
		get
		{
			return TargetHobby__BackingField;
		}
		set
		{
			TargetHobby__BackingField = value;
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

	public DataSecretAgentMissionTargetPeople PersonData => null;

	public string TargetSexualityString()
	{
		return null;
	}

	public string TargetDescription()
	{
		return null;
	}
}
