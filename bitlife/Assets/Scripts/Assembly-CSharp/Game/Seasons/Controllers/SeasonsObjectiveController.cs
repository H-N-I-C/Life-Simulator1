using System;
using System.Collections.Generic;
using Game.Seasons.Models;

namespace Game.Seasons.Controllers
{
	public class SeasonsObjectiveController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__5_0;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__6_0;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__6_1;

			public static Func<SeasonRewardModelItem, string> _003C_003E9__9_0;

			internal bool _003CGetNewCompletedVipObjectives_003Eb__5_0(SeasonObjectiveModel t)
			{
				return false;
			}

			internal bool _003CCheckVipObjectCanBeCompleted_003Eb__6_0(SeasonObjectiveModel t)
			{
				return false;
			}

			internal bool _003CCheckVipObjectCanBeCompleted_003Eb__6_1(SeasonObjectiveModel t)
			{
				return false;
			}

			internal string _003CCheckIfNeedToShowRewardIcon_003Eb__9_0(SeasonRewardModelItem t)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public string objectiveId;

			public int streakDay;

			internal bool _003CTryGetUnCompletedObjectiveByType_003Eb__0(PlayerObjectivesCompletion i)
			{
				return false;
			}

			internal bool _003CTryGetUnCompletedObjectiveByType_003Eb__1(SeasonObjectiveModel i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public string objectiveId;

			public int streakDay;

			internal bool _003CIsObjectiveCompleted_003Eb__0(PlayerObjectivesCompletion i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public int currentSeasonStreak;

			public string objectiveId;

			internal bool _003CIsObjectiveCompleted_003Eb__0(PlayerObjectivesCompletion t)
			{
				return false;
			}
		}

		private readonly List<string> _temporaryCompletedList;

		private SeasonRewardModelItem _lastRewardedStory;

		private static SeasonModel Data;

		public void SetData(SeasonModel data)
		{
		}

		public bool AgeUpActivityCompletion(out List<string> objectives)
		{
			objectives = null;
			return false;
		}

		private static List<string> GetNewCompletedVipObjectives()
		{
			return null;
		}

		private static bool CheckVipObjectCanBeCompleted(SeasonObjectiveModel vipObjective)
		{
			return false;
		}

		private static bool IsObjectiveCompleted(string objectiveId, int currentSeasonStreak)
		{
			return false;
		}

		public void ForceAgeUpActivityCompletion()
		{
		}

		private void CheckIfNeedToShowRewardIcon()
		{
		}

		public void CompleteObjectiveByType(ObjectiveType objectiveType, bool forceButton = false)
		{
		}

		public bool CheckVipEventsCompletion()
		{
			return false;
		}

		public Scenario CompleteObjectiveByTypeWithScenario(List<ObjectiveType> objectives)
		{
			return null;
		}

		private Scenario ContributeToCommunityGoal(string goalType)
		{
			return null;
		}

		public Scenario ContributeToCommunityGoal(CommunityGoalType goalType)
		{
			return null;
		}

		public Scenario ContributeToCommunityGoal(TriggerEvent goalType)
		{
			return null;
		}

		private Scenario GetScenarioOrButtonForObjectives(List<string> completed, bool forceButton = false)
		{
			return null;
		}

		private bool AddButtonForObjectives(List<string> completed, bool forceButton)
		{
			return false;
		}

		public bool TryGetUnCompletedObjectiveByType(ObjectiveType objectiveType, out SeasonObjectiveModel result)
		{
			result = null;
			return false;
		}

		public bool IsObjectiveCompleted(ObjectiveType objectiveType)
		{
			return false;
		}

		public List<string> GetLatestObjectives()
		{
			return null;
		}

		public SeasonRewardModelItem GetLastCompletedCommunityGoal()
		{
			return null;
		}
	}
}
