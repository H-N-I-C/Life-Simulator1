using System.Collections.Generic;

namespace Game
{
	public class DataZooHabitatFeedbackColor
	{
		private string String__BackingField;

		private bool IsPositive__BackingField;

		private int CommonnessScore__BackingField;

		private List<string> FeatureBlacklist__BackingField;

		private List<string> FeatureWhitelist__BackingField;

		public string String
		{
			get
			{
				return String__BackingField;
			}
			set
			{
				String__BackingField = value;
			}
		}

		public bool IsPositive
		{
			get
			{
				return IsPositive__BackingField;
			}
			set
			{
				IsPositive__BackingField = value;
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

		public List<string> FeatureBlacklist
		{
			get
			{
				return FeatureBlacklist__BackingField;
			}
			set
			{
				FeatureBlacklist__BackingField = value;
			}
		}

		public List<string> FeatureWhitelist
		{
			get
			{
				return FeatureWhitelist__BackingField;
			}
			set
			{
				FeatureWhitelist__BackingField = value;
			}
		}
	}
}
