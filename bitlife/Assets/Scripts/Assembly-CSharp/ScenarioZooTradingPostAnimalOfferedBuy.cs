public class ScenarioZooTradingPostAnimalOfferedBuy : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private SimZooAnimal _animal;

	private SimZooCompetitor _owningZoo;

	private double _price;

	private BracketParser _parser;

	public ScenarioZooTradingPostAnimalOfferedBuy(SimZooAnimal animal, SimZooCompetitor owningZoo)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
