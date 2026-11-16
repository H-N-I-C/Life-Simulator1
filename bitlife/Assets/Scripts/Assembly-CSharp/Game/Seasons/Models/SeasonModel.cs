using System;
using System.Collections.Generic;
using Backend;

namespace Game.Seasons.Models
{
	public class SeasonModel
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<bool> _003C_003E9__18_2;

			public static Action<bool> _003C_003E9__19_2;

			internal void _003CCompleteObjective_003Eb__18_2(bool _)
			{
			}

			internal void _003CForceCompleteStreakObjective_003Eb__19_2(bool _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public string objectiveId;

			internal bool _003CCompleteObjective_003Eb__0(SeasonObjectiveModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_1
		{
			public int streakDay;

			public _003C_003Ec__DisplayClass18_0 CS_0024_003C_003E8__locals1;

			internal bool _003CCompleteObjective_003Eb__1(PlayerObjectivesCompletion x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public string objectiveId;

			public int streakDay;

			internal bool _003CForceCompleteStreakObjective_003Eb__0(SeasonObjectiveModel t)
			{
				return false;
			}

			internal bool _003CForceCompleteStreakObjective_003Eb__1(PlayerObjectivesCompletion x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public Action<bool> onResult;

			internal void _003CAwardStreakToPlayer_003Eb__0(bool result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public string goalId;

			internal bool _003CContributeToCommunityGoal_003Eb__0(CommunityGoalDataModel t)
			{
				return false;
			}

			internal bool _003CContributeToCommunityGoal_003Eb__1(CommunityGoalModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public bool vip;

			public string objectiveId;

			public SeasonModel _003C_003E4__this;

			internal bool _003CIsObjectiveCompleted_003Eb__0(SeasonObjectiveModel i)
			{
				return false;
			}

			internal bool _003CIsObjectiveCompleted_003Eb__1(PlayerObjectivesCompletion i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass24_0
		{
			public bool vip;

			public int currentStreakDay;

			public List<SeasonObjectiveModel> objectives;

			internal bool _003CTryGetObjectivesProgress_003Eb__0(SeasonObjectiveModel i)
			{
				return false;
			}

			internal bool _003CTryGetObjectivesProgress_003Eb__1(PlayerObjectivesCompletion i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass24_1
		{
			public PlayerObjectivesCompletion i;

			internal bool _003CTryGetObjectivesProgress_003Eb__2(SeasonObjectiveModel objective)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass28_0
		{
			public int complete;

			public int specialCount;

			internal void _003CGetProgress_003Eb__0(CommunityGoalDataModel eventItem)
			{
			}

			internal void _003CGetProgress_003Eb__1(ChallengeMetadata metaData, int completed, int required)
			{
			}

			internal void _003CGetProgress_003Eb__2(string name, string emoji, HOLIDAY_COLLECTIBLE_TYPE type, string secondLine, int completed, int required)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass30_0
		{
			public int bits;

			internal bool _003CGetTrackProgress_003Eb__0(TrackItemModel i)
			{
				return false;
			}
		}

		private SeasonsPlayerDataModel SeasonsPlayerData__BackingField;

		private SeasonsDataModel SeasonsData__BackingField;

		private SeasonsStreakModel StreakPlayerData__BackingField;

		private List<SeasonRewardModelItem> Rewards__BackingField;

		public List<StuffItem> MyStuffItems;

		public SeasonsPlayerDataModel SeasonsPlayerData
		{
			get
			{
				return SeasonsPlayerData__BackingField;
			}
			set
			{
				SeasonsPlayerData__BackingField = value;
			}
		}

		public SeasonsDataModel SeasonsData
		{
			get
			{
				return SeasonsData__BackingField;
			}
			set
			{
				SeasonsData__BackingField = value;
			}
		}

		public SeasonsStreakModel StreakPlayerData
		{
			get
			{
				return StreakPlayerData__BackingField;
			}
			set
			{
				StreakPlayerData__BackingField = value;
			}
		}

		public List<SeasonRewardModelItem> Rewards
		{
			get
			{
				return Rewards__BackingField;
			}
			set
			{
				Rewards__BackingField = value;
			}
		}

		public bool TryCompleteObjective(ObjectiveType objectiveType, out string objectiveId)
		{
			objectiveId = null;
			return false;
		}

		public bool CompleteObjective(string objectiveId)
		{
			return false;
		}

		public bool ForceCompleteStreakObjective(string objectiveId)
		{
			return false;
		}

		public void AwardStreakToPlayer(Action<bool> onResult)
		{
		}

		public bool ContributeToCommunityGoal(string goalId)
		{
			return false;
		}

		private void FixStreakUI()
		{
		}

		public bool IsObjectiveCompleted(string objectiveId, bool vip = false)
		{
			return false;
		}

		public bool TryGetObjectivesProgress(out int completed, out int possible, bool vip = false)
		{
			completed = default;
			possible = default;
			return false;
		}

		public bool IsPremiumRewardAvailable()
		{
			return false;
		}

		public void SetBattlePassPurchased()
		{
		}

		public int GetCompletionForNarrative()
		{
			return 0;
		}

		public void GetProgress(SeasonsItemType itemType, Action<SeasonProgressModel> result, int initialCredits = 0, float progression = -1f)
		{
		}

		private SeasonProgressModel GetTrackProgress(int initialCredits, float progression)
		{
			return null;
		}

		public SeasonProgressModel GetTrackProgress(bool clampBits = true, bool local = true, int initialCredits = 0, float progression = -1f)
		{
			return null;
		}

		private bool _003CGetCompletionForNarrative_003Eb__27_0(SeasonNarrativeCompletionModel t)
		{
			return false;
		}
	}
}
