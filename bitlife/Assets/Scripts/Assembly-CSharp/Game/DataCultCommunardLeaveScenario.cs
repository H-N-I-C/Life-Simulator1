namespace Game
{
	public class DataCultCommunardLeaveScenario
	{
		private string ScenarioSnippet__BackingField;

		private CULT_LEAVE_REASON Reason__BackingField;

		private int MinLoyalty__BackingField;

		private int MaxLoyalty__BackingField;

		public string ScenarioSnippet
		{
			get
			{
				return ScenarioSnippet__BackingField;
			}
			set
			{
				ScenarioSnippet__BackingField = value;
			}
		}

		public CULT_LEAVE_REASON Reason
		{
			get
			{
				return Reason__BackingField;
			}
			set
			{
				Reason__BackingField = value;
			}
		}

		public int MinLoyalty
		{
			get
			{
				return MinLoyalty__BackingField;
			}
			set
			{
				MinLoyalty__BackingField = value;
			}
		}

		public int MaxLoyalty
		{
			get
			{
				return MaxLoyalty__BackingField;
			}
			set
			{
				MaxLoyalty__BackingField = value;
			}
		}
	}
}
