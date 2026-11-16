using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Game;
using UnityEngine;

namespace Backend
{
	public class MyStuffController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<StuffItem, int> _003C_003E9__26_3;

			public static Func<StuffItem, bool> _003C_003E9__26_4;

			public static Action<List<StuffItem>> _003C_003E9__26_0;

			public static Action _003C_003E9__28_0;

			public static Func<StuffItem, bool> _003C_003E9__58_0;

			internal void _003CShowMyStuffMenu_003Eb__26_0(List<StuffItem> result)
			{
			}

			internal int _003CShowMyStuffMenu_003Eb__26_3(StuffItem x)
			{
				return 0;
			}

			internal bool _003CShowMyStuffMenu_003Eb__26_4(StuffItem t)
			{
				return false;
			}

			internal void _003CInit_003Eb__28_0()
			{
			}

			internal bool _003CGetStreakSaverCount_003Eb__58_0(StuffItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public Action<bool, string> finished;

			public StuffItem item;

			internal void _003CStartSpecialLife_003Eb__0(bool result, string text)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetItems_003Eb__2_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass19_0 _003C_003E4__this;

				private (string, bool) _003Cresponse_003E5__2;

				private TaskAwaiter<(string, bool)> _003C_003Eu__1;

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

			public Action<List<StuffItem>> action;

			public string gameVersion;

			public Action _003C_003E9__4;

			public Action _003C_003E9__3;

			internal void _003CGetItems_003Eb__0()
			{
			}

			internal void _003CGetItems_003Eb__1()
			{
			}

			internal Task _003CGetItems_003Eb__2()
			{
				return null;
			}

			internal void _003CGetItems_003Eb__4()
			{
			}

			internal void _003CGetItems_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CUseItem_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass20_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass20_1 _003C_003E8__1;

				private TaskAwaiter<bool> _003C_003Eu__1;

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

			public StuffItem item;

			public Action<MYSTUFF_USE_STATE> action;

			internal Task _003CUseItem_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_1
		{
			public bool success;

			public _003C_003Ec__DisplayClass20_0 CS_0024_003C_003E8__locals1;

			internal void _003CUseItem_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public Action<bool> onComplete;

			internal void _003CExecutePowerUp_003Eb__0(bool lifeResult)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass25_0
		{
			public Action<IMyStuffUseCheck> onComplete;

			public IMyStuffUseCheck check;

			internal void _003CPreExecutePowerUp_003Eb__0(bool result)
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		internal struct _003C_003Ec__DisplayClass26_0
		{
			public List<StuffItem> result;
		}

		private sealed class _003C_003Ec__DisplayClass26_1
		{
			public string currentSeasonId;
		}

		private sealed class _003C_003Ec__DisplayClass26_2
		{
			public KeyValuePair<MYSTUFF_CATEGORY, string> category;

			public _003C_003Ec__DisplayClass26_1 CS_0024_003C_003E8__locals1;

			internal bool _003CShowMyStuffMenu_003Eb__2(StuffItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass28_0
		{
			public int deferredTimer;

			internal void _003CInit_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass30_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CRedeemItem_003Eb__1_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass30_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass30_1 _003C_003E8__1;

				private bool _003Csuccess_003E5__2;

				private string _003Cresult_003E5__3;

				private TaskAwaiter<(bool, string)> _003C_003Eu__1;

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

			public string itemId;

			public string transactionId;

			public Action<MYSTUFF_REDEEM> onComplete;

			public Action _003C_003E9__2;

			internal bool _003CRedeemItem_003Eb__0(FailedProduct t)
			{
				return false;
			}

			internal Task _003CRedeemItem_003Eb__1()
			{
				return null;
			}

			internal void _003CRedeemItem_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass30_1
		{
			public MYSTUFF_REDEEM returnValue;

			public _003C_003Ec__DisplayClass30_0 CS_0024_003C_003E8__locals1;

			internal void _003CRedeemItem_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass31_0
		{
			public string itemId;

			public string transactionId;

			internal bool _003CRemoveQueue_003Eb__0(FailedProduct t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass37_0
		{
			public string id;

			internal bool _003CGetItemById_003Eb__0(StuffItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass39_0
		{
			public DataMyStuffCatalog originalId;

			internal bool _003CGetItemByRedeemId_003Eb__0(StuffItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass42_0
		{
			public Action<MYSTUFF_REDEEM> callback;

			internal void _003CRedeemIap_003Eb__0(MYSTUFF_REDEEM resultRedeem)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass42_1
		{
			public MYSTUFF_REDEEM resultRedeem;

			public _003C_003Ec__DisplayClass42_0 CS_0024_003C_003E8__locals1;

			internal void _003CRedeemIap_003Eb__1(List<StuffItem> newStuff)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass52_0
		{
			public string lifeId;

			internal bool _003CIsCustomLifeLocked_003Eb__0(StuffItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass53_0
		{
			public string contentId;

			internal bool _003CIsContentUnlocked_003Eb__0(StuffItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass54_0
		{
			public string lifeId;

			internal bool _003CGetCustomLifeStatus_003Eb__0(StuffItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass59_0
		{
			public bool onlyTimed;

			internal bool _003CGetItemsCount_003Eg__GetCondition_007C0(StuffItem item)
			{
				return false;
			}
		}

		private sealed class _003CShowList_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public List list;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowList_003Ed__27(int _003C_003E1__state)
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

		private const int TIMER_COUNT = 5;

		private static readonly Regex _regexStartLives;

		public static readonly Dictionary<MYSTUFF_CATEGORY, string> CategoryNames;

		private static bool _markReset;

		private static readonly Dictionary<string, Sprite> _spriteCache;

		private static List<StuffItem> _cached;

		private static bool _isWaitingRedeem;

		private static DateTime _lastDateRefresh;

		private static ConcurrentQueue<FailedProduct> _queueFailed;

		private const string QueueKey = "failed_queue";

		public const string ContentArsonId = "CONTENT_ARSON";

		public const string ContentSeniorCenterId = "CONTENT_SENIOR_CENTER";

		private static int _timedIapExclusionDepth;

		public static DateTime LastDateRefresh => default;

		public static List<StuffItem> Cached => null;

		public static bool RequiredUpdate => false;

		public static void StartSpecialLife(StuffItem item, Action<bool, string> finished)
		{
		}

		public static void GetItems(string gameVersion, Action<List<StuffItem>> action, bool reset = false)
		{
		}

		public static void UseItem(StuffItem item, Action<MYSTUFF_USE_STATE> action)
		{
		}

		private static bool CanUseItem(StuffItem item)
		{
			return false;
		}

		private static IMyStuffUseCheck GetExecuteItem(string itemId)
		{
			return null;
		}

		public static void ExecutePowerUp(IMyStuffUseCheck check, Action<bool> onComplete)
		{
		}

		public static string GetErrorMessage(StuffItem item)
		{
			return null;
		}

		public static void PreExecutePowerUp(StuffItem item, Action<IMyStuffUseCheck> onComplete)
		{
		}

		public static void ShowMyStuffMenu()
		{
		}

		private static IEnumerator ShowList(List list)
		{
			return null;
		}

		private static void Init()
		{
		}

		public static void Logout()
		{
		}

		public static void RedeemItem(string itemId, string transactionId, Action<MYSTUFF_REDEEM> onComplete = null)
		{
		}

		private static void RemoveQueue(string itemId, string transactionId)
		{
		}

		private static void ProcessRedeemItems()
		{
		}

		private static void SaveQueue()
		{
		}

		private static void LoadQueue()
		{
		}

		private static void ResetQueue()
		{
		}

		private static string GetQueueKey()
		{
			return null;
		}

		public static StuffItem GetItemById(string id)
		{
			return null;
		}

		public static DataMyStuffCatalog GetDataItemById(string id)
		{
			return null;
		}

		public static StuffItem GetItemByRedeemId(string id)
		{
			return null;
		}

		public static DataMyStuffCatalog GetDataItemByActivationCode(string id)
		{
			return null;
		}

		public static List<string> GetDataItemExtraEmojisById(string id)
		{
			return null;
		}

		public static void RedeemIap(string iap, string transactionId, Action<MYSTUFF_REDEEM> callback)
		{
		}

		public static Sprite GetSpriteForItem(string itemId)
		{
			return null;
		}

		public static string GetEmojiForItem(string itemId)
		{
			return null;
		}

		public static string GetEmojiForItem(DataMyStuffCatalog item)
		{
			return null;
		}

		public static bool WithoutTimedIapAccess(Func<bool> evaluate)
		{
			return false;
		}

		public static bool IsIapActive(IAP_TYPE iapType)
		{
			return false;
		}

		public static void LoadStuffItemsExternalSource(List<StuffItem> items)
		{
		}

		public static void MarkResetCache()
		{
		}

		public static void ShowScenarioCustomLifeLocked(string itemId)
		{
		}

		public static bool IsCustomLifeLocked(string lifeId)
		{
			return false;
		}

		public static bool IsContentUnlocked(string contentId)
		{
			return false;
		}

		public static string GetCustomLifeStatus(string lifeId)
		{
			return null;
		}

		public static string GetDataFeedbackForId(string itemId)
		{
			return null;
		}

		public static string GetCategoryName(MYSTUFF_CATEGORY category)
		{
			return null;
		}

		public static string GetFormattedQuantityForHours(int hours)
		{
			return null;
		}

		public static string GetStreakSaverCount()
		{
			return null;
		}

		public static string GetItemsCount(bool onlyTimed)
		{
			return null;
		}

		public static bool IsStartLife(string lifeId)
		{
			return false;
		}

		public static bool IsContentItem(string itemId)
		{
			return false;
		}

		internal static List _003CShowMyStuffMenu_003Eg__GetList_007C26_1(ref _003C_003Ec__DisplayClass26_0 P_0)
		{
			return null;
		}
	}
}
