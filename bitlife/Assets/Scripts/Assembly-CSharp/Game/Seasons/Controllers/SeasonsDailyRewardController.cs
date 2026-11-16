using System;
using System.Collections.Generic;
using Game.Seasons.Models;

namespace Game.Seasons.Controllers
{
	public static class SeasonsDailyRewardController
	{
		private enum SnapshotRequestKind
		{
			None = 0,
			State = 1,
			Return = 2
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Predicate<DailyMarketplaceOfferModel> _003C_003E9__106_0;

			public static Func<DailyMarketplaceOfferModel, bool> _003C_003E9__106_2;

			public static Func<DailyMarketplaceOfferModel, bool> _003C_003E9__106_3;

			public static Func<DailyMarketplaceOfferModel, bool> _003C_003E9__106_4;

			public static Func<DailyMarketplaceOfferModel, int> _003C_003E9__106_5;

			public static Func<DailyMarketplaceOfferModel, int> _003C_003E9__106_6;

			public static Func<DailyMarketplaceOfferModel, bool> _003C_003E9__106_8;

			public static Func<DailyMarketplaceOfferModel, int> _003C_003E9__106_9;

			public static Func<DailyMarketplaceOfferModel, int> _003C_003E9__106_10;

			public static Action<ListItem> _003C_003E9__106_1;

			public static Action<int> _003C_003E9__111_1;

			public static Action<int> _003C_003E9__115_0;

			public static Action<int> _003C_003E9__116_0;

			internal bool _003CBuildMarketplaceList_003Eb__106_0(DailyMarketplaceOfferModel offer)
			{
				return false;
			}

			internal bool _003CBuildMarketplaceList_003Eb__106_2(DailyMarketplaceOfferModel o)
			{
				return false;
			}

			internal bool _003CBuildMarketplaceList_003Eb__106_3(DailyMarketplaceOfferModel o)
			{
				return false;
			}

			internal bool _003CBuildMarketplaceList_003Eb__106_4(DailyMarketplaceOfferModel o)
			{
				return false;
			}

			internal int _003CBuildMarketplaceList_003Eb__106_5(DailyMarketplaceOfferModel o)
			{
				return 0;
			}

			internal int _003CBuildMarketplaceList_003Eb__106_6(DailyMarketplaceOfferModel o)
			{
				return 0;
			}

			internal bool _003CBuildMarketplaceList_003Eb__106_8(DailyMarketplaceOfferModel o)
			{
				return false;
			}

			internal int _003CBuildMarketplaceList_003Eb__106_9(DailyMarketplaceOfferModel o)
			{
				return 0;
			}

			internal int _003CBuildMarketplaceList_003Eb__106_10(DailyMarketplaceOfferModel o)
			{
				return 0;
			}

			internal void _003CBuildMarketplaceList_003Eb__106_1(ListItem item)
			{
			}

			internal void _003CPurchaseMarketplaceOffer_003Eb__111_1(int _)
			{
			}

			internal void _003CShowInsufficientGold_003Eb__115_0(int _)
			{
			}

			internal void _003CShowApiFailure_003Eb__116_0(int _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass102_0
		{
			public long balance;

			internal bool _003CHasAffordableMarketplaceOffer_003Eb__0(DailyMarketplaceOfferModel offer)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass105_0
		{
			public Action<bool> completed;

			public int generation;

			public int token;

			public int price;

			internal void _003CPurchaseTimeMachineWithCoins_003Eb__0()
			{
			}

			internal void _003CPurchaseTimeMachineWithCoins_003Eb__1(DailyApiResult result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass106_0
		{
			public string section;

			internal bool _003CBuildMarketplaceList_003Eb__7(DailyMarketplaceOfferModel o)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass107_0
		{
			public DailyMarketplaceOfferModel offer;

			internal void _003CAddMarketplaceRow_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass109_0
		{
			public Func<bool> canPresent;

			public bool completionSent;

			public bool reportOpening;

			public Action<bool> openingCompleted;

			public int generation;

			public string account;

			public int epoch;

			public bool development;

			public Func<bool> contextIsCurrent;

			public List list;

			internal bool _003CShowMarketplaceList_003Eb__0()
			{
				return false;
			}

			internal void _003CShowMarketplaceList_003Eb__1(bool opened)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass110_0
		{
			public DailyMarketplaceOfferModel offer;

			internal void _003CConfirmMarketplaceOffer_003Eb__0(int index)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass111_0
		{
			public int generation;

			public int token;

			public DailyMarketplaceOfferModel offer;

			internal void _003CPurchaseMarketplaceOffer_003Eb__0(DailyApiResult result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass111_1
		{
			public DailyMarketplacePurchaseResponseModel response;

			internal bool _003CPurchaseMarketplaceOffer_003Eb__2(DailyMarketplaceOfferModel item)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass113_0
		{
			public Action<bool> completed;

			public int generation;

			public int token;

			internal void _003CRefreshLifeCompletionState_003Eb__0()
			{
			}

			internal void _003CRefreshLifeCompletionState_003Eb__1(DailyApiResult result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass114_0
		{
			public Action<DailyApiResult> callback;

			public int generation;

			public int token;

			internal void _003CRedeemLifeCoins_003Eb__0()
			{
			}

			internal void _003CRedeemLifeCoins_003Eb__1()
			{
			}

			internal void _003CRedeemLifeCoins_003Eb__2(DailyApiResult result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass65_0
		{
			public int generation;

			internal void _003COnConfidenceFlagsResolved_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass67_0
		{
			public DateTime now;

			internal bool _003CIsStateRefreshDue_003Eb__0(DailyMarketplaceOfferModel offer)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass74_0
		{
			public SnapshotRequestKind kind;

			public int token;

			public int generation;

			public string account;

			public int epoch;

			internal void _003CRetryPendingLoad_003Eb__0(DailyApiResult result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass80_0
		{
			public int generation;

			internal void _003CScheduleDarklyCheck_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass81_0
		{
			public int generation;

			public Life ageThreeLife;

			internal void _003CTryShowDailyRewardDarkly_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass84_0
		{
			public int generation;

			public string account;

			public HashSet<string> keys;

			internal void _003CShowRecoveryNotification_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass90_0
		{
			public int token;

			public Action timeout;

			internal void _003CBeginTransaction_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass92_0
		{
			public Action failed;

			public int generation;

			public int token;

			public string requestId;

			public List<DailyPendingRewardModel> pendingAtClaim;

			public Action<int, List<DailyPendingRewardModel>> completed;

			internal void _003CClaimDailyRewards_003Eb__0()
			{
			}

			internal void _003CClaimDailyRewards_003Eb__1(DailyApiResult result)
			{
			}
		}

		private const int RetryDelaySeconds = 30;

		private const float SafePointPollSeconds = 1f;

		private const float ConfidencePresentationDelaySeconds = 1f;

		private const float ConfidenceReadinessTimeoutSeconds = 10f;

		private const string PendingGrantKey = "daily_reward_pending_grant_v2";

		private static SeasonsDailyRewardModel Data__BackingField;

		private static DailyMarketplaceModel Marketplace__BackingField;

		private static bool IsPresentationReserved__BackingField;

		private static Action m_OnDailyRewardDataAvailable;

		private static int _shownPeriod;

		private static int _transactionToken;

		private static string _claimRequestId;

		private static int _claimVersion;

		private static string _timeMachineRequestId;

		private static string _timeMachineLifeId;

		private static int _timeMachineYears;

		private static int _timeMachinePrice;

		private static DateTime _receivedAt;

		private static DateTime _serverAt;

		private static bool _started;

		private static bool _requestInFlight;

		private static SnapshotRequestKind _snapshotRequestKind;

		private static SnapshotRequestKind _pendingSnapshotRequest;

		private static int _snapshotRequestToken;

		private static double _snapshotCallStartedAt;

		private static double? _snapshotResponseCallStartedAt;

		private static int _snapshotEpoch;

		private static bool _darklyCheckScheduled;

		private static bool _confidenceDelayScheduled;

		private static bool _confidencePresentationReady;

		private static bool _retryScheduled;

		private static double _retryAtRealtime;

		private static bool _purchaseInFlight;

		private static Action _transactionCancelled;

		private static int _accountGeneration;

		private static Life _ageThreeLife;

		private static Action _ageThreeContinuation;

		private static bool _ageThreePanelShowing;

		private static LoadingIndicator _ageThreeLoader;

		private static double _ageThreeConfidenceDeadline;

		private static readonly HashSet<string> HomeViewedAccountsThisSession;

		private static readonly HashSet<string> MarketplaceViewedAccountsThisSession;

		public static SeasonsDailyRewardModel Data
		{
			get
			{
				return Data__BackingField;
			}
			private set
			{
				Data__BackingField = value;
			}
		}

		public static DailyMarketplaceModel Marketplace
		{
			get
			{
				return Marketplace__BackingField;
			}
			private set
			{
				Marketplace__BackingField = value;
			}
		}

		public static bool IsPresentationReserved
		{
			get
			{
				return IsPresentationReserved__BackingField;
			}
			private set
			{
				IsPresentationReserved__BackingField = value;
			}
		}

		public static bool TransactionInFlight => false;

		public static DateTime ServerUtcNow => default;

		private static string AccountKey => null;

		public static event Action OnDailyRewardDataAvailable
		{
			add
			{
			}
			remove
			{
			}
		}

		private static void ResetNotificationSession()
		{
		}

		public static bool IsDailyRewardsActive()
		{
			return false;
		}

		public static bool IsLoaded()
		{
			return false;
		}

		public static void StartDailyRewards()
		{
		}

		private static void PrepareConfidencePresentationGate()
		{
		}

		private static void OnConfidenceFlagsResolved()
		{
		}

		public static void OnApplicationForeground(bool forceRefresh = false)
		{
		}

		private static bool IsStateRefreshDue()
		{
			return false;
		}

		private static void RequestReturn()
		{
		}

		private static void QueueSnapshotRequest(SnapshotRequestKind kind)
		{
		}

		private static void InvalidateBackgroundSnapshot()
		{
		}

		private static void HandleBackgroundSnapshotResponse(DailyApiResult result, SnapshotRequestKind kind, int token, int generation, string account, int epoch)
		{
		}

		private static void HandleReturnResponse(DailyApiResult result, double? callStartedAt = null)
		{
		}

		private static void ScheduleRetry()
		{
		}

		public static void RetryPendingLoad()
		{
		}

		public static bool HasPendingDarkly()
		{
			return false;
		}

		internal static bool IsFirstLifeDailyRewardDeferred()
		{
			return false;
		}

		public static bool TryShowFirstDailyAtAgeThree(Life life, Action continueAgeUp = null)
		{
			return false;
		}

		private static void StopAgeThreeLoader()
		{
		}

		private static void CompleteAgeThreePresentation(bool resume = true)
		{
		}

		private static void ScheduleDarklyCheck()
		{
		}

		private static bool TryShowDailyRewardDarkly()
		{
			return false;
		}

		private static bool CanUseDailyRewardUI()
		{
			return false;
		}

		public static void MarkDailyRewardShown()
		{
		}

		public static void ShowRecoveryNotification()
		{
		}

		private static bool TryFreezeRecoveryRewards(DailyRewardGrantModel receipt, out List<DailyPendingRewardModel> rewards)
		{
			rewards = null;
			return false;
		}

		private static bool TryFreezeDailyClaim(SeasonsDailyRewardModel model, string requestId, IReadOnlyList<DailyPendingRewardModel> pendingAtClaim, out List<DailyPendingRewardModel> rewards)
		{
			rewards = null;
			return false;
		}

		private static bool TryFreezeDailyRewards(DailyRewardGrantModel receipt, string operationKind, IReadOnlyList<DailyPendingRewardModel> fallbackRewards, out List<DailyPendingRewardModel> rewards)
		{
			rewards = null;
			return false;
		}

		private static DailyPendingRewardModel FreezePendingReward(DailyPendingRewardModel reward)
		{
			return null;
		}

		public static void RefreshState()
		{
		}

		private static int BeginTransaction(Action timeout = null, Action cancelled = null)
		{
			return 0;
		}

		private static bool EndTransaction(int token)
		{
			return false;
		}

		public static void ClaimDailyRewards(Action<int, List<DailyPendingRewardModel>> completed, Action failed = null)
		{
		}

		public static void ShowDailyRewardDarkly()
		{
		}

		public static void ResetForAccountChange()
		{
		}

		public static void ResetForFeatureDisabled()
		{
		}

		private static void ResetDailyRewardState(bool resumeAgeUp)
		{
		}

		public static void ShowMarketplace(Action<bool> openingCompleted = null, Func<bool> canPresent = null)
		{
		}

		public static bool ShouldShowHomeNotification()
		{
			return false;
		}

		internal static void MarkHomeNotificationViewed()
		{
		}

		public static bool ShouldShowMarketplaceNotification()
		{
			return false;
		}

		public static bool HasAffordableMarketplaceOffer()
		{
			return false;
		}

		private static bool HasAffordableMarketplaceOffer(DailyMarketplaceModel marketplace, long balance)
		{
			return false;
		}

		private static void RearmNotificationsAfterDailyClaim(SeasonsDailyRewardModel claimed)
		{
		}

		public static bool CanPurchaseTimeMachineWithCoins()
		{
			return false;
		}

		public static void PurchaseTimeMachineWithCoins(int years, string lifeId, Action<bool> completed)
		{
		}

		public static List BuildMarketplaceList()
		{
			return null;
		}

		private static void AddMarketplaceRow(List list, DailyMarketplaceOfferModel offer, bool ownedRow)
		{
		}

		private static void AddHeader(List list, string text, int height, long? balance = null)
		{
		}

		private static void ShowMarketplaceList(bool reportOpening = false, Action<bool> openingCompleted = null, Func<bool> canPresent = null)
		{
		}

		public static void ConfirmMarketplaceOffer(DailyMarketplaceOfferModel offer)
		{
		}

		private static void PurchaseMarketplaceOffer(DailyMarketplaceOfferModel offer)
		{
		}

		public static bool CanClaimLifeCompletion()
		{
			return false;
		}

		public static void RefreshLifeCompletionState(Action<bool> completed)
		{
		}

		public static void RedeemLifeCoins(string lifeId, Action<DailyApiResult> callback = null)
		{
		}

		private static void ShowInsufficientGold()
		{
		}

		private static void ShowApiFailure(string errorCode)
		{
		}
	}
}
