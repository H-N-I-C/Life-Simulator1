namespace Game
{
	public class DataOutdoorVehicle
	{
		private string Make__BackingField;

		private string Model__BackingField;

		private RECREATIONAL_VEHICLE_TYPE Type__BackingField;

		private int Cost__BackingField;

		private int PerformanceScore__BackingField;

		private int DepreciationScore__BackingField;

		private int LongevityScore__BackingField;

		private int CoolScore__BackingField;

		private int EaseofTheftScore__BackingField;

		public string Make
		{
			get
			{
				return Make__BackingField;
			}
			set
			{
				Make__BackingField = value;
			}
		}

		public string Model
		{
			get
			{
				return Model__BackingField;
			}
			set
			{
				Model__BackingField = value;
			}
		}

		public RECREATIONAL_VEHICLE_TYPE Type
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

		public int Cost
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

		public int PerformanceScore
		{
			get
			{
				return PerformanceScore__BackingField;
			}
			set
			{
				PerformanceScore__BackingField = value;
			}
		}

		public int DepreciationScore
		{
			get
			{
				return DepreciationScore__BackingField;
			}
			set
			{
				DepreciationScore__BackingField = value;
			}
		}

		public int LongevityScore
		{
			get
			{
				return LongevityScore__BackingField;
			}
			set
			{
				LongevityScore__BackingField = value;
			}
		}

		public int CoolScore
		{
			get
			{
				return CoolScore__BackingField;
			}
			set
			{
				CoolScore__BackingField = value;
			}
		}

		public int EaseofTheftScore
		{
			get
			{
				return EaseofTheftScore__BackingField;
			}
			set
			{
				EaseofTheftScore__BackingField = value;
			}
		}
	}
}
