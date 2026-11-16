public class ScenarioLightningStrike : Scenario
{
	protected string _narrative;

	protected bool _willDie;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
