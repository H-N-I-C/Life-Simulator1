using System;

namespace Backend
{
	public class BackendStatic
	{
		private static readonly int[] GoogleClientId;

		private static readonly int[] GoogleRedirectUrl;

		private static readonly int[] APIKeyName;

		private static readonly int[] AppleIdPool;

		private static readonly int[] GoogleIdPool;

		private static readonly int[] ConfigUrl;

		private static readonly int[] ReceiptKey;

		public static readonly string CatalogVersionKey;

		public static string GetGoogleClientId()
		{
			return null;
		}

		public static string GetReceiptKey()
		{
			return null;
		}

		public static string GetGoogleUrl()
		{
			return null;
		}

		public static string GetApiId()
		{
			return null;
		}

		public static string GetApplePoolId()
		{
			return null;
		}

		public static string GetGooglePoolId()
		{
			return null;
		}

		public static string GetConfigUrl()
		{
			return null;
		}

		private static string CharString(int[] item)
		{
			return null;
		}

		public static DateTime? GetExpirationDate(string jwtToken)
		{
			return null;
		}

		public static string GetSubField(string jwtToken)
		{
			return null;
		}

		public static string GetJwtField(string field, string jwtToken)
		{
			return null;
		}

		private static string Base64UrlDecode(string input)
		{
			return null;
		}

		public static bool IsJwtValid(string dataJwtToken)
		{
			return false;
		}
	}
}
