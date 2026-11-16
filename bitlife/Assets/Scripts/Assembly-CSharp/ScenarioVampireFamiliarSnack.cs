using System.Collections;

public class ScenarioVampireFamiliarSnack : Scenario
{
	private string _narrative;

	private SimVampireFamiliar _familiar;

	private readonly VampireFamiliarSnackActivity _activity;

	public ScenarioVampireFamiliarSnack(SimVampireFamiliar familiar)
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
