using Game;

public class ScenarioDealerStreetPersonSellDrugsTradeOffer : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerStreetPerson _person;

	private readonly SimDealerDrug _drug;

	private readonly int _desiredDoses;

	private readonly double _totalPrice;

	private string _itemString;

	private DataDealerDrugBarterItem _data;

	public ScenarioDealerStreetPersonSellDrugsTradeOffer(SimDealerStreetPerson person, SimDealerDrug drug, int desiredDoses, double pricePerDose)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
