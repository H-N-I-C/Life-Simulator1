namespace Game
{
	public class DataFightOutcomeColor
	{
		private string HeroWinVersion__BackingField;

		private string HeroLoseVersion__BackingField;

		private int CommonnessScore__BackingField;

		public string HeroWinVersion
		{
			get
			{
				return HeroWinVersion__BackingField;
			}
			set
			{
				HeroWinVersion__BackingField = value;
			}
		}

		public string HeroLoseVersion
		{
			get
			{
				return HeroLoseVersion__BackingField;
			}
			set
			{
				HeroLoseVersion__BackingField = value;
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
