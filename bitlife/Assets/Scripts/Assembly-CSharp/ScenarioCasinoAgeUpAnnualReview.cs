public class ScenarioCasinoAgeUpAnnualReview : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool HasItemsInstalled()
	{
		return false;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
