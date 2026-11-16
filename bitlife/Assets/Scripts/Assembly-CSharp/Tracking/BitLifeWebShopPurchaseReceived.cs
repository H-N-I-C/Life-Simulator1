using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeWebShopPurchaseReceived : IGameTelemetryEvent
	{
		private string WebShopPurchasedProduct__BackingField;

		public string WebShopPurchasedProduct
		{
			get
			{
				return WebShopPurchasedProduct__BackingField;
			}
			set
			{
				WebShopPurchasedProduct__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
