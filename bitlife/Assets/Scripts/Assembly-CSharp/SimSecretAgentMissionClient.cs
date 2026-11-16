using System;

[Serializable]
public class SimSecretAgentMissionClient
{
	private SECRET_AGENT_MISSION_CLIENT_TYPE Type__BackingField;

	private string DossierDescription__BackingField;

	private string PersonName__BackingField;

	private int PersonGender__BackingField;

	private string CompanyName__BackingField;

	private string GovernmentName__BackingField;

	public SECRET_AGENT_MISSION_CLIENT_TYPE Type
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

	public string DossierDescription
	{
		get
		{
			return DossierDescription__BackingField;
		}
		set
		{
			DossierDescription__BackingField = value;
		}
	}

	public string PersonName
	{
		get
		{
			return PersonName__BackingField;
		}
		set
		{
			PersonName__BackingField = value;
		}
	}

	public int PersonGender
	{
		get
		{
			return PersonGender__BackingField;
		}
		set
		{
			PersonGender__BackingField = value;
		}
	}

	public string CompanyName
	{
		get
		{
			return CompanyName__BackingField;
		}
		set
		{
			CompanyName__BackingField = value;
		}
	}

	public string GovernmentName
	{
		get
		{
			return GovernmentName__BackingField;
		}
		set
		{
			GovernmentName__BackingField = value;
		}
	}

	public string ClientNameString()
	{
		return null;
	}

	public string BriefingDescriptor()
	{
		return null;
	}
}
