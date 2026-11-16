using Game;

public class ScenarioRacingDriverSabotageBreakingNews : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCarDriver _driver;

	private readonly SimRaceCar _raceCar;

	private string _sabotageType;

	private readonly DataRacingRacerSabotageMethod _sabotage;

	public ScenarioRacingDriverSabotageBreakingNews(SimRaceCarDriver driver, DataRacingRacerSabotageMethod sabotage)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
