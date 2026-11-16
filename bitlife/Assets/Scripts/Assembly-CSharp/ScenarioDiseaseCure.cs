public class ScenarioDiseaseCure : Scenario
{
	protected SimDisease _disease;

	public ScenarioDiseaseCure(SimDisease theDisease)
	{
	}

	public override void DismissSelected()
	{
	}

	public new bool PreventRandomPushAfterScenario()
	{
		return false;
	}
}
