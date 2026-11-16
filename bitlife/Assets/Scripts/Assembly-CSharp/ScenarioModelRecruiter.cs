public class ScenarioModelRecruiter : Scenario
{
	private string _narrative;

	private MODEL_RECRUITER_ORIGIN _origin;

	private SimSpecialJobModel _modelJob;

	private SimModelingAgency _agency;

	private bool _hasModelJobPack;

	public ScenarioModelRecruiter(MODEL_RECRUITER_ORIGIN origin)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public static bool RecruiterWillNotice()
	{
		return false;
	}
}
