public class ScenarioDealerStreetPersonSellDrugsTheftRepercussions : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerStreetPerson _person;

	private readonly SimDealerDrug _drug;

	private readonly int _doses;

	private readonly double _lostSaleAmount;

	private SimDealerCartelMember _lieutenant;

	private SimDealerCartelMember _kingpin;

	public ScenarioDealerStreetPersonSellDrugsTheftRepercussions(SimDealerStreetPerson person, SimDealerDrug drug, int doses, double lostSaleAmount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void ShowKingpinInsultsFeedback()
	{
	}

	private void ShowKingpinAttacksFeedback()
	{
	}
}
