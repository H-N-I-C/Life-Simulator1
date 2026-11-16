using System;
using System.Collections.Generic;
using Backend;
using UnityEngine;

namespace CandyBusiness
{
	public static class BusinessManager
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<int> _003C_003E9__9_0;

			internal void _003CSaveDictionary_003Eb__9_0(int index)
			{
			}
		}

		public static void StartSDK(SDKInteraction _sdkInteraction, APP_PLATFORM platform, bool _devMode)
		{
		}

		public static void StopSDKs()
		{
		}

		public static void LogEvent(SDKInteraction _sdkInteraction, APP_PLATFORM _platform, string eventName, bool _devMode, Dictionary<string, string> eventParameters = null)
		{
		}

		public static bool IsOnline()
		{
			return false;
		}

		public static LoginStatus IsSignedOn()
		{
			return LoginStatus.LoggedOut;
		}

		public static bool AreInAppPurchasesReady()
		{
			return false;
		}

		public static void RunSemaphore(APP_PLATFORM _platform, string _appUrl, string _bundleID, string _appVersion, MonoBehaviour _obj)
		{
		}

		public static void SaveTextFile(string file, string filename)
		{
		}

		public static void LoadTextFile(string filename, out string fileContents)
		{
			fileContents = null;
		}

		public static void SaveDictionary(Dictionary<string, object> theDictionary, string filename)
		{
		}

		public static Dictionary<string, object> LoadDictionary(string filename)
		{
			return null;
		}

		public static void ToggleBannerVisibility(bool _isVisible)
		{
		}

		public static void ShowRewardedAD(Scenario postPlayScenario, string postPlayLog, int origin, Action callback)
		{
		}

		public static void ShowRewardedAD(Scenario postPlayScenario, string postPlayLog, string origin, Action callback)
		{
		}

		public static void ShowInterstitial()
		{
		}

		private static byte[] ObjectToByteArray(object obj)
		{
			return null;
		}

		private static object ByteArrayToObject(byte[] arrBytes)
		{
			return null;
		}
	}
}
