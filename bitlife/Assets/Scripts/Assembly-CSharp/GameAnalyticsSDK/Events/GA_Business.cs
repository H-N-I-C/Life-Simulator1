using System.Collections.Generic;

namespace GameAnalyticsSDK.Events
{
	public static class GA_Business
	{
		public static void NewEvent(string currency, int amount, string itemType, string itemId, string cartType, string receipt, bool autoFetchReceipt, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void NewEvent(string currency, int amount, string itemType, string itemId, string cartType, IDictionary<string, object> fields, bool mergeFields)
		{
		}
	}
}
