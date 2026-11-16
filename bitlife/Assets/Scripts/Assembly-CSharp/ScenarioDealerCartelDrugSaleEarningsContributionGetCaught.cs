public class ScenarioDealerCartelDrugSaleEarningsContributionGetCaught : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _receivingMember;

	private readonly double _amount;

	private SimDealerCartelMember _kingpin;

	private readonly double _drugCost;

	private readonly SimDealerDrug _drug;

	public ScenarioDealerCartelDrugSaleEarningsContributionGetCaught(SimDealerDrug drug, SimDealerCartelMember receivingMember, double amount, double drugCost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
