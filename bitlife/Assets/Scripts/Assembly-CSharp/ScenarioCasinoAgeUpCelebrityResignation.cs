public class ScenarioCasinoAgeUpCelebrityResignation : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private SimCasinoCelebrity _celebrity;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
