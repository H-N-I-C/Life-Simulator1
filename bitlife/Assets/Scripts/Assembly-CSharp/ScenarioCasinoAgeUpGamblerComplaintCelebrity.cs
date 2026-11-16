public class ScenarioCasinoAgeUpGamblerComplaintCelebrity : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private SimCasinoGambler _gambler;

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
