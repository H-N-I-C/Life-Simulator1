namespace Game.Dynamic.Marketplace
{
	public class MarketPlaceEntry
	{
		private MARKTPLACE_TYPE Category__BackingField;

		private IAP_TYPE IapType__BackingField;

		private bool IsBundle__BackingField;

		private bool IsTieredIap__BackingField;

		private MARKTPLACE_ITEMS Item__BackingField;

		private bool IsHidden__BackingField;

		private string CustomLabel__BackingField;

		private object ExtraData__BackingField;

		private int TimeInSeconds__BackingField;

		public MARKTPLACE_TYPE Category
		{
			get
			{
				return Category__BackingField;
			}
			set
			{
				Category__BackingField = value;
			}
		}

		public IAP_TYPE IapType
		{
			get
			{
				return IapType__BackingField;
			}
			set
			{
				IapType__BackingField = value;
			}
		}

		public bool IsBundle
		{
			get
			{
				return IsBundle__BackingField;
			}
			set
			{
				IsBundle__BackingField = value;
			}
		}

		public bool IsTieredIap
		{
			get
			{
				return IsTieredIap__BackingField;
			}
			set
			{
				IsTieredIap__BackingField = value;
			}
		}

		public MARKTPLACE_ITEMS Item
		{
			get
			{
				return Item__BackingField;
			}
			set
			{
				Item__BackingField = value;
			}
		}

		public bool IsHidden
		{
			get
			{
				return IsHidden__BackingField;
			}
			set
			{
				IsHidden__BackingField = value;
			}
		}

		public string CustomLabel
		{
			get
			{
				return CustomLabel__BackingField;
			}
			set
			{
				CustomLabel__BackingField = value;
			}
		}

		public object ExtraData
		{
			get
			{
				return ExtraData__BackingField;
			}
			set
			{
				ExtraData__BackingField = value;
			}
		}

		public int TimeInSeconds
		{
			get
			{
				return TimeInSeconds__BackingField;
			}
			set
			{
				TimeInSeconds__BackingField = value;
			}
		}
	}
}
