using System.Collections.Generic;

namespace Game
{
	public class DataSecretAgentAgencyFrontName
	{
		private SECRET_AGENT_AGENCY_FRONT_TYPE BusinessType__BackingField;

		private List<string> Format__BackingField;

		private List<string> Prefix__BackingField;

		private List<string> Suffix__BackingField;

		public SECRET_AGENT_AGENCY_FRONT_TYPE BusinessType
		{
			get
			{
				return BusinessType__BackingField;
			}
			set
			{
				BusinessType__BackingField = value;
			}
		}

		public List<string> Format
		{
			get
			{
				return Format__BackingField;
			}
			set
			{
				Format__BackingField = value;
			}
		}

		public List<string> Prefix
		{
			get
			{
				return Prefix__BackingField;
			}
			set
			{
				Prefix__BackingField = value;
			}
		}

		public List<string> Suffix
		{
			get
			{
				return Suffix__BackingField;
			}
			set
			{
				Suffix__BackingField = value;
			}
		}
	}
}
