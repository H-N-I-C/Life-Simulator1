public class ScenarioDealerAgeUpPotentialHire : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private SimDealerCartelMember _potentialMember;

	private SimDealerCartelMember _randomMember;

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
