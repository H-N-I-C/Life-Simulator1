using System.Collections.Generic;

namespace Game
{
	public class DataLuxuryLuxuryCarSpecialFeature
	{
		private string Feature__BackingField;

		private string ProfileColor__BackingField;

		private long Cost__BackingField;

		private List<string> CompatibleCategory__BackingField;

		private int CommonnessScore__BackingField;

		public string Feature
		{
			get
			{
				return Feature__BackingField;
			}
			set
			{
				Feature__BackingField = value;
			}
		}

		public string ProfileColor
		{
			get
			{
				return ProfileColor__BackingField;
			}
			set
			{
				ProfileColor__BackingField = value;
			}
		}

		public long Cost
		{
			get
			{
				return Cost__BackingField;
			}
			set
			{
				Cost__BackingField = value;
			}
		}

		public List<string> CompatibleCategory
		{
			get
			{
				return CompatibleCategory__BackingField;
			}
			set
			{
				CompatibleCategory__BackingField = value;
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
