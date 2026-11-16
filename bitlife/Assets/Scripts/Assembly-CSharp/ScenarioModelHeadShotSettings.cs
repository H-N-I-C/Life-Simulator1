using System.Collections;

public class ScenarioModelHeadShotSettings : Scenario
{
	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private double _cost;

	private bool _isGrayscale;

	public ScenarioModelHeadShotSettings(double cost, bool isGrayscale)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
