using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameAnalyticsSDK.Wrapper
{
	public class GA_Wrapper
	{
		[PreserveSig]
		private static extern void configureAvailableCustomDimensions01(string list);

		[PreserveSig]
		private static extern void configureAvailableCustomDimensions02(string list);

		[PreserveSig]
		private static extern void configureAvailableCustomDimensions03(string list);

		[PreserveSig]
		private static extern void configureAvailableResourceCurrencies(string list);

		[PreserveSig]
		private static extern void configureAvailableResourceItemTypes(string list);

		[PreserveSig]
		private static extern void configureSdkGameEngineVersion(string unitySdkVersion);

		[PreserveSig]
		private static extern void configureGameEngineVersion(string unityEngineVersion);

		[PreserveSig]
		private static extern void configureBuild(string build);

		[PreserveSig]
		private static extern void configureUserId(string userId);

		[PreserveSig]
		private static extern void configureExternalUserId(string userId);

		[PreserveSig]
		private static extern void configureAutoDetectAppVersion(bool flag);

		[PreserveSig]
		private static extern void gaInitialize(string gamekey, string gamesecret, bool nativeErrorReporting);

		[PreserveSig]
		private static extern void setCustomDimension01(string customDimension);

		[PreserveSig]
		private static extern void setCustomDimension02(string customDimension);

		[PreserveSig]
		private static extern void setCustomDimension03(string customDimension);

		[PreserveSig]
		private static extern void setGlobalCustomEventFields(string customFields);

		[PreserveSig]
		private static extern void addBusinessEvent(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addBusinessEventAndAutoFetchReceipt(string currency, int amount, string itemType, string itemId, string cartType, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addResourceEvent(int flowType, string currency, float amount, string itemType, string itemId, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addProgressionEvent(int progressionStatus, string progression01, string progression02, string progression03, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addProgressionEventWithScore(int progressionStatus, string progression01, string progression02, string progression03, int score, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addDesignEvent(string eventId, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addDesignEventWithValue(string eventId, float value, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addErrorEvent(int severity, string message, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addAdEventWithDuration(int adAction, int adType, string adSdkName, string adPlacement, long duration, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addAdEventWithReason(int adAction, int adType, string adSdkName, string adPlacement, int noAdReason, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addAdEvent(int adAction, int adType, string adSdkName, string adPlacement, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void addImpressionEvent(string adNetworkName, string adNetworkVersion, string impressionData, string fields, bool mergeFields);

		[PreserveSig]
		private static extern void setEnabledInfoLog(bool enabled);

		[PreserveSig]
		private static extern void setEnabledVerboseLog(bool enabled);

		[PreserveSig]
		private static extern void setManualSessionHandling(bool enabled);

		[PreserveSig]
		private static extern void setEventSubmission(bool enabled);

		[PreserveSig]
		private static extern void setEventSubmission(bool enabled, bool doCache);

		[PreserveSig]
		private static extern void gameAnalyticsStartSession();

		[PreserveSig]
		private static extern void gameAnalyticsEndSession();

		[PreserveSig]
		private static extern string getRemoteConfigsValueAsString(string key, string defaultValue);

		[PreserveSig]
		private static extern bool isRemoteConfigsReady();

		[PreserveSig]
		private static extern string getRemoteConfigsContentAsString();

		[PreserveSig]
		private static extern string getRemoteConfigsContentAsJSON();

		[PreserveSig]
		private static extern string getABTestingId();

		[PreserveSig]
		private static extern string getABTestingVariantId();

		[PreserveSig]
		private static extern void startTimer(string key);

		[PreserveSig]
		private static extern void pauseTimer(string key);

		[PreserveSig]
		private static extern void resumeTimer(string key);

		[PreserveSig]
		private static extern long stopTimer(string key);

		[PreserveSig]
		public static extern string getUserId();

		[PreserveSig]
		public static extern string getExternalUserId();

		[PreserveSig]
		public static extern void useRandomizedId(bool flag);

		[PreserveSig]
		public static extern void enableSDKInitEvent(bool flag);

		[PreserveSig]
		public static extern void enableFpsHistogram(bool flag);

		[PreserveSig]
		public static extern void enableMemoryHistogram(bool flag);

		[PreserveSig]
		public static extern void enableHealthHardwareInfo(bool flag);

		private static void initialize(string gamekey, string gamesecret)
		{
		}

		public static void SetAvailableCustomDimensions01(string list)
		{
		}

		public static void SetAvailableCustomDimensions02(string list)
		{
		}

		public static void SetAvailableCustomDimensions03(string list)
		{
		}

		public static void SetAvailableResourceCurrencies(string list)
		{
		}

		public static void SetAvailableResourceItemTypes(string list)
		{
		}

		public static void SetUnitySdkVersion(string unitySdkVersion)
		{
		}

		public static void SetUnityEngineVersion(string unityEngineVersion)
		{
		}

		public static void SetBuild(string build)
		{
		}

		public static void SetCustomUserId(string userId)
		{
		}

		public static void SetEnabledManualSessionHandling(bool enabled)
		{
		}

		public static void SetEnabledEventSubmission(bool enabled)
		{
		}

		public static void SetEnabledEventSubmission(bool enabled, bool doCache)
		{
		}

		public static void SetAutoDetectAppVersion(bool flag)
		{
		}

		public static void StartSession()
		{
		}

		public static void EndSession()
		{
		}

		public static void Initialize(string gamekey, string gamesecret)
		{
		}

		public static void SetCustomDimension01(string customDimension)
		{
		}

		public static void SetCustomDimension02(string customDimension)
		{
		}

		public static void SetCustomDimension03(string customDimension)
		{
		}

		public static void SetGlobalCustomEventFields(IDictionary<string, object> customFields)
		{
		}

		public static void AddBusinessEvent(string currency, int amount, string itemType, string itemId, string cartType, string receipt, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddBusinessEventAndAutoFetchReceipt(string currency, int amount, string itemType, string itemId, string cartType, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddResourceEvent(GAResourceFlowType flowType, string currency, float amount, string itemType, string itemId, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddProgressionEventWithScore(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddDesignEvent(string eventID, float eventValue, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddDesignEvent(string eventID, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddErrorEvent(GAErrorSeverity severity, string message, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddAdEventWithDuration(GAAdAction adAction, GAAdType adType, string adSdkName, string adPlacement, long duration, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddAdEventWithReason(GAAdAction adAction, GAAdType adType, string adSdkName, string adPlacement, GAAdError noAdReason, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void AddAdEvent(GAAdAction adAction, GAAdType adType, string adSdkName, string adPlacement, IDictionary<string, object> fields, bool mergeFields)
		{
		}

		public static void SetInfoLog(bool enabled)
		{
		}

		public static void SetVerboseLog(bool enabled)
		{
		}

		public static string GetRemoteConfigsValueAsString(string key, string defaultValue)
		{
			return null;
		}

		public static bool IsRemoteConfigsReady()
		{
			return false;
		}

		public static string GetRemoteConfigsContentAsString()
		{
			return null;
		}

		public static string GetRemoteConfigsContentAsJSON()
		{
			return null;
		}

		public static string GetABTestingId()
		{
			return null;
		}

		public static string GetABTestingVariantId()
		{
			return null;
		}

		public static void SetExternalUserId(string userId)
		{
		}

		public static string GetExternalUserId()
		{
			return null;
		}

		private static string DictionaryToJsonString(IDictionary<string, object> dict)
		{
			return null;
		}

		public static void StartTimer(string key)
		{
		}

		public static void PauseTimer(string key)
		{
		}

		public static void ResumeTimer(string key)
		{
		}

		public static long StopTimer(string key)
		{
			return 0L;
		}

		public static void EnableSDKInitEvent(bool flag)
		{
		}

		public static void EnableFpsHistogram(bool flag)
		{
		}

		public static void EnableMemoryHistogram(bool flag)
		{
		}

		public static void EnableHealthHardwareInfo(bool flag)
		{
		}
	}
}
