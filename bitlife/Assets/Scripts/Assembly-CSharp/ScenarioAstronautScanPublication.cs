using System.Collections;

public class ScenarioAstronautScanPublication : Scenario
{
	private readonly SimTelescopeRecording _recording;

	private string _narrative;

	private readonly SimSpecialJobAstronaut _special;

	public ScenarioAstronautScanPublication(SimSpecialJobAstronaut special, SimTelescopeRecording recording)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
