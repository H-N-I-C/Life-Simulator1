using System;
using Backend;
using UnityEngine.Purchasing.Security;

namespace CandyBusiness.Confidence
{
	public class ConfidencePurchaseReceipt : IPurchaseReceipt
	{
		private readonly string transactionID__BackingField;

		private readonly string productID__BackingField;

		private readonly DateTime purchaseDate__BackingField;

		private readonly PurchaseMarketplace Marketplace__BackingField;

		private readonly decimal? Price__BackingField;

		private readonly string Currency__BackingField;

		public string transactionID => transactionID__BackingField;

		public string productID => productID__BackingField;

		public DateTime purchaseDate => purchaseDate__BackingField;

		public PurchaseMarketplace Marketplace => Marketplace__BackingField;

		public decimal? Price => Price__BackingField;

		public string Currency => Currency__BackingField;

		private ConfidencePurchaseReceipt(string transactionId, string productId, DateTime purchaseDate, PurchaseMarketplace marketplace, decimal? price, string currency)
		{
		}

		public ConfidencePurchaseReceipt(string transactionId, string productId, DateTime purchaseDate)
		{
		}

		public static ConfidencePurchaseReceipt FromTransaction(TransactionItemModel product)
		{
			return null;
		}

		public static ConfidencePurchaseReceipt FromReceipt(IPurchaseReceipt receipt)
		{
			return null;
		}
	}
}
