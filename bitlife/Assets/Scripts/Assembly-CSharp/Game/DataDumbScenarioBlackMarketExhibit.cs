using System.Collections;

namespace Game
{
	public class DataDumbScenarioBlackMarketExhibit : DataDumbScenario
	{
		private bool HeroHasLover__BackingField;

		private int LoverGender__BackingField;

		private int MinCondition__BackingField;

		private int MaxCondition__BackingField;

		private ArrayList ExhibitCategory__BackingField;

		public bool HeroHasLover
		{
			get
			{
				return HeroHasLover__BackingField;
			}
			set
			{
				HeroHasLover__BackingField = value;
			}
		}

		public int LoverGender
		{
			get
			{
				return LoverGender__BackingField;
			}
			set
			{
				LoverGender__BackingField = value;
			}
		}

		public int MinCondition
		{
			get
			{
				return MinCondition__BackingField;
			}
			set
			{
				MinCondition__BackingField = value;
			}
		}

		public int MaxCondition
		{
			get
			{
				return MaxCondition__BackingField;
			}
			set
			{
				MaxCondition__BackingField = value;
			}
		}

		public ArrayList ExhibitCategory
		{
			get
			{
				return ExhibitCategory__BackingField;
			}
			set
			{
				ExhibitCategory__BackingField = value;
			}
		}
	}
}
