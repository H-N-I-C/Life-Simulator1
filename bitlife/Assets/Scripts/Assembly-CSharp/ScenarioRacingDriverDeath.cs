public class ScenarioRacingDriverDeath : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCarDriver _driver;

	private readonly string _deathString;

	public ScenarioRacingDriverDeath(SimRaceCarDriver driver, string deathString)
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
