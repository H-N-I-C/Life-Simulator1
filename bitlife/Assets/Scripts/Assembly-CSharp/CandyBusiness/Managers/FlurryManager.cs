using System.Collections.Generic;
using FlurrySDK;
using UnityEngine.Purchasing;

namespace CandyBusiness.Managers
{
	public class FlurryManager
	{
		public static Flurry.EventRecordStatus LastEventLoggingStatus;

		public static bool startedUp;

		private static string FLURRY_API_KEY;

		public static void StartFlurry(APP_PLATFORM _platform, bool _devMode)
		{
		}

		public static void StopFlurry()
		{
		}

		public static void LogEvent(string eventName, APP_PLATFORM _platform, bool _devMode, Dictionary<string, string> eventParameters = null)
		{
		}

		public static void LogStandardEventAdClick()
		{
		}

		public static void LogStandardEventLevelStart()
		{
		}

		public static void LogStandardEventLevelUp()
		{
		}

		public static void LogStandardEventLevelCompleted()
		{
		}

		public static void LogStandardEventPurchaseEventForProduct(Product aProduct, decimal adjustedPrice)
		{
		}

		public static void SetFlurryKey(APP_PLATFORM _platform, bool _devMode)
		{
		}

		private static void SetAndroidFlurryKey(bool _devMode)
		{
		}

		private static void SetIOSFlurryKey(bool _devMode)
		{
		}
	}
}
