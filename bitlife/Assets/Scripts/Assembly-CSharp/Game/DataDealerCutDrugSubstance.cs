namespace Game
{
	public class DataDealerCutDrugSubstance
	{
		private string Substance__BackingField;

		private int CommonnessScore__BackingField;

		private int MinCostPerDose__BackingField;

		private int MaxCostPerDose__BackingField;

		private int DemandImpact__BackingField;

		private string Effect__BackingField;

		private bool IsFatal__BackingField;

		public string Substance
		{
			get
			{
				return Substance__BackingField;
			}
			set
			{
				Substance__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
			}
		}

		public int MinCostPerDose
		{
			get
			{
				return MinCostPerDose__BackingField;
			}
			set
			{
				MinCostPerDose__BackingField = value;
			}
		}

		public int MaxCostPerDose
		{
			get
			{
				return MaxCostPerDose__BackingField;
			}
			set
			{
				MaxCostPerDose__BackingField = value;
			}
		}

		public int DemandImpact
		{
			get
			{
				return DemandImpact__BackingField;
			}
			set
			{
				DemandImpact__BackingField = value;
			}
		}

		public string Effect
		{
			get
			{
				return Effect__BackingField;
			}
			set
			{
				Effect__BackingField = value;
			}
		}

		public bool IsFatal
		{
			get
			{
				return IsFatal__BackingField;
			}
			set
			{
				IsFatal__BackingField = value;
			}
		}
	}
}
