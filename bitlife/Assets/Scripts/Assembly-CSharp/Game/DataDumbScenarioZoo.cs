using System.Collections.Generic;

namespace Game
{
	public class DataDumbScenarioZoo : DataDumbScenario
	{
		private bool HeroHasLover__BackingField;

		private int LoverGender__BackingField;

		private bool InvolvesAnimal__BackingField;

		private List<string> AnimalWhitelist__BackingField;

		private string RequiresSpecificEmployee__BackingField;

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

		public bool InvolvesAnimal
		{
			get
			{
				return InvolvesAnimal__BackingField;
			}
			set
			{
				InvolvesAnimal__BackingField = value;
			}
		}

		public List<string> AnimalWhitelist
		{
			get
			{
				return AnimalWhitelist__BackingField;
			}
			set
			{
				AnimalWhitelist__BackingField = value;
			}
		}

		public string RequiresSpecificEmployee
		{
			get
			{
				return RequiresSpecificEmployee__BackingField;
			}
			set
			{
				RequiresSpecificEmployee__BackingField = value;
			}
		}
	}
}
