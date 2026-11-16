namespace Game.Genesis.BluePrints
{
	public class BluePrintBankBalance : IBluePrint, IAbortBlueprintChainOnFailure
	{
		private int _intValue;

		private string _stringValue;

		private readonly bool _resolvedAdjustmentMode;

		private BluePrintData _data;

		private bool _adjustmentApplied;

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

		public bool ResolvedAdjustmentMode => false;

		public string RawAdjustment => null;

		public BluePrintBankBalance(bool resolvedAdjustmentMode = false)
		{
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

		private bool TryResolveAdjustment(out double adjustment)
		{
			adjustment = default;
			return false;
		}
	}
}
