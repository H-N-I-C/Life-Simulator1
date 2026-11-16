using System.Collections.Generic;

namespace Game
{
	public class DataOutdoorBikeRideDumb
	{
		private int HeroMinAge__BackingField;

		private int HeroMaxAge__BackingField;

		private string Scenario__BackingField;

		private string CTA__BackingField;

		private List<DataOutdoorBikeRideDumbChoice> ChoiceList__BackingField;

		public int HeroMinAge
		{
			get
			{
				return HeroMinAge__BackingField;
			}
			set
			{
				HeroMinAge__BackingField = value;
			}
		}

		public int HeroMaxAge
		{
			get
			{
				return HeroMaxAge__BackingField;
			}
			set
			{
				HeroMaxAge__BackingField = value;
			}
		}

		public string Scenario
		{
			get
			{
				return Scenario__BackingField;
			}
			set
			{
				Scenario__BackingField = value;
			}
		}

		public string CTA
		{
			get
			{
				return CTA__BackingField;
			}
			set
			{
				CTA__BackingField = value;
			}
		}

		public List<DataOutdoorBikeRideDumbChoice> ChoiceList
		{
			get
			{
				return ChoiceList__BackingField;
			}
			set
			{
				ChoiceList__BackingField = value;
			}
		}
	}
}
