public class ScenarioRacingDriverEstablishRivalry : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCarDriver _driver;

	private SimRaceCarDriver _currentRival;

	public ScenarioRacingDriverEstablishRivalry(SimRaceCarDriver driver)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
