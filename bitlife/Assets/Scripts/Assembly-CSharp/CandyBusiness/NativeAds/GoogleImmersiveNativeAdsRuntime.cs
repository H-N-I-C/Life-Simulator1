using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public sealed class GoogleImmersiveNativeAdsRuntime : INativeAdsProviderRuntime
	{
		private sealed class _003C_003Ec__DisplayClass11_0
		{
			public GoogleImmersiveNativeAdsRuntime _003C_003E4__this;

			public MonoBehaviour host;

			public Action<INativeAd> onNativeAdLoaded;

			public bool retryLoad;

			public NativeAdsDataSO data;

			public Action _003C_003E9__3;

			public Action _003C_003E9__2;

			public Action _003C_003E9__1;

			internal void _003CInitialize_003Eb__0(InitializationStatus _)
			{
			}

			internal void _003CInitialize_003Eb__1()
			{
			}

			internal void _003CInitialize_003Eb__2()
			{
			}

			internal void _003CInitialize_003Eb__3()
			{
			}
		}

		private sealed class _003CInitializeAdUnits_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GoogleImmersiveNativeAdsRuntime _003C_003E4__this;

			public NativeAdsDataSO data;

			public Action<INativeAd> onNativeAdLoaded;

			public bool retryLoad;

			private int _003Ccounter_003E5__2;

			private float _003CdelaySeconds_003E5__3;

			private IEnumerator<string> _003C_003E7__wrap3;

			private string _003CadUnit_003E5__5;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CInitializeAdUnits_003Ed__12(int _003C_003E1__state)
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

		private sealed class _003CSuperviseLoaders_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GoogleImmersiveNativeAdsRuntime _003C_003E4__this;

			public MonoBehaviour host;

			private HashSet<GoogleImmersiveNativeAdsLoader> _003Cwatched_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSuperviseLoaders_003Ed__18(int _003C_003E1__state)
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

		private sealed class _003CWatchForStuckAdUnit_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GoogleImmersiveNativeAdsRuntime _003C_003E4__this;

			public GoogleImmersiveNativeAdsLoader loader;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWatchForStuckAdUnit_003Ed__19(int _003C_003E1__state)
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

		private readonly List<GoogleImmersiveNativeAdsLoader> _loaders;

		private readonly IReadOnlyList<string> _adUnits;

		private const float StuckAdUnitWatchdogSeconds = 45f;

		private const float StuckWatchdogPollSeconds = 1f;

		private bool _watchdogStarted;

		private bool _isShutdown;

		public NativeAdsProvider Provider => NativeAdsProvider.GoogleImmersive;

		public string[] AdTypes => null;

		public string DefaultAdType => null;

		public GoogleImmersiveNativeAdsRuntime(IReadOnlyList<string> adUnits = null)
		{
		}

		public bool IsSupportedAtRuntime()
		{
			return false;
		}

		public void Initialize(MonoBehaviour host, Action<INativeAd> onNativeAdLoaded, bool retryLoad, NativeAdsDataSO data = null)
		{
		}

		private IEnumerator InitializeAdUnits(MonoBehaviour host, Action<INativeAd> onNativeAdLoaded, bool retryLoad, NativeAdsDataSO data)
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

		private IEnumerator WatchForStuckAdUnit(GoogleImmersiveNativeAdsLoader loader)
		{
			return null;
		}

		private void HandleError()
		{
		}

		public void Shutdown()
		{
		}
	}
}
