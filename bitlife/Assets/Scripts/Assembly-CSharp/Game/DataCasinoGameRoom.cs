namespace Game
{
	public class DataCasinoGameRoom
	{
		private CASINO_GAME_TYPE Game__BackingField;

		private int InstallationCost__BackingField;

		private string UnitString__BackingField;

		private int BaseUnitCapacity__BackingField;

		private int CostPerUnit__BackingField;

		private int DefaultHouseWinRate__BackingField;

		private int DefaultBet__BackingField;

		private int MinBet__BackingField;

		private int MaxBet__BackingField;

		public CASINO_GAME_TYPE Game
		{
			get
			{
				return Game__BackingField;
			}
			set
			{
				Game__BackingField = value;
			}
		}

		public int InstallationCost
		{
			get
			{
				return InstallationCost__BackingField;
			}
			set
			{
				InstallationCost__BackingField = value;
			}
		}

		public string UnitString
		{
			get
			{
				return UnitString__BackingField;
			}
			set
			{
				UnitString__BackingField = value;
			}
		}

		public int BaseUnitCapacity
		{
			get
			{
				return BaseUnitCapacity__BackingField;
			}
			set
			{
				BaseUnitCapacity__BackingField = value;
			}
		}

		public int CostPerUnit
		{
			get
			{
				return CostPerUnit__BackingField;
			}
			set
			{
				CostPerUnit__BackingField = value;
			}
		}

		public int DefaultHouseWinRate
		{
			get
			{
				return DefaultHouseWinRate__BackingField;
			}
			set
			{
				DefaultHouseWinRate__BackingField = value;
			}
		}

		public int DefaultBet
		{
			get
			{
				return DefaultBet__BackingField;
			}
			set
			{
				DefaultBet__BackingField = value;
			}
		}

		public int MinBet
		{
			get
			{
				return MinBet__BackingField;
			}
			set
			{
				MinBet__BackingField = value;
			}
		}

		public int MaxBet
		{
			get
			{
				return MaxBet__BackingField;
			}
			set
			{
				MaxBet__BackingField = value;
			}
		}
	}
}
