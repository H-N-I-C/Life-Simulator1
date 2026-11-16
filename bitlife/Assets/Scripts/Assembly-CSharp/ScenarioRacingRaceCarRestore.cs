using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingRaceCarRestore : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCar _raceCar;

	private SimCarRepair _neededRepair;

	private List<double> _costArray;

	private readonly BracketParser _parser;

	public ScenarioRacingRaceCarRestore(SimRaceCar raceCar)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void SetupCostArray()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
