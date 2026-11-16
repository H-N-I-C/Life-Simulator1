using System;
using System.Collections;
using System.Collections.Generic;
using Backend;
using Backend.Authentication;
using Game.Seasons.Models;
using UnityEngine;
using UnityOpenFeature.Client;
using UnityOpenFeature.Core;

namespace Game.Seasons.Controllers
{
	public class SeasonsController
	{
		public enum SeasonsLoadMode
		{
			Disabled = 0,
			StreakOnly = 1,
			Season = 2,
			DailyReward = 3
		}

		[Serializable]
		public struct PlayerCurrencyInfo
		{
			public string name;

			public int amount;

			public static PlayerCurrencyInfo Current => default;
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<GetConfigResponse> _003C_003E9__61_0;

			public static Action _003C_003E9__61_1;

			public static Action _003C_003E9__61_2;

			public static Action _003C_003E9__61_3;

			public static Action _003C_003E9__61_4;

			public static Func<IFeatureClient, ResolutionDetails<string>> _003C_003E9__62_0;

			public static Action<LoadNarrativeState> _003C_003E9__77_0;

			public static Action _003C_003E9__92_0;

			public static Action _003C_003E9__96_0;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__119_0;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__120_0;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__121_0;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__122_0;

			public static Func<SeasonRewardModelItem, bool> _003C_003E9__125_0;

			public static Func<SeasonRewardModelItem, string> _003C_003E9__125_1;

			public static Func<StuffItem, bool> _003C_003E9__132_0;

			public static Func<TrackItemModel, bool> _003C_003E9__141_0;

			internal void _003CInit_003Eb__61_0(GetConfigResponse data)
			{
			}

			internal void _003CInit_003Eb__61_1()
			{
			}

			internal void _003CInit_003Eb__61_2()
			{
			}

			internal void _003CInit_003Eb__61_3()
			{
			}

			internal void _003CInit_003Eb__61_4()
			{
			}

			internal ResolutionDetails<string> _003CSubscribeRewardExperience_003Eb__62_0(IFeatureClient client)
			{
				return null;
			}

			internal void _003CItemActionPerformed_003Eb__77_0(LoadNarrativeState result)
			{
			}

			internal void _003CRetry_003Eb__92_0()
			{
			}

			internal void _003CShowActivateBattlePass_003Eb__96_0()
			{
			}

			internal bool _003CGetTotalTasksCompletedForDay_003Eb__119_0(SeasonObjectiveModel t)
			{
				return false;
			}

			internal bool _003CGetTotalTasksForDay_003Eb__120_0(SeasonObjectiveModel t)
			{
				return false;
			}

			internal bool _003CGetTotalBonusTasksCompletedForDay_003Eb__121_0(SeasonObjectiveModel t)
			{
				return false;
			}

			internal bool _003CGetTotalBonusTasksForDay_003Eb__122_0(SeasonObjectiveModel t)
			{
				return false;
			}

			internal bool _003CGetCommunityGoalRewardsList_003Eb__125_0(SeasonRewardModelItem t)
			{
				return false;
			}

			internal string _003CGetCommunityGoalRewardsList_003Eb__125_1(SeasonRewardModelItem t)
			{
				return null;
			}

			internal bool _003CCheckTimedData_003Eb__132_0(StuffItem t)
			{
				return false;
			}

			internal bool _003CHasClaimItems_003Eb__141_0(TrackItemModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass119_0
		{
			public List<PlayerObjectivesCompletion> ids;

			internal bool _003CGetTotalTasksCompletedForDay_003Eb__1(SeasonObjectiveModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass119_1
		{
			public SeasonObjectiveModel t;

			internal bool _003CGetTotalTasksCompletedForDay_003Eb__2(PlayerObjectivesCompletion x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass121_0
		{
			public List<PlayerObjectivesCompletion> ids;

			internal bool _003CGetTotalBonusTasksCompletedForDay_003Eb__1(SeasonObjectiveModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass121_1
		{
			public SeasonObjectiveModel t;

			internal bool _003CGetTotalBonusTasksCompletedForDay_003Eb__2(PlayerObjectivesCompletion x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass126_0
		{
			public REWARD_TYPES itemId;

			internal bool _003CIsSeasonalActivityUnlocked_003Eb__0(SeasonRewardModelItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass141_0
		{
			public List<SeasonRedeemModel> items;

			internal bool _003CHasClaimItems_003Eb__1(TrackItemModel t)
			{
				return false;
			}

			internal bool _003CHasClaimItems_003Eb__2(TrackItemModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass141_1
		{
			public TrackItemModel t;

			internal bool _003CHasClaimItems_003Eb__3(SeasonRedeemModel x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass141_2
		{
			public TrackItemModel t;

			internal bool _003CHasClaimItems_003Eb__4(SeasonRedeemModel x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass62_0
		{
			public string account;

			internal void _003CSubscribeRewardExperience_003Eb__1(string mode)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass81_0
		{
			public ShopListItemModel shopItem;

			internal void _003CItemActionPerformedForShop_003Eb__0(List<StuffItem> items)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass88_0
		{
			public string objectiveId;

			internal bool _003CGetObjective_003Eb__0(SeasonObjectiveModel i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass89_0
		{
			public Action<SeasonModel, SeasonModelError> callback;

			internal void _003CLoadSeasonsData_003Eg__Finalize_007C0(string data, bool success)
			{
			}

			internal void _003CLoadSeasonsData_003Eb__1((string, bool) result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass90_0
		{
			public int generation;

			public string seasonId;

			public bool loadAssets;

			public Action finished;

			internal bool _003CLoadSeason_003Eg__IsCurrentRequest_007C0()
			{
				return false;
			}

			internal void _003CLoadSeason_003Eg__LoadSeasonData_007C1()
			{
			}

			internal void _003CLoadSeason_003Eb__4(SeasonModel data, SeasonModelError error)
			{
			}

			internal void _003CLoadSeason_003Eb__2(List<UnityEngine.Object> allAssets)
			{
			}

			internal void _003CLoadSeason_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass90_1
		{
			public SeasonModel data;

			public _003C_003Ec__DisplayClass90_0 CS_0024_003C_003E8__locals1;

			internal void _003CLoadSeason_003Eb__5()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass93_0
		{
			public Action callback;

			internal void _003CRefreshSeason_003Eb__0(SeasonModel data, SeasonModelError error)
			{
			}
		}

		private sealed class _003CRunTimer_003Ed__106 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRunTimer_003Ed__106(int _003C_003E1__state)
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

		public const string NoSeason = "noseason";

		private const string SeasonsCurrentStreakKey = "SEASONS_CURRENT_STREAK_DAY";

		private const string SeasonsPlayerCurrencyKey = "PREVIOUS_SEASONS_PLAYER_CURRENCY";

		public static Action<SeasonsItemType, SeasonPanelAnimationAction> OnPanelAnimationAction;

		private static Action m_OnSeasionsDataAvailable;

		private static SeasonsSettings _settings;

		private static string _loadedSeason;

		private static List<SeasonsLoadMode> _loadMode;

		private static IDisposable _rewardSubscription;

		private static int _rewardGeneration;

		private static string _rewardMode;

		private static string _rewardAccount;

		private static bool _gameReady;

		private static bool _onboardingReady;

		private static bool _rewardStartupStarted;

		public static SeasonsGameSettings SeasonsGameSettings;

		public static SeasonsInfoButtonController SeasonsInfoButtonController;

		private static SeasonModel Data__BackingField;

		private static SeasonsViewController ViewController__BackingField;

		private static SeasonsNarrativeController NarrativeController__BackingField;

		private static SeasonsObjectiveController ObjectiveController__BackingField;

		private static SeasonsAssetsController SeasonsAssetsController__BackingField;

		private static SeasonsBarMessageController BarMessageController__BackingField;

		public static bool DebugOverrideSeasonalCommunityGoalsEnabled;

		public static bool DebugOverrideShopEnabled;

		private static Action<SeasonTimeInfo> m_OnSeasonTimeChanged;

		public const string ItemActionShowHuntList = "Show Hunt List";

		public const string ItemActionShowCommunityFromMain = "Show Community From Main";

		private static Action<SeasonModel> m_OnUserUpdatedData;

		private static Coroutine streakTimerCoroutine;

		private static SeasonTimeInfo TimeElapsed__BackingField;

		public static bool OnboardingReady => false;

		public static string NextSeasonName => null;

		public static string SeasonName => null;

		public static string SeasonShortName => null;

		public static string NextSeasonShortName => null;

		public static string SeasonDescription => null;

		public static SeasonModel Data
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

		public static SeasonsViewController ViewController
		{
			get
			{
				return ViewController__BackingField;
			}
			private set
			{
				ViewController__BackingField = value;
			}
		}

		public static SeasonsNarrativeController NarrativeController
		{
			get
			{
				return NarrativeController__BackingField;
			}
			private set
			{
				NarrativeController__BackingField = value;
			}
		}

		public static SeasonsObjectiveController ObjectiveController
		{
			get
			{
				return ObjectiveController__BackingField;
			}
			private set
			{
				ObjectiveController__BackingField = value;
			}
		}

		public static SeasonsAssetsController SeasonsAssetsController
		{
			get
			{
				return SeasonsAssetsController__BackingField;
			}
			private set
			{
				SeasonsAssetsController__BackingField = value;
			}
		}

		public static SeasonsBarMessageController BarMessageController
		{
			get
			{
				return BarMessageController__BackingField;
			}
			private set
			{
				BarMessageController__BackingField = value;
			}
		}

		private static bool HasRewardAssignment => false;

		public static bool RewardExperienceResolved => false;

		public static bool HasActiveLife => false;

		public static SeasonTimeInfo TimeElapsed
		{
			get
			{
				return TimeElapsed__BackingField;
			}
			private set
			{
				TimeElapsed__BackingField = value;
			}
		}

		public static bool CanStartDailyRewards => false;

		public static event Action OnSeasionsDataAvailable
		{
			add
			{
			}
			remove
			{
			}
		}

		public static event Action<SeasonTimeInfo> OnSeasonTimeChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		public static event Action<SeasonModel> OnUserUpdatedData
		{
			add
			{
			}
			remove
			{
			}
		}

		public static bool IsActive()
		{
			return false;
		}

		public static bool IsSeasonalCommunityGoalsEnabled()
		{
			return false;
		}

		public static bool IsShopEnabled()
		{
			return false;
		}

		private static void Init()
		{
		}

		private static void SubscribeRewardExperience()
		{
		}

		private static void TryStartRewardExperience()
		{
		}

		private static void StartSeasons()
		{
		}

		private static void StartSeasonsLoader()
		{
		}

		public static List<SeasonsLoadMode> GetLoadMode()
		{
			return null;
		}

		private static void LoadConfiguredSeason(Action finished = null)
		{
		}

		public static void ItemActionPerformed(ListItem item)
		{
		}

		private static void DebugItemActionPerformed(ListItem item)
		{
		}

		private static void ShowFirstOpenObjectOrNone()
		{
		}

		private static void ShowObjectLocation(string location)
		{
		}

		public static void ItemActionPerformedForShop(ListItem item)
		{
		}

		public static void AddTimeElapsedListener(Action<SeasonTimeInfo> callback)
		{
		}

		public static void RemoveTimeElapsedListener(Action<SeasonTimeInfo> callback)
		{
		}

		public static void NotifyUserUpdatedData()
		{
		}

		public static SeasonObjectiveModel GetObjective(string objectiveId)
		{
			return null;
		}

		private static void LoadSeasonsData(string seasonId, Action<SeasonModel, SeasonModelError> callback)
		{
		}

		public static void LoadSeason(string seasonId, bool loadAssets = true, Action finished = null)
		{
		}

		private static bool IsCurrentSeasonEnded(SeasonModel data, out string nextSeasonId)
		{
			nextSeasonId = null;
			return false;
		}

		private static void Retry(int delay = 30, bool checkForActiveSeason = false)
		{
		}

		public static void RefreshSeason(Action callback = null)
		{
		}

		private static void SetData(SeasonModel data)
		{
		}

		public static void ShowWelcomeBadge()
		{
		}

		public static void ShowActivateBattlePass()
		{
		}

		private static void StopTimer()
		{
		}

		private static void StartTimer(Action action)
		{
		}

		private static IEnumerator RunTimer()
		{
			return null;
		}

		private static bool NotifyTime()
		{
			return false;
		}

		private static bool NotifyTime(Action<SeasonTimeInfo> callback)
		{
			return false;
		}

		public static void LoadNarrative(string narrativeId, bool watchedRv, Action<LoadNarrativeState> callback)
		{
		}

		public static bool IsPremiumRewardAvailable()
		{
			return false;
		}

		public static bool IsStreakJeopardy()
		{
			return false;
		}

		public static STREAK_STATUS GetStreakState()
		{
			return STREAK_STATUS.CURRENT;
		}

		public static string GetCurrentSeasonId()
		{
			return null;
		}

		public static bool IsCurrentLifeTimedOut()
		{
			return false;
		}

		public static bool IsNextSeasonLoaded()
		{
			return false;
		}

		public static DateTime GetNextSeasonDate()
		{
			return default;
		}

		public static bool TryGetNextSeasonTimeUntilStart(out TimeSpan? time, out string name)
		{
			time = null;
			name = null;
			return false;
		}

		public static void AddExtraCreditsToPlayer(int extraCredits)
		{
		}

		public static int GetTotalTasksCompletedForDay()
		{
			return 0;
		}

		public static int GetTotalTasksForDay()
		{
			return 0;
		}

		public static int GetTotalBonusTasksCompletedForDay()
		{
			return 0;
		}

		public static int GetTotalBonusTasksForDay()
		{
			return 0;
		}

		public static SeasonsSettingsWelcome GetWelcomeData()
		{
			return null;
		}

		public static void GetSeasonalActivities(ArrayList listItems)
		{
		}

		public static List GetCommunityGoalRewardsList()
		{
			return null;
		}

		public static bool IsSeasonalActivityUnlocked(REWARD_TYPES itemId)
		{
			return false;
		}

		public static SeasonsSettings GetSettings()
		{
			return null;
		}

		public static bool IsInvalidGameStateForCustomLife()
		{
			return false;
		}

		public static bool IsStreaksUIHidden()
		{
			return false;
		}

		public static void Logout()
		{
		}

		public static void SetCurrentStreakDayInfoButton()
		{
		}

		private static void CheckTimedData()
		{
		}

		public static bool IsNoSeason()
		{
			return false;
		}

		public static bool IsStreakOnly()
		{
			return false;
		}

		public static bool IsSeasonLoadedMode()
		{
			return false;
		}

		public static bool IsSeasonUnlockedForPlayer()
		{
			return false;
		}

		public static void AddCreditsToPlayer(int extraCredits)
		{
		}

		public static void AddObjectiveCompleted(PlayerObjectivesCompletion objective, bool isVip)
		{
		}

		public static string GetCurrentDailyAdventureName()
		{
			return null;
		}

		public static List<CommunityGoalDataModel> GetCommunityGoals()
		{
			return null;
		}

		public static bool HasClaimItems()
		{
			return false;
		}

		public static bool IsLoadedSeasonWithinTime()
		{
			return false;
		}

		public static int GetBitsMarginal(TrackItemModel itemData)
		{
			return 0;
		}

		public static void SaveNewPlayerCurrencyBalance()
		{
		}

		public static void CheckOldPlayerCurrencyBalance()
		{
		}

		public static bool IsStreakActive()
		{
			return false;
		}

		public static bool IsDailyRewardsActive()
		{
			return false;
		}

		public static string GetLastSeasonOrEmpty()
		{
			return null;
		}

		public static string GetEmojiStringForData(DataMyStuffCatalog itemData)
		{
			return null;
		}

		public static bool IsDateCompatibleWithBattlePass(string createdAt)
		{
			return false;
		}
	}
}
