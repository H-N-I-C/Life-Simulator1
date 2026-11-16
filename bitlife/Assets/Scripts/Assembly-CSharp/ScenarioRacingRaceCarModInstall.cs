using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingRaceCarModInstall : Scenario
{
	private string _narrative;

	private SimRaceCar _raceCar;

	private List<SimRaceCarMod> _mods;

	private ArrayList _dropDownContents;

	public ScenarioRacingRaceCarModInstall(SimRaceCar raceCar)
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
