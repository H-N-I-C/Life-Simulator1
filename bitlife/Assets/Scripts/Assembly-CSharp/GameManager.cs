using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Backend;
using BitLife.UI.Managers;
using Game;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<int> _003C_003E9__97_0;

		public static Action<int> _003C_003E9__98_0;

		public static Action<int> _003C_003E9__99_0;

		public static Action<int> _003C_003E9__100_0;

		public static Action<int> _003C_003E9__101_0;

		public static Action<int> _003C_003E9__120_0;

		internal void _003COfflineAlert_003Eb__97_0(int index)
		{
		}

		internal void _003CRestorePurchaseSuccess_003Eb__98_0(int index)
		{
		}

		internal void _003CRestorePurchaseNotFound_003Eb__99_0(int index)
		{
		}

		internal void _003CRestorePurchaseError_003Eb__100_0(int index)
		{
		}

		internal void _003CShowPollAlert_003Eb__101_0(int index)
		{
		}

		internal void _003CPurchaseAwaitingApprovalAlert_003Eb__120_0(int index)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass105_0
	{
		public int index;

		public GameManager _003C_003E4__this;

		public int localizedIndex;

		internal void _003CSlotSwitchLifeDelay_003Eb__0()
		{
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass87_0
	{
		public GameManager _003C_003E4__this;

		public SimPersonName personName;

		public int gender;

		public string countryCode;

		public string city;

		public GodModePersonConfig personConfig;
	}

	private sealed class _003C_003Ec__DisplayClass91_0
	{
		public DataCountry aCountry;

		internal bool _003CCreateSavedLifeIfApplicable_003Eb__0(SimPlace x)
		{
			return false;
		}
	}

	private sealed class _003CAnalyticsCallDelay_003Ed__104 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float seconds;

		public string message;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CAnalyticsCallDelay_003Ed__104(int _003C_003E1__state)
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

	private sealed class _003CDelayedAnalyticsCall_003Ed__59 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDelayedAnalyticsCall_003Ed__59(int _003C_003E1__state)
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

	private sealed class _003CDelayedAppTrackingPrompt_003Ed__58 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDelayedAppTrackingPrompt_003Ed__58(int _003C_003E1__state)
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

	private sealed class _003CNewLifeAd_003Ed__108 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CNewLifeAd_003Ed__108(int _003C_003E1__state)
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

	private sealed class _003CRequestChallenge_003Ed__109 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CRequestChallenge_003Ed__109(int _003C_003E1__state)
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

	private sealed class _003CRestoreLostLives_003Ed__102 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CRestoreLostLives_003Ed__102(int _003C_003E1__state)
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

	private sealed class _003CSlotSwitchLifeDelay_003Ed__105 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int index;

		public GameManager _003C_003E4__this;

		public float seconds;

		private _003C_003Ec__DisplayClass105_0 _003C_003E8__1;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSlotSwitchLifeDelay_003Ed__105(int _003C_003E1__state)
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

	private sealed class _003CStaggerLoadExtraSDKs_003Ed__113 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameManager _003C_003E4__this;

		private float _003CdelaySeconds_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CStaggerLoadExtraSDKs_003Ed__113(int _003C_003E1__state)
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

	private sealed class _003CStartServerDelayed_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CStartServerDelayed_003Ed__53(int _003C_003E1__state)
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

	private sealed class _003CSwitchLifeDelay_003Ed__107 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SimChild _child;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSwitchLifeDelay_003Ed__107(int _003C_003E1__state)
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

	private sealed class _003CUnloadUnusedAssetsAfterDelay_003Ed__75 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CUnloadUnusedAssetsAfterDelay_003Ed__75(int _003C_003E1__state)
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

	public static GameManager Inst;

	public const bool IsDevMode = false;

	[NonSerialized]
	public Life Life;

	[NonSerialized]
	public SimHero Hero;

	[NonSerialized]
	public SimPast Past;

	public GameVars GameVars;

	public WrappedVideoController WrappedVideo;

	public MonetizationVars MonetizationVars;

	public int LivesCompleted;

	public int CachedTimeMachineGoBackYears;

	public bool CachedTimeMachineWasInPrison;

	public bool CachedTimeMachineWasDead;

	private DateTime SessionStartDate__BackingField;

	private bool SemaphoreRan__BackingField;

	private bool GDPROnScreen__BackingField;

	private bool DidomiReady__BackingField;

	private bool SDKsInitialized__BackingField;

	private bool PostStartSequence__BackingField;

	[NonSerialized]
	public SimHeirloom TodaysHeirloom;

	[NonSerialized]
	public bool ActivatePrisonEscapeDisguiseMode;

	[NonSerialized]
	public bool ActivateMinesweeperMinePreviewMode;

	public UIManager UIManager;

	private bool forcedRender;

	private bool ecoModeIsOn;

	private float playTime;

	private float startTime;

	private bool sdksStarted;

	private static Action<Life> m_OnLifeLoaded;

	private Coroutine clean;

	private float waitTime;

	private float maxWaitTime;

	public bool DevMode => false;

	public DateTime SessionStartDate
	{
		get
		{
			return SessionStartDate__BackingField;
		}
		set
		{
			SessionStartDate__BackingField = value;
		}
	}

	public bool SemaphoreRan
	{
		get
		{
			return SemaphoreRan__BackingField;
		}
		set
		{
			SemaphoreRan__BackingField = value;
		}
	}

	public bool GDPROnScreen
	{
		get
		{
			return GDPROnScreen__BackingField;
		}
		set
		{
			GDPROnScreen__BackingField = value;
		}
	}

	public bool DidomiReady
	{
		get
		{
			return DidomiReady__BackingField;
		}
		set
		{
			DidomiReady__BackingField = value;
		}
	}

	public bool SDKsInitialized
	{
		get
		{
			return SDKsInitialized__BackingField;
		}
		set
		{
			SDKsInitialized__BackingField = value;
		}
	}

	public bool PostStartSequence
	{
		get
		{
			return PostStartSequence__BackingField;
		}
		set
		{
			PostStartSequence__BackingField = value;
		}
	}

	public static event Action<Life> OnLifeLoaded
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

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private IEnumerator StartServerDelayed()
	{
		return null;
	}

	private void Start()
	{
	}

	private void RegisterEventHandlers()
	{
	}

	private void DidomiError(object sender, object e)
	{
	}

	public void InitializeDidomi()
	{
	}

	public IEnumerator DelayedAppTrackingPrompt()
	{
		return null;
	}

	public IEnumerator DelayedAnalyticsCall()
	{
		return null;
	}

	public void TurnOnEcoMode()
	{
	}

	public void TurnOffEcoMode()
	{
	}

	public void ForceRender()
	{
	}

	private void Update()
	{
	}

	public static string GetHierarchyString(Transform t)
	{
		return null;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
	}

	private void OnApplicationFocus(bool hasFocus)
	{
	}

	private void OnApplicationQuit()
	{
	}

	private void StartSession()
	{
	}

	private void EndSession()
	{
	}

	public void DestroyAndUnloadAsset(UnityEngine.Object obj)
	{
	}

	public void UnloadAsset()
	{
	}

	private IEnumerator UnloadUnusedAssetsAfterDelay()
	{
		return null;
	}

	public void StartVideo_VideoClick()
	{
	}

	public void StartVideo_DismissButtonClick()
	{
	}

	public void StartVideo_FinishedPlaying()
	{
	}

	public void ShowPaywallSpecialCareerPromoWithIAPOrigin(int IAP_Origin, int jobType)
	{
	}

	public void ShowPaywallWithIAPOrigin(int IAP_Origin, int darklyType, Scenario followUpScenario = null, Scenario followUpChainedScenario = null, int jobType = 0, int exPackType = 0)
	{
	}

	public void ShowBitizenshipPaywallWithIAPOrigin(int IAP_Origin)
	{
	}

	public void ShowGodModePaywallWithIAPOrigin(int IAP_Origin)
	{
	}

	public static int AppPlatform()
	{
		return 0;
	}

	public void CollapseAnyOpenListsOnDismiss()
	{
	}

	public void CheckForHeirloom()
	{
	}

	public void CreateNewRandomLife()
	{
	}

	public void CreateNewCustomLife(SimPersonName personName, int gender, string countryCode, string city, GodModePersonConfig personConfig, bool randomCreation, StuffItem tokenLifeItem = null, string startLiveId = "")
	{
	}

	public void CreateNewRepeatLife()
	{
	}

	public void CreateNewGenerationLifeWithChild(SimChild child)
	{
	}

	public void StartLifeAndIsAdEligible(bool adEligible)
	{
	}

	public bool CreateSavedLifeIfApplicable()
	{
		return false;
	}

	public void CheckLifeFileForError()
	{
	}

	public void RequestTimeMachineWithGoBackYears(int years, TIME_MACHINE_ORIGIN origin)
	{
	}

	public void RequestUnlimitedTimeMachineWithGoBackYears(int years, TIME_MACHINE_ORIGIN origin)
	{
	}

	public void UseTimeMachine(Life preloadedDestination = null)
	{
	}

	public void SwitchToTimeMachineLife(Life life)
	{
	}

	public static void OfflineAlert()
	{
	}

	public void RestorePurchaseSuccess()
	{
	}

	public void RestorePurchaseNotFound()
	{
	}

	public void RestorePurchaseError()
	{
	}

	public void ShowPollAlert()
	{
	}

	public IEnumerator RestoreLostLives()
	{
		return null;
	}

	public void DoSlotSwitchLifeDelay(float seconds, int index)
	{
	}

	public IEnumerator AnalyticsCallDelay(float seconds, string message)
	{
		return null;
	}

	private IEnumerator SlotSwitchLifeDelay(float seconds, int index)
	{
		return null;
	}

	public void DoSwitchLifeDelay(SimChild _child)
	{
	}

	private IEnumerator SwitchLifeDelay(SimChild _child)
	{
		return null;
	}

	private IEnumerator NewLifeAd()
	{
		return null;
	}

	private IEnumerator RequestChallenge()
	{
		return null;
	}

	private void GDPRAccept()
	{
	}

	private void PreStartSDKs()
	{
	}

	private void StartSDKs()
	{
	}

	private IEnumerator StaggerLoadExtraSDKs()
	{
		return null;
	}

	public void SDKInitialization()
	{
	}

	public void IOSStartSDKs(string message)
	{
	}

	public void iOSSMSCallback(string result)
	{
	}

	public void iOSEmailCallback(string result)
	{
	}

	public void BannerAdsCheck()
	{
	}

	private void OnLanguageChanged(string newLanguage)
	{
	}

	public void PurchaseAwaitingApprovalAlert()
	{
	}

	private void _003CInitializeDidomi_003Eb__57_0()
	{
	}

	private void _003CCreateNewCustomLife_003Eg__GenerateNewHero_007C87_0(ref _003C_003Ec__DisplayClass87_0 P_0)
	{
	}
}
