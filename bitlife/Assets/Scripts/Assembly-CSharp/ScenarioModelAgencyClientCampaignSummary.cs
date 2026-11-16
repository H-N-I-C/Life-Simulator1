using Game;

public class ScenarioModelAgencyClientCampaignSummary : Scenario
{
	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private SimModelingAgencyClient _client;

	private SimModelGig _gig;

	private readonly int _argueScore;

	private readonly string _poseBodyString;

	private readonly string _loverSnippet;

	private readonly string _feedbackLog;

	private DataModelCampaignSummaryColor _data;

	private double _netEarnings;

	public ScenarioModelAgencyClientCampaignSummary(SimModelingAgencyClient client, SimModelGig gig, string poseBodyString, string feedbackLog, string loverSnippet, int argueScore)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
