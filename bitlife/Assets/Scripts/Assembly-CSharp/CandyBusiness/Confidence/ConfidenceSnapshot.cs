using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using CandyBusiness.Confidence.Properties;
using Game;
using UnityEngine;

namespace CandyBusiness.Confidence
{
	public class ConfidenceSnapshot : IDisposable
	{
		[Serializable]
		public class TrackedReceipt
		{
			public string transactionId;

			public string productId;

			public DateTime purchaseDate;

			public string price;

			public string marketplace;

			public string currency;

			public bool TryGetPrice(out float value)
			{
				value = default;
				return false;
			}
		}

		public class LocalState
		{
			[Serializable]
			private sealed class _003C_003Ec
			{
				public static readonly _003C_003Ec _003C_003E9;

				public static Comparison<TrackedReceipt> _003C_003E9__21_0;

				public static Func<ConfidenceAttributeName, string> _003C_003E9__30_0;

				internal int _003CReconcileReceipts_003Eb__21_0(TrackedReceipt a, TrackedReceipt b)
				{
					return 0;
				}

				internal string _003CPruneUnknownKeys_003Eb__30_0(ConfidenceAttributeName n)
				{
					return null;
				}
			}

			public const int CurrentVersion = 1;

			public const string PriceUnavailableCode = "N/A";

			private DateTime lastSynced__BackingField;

			private Dictionary<string, object> attributes__BackingField;

			private Dictionary<string, object> flags__BackingField;

			private List<TrackedReceipt> trackedReceipts__BackingField;

			public int version => 0;

			public DateTime lastSynced
			{
				get
				{
					return lastSynced__BackingField;
				}
				protected set
				{
					lastSynced__BackingField = value;
				}
			}

			public Dictionary<string, object> attributes
			{
				get
				{
					return attributes__BackingField;
				}
				protected set
				{
					attributes__BackingField = value;
				}
			}

			public Dictionary<string, object> flags
			{
				get
				{
					return flags__BackingField;
				}
				protected set
				{
					flags__BackingField = value;
				}
			}

			public List<TrackedReceipt> trackedReceipts
			{
				get
				{
					return trackedReceipts__BackingField;
				}
				protected set
				{
					trackedReceipts__BackingField = value;
				}
			}

			public List<TrackedReceipt> GetTrackedReceipts()
			{
				return null;
			}

			public int ReconcileReceipts(IEnumerable<ConfidencePurchaseReceipt> receipts)
			{
				return 0;
			}

			private static string ReceiptKey(TrackedReceipt receipt)
			{
				return null;
			}

			public void SyncAttributes(IEnumerable<ConfidenceAttribute> source)
			{
			}

			public int ApplyFlags(IEnumerable<IConfidenceProperty> properties)
			{
				return 0;
			}

			public bool TryGet<T>(string key, out T value)
			{
				value = default;
				return false;
			}

			public T Get<T>(string key, T defaultValue = default(T))
			{
				return default;
			}

			public T GetPropertyValue<T>(string key, T defaultValue = default(T))
			{
				return default;
			}

			private static bool TryConvertCachedValue<T>(object value, out T result)
			{
				result = default;
				return false;
			}

			public void MarkSynced()
			{
			}

			public void PruneUnknownKeys()
			{
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action _003C_003E9__8_2;

			public static Action _003C_003E9__8_3;

			public static Action _003C_003E9__8_4;

			public static Action _003C_003E9__8_5;

			public static Action _003C_003E9__8_6;

			public static Action _003C_003E9__8_7;

			public static Action _003C_003E9__8_8;

			public static Action _003C_003E9__8_9;

			public static Action _003C_003E9__8_10;

			public static Action _003C_003E9__8_12;

			public static Func<MarketPlacePurchaseInfo, IEnumerable<string>> _003C_003E9__14_0;

			public static Func<MarketPlacePurchaseInfo, bool> _003C_003E9__14_1;

			public static Func<MarketPlacePurchaseInfo, bool> _003C_003E9__14_2;

			internal void _003CRefreshAttributes_003Eb__8_2()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_3()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_4()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_5()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_6()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_7()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_8()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_9()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_10()
			{
			}

			internal void _003CRefreshAttributes_003Eb__8_12()
			{
			}

			internal IEnumerable<string> _003CRefreshTransactions_003Eb__14_0(MarketPlacePurchaseInfo i)
			{
				return null;
			}

			internal bool _003CRefreshTransactions_003Eb__14_1(MarketPlacePurchaseInfo i)
			{
				return false;
			}

			internal bool _003CRefreshTransactions_003Eb__14_2(MarketPlacePurchaseInfo i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public IEnumerable<ConfidencePurchaseReceipt> newReceipts;

			public int receiptsAdded;

			public ConfidenceSnapshot _003C_003E4__this;

			public GameVars gameVars;

			public MonetizationVars monetizationVars;

			internal void _003CRefreshAttributes_003Eb__0()
			{
			}

			internal void _003CRefreshAttributes_003Eb__1()
			{
			}

			internal void _003CRefreshAttributes_003Eb__11()
			{
			}

			internal void _003CRefreshAttributes_003Eb__13()
			{
			}

			internal void _003CRefreshAttributes_003Eb__14()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CLoadRoutine_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder<bool> _003C_003Et__builder;

			public int maxRetries;

			public int retryDelayMs;

			public ConfidenceSnapshot _003C_003E4__this;

			public string path;

			private CancellationToken _003Ctoken_003E5__2;

			private bool _003Csuccess_003E5__3;

			private int _003Cattempt_003E5__4;

			private LocalState _003Cstate_003E5__5;

			private bool _003CioFailed_003E5__6;

			private BackgroundThreadAwaitable _003C_003Eu__1;

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

		private const string AppsFlyerIdPlayerPrefsKey = "AppsFlyerID";

		private LocalState State__BackingField;

		private readonly CancellationTokenSource cts;

		private const string Platform = "iOS";

		public LocalState State
		{
			get
			{
				return State__BackingField;
			}
			protected set
			{
				State__BackingField = value;
			}
		}

		public int HydrateAttributes()
		{
			return 0;
		}

		public Dictionary<string, object> BuildEvaluationContext()
		{
			return null;
		}

		public int RefreshAttributes(IEnumerable<ConfidencePurchaseReceipt> newReceipts = null)
		{
			return 0;
		}

		private static void Collect(string name, Action collect)
		{
		}

		private int ReconcileHistoricalReceipts()
		{
			return 0;
		}

		private static bool IsWebShopStateKnown()
		{
			return false;
		}

		private static void RefreshInstallTime()
		{
		}

		private static void RefreshSeason()
		{
		}

		private void RefreshTransactions()
		{
		}

		public bool TrySerialize(out string json)
		{
			json = null;
			return false;
		}

		public Awaitable<bool> LoadAsync(string path, int maxRetries = 3, int retryDelayMs = 500)
		{
			return null;
		}

		private Awaitable<bool> LoadRoutine(string path, int maxRetries, int retryDelayMs)
		{
			return null;
		}

		public void Dispose()
		{
		}
	}
}
