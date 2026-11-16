using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BitLife.UI.Containers;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public class NativeAdsController : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<INativeAd, bool> _003C_003E9__53_0;

			public static Func<ListItem, bool> _003C_003E9__57_0;

			public static Func<INativeAd, bool> _003C_003E9__57_1;

			internal bool _003CSetParentAndShow_003Eb__53_0(INativeAd t)
			{
				return false;
			}

			internal bool _003CProcessArrayList_003Eb__57_0(ListItem t)
			{
				return false;
			}

			internal bool _003CProcessArrayList_003Eb__57_1(INativeAd t)
			{
				return false;
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003C_003Ec__DisplayClass49_0
		{
			public INativeAdsProviderRuntime nativeAdsProvider;
		}

		private sealed class _003C_003Ec__DisplayClass58_0
		{
			public string title;

			internal bool _003CIsFilterList_003Eb__0(string t)
			{
				return false;
			}
		}

		private sealed class _003CWaitForConfidence_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public NativeAdsController _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWaitForConfidence_003Ed__43(int _003C_003E1__state)
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

		public const string ForcePeriodicInterstitialAdsKey = "NativeAds_ForcePeriodicInterstitialAds";

		private const string LastProviderPlayerPrefsKey = "NativeAdsController_LastProvider";

		private const float ConfidenceStartupTimeoutSeconds = 30f;

		private static readonly List<string> _filterLists;

		private static NativeAdsController Instance__BackingField;

		private static bool _listSwitchIsOn;

		private static bool _retryLoad;

		private static int _itemsFirstRow;

		private static int _listMinItems;

		private static int _listEveryNItems;

		private static int _listRowHeight;

		private static int _minutesForAds;

		private static int _listCooldownTime;

		private static int _listProbabilityToShow;

		private static bool _configApplied;

		private static bool _runtimeInitialized;

		private static bool _adReady;

		private static bool _statusIsActive;

		private static bool _statusIsDisplaying;

		private static bool _providerRuntimeStartedOnce;

		private static string _configuredAdType;

		private static int _configuredRowHeight;

		private static NativeAdsProvider _configuredProvider;

		private static string _adType;

		private static NativeAdsDataSO _currentAdType;

		private static NativeAdsProvider _nativeAdsProvider;

		private static bool _coolDownActive;

		private static readonly List<INativeAd> InGameAd;

		private static readonly List<INativeAd> AssignedItems;

		private static readonly Dictionary<GameObject, List<INativeAd>> AssignedAdsByHost;

		private INativeAdsProviderRuntime _providerRuntime;

		private IReadOnlyList<string> _configuredAdUnits;

		private bool _configurationReady;

		private Coroutine _confidenceWait;

		private static string AdTypePlayerPrefKey => null;

		public static NativeAdsController Instance
		{
			get
			{
				return Instance__BackingField;
			}
			private set
			{
				Instance__BackingField = value;
			}
		}

		public static NativeAdsController Create()
		{
			return null;
		}

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private static void ResetSessionState()
		{
		}

		private IEnumerator WaitForConfidence()
		{
			return null;
		}

		private void OnConfidenceResolutionCompleted(bool succeeded)
		{
		}

		private void OnImmersiveUpdated(bool adUnitsChanged)
		{
		}

		private static void ResolveConfiguredProvider(NativeAdsProvider confidenceProvider)
		{
		}

		private void StartProviderRuntime()
		{
		}

		private void RestartProviderRuntime()
		{
		}

		private static void ApplyAdType(INativeAdsProviderRuntime nativeAdsProvider, string newAdType)
		{
		}

		private static string GetProviderResourceKey(NativeAdsProvider provider)
		{
			return null;
		}

		private void HandleNativeAdLoaded(INativeAd nativeAd)
		{
		}

		private static void RepopulateEmptyLiveAdTiles()
		{
		}

		public static void SetParentAndShow(GameObject parent)
		{
		}

		private static void LogAttachedAdRenderState(GameObject parent, float appliedScale)
		{
		}

		public static void PrepareList()
		{
		}

		public static void ReleaseAdsFor(GameObject host)
		{
		}

		public static ArrayList ProcessArrayList(List data)
		{
			return null;
		}

		private static bool IsFilterList(string title)
		{
			return false;
		}

		public static void ResetList(UIList list)
		{
		}

		private static bool IsActive()
		{
			return false;
		}

		private static void UpdateDisplayingStatus(string source)
		{
		}

		private static bool IsProviderSupportedAtRuntime()
		{
			return false;
		}

		public static void SetCoolDown()
		{
		}

		private void MonoSetCoolDown()
		{
		}

		private void CoolDownEnd()
		{
		}

		private static bool ExperiencedEnoughForImmersiveAds()
		{
			return false;
		}

		private static int GetRequiredPooledAdsPerRow()
		{
			return 0;
		}

		internal static void _003CApplyAdType_003Eg__ResolveAdType_007C49_0(ref string input, ref _003C_003Ec__DisplayClass49_0 P_1)
		{
		}

		internal static void _003CApplyAdType_003Eg__SyncRowHeightWithCurrentAdType_007C49_1()
		{
		}

		internal static NativeAdsDataSO _003CApplyAdType_003Eg__GetAdType_007C49_2(NativeAdsProvider provider, string adType, string defaultAdType)
		{
			return null;
		}
	}
}
