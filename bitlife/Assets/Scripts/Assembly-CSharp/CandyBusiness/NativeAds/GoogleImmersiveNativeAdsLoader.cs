using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;

namespace CandyBusiness.NativeAds
{
	public class GoogleImmersiveNativeAdsLoader
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action _003C_003E9__27_0;

			public static Func<KeyValuePair<string, string>, string> _003C_003E9__29_1;

			internal void _003CHandleAdImpression_003Eb__27_0()
			{
			}

			internal string _003CHandleAdImpressionPaid_003Eb__29_1(KeyValuePair<string, string> kvp)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public ImmersiveInGameDisplayAdEventArgs args;

			internal void _003CDiscardLoadedAd_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass28_0
		{
			public GoogleImmersiveNativeAdsLoader _003C_003E4__this;

			public ImmersiveInGameDisplayAdEventArgs args;

			internal void _003CHandleImmersiveInGameDisplayAdLoaded_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass29_0
		{
			public AdValue data;

			public GoogleImmersiveNativeAdsLoader _003C_003E4__this;

			internal void _003CHandleAdImpressionPaid_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass31_0
		{
			public GoogleImmersiveNativeAdsLoader _003C_003E4__this;

			public LoadAdError error;

			internal void _003CHandleAdLoadFailed_003Eb__0()
			{
			}
		}

		private readonly AdLoader _adLoader;

		private readonly string _adUnit;

		private readonly Action<INativeAd> _onLoadComplete;

		private readonly bool _retryLoad;

		private static bool _reportActiveForThisSession;

		private int _progressTimerValue;

		private int _retryAttempt;

		private int _retriesIssued;

		private bool _isDisposed;

		private float _retryDueAtRealtime;

		private bool _isLoaded;

		private bool HasReceivedCallback__BackingField;

		public bool IsRetryDue => false;

		public bool HasReceivedCallback
		{
			get
			{
				return HasReceivedCallback__BackingField;
			}
			private set
			{
				HasReceivedCallback__BackingField = value;
			}
		}

		public string AdUnit => null;

		public void RetryNow()
		{
		}

		public GoogleImmersiveNativeAdsLoader(NativeAdsDataSO nativeAdsDataSo, Action<INativeAd> onLoadComplete, Action onError, string adUnitId, bool retryLoad)
		{
		}

		private void RequestAd()
		{
		}

		public void Dispose()
		{
		}

		private static void DiscardLoadedAd(object sender, ImmersiveInGameDisplayAdEventArgs args)
		{
		}

		private static void IgnoreLoadFailure(LoadAdError error)
		{
		}

		private static void IgnoreAdEvent(object sender, EventArgs args)
		{
		}

		private static void IgnorePaidEvent(AdValue value)
		{
		}

		private void HandleAdImpression(object sender, EventArgs e)
		{
		}

		private void HandleImmersiveInGameDisplayAdLoaded(object sender, ImmersiveInGameDisplayAdEventArgs args)
		{
		}

		private void HandleAdImpressionPaid(AdValue data)
		{
		}

		private void HandleAdClick(object sender, EventArgs args)
		{
		}

		private void HandleAdLoadFailed(LoadAdError error)
		{
		}

		private void HandleAdLoadFailedOnMainThread(LoadAdError error)
		{
		}

		private void LogAdError(LoadAdError error)
		{
		}
	}
}
