public class ScenarioEthicsSaveLife : Scenario
{
	protected string _narrative;

	protected int _failedRescueDeathType;

	protected int _rescueeGender;

	protected bool _rescueeIsChild;

	protected int _rescueDangerScore;

	protected int _rescueAuthorities;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
