using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioZooHabitatAddAnimalTradingPostPurchaseRequest : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private SimZooHabitat _habitat;

	private List<DataZooAnimal> _animals;

	private ArrayList _breedDropDownArray;

	public ScenarioZooHabitatAddAnimalTradingPostPurchaseRequest(SimZooHabitat habitat)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
