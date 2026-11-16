namespace Game.Genesis.BluePrints
{
	public sealed class BluePrintImpactNpcRelationship : IBluePrint
	{
		private BluePrintData _data;

		private string _entityKey;

		private string _operand;

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
