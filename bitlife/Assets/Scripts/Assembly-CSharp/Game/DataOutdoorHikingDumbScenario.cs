using System.Collections.Generic;

namespace Game
{
	public class DataOutdoorHikingDumbScenario
	{
		private int HeroMinAge__BackingField;

		private int HeroMaxAge__BackingField;

		private string GenderBracket__BackingField;

		private bool RequiresCompanion__BackingField;

		private List<OUTDOOR_HIKING_COMPANION> CompanionType__BackingField;

		private bool RandomizeChoices__BackingField;

		private string Title__BackingField;

		private string Scenario__BackingField;

		private string CTA__BackingField;

		private List<DataOutdoorHikingDumbScenarioChoice> Choices__BackingField;

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

		public string GenderBracket
		{
			get
			{
				return GenderBracket__BackingField;
			}
			set
			{
				GenderBracket__BackingField = value;
			}
		}

		public bool RequiresCompanion
		{
			get
			{
				return RequiresCompanion__BackingField;
			}
			set
			{
				RequiresCompanion__BackingField = value;
			}
		}

		public List<OUTDOOR_HIKING_COMPANION> CompanionType
		{
			get
			{
				return CompanionType__BackingField;
			}
			set
			{
				CompanionType__BackingField = value;
			}
		}

		public bool RandomizeChoices
		{
			get
			{
				return RandomizeChoices__BackingField;
			}
			set
			{
				RandomizeChoices__BackingField = value;
			}
		}

		public string Title
		{
			get
			{
				return Title__BackingField;
			}
			set
			{
				Title__BackingField = value;
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

		public List<DataOutdoorHikingDumbScenarioChoice> Choices
		{
			get
			{
				return Choices__BackingField;
			}
			set
			{
				Choices__BackingField = value;
			}
		}
	}
}
