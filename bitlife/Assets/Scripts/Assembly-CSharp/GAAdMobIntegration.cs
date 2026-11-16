using System;
using GoogleMobileAds.Api;

public class GAAdMobIntegration
{
	[Serializable]
	public class AdMobImpressionData
	{
		public string adunit_id;

		public string currency;

		public int precision;

		public string adunit_format;

		public string network_class_name;

		public long revenue;
	}

	private sealed class _003C_003Ec__DisplayClass1_0
	{
		public string adUnitId;

		public BannerView ad;

		public Action<string, string> callback;

		internal void _003CListenForImpressions_003Eb__0(AdValue args)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass2_0
	{
		public string adUnitId;

		public InterstitialAd ad;

		public Action<string, string> callback;

		internal void _003CListenForImpressions_003Eb__0(AdValue args)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass3_0
	{
		public string adUnitId;

		public RewardedAd ad;

		public Action<string, string> callback;

		internal void _003CListenForImpressions_003Eb__0(AdValue args)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public string adUnitId;

		public RewardedInterstitialAd ad;

		public Action<string, string> callback;

		internal void _003CListenForImpressions_003Eb__0(AdValue args)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string adUnitId;

		public AppOpenAd ad;

		public Action<string, string> callback;

		internal void _003CListenForImpressions_003Eb__0(AdValue args)
		{
		}
	}

	public static void ListenForImpressions(string adUnitId, BannerView ad, Action<string, string> callback)
	{
	}

	public static void ListenForImpressions(string adUnitId, InterstitialAd ad, Action<string, string> callback)
	{
	}

	public static void ListenForImpressions(string adUnitId, RewardedAd ad, Action<string, string> callback)
	{
	}

	public static void ListenForImpressions(string adUnitId, RewardedInterstitialAd ad, Action<string, string> callback)
	{
	}

	public static void ListenForImpressions(string adUnitId, AppOpenAd ad, Action<string, string> callback)
	{
	}
}
