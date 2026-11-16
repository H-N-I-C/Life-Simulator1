using System.Collections;
using System.Collections.Generic;

public class ScenarioZooHabitatAddAnimalExisting : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooHabitat _habitat;

	private List<SimZooAnimal> _animals;

	private ArrayList _dropDownArray;

	public ScenarioZooHabitatAddAnimalExisting(SimZooHabitat habitat)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
