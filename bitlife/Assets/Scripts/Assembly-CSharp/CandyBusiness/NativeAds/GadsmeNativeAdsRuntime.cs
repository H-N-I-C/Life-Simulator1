using System;
using System.Collections;
using System.Collections.Generic;
using Gadsme;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public sealed class GadsmeNativeAdsRuntime : INativeAdsProviderRuntime
	{
		private sealed class _003CInitializePlacements_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public NativeAdsDataSO data;

			public GadsmeNativeAdsRuntime _003C_003E4__this;

			public Action<INativeAd> onNativeAdLoaded;

			public bool retryLoad;

			private IReadOnlyList<GadsmeNativeRequest> _003CrowRequests_003E5__2;

			private float _003CdelaySeconds_003E5__3;

			private IEnumerator<GadsmeNativeRequest> _003C_003E7__wrap3;

			private GadsmeNativeRequest _003CrowRequest_003E5__5;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CInitializePlacements_003Ed__26(int _003C_003E1__state)
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

			private void _003C_003Em__Finally1()
			{
			}

			void IEnumerator.Reset()
			{
			}
		}

		private sealed class _003CSuperviseLoaders_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GadsmeNativeAdsRuntime _003C_003E4__this;

			public MonoBehaviour host;

			private HashSet<GadsmeNativeAdsLoader> _003Cwatched_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSuperviseLoaders_003Ed__28(int _003C_003E1__state)
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

		private sealed class _003CWatchForStuckRow_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GadsmeNativeAdsRuntime _003C_003E4__this;

			public GadsmeNativeAdsLoader loader;

			private float _003CelapsedSeconds_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWatchForStuckRow_003Ed__29(int _003C_003E1__state)
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

		private static readonly string[] _adTypes;

		private readonly List<GadsmeNativeAdsLoader> _loaders;

		private readonly List<(GadsmeNativeAdsLoader, GadsmeNativeRequest)> _watchTargets;

		private bool _watchdogStarted;

		private bool _isShutdown;

		private readonly GadsmeImpressionReporter _impressionReporter;

		private static int _activeImpressionListenerCount;

		private static bool _gadsmeSdkInitialized;

		private bool _isImpressionSubscribed;

		private string _adType;

		private static int _mainThreadId;

		private const float StuckRowWatchdogSeconds = 45f;

		private const float StuckRowRecoveryPollSeconds = 5f;

		public NativeAdsProvider Provider => NativeAdsProvider.GoogleImmersive;

		public string[] AdTypes => null;

		public string DefaultAdType => null;

		public bool IsSupportedAtRuntime()
		{
			return false;
		}

		public void Initialize(MonoBehaviour host, Action<INativeAd> onNativeAdLoaded, bool retryLoad, NativeAdsDataSO data = null)
		{
		}

		private static void InitializeSdk()
		{
		}

		private void SubscribeToImpressionEvent()
		{
		}

		private void UnsubscribeFromImpressionEvent()
		{
		}

		private void HandleAdRequestEvent(GadsmeAdRequestData requestData)
		{
		}

		private void HandleImpressionEvent(GadsmeImpressionData impressionData)
		{
		}

		private IEnumerator InitializePlacements(MonoBehaviour host, Action<INativeAd> onNativeAdLoaded, bool retryLoad, NativeAdsDataSO data)
		{
			return null;
		}

		public void StartStuckWatchdog(MonoBehaviour host)
		{
		}

		private IEnumerator SuperviseLoaders(MonoBehaviour host)
		{
			return null;
		}

		private IEnumerator WatchForStuckRow(GadsmeNativeAdsLoader loader, GadsmeNativeRequest rowRequest)
		{
			return null;
		}

		public void DisposeLoadedRows()
		{
		}

		public void Shutdown()
		{
		}
	}
}
