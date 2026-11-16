namespace Game.Genesis.BluePrints
{
	public class BluePrintAddToGfcCollection : IBluePrint, ISuppressFollowUp, IAbortBlueprintChainOnFailure
	{
		private string _containerId;

		private string _collectionName;

		private string _entityOperand;

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

		public string EntityType => null;

		public string EntityOperand => null;

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

		private string ResolveEntityOperand(string value)
		{
			return null;
		}

		private static bool ContainsResolutionSyntax(string value)
		{
			return false;
		}

		private static bool ContainsUnresolvedSyntax(string value)
		{
			return false;
		}
	}
}
