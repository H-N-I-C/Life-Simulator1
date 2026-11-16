using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AppsFlyerSDK;
using UnityEngine;
using UnityEngine.Purchasing;

public class AppsFlyerManager : MonoBehaviour, IAppsFlyerPurchaseValidation, IAppsFlyerConversionData
{
	private sealed class _003C_003Ec__DisplayClass44_0
	{
		public Product aProduct;

		internal void _003CdidFinishValidateReceipt_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass66_0
	{
		public AppsFlyerManager _003C_003E4__this;

		public string id;

		internal bool _003CIsIAPActiveById_003Eb__0()
		{
			return false;
		}
	}

	private sealed class _003CAdd100ToHero_003Ed__59 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CAdd100ToHero_003Ed__59(int _003C_003E1__state)
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

	[StructLayout((LayoutKind)3)]
	private struct _003CPostPurchaseClientEvents_003Ed__61 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public string id;

		public bool isOriginalPurchase;

		public string transactionId;

		public Action afterPurchase;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public string devKey;

	public string appID;

	public bool isDebug;

	public bool getConversionData;

	public static AppsFlyerManager Inst;

	private static bool IsStarted__BackingField;

	private static bool HasReceivedAttributionData__BackingField;

	private static bool HasReceivedConversionData__BackingField;

	public List<Product> ProductList;

	public bool Purchase;

	public bool WillShowSuccessPrompt;

	private static string AppsFlyerID__BackingField;

	private static readonly AppsFlyerDeferredEventCache _eventCache;

	private static readonly List<string> _pendingConsumableTransactionIds;

	private static Action<Product> m_OnValidationProcess;

	private readonly HashSet<string> _loggedValidationTransactions;

	private static readonly HashSet<string> _validationTransactionIdKeys;

	private const string EventPrefix = "AF_EVENT_KEY_";

	public static bool IsStarted
	{
		get
		{
			return IsStarted__BackingField;
		}
		private set
		{
			IsStarted__BackingField = value;
		}
	}

	public static bool HasReceivedAttributionData
	{
		get
		{
			return HasReceivedAttributionData__BackingField;
		}
		private set
		{
			HasReceivedAttributionData__BackingField = value;
		}
	}

	public static bool HasReceivedConversionData
	{
		get
		{
			return HasReceivedConversionData__BackingField;
		}
		private set
		{
			HasReceivedConversionData__BackingField = value;
		}
	}

	public static DateTime? InstallTime => null;

	public static bool IsFirstSession => false;

	public static string AppsFlyerID
	{
		get
		{
			return AppsFlyerID__BackingField;
		}
		private set
		{
			AppsFlyerID__BackingField = value;
		}
	}

	public static event Action<Product> OnValidationProcess
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

	public void StartSDK(APP_PLATFORM platform)
	{
	}

	private void InitPurchaseConnector(APP_PLATFORM platform)
	{
	}

	public void LogConsumableTransactionIfNeeded(string transactionId)
	{
	}

	private static void FlushPendingConsumableTransactions()
	{
	}

	private static bool IsIOSMajorVersionAtLeast(int major)
	{
		return false;
	}

	public bool ValidateProduct(bool isPurchase)
	{
		return false;
	}

	public bool ValidationProcess(ProductArgs args)
	{
		return false;
	}

	private void HandleRejectedValidation()
	{
	}

	public void RestorePurchaseProcess(ProductArgs args)
	{
	}

	private static string GetAppsFlyerDevKey()
	{
		return null;
	}

	public void didFinishValidateReceipt()
	{
	}

	public void didReceivePurchaseRevenueValidationInfo(string validationInfo)
	{
	}

	public void didReceivePurchaseRevenueError(string error)
	{
	}

	private void LogReceiptValidationResult(string result, string payload)
	{
	}

	private static string TryExtractValidationTransactionId(string payload)
	{
		return null;
	}

	private static string FindValidationTransactionId(object node)
	{
		return null;
	}

	public static void LogOneTimeEvent(string eventName, Dictionary<string, string> eventValues = null)
	{
	}

	public static bool LogEvent(string eventName, Dictionary<string, string> eventValues = null)
	{
		return false;
	}

	public static bool LogEvent(string eventName, bool _devMode, Dictionary<string, string> eventValues = null)
	{
		return false;
	}

	public void onConversionDataSuccess(string conversionData)
	{
	}

	public void onConversionDataFail(string error)
	{
	}

	public void onAppOpenAttribution(string attributionData)
	{
	}

	private IEnumerator Add100ToHero()
	{
		return null;
	}

	public void onAppOpenAttributionFailure(string error)
	{
	}

	public void PostPurchaseClientEvents(string id, bool isOriginalPurchase = true, string transactionId = null, Action afterPurchase = null)
	{
	}

	public void RestorePurchaseEvents(Product aProduct)
	{
	}

	public void RestorePurchaseEventsById(string id)
	{
	}

	public void GiveWebConsumablePurchaseEventsById(string id, string transactionId)
	{
	}

	public void GiveWebPurchaseEventsById(string id)
	{
	}

	public bool IsIAPActiveById(string id)
	{
		return false;
	}

	private bool IsPermanentIAPActiveById(string id)
	{
		return false;
	}
}
