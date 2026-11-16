namespace Game.Genesis.BluePrints
{
	public sealed class BluePrintGrantFame : IBluePrint, ISuppressFollowUp, IAbortBlueprintChainOnFailure
	{
		private FAME_TYPE _fameType;

		private bool _payloadIsValid;

		private bool _executed;

		private bool _succeeded;

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

		private void EnsureExecuted()
		{
		}
	}
}
