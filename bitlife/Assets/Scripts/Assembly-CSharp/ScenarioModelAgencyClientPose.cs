using Game;

public class ScenarioModelAgencyClientPose : Scenario
{
	private string _narrative;

	private string _activity;

	private int _activityRiskLevel;

	private readonly string _previousActivity;

	private int _numTimesRequestedNotToDoIt;

	private SimSpecialJobModel _modelJob;

	private readonly SimModelingAgencyClient _client;

	private SimModelGig _gig;

	private BracketParser _parser;

	private DataModelPhotoShootPose _data;

	public ScenarioModelAgencyClientPose(SimModelingAgencyClient client, SimModelGig gig, string previousActivity = null, int numTimesRequestedNotToDoIt = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private bool _003CChoiceSelectedAtIndex_003Eb__12_0(string t)
	{
		return false;
	}
}
