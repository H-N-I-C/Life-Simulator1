using System.Runtime.InteropServices;
using UnityEngine;

public class IronSourceAdQuality : CodeGeneratedSingleton
{
	private static GameObject adQualityGameObject;

	private const string TAG = "IronSource AdQuality";

	private const bool DEBUG = false;

	protected override bool DontDestroySingleton => false;

	[PreserveSig]
	private static extern int ironSourceAdQuality_initialize(string appKey, string userId, bool userIdSet, bool testMode, bool debug, int logLevel, string initializationSource, bool coppa, int deviceIdType, bool isInitCallbackSet);

	[PreserveSig]
	private static extern int ironSourceAdQuality_changeUserId(string userId);

	[PreserveSig]
	private static extern int ironSourceAdQuality_setUserConsent(bool userConsent);

	[PreserveSig]
	private static extern int ironSourceAdQuality_sendCustomMediationRevenue(int mediationNetwork, int adType, string placement, double revenue);

	[PreserveSig]
	private static extern int ironSourceAdQuality_setSegment(string jsonString);

	protected override void InitAfterRegisteringAsSingleInstance()
	{
	}

	public static void Initialize(string appKey)
	{
	}

	public static void Initialize(string appKey, ISAdQualityConfig adQualityConfig)
	{
	}

	private static void Initialize(string appKey, string userId, bool userIdSet, bool testMode, ISAdQualityLogLevel logLevel, string initializationSource, bool coppa, ISAdQualityDeviceIdType deviceIdType, ISAdQualityInitCallback adQualityInitCallback)
	{
	}

	public static void ChangeUserId(string userId)
	{
	}

	public static void SetUserConsent(bool userConsent)
	{
	}

	public static void SendCustomMediationRevenue(ISAdQualityCustomMediationRevenue customMediationRevenue)
	{
	}

	public static void setSegment(ISAdQualitySegment segment)
	{
	}
}
