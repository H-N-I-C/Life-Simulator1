using System.Collections.Generic;

namespace Game
{
	public class DataZooTradeHabitatFeature
	{
		private string Type__BackingField;

		private List<string> CompatibleHabitats__BackingField;

		private float Cost__BackingField;

		private int HappinessScore__BackingField;

		private int CommonnessScore__BackingField;

		public string Type
		{
			get
			{
				return Type__BackingField;
			}
			set
			{
				Type__BackingField = value;
			}
		}

		public List<string> CompatibleHabitats
		{
			get
			{
				return CompatibleHabitats__BackingField;
			}
			set
			{
				CompatibleHabitats__BackingField = value;
			}
		}

		public float Cost
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

		public int HappinessScore
		{
			get
			{
				return HappinessScore__BackingField;
			}
			set
			{
				HappinessScore__BackingField = value;
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
