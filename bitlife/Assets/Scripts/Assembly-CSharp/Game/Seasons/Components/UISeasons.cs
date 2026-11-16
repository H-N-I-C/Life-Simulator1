using System;
using System.Collections.Generic;
using Game.Seasons.Controllers;
using Game.Seasons.Models;
using UnityEngine;

namespace Game.Seasons.Components
{
	public class UISeasons
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			internal void _003C_002Ecctor_003Eb__41_0()
			{
			}

			internal void _003C_002Ecctor_003Eb__41_1()
			{
			}

			internal void _003C_002Ecctor_003Eb__41_2()
			{
			}

			internal void _003C_002Ecctor_003Eb__41_3()
			{
			}

			internal void _003C_002Ecctor_003Eb__41_4()
			{
			}
		}

		private static UISeasonsBar seasonsBar;

		private static UISeasonsCommunityBar communityBar;

		private static UISeasonsStreakBar streakBar;

		private static UISeasonsDailyBar dailyRewardBar;

		public static Action<UISeasonsBar, bool> OnShowSeasonsBar;

		public static Action<UISeasonsCommunityBar, bool> OnShowCommunityBar;

		public static Action<UISeasonsStreakBar, bool> OnShowStreakView;

		public static Action<UISeasonsDailyBar, bool> OnShowDailyRewardView;

		private static SeasonsViewController View;

		private static Dictionary<SeasonsItemType, Action> SeasonsBarActions;

		public static bool IsSeasonsBarVisible => false;

		public static bool IsCommunityBarVisible => false;

		public static bool IsStreakBarVisible => false;

		public static bool IsDailyRewardBarVisible => false;

		public static void ToggleSeasonsBar()
		{
		}

		public static void ToggleCommunityBar()
		{
		}

		public static void ToggleStreakBar()
		{
		}

		public static void ToggleDailyRewardBar()
		{
		}

		public static void SeasonsBarClicked(SeasonsItemType itemType)
		{
		}

		public static void StreakBarClicked(STREAK_STATUS streakStatus)
		{
		}

		public static void DailyRewardBarClicked()
		{
		}

		public static void TrackItemRedeemed(SeasonRedeemModel data, Action<bool> success)
		{
		}

		public static void Initialize(SeasonsViewController view)
		{
		}

		public static void Show()
		{
		}

		public static void Hide()
		{
		}

		public static void ShowStreakOnly(bool show)
		{
		}

		private static bool TryShowSeasonsBar(bool show)
		{
			return false;
		}

		private static bool TryShowCommunityBar(bool show)
		{
			return false;
		}

		private static bool TryShowStreakBar(bool show)
		{
			return false;
		}

		public static void ShowDailyRewardBar(bool show)
		{
		}

		private static bool TryShowDailyRewardBar(bool show)
		{
			return false;
		}

		private static bool CanShowInSeason()
		{
			return false;
		}

		private static bool CanShowSeasonsBar(bool show)
		{
			return false;
		}

		private static bool CanShowCommunityBar(bool show)
		{
			return false;
		}

		private static void Show(bool show)
		{
		}

		private static bool TryShow<T>(string resource, ref T result, bool show) where T : Component
		{
			return false;
		}
	}
}
