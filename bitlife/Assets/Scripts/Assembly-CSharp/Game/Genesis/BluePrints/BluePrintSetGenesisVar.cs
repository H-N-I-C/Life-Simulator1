namespace Game.Genesis.BluePrints
{
	public class BluePrintSetGenesisVar : IBluePrint, ISuppressFollowUp
	{
		private string _varName;

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

		public string VarName => null;

		public string Value => null;

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

		private string ResolveValue(string value)
		{
			return null;
		}
	}
}
