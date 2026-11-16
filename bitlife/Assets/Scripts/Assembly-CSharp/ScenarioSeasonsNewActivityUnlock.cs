using Game.Seasons.Models;

public class ScenarioSeasonsNewActivityUnlock : Scenario
{
	private readonly SeasonRewardModelItem _communityGoalData;

	public override string EmojiString()
	{
		return null;
	}

	public ScenarioSeasonsNewActivityUnlock(SeasonRewardModelItem communityGoalData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int originalIndex)
	{
	}
}
