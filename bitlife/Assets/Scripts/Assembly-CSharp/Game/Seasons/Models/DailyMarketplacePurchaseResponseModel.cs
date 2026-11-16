using System;
using System.Collections.Generic;
using Backend;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyMarketplacePurchaseResponseModel
	{
		private DailyRewardGrantModel Receipt__BackingField;

		private List<DailyRewardGrantModel> EconomicReceipts__BackingField;

		private DailyStreakResetModel StreakReset__BackingField;

		private List<DailyRentalEntitlementModel> RentalEntitlements__BackingField;

		private bool? ReceiptReplayAvailable__BackingField;

		private long TotalCoins__BackingField;

		private DailyMarketplaceOfferModel Offer__BackingField;

		private Dictionary<string, int> RedeemedItems__BackingField;

		private bool RefreshMyStuff__BackingField;

		private List<StuffItem> MyStuffItems__BackingField;

		private string ConfigVersion__BackingField;

		public DailyRewardGrantModel Receipt
		{
			get
			{
				return Receipt__BackingField;
			}
			set
			{
				Receipt__BackingField = value;
			}
		}

		public List<DailyRewardGrantModel> EconomicReceipts
		{
			get
			{
				return EconomicReceipts__BackingField;
			}
			set
			{
				EconomicReceipts__BackingField = value;
			}
		}

		public DailyStreakResetModel StreakReset
		{
			get
			{
				return StreakReset__BackingField;
			}
			set
			{
				StreakReset__BackingField = value;
			}
		}

		public List<DailyRentalEntitlementModel> RentalEntitlements
		{
			get
			{
				return RentalEntitlements__BackingField;
			}
			set
			{
				RentalEntitlements__BackingField = value;
			}
		}

		public bool? ReceiptReplayAvailable
		{
			get
			{
				return ReceiptReplayAvailable__BackingField;
			}
			set
			{
				ReceiptReplayAvailable__BackingField = value;
			}
		}

		public long TotalCoins
		{
			get
			{
				return TotalCoins__BackingField;
			}
			set
			{
				TotalCoins__BackingField = value;
			}
		}

		public DailyMarketplaceOfferModel Offer
		{
			get
			{
				return Offer__BackingField;
			}
			set
			{
				Offer__BackingField = value;
			}
		}

		public Dictionary<string, int> RedeemedItems
		{
			get
			{
				return RedeemedItems__BackingField;
			}
			set
			{
				RedeemedItems__BackingField = value;
			}
		}

		public bool RefreshMyStuff
		{
			get
			{
				return RefreshMyStuff__BackingField;
			}
			set
			{
				RefreshMyStuff__BackingField = value;
			}
		}

		public List<StuffItem> MyStuffItems
		{
			get
			{
				return MyStuffItems__BackingField;
			}
			set
			{
				MyStuffItems__BackingField = value;
			}
		}

		public string ConfigVersion
		{
			get
			{
				return ConfigVersion__BackingField;
			}
			set
			{
				ConfigVersion__BackingField = value;
			}
		}
	}
}
