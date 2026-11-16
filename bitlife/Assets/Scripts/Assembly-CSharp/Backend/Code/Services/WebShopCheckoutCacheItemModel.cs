using System;

namespace Backend.Code.Services
{
	public class WebShopCheckoutCacheItemModel
	{
		private DateTime LastCheckoutTime__BackingField;

		private string CheckoutUrl__BackingField;

		public DateTime LastCheckoutTime
		{
			get
			{
				return LastCheckoutTime__BackingField;
			}
			set
			{
				LastCheckoutTime__BackingField = value;
			}
		}

		public string CheckoutUrl
		{
			get
			{
				return CheckoutUrl__BackingField;
			}
			set
			{
				CheckoutUrl__BackingField = value;
			}
		}
	}
}
