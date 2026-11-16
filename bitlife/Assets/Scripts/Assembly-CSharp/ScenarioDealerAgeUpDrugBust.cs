public class ScenarioDealerAgeUpDrugBust : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
