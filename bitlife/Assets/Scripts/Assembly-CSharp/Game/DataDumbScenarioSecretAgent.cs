namespace Game
{
	public class DataDumbScenarioSecretAgent : DataDumbScenario
	{
		private bool HeroHasLover__BackingField;

		private int LoverGender__BackingField;

		private bool RequiresRivalAgency__BackingField;

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

		public bool RequiresRivalAgency
		{
			get
			{
				return RequiresRivalAgency__BackingField;
			}
			set
			{
				RequiresRivalAgency__BackingField = value;
			}
		}
	}
}
