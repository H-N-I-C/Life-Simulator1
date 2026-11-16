using System;

[Serializable]
public class SimRaceGoal
{
	public string GoalString;

	public int Index;

	public TimeSpan Time;

	public bool Completed;

	public SimRaceGoal(int index, string time)
	{
	}

	public string GetGoalString()
	{
		return null;
	}

	public bool HasAchievedGoal(SimRacingTrack track)
	{
		return false;
	}

	public void CompleteGoal()
	{
	}

	public string GetGoalTimeString()
	{
		return null;
	}
}
