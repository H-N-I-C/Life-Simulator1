using System.Collections;
using Game;

public class ScenarioModelCastingCallAudition : Scenario
{
	private string _narrative;

	private readonly SimModelGig _gig;

	private SimSpecialJobModel _modelJob;

	private DataModelAuditionSnippet _data;

	public ScenarioModelCastingCallAudition(SimModelGig gig)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
