using System.Collections;
using System.Collections.Generic;

public class ScenarioZooAnimalBreed : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private double _cost;

	private List<SimZooAnimal> _suitors;

	private ArrayList _dropDownArray;

	public ScenarioZooAnimalBreed(SimZooAnimal animal)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
