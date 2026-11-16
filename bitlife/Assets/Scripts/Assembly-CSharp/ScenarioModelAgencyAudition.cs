using System.Collections;
using Game;

public class ScenarioModelAgencyAudition : Scenario
{
	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private SimModelingAgency _agency;

	private DataModelAuditionSnippet _dataSnippet;

	public ScenarioModelAgencyAudition(SimModelingAgency agency)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
