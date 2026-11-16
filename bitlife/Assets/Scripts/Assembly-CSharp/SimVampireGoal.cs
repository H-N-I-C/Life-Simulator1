using System;
using System.Collections.Generic;

[Serializable]
public class SimVampireGoal
{
	private VAMPIRE_GOAL_ACTION _action;

	private int _requiredCount;

	private int _progressCount;

	private string _requiredOccupation;

	private VAMPIRE_GOAL_STAT_TYPE _requiredStat;

	private int _requiredSex;

	private int _requiredSexuality;

	private bool _initialProgressHasBeenShown;

	public string RequiredOccupation => null;

	public SimVampireGoal(VAMPIRE_GOAL_REQUIREMENT_TYPE type, VAMPIRE_GOAL_ACTION action)
	{
	}

	public string GetGoalProgressString()
	{
		return null;
	}

	public string GetGoalString()
	{
		return null;
	}

	private string GetActionString(bool pastTense = false)
	{
		return null;
	}

	public int GetRequiredCount()
	{
		return 0;
	}

	public int GetProgressCount()
	{
		return 0;
	}

	public bool HasOccupationRequirement()
	{
		return false;
	}

	public bool HasStatRequirement()
	{
		return false;
	}

	public string GetRequirementString(bool plural = false)
	{
		return null;
	}

	public bool ProcessPersonForGoal(SimPerson person, VAMPIRE_GOAL_ACTION action)
	{
		return false;
	}

	private bool PersonMatchesRequirement(SimPerson person)
	{
		return false;
	}

	public bool GoalCompleted()
	{
		return false;
	}

	public static int GetMinimumStatValueForStatType(VAMPIRE_GOAL_STAT_TYPE statType)
	{
		return 0;
	}

	public static int GetMaximumStatValueForStatType(VAMPIRE_GOAL_STAT_TYPE statType)
	{
		return 0;
	}

	public static string GetStatTypeString(VAMPIRE_GOAL_STAT_TYPE statType, bool plural = false)
	{
		return null;
	}

	public static VAMPIRE_GOAL_STAT_TYPE GetStatTypeForString(string statTypeString)
	{
		return VAMPIRE_GOAL_STAT_TYPE.NONE;
	}

	public string GetTypeString()
	{
		return null;
	}

	public static List<string> GetStatTypeStringList()
	{
		return null;
	}

	public void InitialProgressFeedbackShown()
	{
	}

	public bool IsFirstImpact()
	{
		return false;
	}

	public void SetToAlmostFinished()
	{
	}
}
