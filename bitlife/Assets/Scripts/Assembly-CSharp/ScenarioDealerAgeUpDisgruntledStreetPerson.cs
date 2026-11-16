public class ScenarioDealerAgeUpDisgruntledStreetPerson : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private SimDealerStreetPerson _person;

	private SimDealerDisgruntledStreetPersonDrugCutSale _badSale;

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
