using System.Collections.Generic;

public class ScenarioZooAnimalTradingPostTradeOffer : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private double _marketValue;

	private readonly SimZooCompetitor _tradeZoo;

	private readonly SimZooAnimal _tradeAnimal;

	private List<SimZooCompetitor> _excludeList;

	private readonly BracketParser _parser;

	public ScenarioZooAnimalTradingPostTradeOffer(SimZooAnimal animal, SimZooCompetitor tradeZoo, SimZooAnimal tradeAnimal, List<SimZooCompetitor> excludeList = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
