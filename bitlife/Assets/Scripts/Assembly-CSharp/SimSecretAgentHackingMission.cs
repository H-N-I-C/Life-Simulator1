using System;

[Serializable]
public class SimSecretAgentHackingMission : SimSecretAgentMission
{
	private SECRET_AGENT_HACKING_MISSION_FILE_ACTION FileAction__BackingField;

	private SECRET_AGENT_HACKING_MISSION_FILE_TYPE FileType__BackingField;

	private string FileName__BackingField;

	private string DriveName__BackingField;

	private string FileColor__BackingField;

	private string Description__BackingField;

	private string ComputerType__BackingField;

	private string TargetType__BackingField;

	private int TargetRisk__BackingField;

	public SECRET_AGENT_HACKING_MISSION_FILE_ACTION FileAction
	{
		get
		{
			return FileAction__BackingField;
		}
		set
		{
			FileAction__BackingField = value;
		}
	}

	public SECRET_AGENT_HACKING_MISSION_FILE_TYPE FileType
	{
		get
		{
			return FileType__BackingField;
		}
		set
		{
			FileType__BackingField = value;
		}
	}

	public string FileName
	{
		get
		{
			return FileName__BackingField;
		}
		set
		{
			FileName__BackingField = value;
		}
	}

	public string DriveName
	{
		get
		{
			return DriveName__BackingField;
		}
		set
		{
			DriveName__BackingField = value;
		}
	}

	public string FileColor
	{
		get
		{
			return FileColor__BackingField;
		}
		set
		{
			FileColor__BackingField = value;
		}
	}

	public string Description
	{
		get
		{
			return Description__BackingField;
		}
		set
		{
			Description__BackingField = value;
		}
	}

	public string ComputerType
	{
		get
		{
			return ComputerType__BackingField;
		}
		set
		{
			ComputerType__BackingField = value;
		}
	}

	public string TargetType
	{
		get
		{
			return TargetType__BackingField;
		}
		set
		{
			TargetType__BackingField = value;
		}
	}

	private int TargetRisk
	{
		get
		{
			return TargetRisk__BackingField;
		}
		set
		{
			TargetRisk__BackingField = value;
		}
	}

	public int GetDriveCount()
	{
		return 0;
	}

	public int GetFileCountPerDrive()
	{
		return 0;
	}

	public string FileActionString(SECRET_AGENT_HACKING_MISSION_FILE_ACTION action)
	{
		return null;
	}

	public string FileTypeString()
	{
		return null;
	}
}
