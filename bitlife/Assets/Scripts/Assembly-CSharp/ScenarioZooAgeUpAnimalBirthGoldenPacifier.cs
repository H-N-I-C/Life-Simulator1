using System.Collections;

public class ScenarioZooAgeUpAnimalBirthGoldenPacifier : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _parentAnimal;

	private SimZooAnimal _babyAnimal;

	public ScenarioZooAgeUpAnimalBirthGoldenPacifier(SimZooAnimal parentAnimal)
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
