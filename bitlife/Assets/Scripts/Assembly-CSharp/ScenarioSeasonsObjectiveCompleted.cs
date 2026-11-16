using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using Game.Seasons;
using Game.Seasons.Models;

public class ScenarioSeasonsObjectiveCompleted : Scenario
{
	public class DoubledRewardsFeedbackScenario : Scenario
	{
		private int _credits;

		public override string EmojiString()
		{
			return null;
		}

		public DoubledRewardsFeedbackScenario(int credits)
		{
		}

		public override void RefreshScenario()
		{
		}

		public override void ChoiceSelectedAtIndex(int indexZI)
		{
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SeasonObjectiveModel, int> _003C_003E9__13_1;

		internal int _003CRefreshScenario_003Eb__13_1(SeasonObjectiveModel t)
		{
			return 0;
		}
	}

	private readonly List<string> _objectiveIds;

	private List<CandyText> _tableEntries;

	private readonly int _tasksCompleted;

	private readonly int _totalTasks;

	private readonly int _bonusTasksCompleted;

	private readonly int _totalBonusTasks;

	private bool _videoAvailable;

	private int _totalCredits;

	private int _baseCredits;

	private DoubledRewardsFeedbackScenario _postPlayScenario;

	public ScenarioSeasonsObjectiveCompleted(List<string> objectives)
	{
	}

	~ScenarioSeasonsObjectiveCompleted()
	{
	}

	public override void OnTableDictionaryEntriesCreated(List<CandyText> entries)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void OnSeasonTimeChanged(SeasonTimeInfo info)
	{
	}

	public override void Closed()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private bool _003CRefreshScenario_003Eb__13_0(SeasonObjectiveModel i)
	{
		return false;
	}

	private void _003CChoiceSelectedAtIndex_003Eb__14_0()
	{
	}
}
