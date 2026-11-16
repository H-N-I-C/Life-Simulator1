using Game;

public class ScenarioModelCastingCallPose : Scenario
{
	private string _narrative;

	private string _activity;

	private int _activityRiskLevel;

	private string _previousActivity;

	private int _numTimesRequestedNotToDoIt;

	private SimModelGig _gig;

	private SimSpecialJobModel _modelJob;

	private DataModelPhotoShootPose _data;

	private BracketParser _parser;

	public ScenarioModelCastingCallPose(SimModelGig gig, string previousActivity = null, int numTimesRequestedNotToDoIt = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private bool _003CChoiceSelectedAtIndex_003Eb__11_0(string t)
	{
		return false;
	}
}
