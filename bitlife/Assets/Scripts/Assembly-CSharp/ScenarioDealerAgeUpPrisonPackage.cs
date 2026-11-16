public class ScenarioDealerAgeUpPrisonPackage : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private SimDealerCartelMember _sendingMember;

	private string _packageContents;

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
