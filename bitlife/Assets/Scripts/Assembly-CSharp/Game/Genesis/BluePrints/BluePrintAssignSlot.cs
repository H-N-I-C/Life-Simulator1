namespace Game.Genesis.BluePrints
{
	public class BluePrintAssignSlot : IBluePrint, ISuppressFollowUp, IAbortBlueprintChainOnFailure
	{
		private const string ClearSentinel = "-";

		private string _root;

		private string _referenceProperty;

		private string _entityRef;

		private bool _isClear;

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

		public string Root => null;

		public string SlotPath => null;

		public string ReferenceProperty => null;

		public string EntityRef => null;

		public bool IsClear => false;

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

		private string ResolveEntityRef(string value)
		{
			return null;
		}
	}
}
