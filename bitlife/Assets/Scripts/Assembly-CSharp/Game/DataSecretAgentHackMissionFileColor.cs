namespace Game
{
	public class DataSecretAgentHackMissionFileColor
	{
		private SECRET_AGENT_HACKING_MISSION_FILE_TYPE FileType__BackingField;

		private string Snippet__BackingField;

		private SECRET_AGENT_MISSION_CLIENT_TYPE ClientType__BackingField;

		private int CommonnessScore__BackingField;

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

		public string Snippet
		{
			get
			{
				return Snippet__BackingField;
			}
			set
			{
				Snippet__BackingField = value;
			}
		}

		public SECRET_AGENT_MISSION_CLIENT_TYPE ClientType
		{
			get
			{
				return ClientType__BackingField;
			}
			set
			{
				ClientType__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
			}
		}
	}
}
