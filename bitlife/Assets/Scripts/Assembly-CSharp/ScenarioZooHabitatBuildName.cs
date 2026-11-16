using System.Collections;

public class ScenarioZooHabitatBuildName : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooHabitat _habitat;

	public ScenarioZooHabitatBuildName(SimZooHabitat habitat)
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
