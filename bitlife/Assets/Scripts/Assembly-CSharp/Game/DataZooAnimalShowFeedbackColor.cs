using System.Collections.Generic;

namespace Game
{
	public class DataZooAnimalShowFeedbackColor
	{
		private List<string> AnimalBlacklist__BackingField;

		private string String__BackingField;

		private bool Positive__BackingField;

		private string ShowType__BackingField;

		private int CommonnessScore__BackingField;

		public List<string> AnimalBlacklist
		{
			get
			{
				return AnimalBlacklist__BackingField;
			}
			set
			{
				AnimalBlacklist__BackingField = value;
			}
		}

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

		public bool Positive
		{
			get
			{
				return Positive__BackingField;
			}
			set
			{
				Positive__BackingField = value;
			}
		}

		public string ShowType
		{
			get
			{
				return ShowType__BackingField;
			}
			set
			{
				ShowType__BackingField = value;
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
