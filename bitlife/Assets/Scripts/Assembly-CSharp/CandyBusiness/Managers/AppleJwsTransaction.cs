using System;
using Newtonsoft.Json.Linq;
using UnityEngine.Purchasing.Security;

namespace CandyBusiness.Managers
{
	public sealed class AppleJwsTransaction : IPurchaseReceipt
	{
		private string transactionID__BackingField;

		private string productID__BackingField;

		private DateTime purchaseDate__BackingField;

		private DateTime? RevocationDate__BackingField;

		public string transactionID
		{
			get
			{
				return transactionID__BackingField;
			}
			private set
			{
				transactionID__BackingField = value;
			}
		}

		public string productID
		{
			get
			{
				return productID__BackingField;
			}
			private set
			{
				productID__BackingField = value;
			}
		}

		public DateTime purchaseDate
		{
			get
			{
				return purchaseDate__BackingField;
			}
			private set
			{
				purchaseDate__BackingField = value;
			}
		}

		public DateTime? RevocationDate
		{
			get
			{
				return RevocationDate__BackingField;
			}
			private set
			{
				RevocationDate__BackingField = value;
			}
		}

		private AppleJwsTransaction()
		{
		}

		public static bool TryParse(string jws, out AppleJwsTransaction transaction)
		{
			transaction = null;
			return false;
		}

		private static DateTime? ReadDate(JToken token)
		{
			return null;
		}

		private static byte[] DecodeBase64Url(string input)
		{
			return null;
		}
	}
}
