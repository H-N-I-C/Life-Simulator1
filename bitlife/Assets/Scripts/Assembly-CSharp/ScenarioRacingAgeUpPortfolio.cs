public class ScenarioRacingAgeUpPortfolio : Scenario
{
	private readonly SimRacing _racing;

	private static int _racesWon;

	private static int _racesLost;

	private static int _cupsWon;

	private static int _cupsLost;

	private string _narrative;

	public ScenarioRacingAgeUpPortfolio(SimRacing racing)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public static bool ShowAgeUpScenario(SimRacing racing)
	{
		return false;
	}
}
