using Genesis.FeatureContainers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintGfcModifyProperty : IBluePrint, ISuppressFollowUp
	{
		private string _containerId;

		private string _propertyName;

		private string _operation;

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

		public string PropertyName => null;

		public string Operation => null;

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

		private bool TryResolveOperation(out char operationKind, out int operand)
		{
			operationKind = default;
			operand = default;
			return false;
		}

		private string ResolveOperand(string value)
		{
			return null;
		}

		private bool ApplyOperation(GenesisFeatureContainer container, char operationKind, int operand)
		{
			return false;
		}
	}
}
