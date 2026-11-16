public class ScenarioZooAnimalTradingPostSellOffer : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private SimZooAnimal _animal;

	private double _price;

	private bool _illegitimateBuyer;

	private string _buyerName;

	private SimZooCompetitor _zooBuyer;

	private double _offerPrice;

	private bool _alreadyOfferedIllegitimateBuyer;

	public ScenarioZooAnimalTradingPostSellOffer(SimZooAnimal animal, double price, bool alreadyOfferedIllegitimateBuyer = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
