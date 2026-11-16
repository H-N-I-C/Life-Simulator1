namespace Game.Genesis.BluePrints
{
	public class BluePrintDefendLawsuit : IBluePrint
	{
		private int _intValue;

		private string _stringValue;

		private float _floatValue;

		private string _reason;

		private string _who;

		private float _damages;

		private string _endFlow;

		private BluePrintData _data;

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
