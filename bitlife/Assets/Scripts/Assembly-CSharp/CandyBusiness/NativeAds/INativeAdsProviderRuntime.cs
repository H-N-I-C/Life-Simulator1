using System;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public interface INativeAdsProviderRuntime
	{
		NativeAdsProvider Provider { get; }

		string[] AdTypes { get; }

		string DefaultAdType { get; }

		bool IsSupportedAtRuntime();

		void Initialize(MonoBehaviour host, Action<INativeAd> onNativeAdLoaded, bool retryLoad, NativeAdsDataSO data = null);

		void Shutdown();

		void StartStuckWatchdog(MonoBehaviour host);
	}
}
