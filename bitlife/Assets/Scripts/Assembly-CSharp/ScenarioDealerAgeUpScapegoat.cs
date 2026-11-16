public class ScenarioDealerAgeUpScapegoat : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private SimDealerCartelMember _kingpin;

	private SimDealerCartelMember _jailedMember;

	private double _cashOffer;

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
