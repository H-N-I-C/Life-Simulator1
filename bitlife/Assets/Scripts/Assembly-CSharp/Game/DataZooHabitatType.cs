namespace Game
{
	public class DataZooHabitatType
	{
		private string Type__BackingField;

		private bool AddHabitat__BackingField;

		private float Cost__BackingField;

		private int AnnualUpkeepFees__BackingField;

		public string Type
		{
			get
			{
				return Type__BackingField;
			}
			set
			{
				Type__BackingField = value;
			}
		}

		public bool AddHabitat
		{
			get
			{
				return AddHabitat__BackingField;
			}
			set
			{
				AddHabitat__BackingField = value;
			}
		}

		public float Cost
		{
			get
			{
				return Cost__BackingField;
			}
			set
			{
				Cost__BackingField = value;
			}
		}

		public int AnnualUpkeepFees
		{
			get
			{
				return AnnualUpkeepFees__BackingField;
			}
			set
			{
				AnnualUpkeepFees__BackingField = value;
			}
		}
	}
}
