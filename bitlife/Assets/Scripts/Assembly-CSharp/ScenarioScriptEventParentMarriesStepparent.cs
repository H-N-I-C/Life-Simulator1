public class ScenarioScriptEventParentMarriesStepparent : Scenario
{
	protected SimParent _parent;

	public ScenarioScriptEventParentMarriesStepparent(SimParent theParent)
	{
	}

	public override void RefreshScenario()
	{
	}

	public new bool PreventRandomPushAfterScenario()
	{
		return false;
	}
}
