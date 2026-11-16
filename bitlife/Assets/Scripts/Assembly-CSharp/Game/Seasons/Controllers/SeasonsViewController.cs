using System;
using Game.Seasons.Models;

namespace Game.Seasons.Controllers
{
	public class SeasonsViewController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<ListItem> _003C_003E9__12_0;

			internal void _003CAddDefaultInfoItem_003Eb__12_0(ListItem item)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public bool isSeasonalCommunityGoalsEnabled;

			public List list;

			internal void _003CShowObjectivesList_003Eb__0(SeasonObjectiveModel i)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass10_1
		{
			public SeasonObjectiveModel i;

			internal void _003CShowObjectivesList_003Eb__1(ListItem item)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass11_0
		{
			public SeasonsViewController _003C_003E4__this;

			public List list;

			internal void _003CShowEventsList_003Eb__0(ListItem item)
			{
			}

			internal void _003CShowEventsList_003Eb__1(CommunityGoalDataModel eventItem)
			{
			}

			internal void _003CShowEventsList_003Eb__2(ChallengeMetadata metaData, int completed, int required)
			{
			}

			internal void _003CShowEventsList_003Eb__3(string name, string emoji, HOLIDAY_COLLECTIBLE_TYPE type, string secondLine, int completed, int required)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass11_1
		{
			public CommunityGoalDataModel eventItem;

			internal void _003CShowEventsList_003Eb__4(ListItem item)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass11_2
		{
			public int completed;

			public int required;

			public ChallengeMetadata metaData;

			internal void _003CShowEventsList_003Eb__5(ListItem item)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass11_3
		{
			public string secondLine;

			public int completed;

			public int required;

			internal void _003CShowEventsList_003Eb__6(ListItem item)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass14_0
		{
			public List list;

			internal void _003CShowShopList_003Eb__0(ShopListItemModel shopItem)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass14_1
		{
			public ShopListItemModel shopItem;

			internal void _003CShowShopList_003Eb__1(ListItem item)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public SeasonRedeemModel data;

			public Action<bool> success;

			internal bool _003CRedeemTrackItem_003Eb__0(SeasonRedeemModel t)
			{
				return false;
			}

			internal void _003CRedeemTrackItem_003Eb__1(bool result)
			{
			}
		}

		private SeasonModel Data__BackingField;

		private Scenario _nextScenarioAfterAnimation;

		public SeasonModel Data
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

		public SeasonsViewController SetData(SeasonModel data)
		{
			return null;
		}

		public void ShowUI()
		{
		}

		public void HideUI()
		{
		}

		public void ShowRedeemTrack()
		{
		}

		public void ShowObjectivesList()
		{
		}

		public void ShowEventsList()
		{
		}

		private void AddDefaultInfoItem(List targetList, string title, string secondLine)
		{
		}

		public void ShowShop()
		{
		}

		public void ShowShopList(bool showDefaultIfShopDisabled = false)
		{
		}

		private void ShowDisabledShopScenario()
		{
		}

		public void ShowNoSeasonScenario()
		{
		}

		private bool CanShowSeasonsList()
		{
			return false;
		}

		public void ShowStreakView(STREAK_STATUS streakStatus)
		{
		}

		public void RedeemTrackItem(SeasonRedeemModel data, Action<bool> success)
		{
		}

		public void ShowBitsAnimation(Scenario nextScenario)
		{
		}

		private void PlayBitsAnimation(SeasonsItemType panelType, SeasonPanelAnimationAction actionType)
		{
		}
	}
}
