using System;
using System.Collections.Generic;
using Backend.Authentication;
using Game.Seasons.Models;

namespace Game.Seasons.Controllers
{
	public class SeasonsSaveDataController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<SeasonTimeInfo> _003C_003E9__9_0;

			public static Action _003C_003E9__11_0;

			public static Func<QueueSaveData, bool> _003C_003E9__12_0;

			public static Func<QueueSaveData, bool> _003C_003E9__16_0;

			public static Func<QueueSaveData, bool> _003C_003E9__17_0;

			public static Action<bool> _003C_003E9__17_1;

			public static Action<bool> _003C_003E9__17_2;

			public static Action<bool> _003C_003E9__22_0;

			internal void _003CInit_003Eb__9_0(SeasonTimeInfo data)
			{
			}

			internal void _003CRefreshPlayerData_003Eb__11_0()
			{
			}

			internal bool _003CSaveSeasonsPlayerData_003Eb__12_0(QueueSaveData t)
			{
				return false;
			}

			internal bool _003CSaveSeasonsStreakData_003Eb__16_0(QueueSaveData t)
			{
				return false;
			}

			internal bool _003CSendNextCallToServer_003Eb__17_0(QueueSaveData t)
			{
				return false;
			}

			internal void _003CSendNextCallToServer_003Eb__17_1(bool result)
			{
			}

			internal void _003CSendNextCallToServer_003Eb__17_2(bool streakResult)
			{
			}

			internal void _003CRegisterPremiumRewards_003Eb__22_0(bool result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass13_0
		{
			public Action<bool> callback;

			internal void _003CSavePlayerDataToServer_003Eb__0(bool result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public Action<bool> callback;

			internal void _003CRequestSaveStreaks_003Eb__0(string result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public string seasonId;

			public string redeemId;

			public bool isPremium;

			public Action<bool> callback;

			internal void _003CRedeemTrackItem_003Eb__0(string result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_1
		{
			public SeasonRedeemModel newRedeemedItem;

			internal bool _003CRedeemTrackItem_003Eb__1(SeasonRedeemModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public string seasonId;

			public string itemItemId;

			public int quantity;

			public Action<bool> callback;

			internal void _003CPurchaseItem_003Eb__0(string result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public string seasonId;

			public string goalId;

			internal void _003CContributeToCommunityGoal_003Eb__0(bool result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public QUEUE_TYPE queueType;

			internal bool _003CExistInQueue_003Eb__0(QueueSaveData t)
			{
				return false;
			}
		}

		private static int _blockedJeopardyDate;

		private static int _blockedDate;

		private static SeasonTimeInfo _currentData;

		private const int DailyDays = 1;

		private static List<QueueSaveData> _queue;

		private static int _secondsToRead;

		private static bool _processing;

		private const string kCacheSeasonData = "seasons_data_cache";

		private static SeasonsSettings _settings;

		public static void Init(SeasonsSettings settings)
		{
		}

		private static void CheckEndOfDay()
		{
		}

		private static void RefreshPlayerData()
		{
		}

		public static void SaveSeasonsPlayerData()
		{
		}

		private static void SavePlayerDataToServer(string jsonText, Action<bool> callback)
		{
		}

		private static void SavePlayerCache()
		{
		}

		private static void LoadCache()
		{
		}

		public static void SaveSeasonsStreakData(Action<bool> onResult)
		{
		}

		private static bool SendNextCallToServer()
		{
			return false;
		}

		private static void RequestSaveStreaks(Action<bool> callback = null)
		{
		}

		public static void RedeemTrackItem(string seasonId, SeasonRedeemModel redeemItem, Action<bool> callback)
		{
		}

		public static void PurchaseItem(string seasonId, string itemItemId, int quantity, Action<bool> callback)
		{
		}

		public static void ContributeToCommunityGoal(string seasonId, string goalId)
		{
		}

		public static void RegisterPremiumRewards()
		{
		}

		private static bool ExistInQueue(QUEUE_TYPE queueType)
		{
			return false;
		}

		public static bool CheckIfNeedSaveAfterUnlock()
		{
			return false;
		}
	}
}
