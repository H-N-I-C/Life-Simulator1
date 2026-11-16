public class ScenarioDealerCartelDrugSaleEarnings : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _drug;

	private readonly double _moneyReceived;

	private SimDealerCartelMember _receivingMember;

	private readonly double _totalStreetValue;

	private readonly double _totalDrugManufacturingCost;

	public ScenarioDealerCartelDrugSaleEarnings(SimDealerDrug drug, int doses, double moneyReceived)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
