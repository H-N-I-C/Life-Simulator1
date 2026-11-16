using Game;

public class ScenarioModelCastingCallCampaignSummary : Scenario
{
	private string _narrative;

	private readonly SimModelGig _gig;

	private SimSpecialJobModel _modelJob;

	private DataModelCampaignSummaryColor _data;

	private readonly int _argueScore;

	private readonly string _poseBodyString;

	private readonly string _loverSnippet;

	public ScenarioModelCastingCallCampaignSummary(SimModelGig gig, string poseBodyString, string loverSnippet, int argueScore)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
