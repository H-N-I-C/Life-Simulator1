namespace Game
{
	public class DataSecretAgentMissionCodeName
	{
		private string Adjective__BackingField;

		private int AdjectiveCommonnessScore__BackingField;

		private string Noun__BackingField;

		private int NounCommonnessScore__BackingField;

		public string Adjective
		{
			get
			{
				return Adjective__BackingField;
			}
			set
			{
				Adjective__BackingField = value;
			}
		}

		public int AdjectiveCommonnessScore
		{
			get
			{
				return AdjectiveCommonnessScore__BackingField;
			}
			set
			{
				AdjectiveCommonnessScore__BackingField = value;
			}
		}

		public string Noun
		{
			get
			{
				return Noun__BackingField;
			}
			set
			{
				Noun__BackingField = value;
			}
		}

		public int NounCommonnessScore
		{
			get
			{
				return NounCommonnessScore__BackingField;
			}
			set
			{
				NounCommonnessScore__BackingField = value;
			}
		}
	}
}
