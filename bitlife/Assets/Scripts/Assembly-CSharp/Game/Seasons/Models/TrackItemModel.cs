namespace Game.Seasons.Models
{
	public class TrackItemModel
	{
		private string RedeemId__BackingField;

		private int Amount__BackingField;

		private int PremiumAmount__BackingField;

		private int BitsRequired__BackingField;

		private string ItemId__BackingField;

		private string ItemIdPremium__BackingField;

		public string RedeemId
		{
			get
			{
				return RedeemId__BackingField;
			}
			set
			{
				RedeemId__BackingField = value;
			}
		}

		public int Amount
		{
			get
			{
				return Amount__BackingField;
			}
			set
			{
				Amount__BackingField = value;
			}
		}

		public int PremiumAmount
		{
			get
			{
				return PremiumAmount__BackingField;
			}
			set
			{
				PremiumAmount__BackingField = value;
			}
		}

		public int BitsRequired
		{
			get
			{
				return BitsRequired__BackingField;
			}
			set
			{
				BitsRequired__BackingField = value;
			}
		}

		public string ItemId
		{
			get
			{
				return ItemId__BackingField;
			}
			set
			{
				ItemId__BackingField = value;
			}
		}

		public string ItemIdPremium
		{
			get
			{
				return ItemIdPremium__BackingField;
			}
			set
			{
				ItemIdPremium__BackingField = value;
			}
		}

		public string GetId(bool isPremium)
		{
			return null;
		}

		public int GetAmount(bool isPremium)
		{
			return 0;
		}
	}
}
