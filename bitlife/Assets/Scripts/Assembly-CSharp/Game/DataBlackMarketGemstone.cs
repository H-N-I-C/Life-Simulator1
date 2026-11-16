namespace Game
{
	public class DataBlackMarketGemstone : DataBlackMarketBase
	{
		private string Gemstone__BackingField;

		private int MinCarats__BackingField;

		private int MaxCarats__BackingField;

		private int SuperMaxCarats__BackingField;

		private int FirstCaratPrice__BackingField;

		private bool BlackMarketOnly__BackingField;

		private bool StorageUnitOnly__BackingField;

		public string Gemstone
		{
			get
			{
				return Gemstone__BackingField;
			}
			set
			{
				Gemstone__BackingField = value;
			}
		}

		public int MinCarats
		{
			get
			{
				return MinCarats__BackingField;
			}
			set
			{
				MinCarats__BackingField = value;
			}
		}

		public int MaxCarats
		{
			get
			{
				return MaxCarats__BackingField;
			}
			set
			{
				MaxCarats__BackingField = value;
			}
		}

		public int SuperMaxCarats
		{
			get
			{
				return SuperMaxCarats__BackingField;
			}
			set
			{
				SuperMaxCarats__BackingField = value;
			}
		}

		public int FirstCaratPrice
		{
			get
			{
				return FirstCaratPrice__BackingField;
			}
			set
			{
				FirstCaratPrice__BackingField = value;
			}
		}

		public bool BlackMarketOnly
		{
			get
			{
				return BlackMarketOnly__BackingField;
			}
			set
			{
				BlackMarketOnly__BackingField = value;
			}
		}

		public bool StorageUnitOnly
		{
			get
			{
				return StorageUnitOnly__BackingField;
			}
			set
			{
				StorageUnitOnly__BackingField = value;
			}
		}
	}
}
