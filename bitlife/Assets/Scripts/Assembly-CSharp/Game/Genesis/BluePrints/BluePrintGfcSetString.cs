namespace Game.Genesis.BluePrints
{
	public sealed class BluePrintGfcSetString : IBluePrint, ISuppressFollowUp, IAbortBlueprintChainOnFailure
	{
		private string _containerId;

		private string _propertyName;

		private string _value;

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
	}
}
