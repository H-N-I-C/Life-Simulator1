using System;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyMarketplaceOfferModel
	{
		private string CatalogId__BackingField;

		private string PermanentItemId__BackingField;

		private bool? ServerCanPurchase__BackingField;

		private string OfferId__BackingField;

		private string ItemId__BackingField;

		private string DisplayName__BackingField;

		private int SortIndex__BackingField;

		private int TrialDurationMinutes__BackingField;

		private int RegularPrice__BackingField;

		private int EffectivePrice__BackingField;

		private bool Discounted__BackingField;

		private int PurchaseCount__BackingField;

		private int MaximumPurchases__BackingField;

		private bool PermanentOwned__BackingField;

		private string ActiveExpiration__BackingField;

		private string RentalWindowId__BackingField;

		private bool RetainedActive__BackingField;

		private string State__BackingField;

		public string CatalogId
		{
			get
			{
				return CatalogId__BackingField;
			}
			set
			{
				CatalogId__BackingField = value;
			}
		}

		public string PermanentItemId
		{
			get
			{
				return PermanentItemId__BackingField;
			}
			set
			{
				PermanentItemId__BackingField = value;
			}
		}

		public bool? ServerCanPurchase
		{
			get
			{
				return ServerCanPurchase__BackingField;
			}
			set
			{
				ServerCanPurchase__BackingField = value;
			}
		}

		public string OfferId
		{
			get
			{
				return OfferId__BackingField;
			}
			set
			{
				OfferId__BackingField = value;
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

		public string DisplayName
		{
			get
			{
				return DisplayName__BackingField;
			}
			set
			{
				DisplayName__BackingField = value;
			}
		}

		public int SortIndex
		{
			get
			{
				return SortIndex__BackingField;
			}
			set
			{
				SortIndex__BackingField = value;
			}
		}

		public int TrialDurationMinutes
		{
			get
			{
				return TrialDurationMinutes__BackingField;
			}
			set
			{
				TrialDurationMinutes__BackingField = value;
			}
		}

		public int RegularPrice
		{
			get
			{
				return RegularPrice__BackingField;
			}
			set
			{
				RegularPrice__BackingField = value;
			}
		}

		public int EffectivePrice
		{
			get
			{
				return EffectivePrice__BackingField;
			}
			set
			{
				EffectivePrice__BackingField = value;
			}
		}

		public bool Discounted
		{
			get
			{
				return Discounted__BackingField;
			}
			set
			{
				Discounted__BackingField = value;
			}
		}

		public int PurchaseCount
		{
			get
			{
				return PurchaseCount__BackingField;
			}
			set
			{
				PurchaseCount__BackingField = value;
			}
		}

		public int MaximumPurchases
		{
			get
			{
				return MaximumPurchases__BackingField;
			}
			set
			{
				MaximumPurchases__BackingField = value;
			}
		}

		public bool PermanentOwned
		{
			get
			{
				return PermanentOwned__BackingField;
			}
			set
			{
				PermanentOwned__BackingField = value;
			}
		}

		public string ActiveExpiration
		{
			get
			{
				return ActiveExpiration__BackingField;
			}
			set
			{
				ActiveExpiration__BackingField = value;
			}
		}

		public string RentalWindowId
		{
			get
			{
				return RentalWindowId__BackingField;
			}
			set
			{
				RentalWindowId__BackingField = value;
			}
		}

		public bool RetainedActive
		{
			get
			{
				return RetainedActive__BackingField;
			}
			set
			{
				RetainedActive__BackingField = value;
			}
		}

		public string State
		{
			get
			{
				return State__BackingField;
			}
			set
			{
				State__BackingField = value;
			}
		}

		public bool IsPermanentlyOwned => false;

		public bool CanPurchase => false;

		public string DisplaySection => null;

		public bool HasActiveTrial(DateTime serverNow)
		{
			return false;
		}

		public bool NeedsExpiryRefresh(DateTime serverNow)
		{
			return false;
		}
	}
}
