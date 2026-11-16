namespace Game
{
	public class DataVampireFeedingLocation
	{
		private string Location__BackingField;

		private int RiskScore__BackingField;

		public string Location
		{
			get
			{
				return Location__BackingField;
			}
			set
			{
				Location__BackingField = value;
			}
		}

		public int RiskScore
		{
			get
			{
				return RiskScore__BackingField;
			}
			set
			{
				RiskScore__BackingField = value;
			}
		}
	}
}
