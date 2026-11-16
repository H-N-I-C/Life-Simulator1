public class ScenarioModelLoverArgument : Scenario
{
	private string _narrative;

	private SimLover _lover;

	private SimModelGig _gig;

	private SimSpecialJobModel _modelJob;

	private readonly string _poseBodyString;

	private readonly string _loverSnippet;

	public ScenarioModelLoverArgument(SimLover lover, SimModelGig gig, string poseBodyString, string loverSnippet)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
