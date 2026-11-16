using System.Collections;
using System.Collections.Generic;

public class ScenarioZooTradingPostAnimalOfferedTrade : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private readonly SimZooCompetitor _owningZoo;

	private List<string> _desiredBreeds;

	private readonly List<SimZooAnimal> _yourAnimalsDropDown;

	private ArrayList _dropDownArray;

	private readonly BracketParser _parser;

	public ScenarioZooTradingPostAnimalOfferedTrade(SimZooAnimal animal, SimZooCompetitor owningZoo)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
