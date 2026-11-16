using System.Collections.Generic;

namespace GameAnalyticsSDK.Setup
{
	public class Organization
	{
		private string Name__BackingField;

		private string ID__BackingField;

		private List<Studio> Studios__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			private set
			{
				Name__BackingField = value;
			}
		}

		public string ID
		{
			get
			{
				return ID__BackingField;
			}
			private set
			{
				ID__BackingField = value;
			}
		}

		public List<Studio> Studios
		{
			get
			{
				return Studios__BackingField;
			}
			private set
			{
				Studios__BackingField = value;
			}
		}

		public Organization(string name, string id)
		{
		}

		public static string[] GetOrganizationNames(List<Organization> organizations, bool addFirstEmpty = true)
		{
			return null;
		}
	}
}
