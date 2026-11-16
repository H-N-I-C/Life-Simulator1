using System;

namespace Backend
{
	public class TransactionItemModel
	{
		public string TransactionId;

		public string ProductId;

		public string Title;

		public string Currency;

		public int Price;

		public string CreatedAt;

		private string IsRedeemed__BackingField;

		public string IsRedeemed
		{
			get
			{
				return IsRedeemed__BackingField;
			}
			set
			{
				IsRedeemed__BackingField = value;
			}
		}

		public DateTime GetDateTime()
		{
			return default;
		}

		public bool IsMissingRedeem()
		{
			return false;
		}
	}
}
