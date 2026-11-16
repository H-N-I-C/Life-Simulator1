public class ScenarioCasinoAgeUpGamblerLost : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	public override void RefreshScenario()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
