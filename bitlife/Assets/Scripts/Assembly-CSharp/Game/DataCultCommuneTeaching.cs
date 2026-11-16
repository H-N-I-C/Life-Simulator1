namespace Game
{
	public class DataCultCommuneTeaching
	{
		private string Scenario__BackingField;

		private string Topic__BackingField;

		private CULT_PHILOSOPHY Philosophy__BackingField;

		private int EngagementScore__BackingField;

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

		public string Topic
		{
			get
			{
				return Topic__BackingField;
			}
			set
			{
				Topic__BackingField = value;
			}
		}

		public CULT_PHILOSOPHY Philosophy
		{
			get
			{
				return Philosophy__BackingField;
			}
			set
			{
				Philosophy__BackingField = value;
			}
		}

		public int EngagementScore
		{
			get
			{
				return EngagementScore__BackingField;
			}
			set
			{
				EngagementScore__BackingField = value;
			}
		}
	}
}
