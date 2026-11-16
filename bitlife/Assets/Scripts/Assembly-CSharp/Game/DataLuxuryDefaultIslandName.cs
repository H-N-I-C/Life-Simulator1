using System.Collections.Generic;

namespace Game
{
	public class DataLuxuryDefaultIslandName
	{
		private string Name__BackingField;

		private List<string> IslandType__BackingField;

		private int CommonnessScore__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			set
			{
				Name__BackingField = value;
			}
		}

		public List<string> IslandType
		{
			get
			{
				return IslandType__BackingField;
			}
			set
			{
				IslandType__BackingField = value;
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
