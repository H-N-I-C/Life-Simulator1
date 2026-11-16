public class ScenarioModelAgencyClientAuditionBooked : Scenario
{
	private string _narrative;

	private double _compensation;

	private readonly bool _requestedBetterGig;

	private bool _betterGigRequestsAllowed;

	private bool _requestedGigFromAgentMenu;

	private SimSpecialJobModel _modelJob;

	private SimModelingAgencyClient _client;

	private SimModelGig _gig;

	public ScenarioModelAgencyClientAuditionBooked(SimModelingAgencyClient client = null, SimModelGig gig = null, double compensation = 0.0, bool requestedBetterGig = false, bool requestedGigFromAgentMenu = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
