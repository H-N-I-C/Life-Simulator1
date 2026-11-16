using System.Collections.Generic;

namespace GameAnalyticsSDK.Setup
{
	public class Studio
	{
		private string Name__BackingField;

		private string ID__BackingField;

		private string OrganizationID__BackingField;

		private List<Game> Games__BackingField;

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

		public string OrganizationID
		{
			get
			{
				return OrganizationID__BackingField;
			}
			private set
			{
				OrganizationID__BackingField = value;
			}
		}

		public List<Game> Games
		{
			get
			{
				return Games__BackingField;
			}
			private set
			{
				Games__BackingField = value;
			}
		}

		public Studio(string name, string id, string orgId, List<Game> games)
		{
		}

		public static string[] GetStudioNames(List<Studio> studios, bool addFirstEmpty = true)
		{
			return null;
		}

		public static string[] GetGameNames(int index, List<Studio> studios)
		{
			return null;
		}
	}
}
