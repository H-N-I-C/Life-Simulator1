public class ScenarioCasinoGamblerDeath : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoGambler _gambler;

	private readonly string _deathString;

	private readonly string _narrativeString;

	public ScenarioCasinoGamblerDeath(SimCasinoGambler gambler, string deathString, string narrativeString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
