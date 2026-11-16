using System.Collections.Generic;

namespace Backend.Authentication
{
	public class WebShopItem
	{
		private sealed class _003C_003Ec__DisplayClass16_0
		{
			public bool isUS;

			public int appPlatform;

			internal bool _003CGetWebShopSettings_003Eb__0(WebShopSettingsItem t)
			{
				return false;
			}
		}

		public string ShopUrl;

		public string ShopUrlAndroid;

		public int RolloutUSiOS;

		public int RolloutOthers;

		public int RolloutUSAndroid;

		public List<string> AllowedRegions;

		public Dictionary<string, string> AliasOverride;

		public List<string> DisableIAPAndroid;

		public List<string> DisableIAPiOS;

		public string MinVersion;

		public string MaxVersion;

		public string MaxVersionAndroid;

		public bool BannerEnabledUS;

		public List<WebShopSettingsItem> WebShopUsSettings;

		public Dictionary<string, string> BundleOverride;

		public string OfferApiVersion;

		public WebShopSettingsItem GetWebShopSettings(bool isUS)
		{
			return null;
		}

		public bool IsValidGameVersion(string version)
		{
			return false;
		}

		public bool IsIAPDisabledForWebshop(IAP_TYPE iapType)
		{
			return false;
		}

		public bool IsIAPDisabledForWebshop(string bundle)
		{
			return false;
		}

		public string GetWebShopUrl()
		{
			return null;
		}

		public string GetCatalogVersion()
		{
			return null;
		}
	}
}
