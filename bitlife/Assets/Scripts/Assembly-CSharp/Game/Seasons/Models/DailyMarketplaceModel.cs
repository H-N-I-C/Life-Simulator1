using System;
using System.Collections.Generic;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyMarketplaceModel
	{
		private string CatalogId__BackingField;

		private string StartsAt__BackingField;

		private string EndsAt__BackingField;

		private long TotalCoins__BackingField;

		private List<DailyMarketplaceOfferModel> Offers__BackingField;

		private Dictionary<string, int> RedeemedItems__BackingField;

		private string ConfigVersion__BackingField;

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

		public string StartsAt
		{
			get
			{
				return StartsAt__BackingField;
			}
			set
			{
				StartsAt__BackingField = value;
			}
		}

		public string EndsAt
		{
			get
			{
				return EndsAt__BackingField;
			}
			set
			{
				EndsAt__BackingField = value;
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

		public List<DailyMarketplaceOfferModel> Offers
		{
			get
			{
				return Offers__BackingField;
			}
			set
			{
				Offers__BackingField = value;
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
