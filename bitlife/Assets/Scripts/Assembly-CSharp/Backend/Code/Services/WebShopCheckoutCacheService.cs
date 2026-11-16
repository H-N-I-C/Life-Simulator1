using System.Collections.Generic;

namespace Backend.Code.Services
{
	public class WebShopCheckoutCacheService
	{
		private const string CheckoutUrlKey = "checkout_url.cache";

		private Dictionary<string, WebShopCheckoutCacheItemModel> _cache;

		private readonly string _pathUrl;

		private void Load()
		{
		}

		public string GetLastCheckoutUrl(string bundleId)
		{
			return null;
		}

		private void Save()
		{
		}

		public void SetLastCheckoutUrl(string bundleId, string checkoutUrlData)
		{
		}
	}
}
