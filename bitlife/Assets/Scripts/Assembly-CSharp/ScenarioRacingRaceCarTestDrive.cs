using System.Collections;

public class ScenarioRacingRaceCarTestDrive : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCar _raceCar;

	public ScenarioRacingRaceCarTestDrive(SimRaceCar raceCar)
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
