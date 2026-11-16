public class ScenarioLoveHookup : Scenario
{
	protected SimLover _lover;

	protected bool _isWithEx;

	protected bool _isAtClub;

	public ScenarioLoveHookup()
	{
	}

	public ScenarioLoveHookup(bool isAtClub)
	{
	}

	public ScenarioLoveHookup(SimLover theExLover)
	{
	}

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
