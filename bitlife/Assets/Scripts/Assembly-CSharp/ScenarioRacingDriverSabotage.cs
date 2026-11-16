using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioRacingDriverSabotage : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string sabotageType;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataRacingRacerSabotageMethod t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimRaceCarDriver _driver;

	private readonly SimRaceCar _raceCar;

	private List<DataRacingRacerSabotageMethod> _listItems;

	public ScenarioRacingDriverSabotage(SimRaceCarDriver driver)
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
