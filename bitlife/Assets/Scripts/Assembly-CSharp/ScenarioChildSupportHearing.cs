using System.Collections.Generic;
using Game;

public class ScenarioChildSupportHearing : Scenario
{
	private readonly SimLover _exLover;

	private readonly List<SimChild> _children;

	private int _exCase;

	private int _yourCase;

	private readonly int _lawFirm;

	private List<DataCustodyCase> _data;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	private ScenarioChoice _rewardedVideoChoice;

	public ScenarioChildSupportHearing(List<SimChild> children, SimLover exLover, int index)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void CalculateCaseScores()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private void UpdateLifeLog(string text)
	{
	}

	private void ImpactHeroCase(int impactScore)
	{
	}

	private int GetCaseScore(int initialValue, double netWorth, bool hasJob)
	{
		return 0;
	}

	private void _003CChoiceSelectedAtIndex_003Eb__11_0()
	{
	}
}
