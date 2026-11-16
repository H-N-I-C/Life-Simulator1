using System.Collections;

public class ScenarioZooAnimalTradingPostTradeAdvertisement : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private double _marketValue;

	private readonly ArrayList _receivableAnimals;

	public ScenarioZooAnimalTradingPostTradeAdvertisement(SimZooAnimal animal, ArrayList receivableAnimals)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
