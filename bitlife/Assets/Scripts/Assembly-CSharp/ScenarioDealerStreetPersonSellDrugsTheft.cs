public class ScenarioDealerStreetPersonSellDrugsTheft : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerStreetPerson _person;

	private readonly SimDealerDrug _drug;

	private readonly int _doses;

	public ScenarioDealerStreetPersonSellDrugsTheft(SimDealerStreetPerson person, SimDealerDrug drug, int doses)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
