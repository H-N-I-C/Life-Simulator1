using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimVampireGoals : IEnumerable<SimVampireGoal>, IEnumerable
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimVampireGoal, bool> _003C_003E9__10_0;

		public static Func<SimVampireGoal, bool> _003C_003E9__28_0;

		internal bool _003CShouldShowInitialProgressFeedback_003Eb__10_0(SimVampireGoal goal)
		{
			return false;
		}

		internal bool _003CGetRandomTypeStringForCurrentGoals_003Eb__28_0(SimVampireGoal goal)
		{
			return false;
		}
	}

	private bool GoalsCompletionHasBeenShown__BackingField;

	private List<SimVampireGoal> _goals;

	private List<SimVampireGoal> _goalsJustImpacted;

	private int _numTimesGoalsNotImpactedFeedbackShown;

	public bool GoalsCompletionHasBeenShown
	{
		get
		{
			return GoalsCompletionHasBeenShown__BackingField;
		}
		set
		{
			GoalsCompletionHasBeenShown__BackingField = value;
		}
	}

	public static SimVampireGoals CreateRandom()
	{
		return null;
	}

	private void GenerateRandomGoals()
	{
	}

	private bool ShouldShowInitialProgressFeedback()
	{
		return false;
	}

	private bool ShouldShowGoalCompletionFeedback()
	{
		return false;
	}

	private bool ShouldShowGoalsNotImpactedFeedback()
	{
		return false;
	}

	public void GoalsNotImpactedFeedbackShown()
	{
	}

	public List<SimVampireGoal> GetGoalsJustImpacted()
	{
		return null;
	}

	public string GetGoalProgressStringForGoal(SimVampireGoal goal)
	{
		return null;
	}

	public string GetGoalProgressStringForGoalNumber(int goalNumber)
	{
		return null;
	}

	public string GetGoalStringForGoalNumber(int goalNumber)
	{
		return null;
	}

	public bool HasOccupationRequirement()
	{
		return false;
	}

	public string GetOccupationRequirementStringOrNull()
	{
		return null;
	}

	public bool HasStatRequirement()
	{
		return false;
	}

	public string GetRequirementStringForGoal(SimVampireGoal goal, bool plural = false)
	{
		return null;
	}

	public void ProcessPersonForGoal(SimPerson person, VAMPIRE_GOAL_ACTION action)
	{
	}

	private bool GoalProgressExists()
	{
		return false;
	}

	public bool GoalsCompleted()
	{
		return false;
	}

	public static VAMPIRE_GOAL_FEEDBACK_TYPE ShowAnyNeededGoalFeedback(SimPerson person, VAMPIRE_GOAL_ACTION action, Scenario parent)
	{
		return VAMPIRE_GOAL_FEEDBACK_TYPE.NONE;
	}

	public IEnumerator<SimVampireGoal> GetEnumerator()
	{
		return null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	public string GetRandomTypeStringForCurrentGoals()
	{
		return null;
	}
}
