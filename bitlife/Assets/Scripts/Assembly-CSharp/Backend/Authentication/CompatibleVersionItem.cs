using System.Collections.Generic;

namespace Backend.Authentication
{
	public class CompatibleVersionItem
	{
		private string MinVersion__BackingField;

		private List<string> BlockerVersions__BackingField;

		private APP_PLATFORM PlatformType__BackingField;

		public string MinVersion
		{
			get
			{
				return MinVersion__BackingField;
			}
			set
			{
				MinVersion__BackingField = value;
			}
		}

		public List<string> BlockerVersions
		{
			get
			{
				return BlockerVersions__BackingField;
			}
			set
			{
				BlockerVersions__BackingField = value;
			}
		}

		public APP_PLATFORM PlatformType
		{
			get
			{
				return PlatformType__BackingField;
			}
			set
			{
				PlatformType__BackingField = value;
			}
		}
	}
}
