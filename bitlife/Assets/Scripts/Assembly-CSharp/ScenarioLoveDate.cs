public class ScenarioLoveDate : Scenario
{
	protected SimLover _loverCandidate;

	protected bool _metAtGym;

	protected bool _metAtLibrary;

	protected bool _gettingAskedOut;

	protected bool _isFromPray;

	protected bool _isFromRacing;

	protected bool _isFromDateMenu;

	public ScenarioLoveDate()
	{
	}

	public ScenarioLoveDate(bool metAtGym)
	{
	}

	public ScenarioLoveDate(SimLover theCandidate, bool isFromPray, bool isFromRacing = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static string GetColorfulRejectionStringForLoverCandidate(SimLover theLover)
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
