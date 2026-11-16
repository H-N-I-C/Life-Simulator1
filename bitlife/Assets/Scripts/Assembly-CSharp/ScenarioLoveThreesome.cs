public class ScenarioLoveThreesome : Scenario
{
	protected string _narrative;

	protected bool _freshRequest;

	protected bool _loverAgrees;

	protected SimLover _threesomeLover;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
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
