namespace Game.Genesis.BluePrints
{
	public class BluePrintRemoveFromGfcCollection : IBluePrint, ISuppressFollowUp
	{
		private string _containerId;

		private string _collectionName;

		private string _entityId;

		private BluePrintData _data;

		private bool _parsed;

		private BluePrintType BluePrintType__BackingField;

		public BluePrintType BluePrintType
		{
			get
			{
				return BluePrintType__BackingField;
			}
			set
			{
				BluePrintType__BackingField = value;
			}
		}

		public string ContainerId => null;

		public string CollectionName => null;

		public string EntityId => null;

		public bool Parsed => false;

		public void Setup(BluePrintData data)
		{
		}

		public bool ConditionSelected()
		{
			return false;
		}

		public Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		private string ResolveEntityId(string value)
		{
			return null;
		}
	}
}
