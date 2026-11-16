using System.Collections;

public class ScenarioZooAgeUpAnimalBirth : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private SimZooAnimal _parentAnimal;

	private SimZooAnimal _babyAnimal;

	public ScenarioZooAgeUpAnimalBirth(SimZooAnimal parentAnimal)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
