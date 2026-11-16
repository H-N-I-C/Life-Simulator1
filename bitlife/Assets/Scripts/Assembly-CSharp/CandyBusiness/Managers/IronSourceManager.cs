using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Services.LevelPlay;
using UnityEngine;

namespace CandyBusiness.Managers
{
	public class IronSourceManager : MonoBehaviour
	{
		private sealed class _003CInitializeLevelPlay_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public IronSourceManager _003C_003E4__this;

			private string _003CcurrentAppKey_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CInitializeLevelPlay_003Ed__33(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private sealed class _003CStaggerInitializeAds_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public IronSourceManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStaggerInitializeAds_003Ed__37(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		public const string IS_BANNER_AD_UNIT_ID = "r36muxszo80kn929";

		public const string IS_INTERSTITIAL_AD_UNIT_ID = "axjao6i0yfy19grh";

		public const string IS_REWARDED_AD_UNIT_ID = "uevzp1sybp7s60fu";

		public static string IS_ANDROID_APP_KEY;

		public static string IS_IOS_APP_KEY;

		public static string IS_QUALITY_APP_KEY;

		private static bool _hasCalledInit;

		private static bool _isLevelPlayInitialized;

		private static bool _adsInitializationComplete;

		private LevelPlayInterstitialAd _interstitialAd;

		private LevelPlayRewardedAd _rewardedAd;

		private LevelPlayBannerAd _bannerAd;

		private bool _pendingInterstitialShow;

		private bool _isInterstitialClosing;

		private bool _pendingRewardedShow;

		private string _pendingRewardedPlacement;

		private bool _isBannerToggling;

		public static IronSourceManager Inst;

		private float _delaySeconds;

		private Action m_OnBannerAdClicked;

		public event Action OnBannerAdClicked
		{
			add
			{
			}
			remove
			{
			}
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnAccessibilityModeChanged(bool enabled)
		{
		}

		private void OnDestroy()
		{
		}

		public void StartAdManager()
		{
		}

		private void AddLevelPlayEventListeners()
		{
		}

		private void RemoveLevelPlayEventListeners()
		{
		}

		private void LoadInterstitialAd()
		{
		}

		private void LoadRewardedAd()
		{
		}

		private bool ShouldBlockAdRequests()
		{
			return false;
		}

		[PreserveSig]
		private static extern void WebKitPreWarm();

		private IEnumerator InitializeLevelPlay()
		{
			return null;
		}

		private void TryInitAdQuality()
		{
		}

		private void OnLevelPlayInitFailed(LevelPlayInitError error)
		{
		}

		private void OnLevelPlayInitSuccess(LevelPlayConfiguration configuration)
		{
		}

		private IEnumerator StaggerInitializeAds()
		{
			return null;
		}

		private void InitializeInterstitialAds()
		{
		}

		private void InitializeRewardedAds()
		{
		}

		private void InitializeBannerAds()
		{
		}

		private void PreloadBannerAd()
		{
		}

		public bool InterstitialAdAvailable()
		{
			return false;
		}

		public bool RewardedAdAvailable()
		{
			return false;
		}

		public void ShowInterstitialAd()
		{
		}

		public void StartRewardedAd(int origin, Action callback = null)
		{
		}

		public void StartRewardedAd(string origin, Action callback = null)
		{
		}

		public void ToggleBannerVisibility(bool isVisible)
		{
		}

		public void InterstitialAdLoadedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void InterstitialAdLoadFailedEvent(LevelPlayAdError error)
		{
		}

		public void InterstitialAdDisplayedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void InterstitialAdShowFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error)
		{
		}

		public void InterstitialAdClickedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void InterstitialAdClosedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void RewardedVideoOnAdAvailable(LevelPlayAdInfo adInfo)
		{
		}

		public void RewardedVideoOnAdLoadFailedEvent(LevelPlayAdError error)
		{
		}

		public void RewardedVideoOnAdLoadedEvent(LevelPlayAdInfo info)
		{
		}

		public void RewardedVideoAdOpenedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void RewardedVideoAdClosedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void RewardedVideoAdRewardedEvent(LevelPlayAdInfo adInfo, LevelPlayReward reward)
		{
		}

		public void RewardedVideoAdShowFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error)
		{
		}

		public void RewardedVideoAdClickedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void BannerAdLoadedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void BannerAdLoadFailedEvent(LevelPlayAdError error)
		{
		}

		public void BannerAdClickedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void BannerAdLeftApplicationEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void BannerAdDisplayedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void BannerAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error)
		{
		}

		public void BannerAdExpandedEvent(LevelPlayAdInfo adInfo)
		{
		}

		public void BannerAdCollapsedEvent(LevelPlayAdInfo adInfo)
		{
		}

		private void HandleBannerAdClick()
		{
		}

		private void ImpressionSuccessEvent(LevelPlayImpressionData impressionData)
		{
		}

		private static void LogLevelPlayLoadFailed(string callbackName, string adUnitId, LevelPlayAdError error)
		{
		}

		public static void Log(string message)
		{
		}

		private static string FormatLevelPlayAdError(LevelPlayAdError error)
		{
			return null;
		}

		private void ReloadRewardedVideo()
		{
		}

		private void ReloadRewardedVideoDelay()
		{
		}
	}
}
