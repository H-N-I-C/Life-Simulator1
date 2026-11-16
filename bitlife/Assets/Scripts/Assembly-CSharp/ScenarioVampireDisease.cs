public class ScenarioVampireDisease : Scenario
{
	private string _narrative;

	private string _remedy;

	private SimPerson _person;

	private int _remedyEffectviness;

	private int _remedyRisk;

	private string _disease;

	private readonly BracketParser _bracketParser;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	public ScenarioVampireDisease(SimPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void PushTakeChancesScenario()
	{
	}

	private void PushTakeItScenario()
	{
	}

	private void PushCureMyselfScenario()
	{
	}

	private void HeroDeathByDisease()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
