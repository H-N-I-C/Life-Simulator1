using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Backend;
using CandyBusiness.Confidence.Dependencies;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CandyBusiness.Confidence
{
	public static class ConfidenceIntegration
	{
		private enum ConfidenceInitState
		{
			Idle = 0,
			Initializing = 1,
			Ready = 2,
			Failed = 3
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CAwaitInternetReachability_003Ed__42 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public CancellationToken token;

			private Awaitable.Awaiter _003C_003Eu__1;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CGateThenResolveAsync_003Ed__25 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			private CancellationToken _003Ctoken_003E5__2;

			private bool _003Cloaded_003E5__3;

			private Awaitable<bool>.Awaiter _003C_003Eu__1;

			private Awaitable.Awaiter _003C_003Eu__2;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CResolveOnceAsync_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public CancellationToken token;

			private float _003CretryDelaySeconds_003E5__2;

			private int _003Cattempt_003E5__3;

			private Task<bool> _003Crequest_003E5__4;

			private Task _003Ccompleted_003E5__5;

			private Awaitable.Awaiter _003C_003Eu__1;

			private TaskAwaiter<Task> _003C_003Eu__2;

			private MainThreadAwaitable _003C_003Eu__3;

			private TaskAwaiter _003C_003Eu__4;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CWaitForAttributeDependenciesAsync_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public ConfidenceDependencySet dependencies;

			public CancellationToken token;

			private Task _003Cwait_003E5__2;

			private TaskAwaiter<Task> _003C_003Eu__1;

			private MainThreadAwaitable _003C_003Eu__2;

			private TaskAwaiter _003C_003Eu__3;

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

		private static ConfidenceSnapshot Snapshot;

		private static ConfidenceFileAutoSaver FileSaver;

		private static ConfidenceInitState InitState;

		private static CancellationTokenSource InitCts;

		private static ConfidenceDependencySet Dependencies;

		private static bool StartupGateCompleted;

		private const int MaxResolveAttempts = 1;

		private const float ResolveTimeoutSeconds = 30f;

		private const float InitialResolveRetrySeconds = 2f;

		private const float MaxResolveRetrySeconds = 60f;

		private const float DependencyRefreshIntervalSeconds = 1f;

		private static ConfidenceRuntime Runtime;

		private static string CachePath__BackingField;

		private static bool IsCacheEncrypted__BackingField;

		public static string CachePath
		{
			get
			{
				return CachePath__BackingField;
			}
			private set
			{
				CachePath__BackingField = value;
			}
		}

		public static bool IsCacheEncrypted
		{
			get
			{
				return IsCacheEncrypted__BackingField;
			}
			private set
			{
				IsCacheEncrypted__BackingField = value;
			}
		}

		static ConfidenceIntegration()
		{
		}

		public static string SerializeSnapshot()
		{
			return null;
		}

		public static string SerializeProperties()
		{
			return null;
		}

		private static void Initialize()
		{
		}

		private static void GateThenResolveAsync()
		{
		}

		private static Awaitable WaitForAttributeDependenciesAsync(ConfidenceDependencySet dependencies, CancellationToken token)
		{
			return null;
		}

		private static Awaitable ResolveOnceAsync(CancellationToken token)
		{
			return null;
		}

		public static void Release()
		{
		}

		private static void HandleApplicationQuitting()
		{
		}

		private static void RefreshFromTick()
		{
		}

		private static void OnAppsFlyerValidationProcess(Product product)
		{
		}

		private static void OnAccountServerControllerLoginSuccess()
		{
		}

		public static void InvalidatePlayerContext()
		{
		}

		private static void OnApplicationFocusChanged(bool focused)
		{
		}

		private static void RetryInitialization()
		{
		}

		private static void OnUniversalPostPurchaseActivities()
		{
		}

		private static void OnCatalogReceived()
		{
		}

		private static void OnWebShopControllerSetRedeemed(TransactionItemModel transaction)
		{
		}

		private static void OnWebShopRestoredPurchase(TransactionItemModel transaction)
		{
		}

		private static void OnConnectorServiceBaseConsumeRestoreItems(List<TransactionItemModel> transactions)
		{
		}

		private static void IAPManagerOnOnProductPurchased(Product product, DateTime timePurchased)
		{
		}

		private static Awaitable AwaitInternetReachability(CancellationToken token)
		{
			return null;
		}

		private static void SubscribeSaveCallbacks()
		{
		}

		private static void UnsubscribeSaveCallbacks()
		{
		}

		private static void ThrowIfInitStateInvalid(CancellationToken token)
		{
		}

		private static void SaveSnapshot(ConfidencePurchaseReceipt newReceipt)
		{
		}

		private static void SaveSnapshot(IEnumerable<ConfidencePurchaseReceipt> newReceipts = null)
		{
		}

		public static void SetFlags()
		{
		}
	}
}
