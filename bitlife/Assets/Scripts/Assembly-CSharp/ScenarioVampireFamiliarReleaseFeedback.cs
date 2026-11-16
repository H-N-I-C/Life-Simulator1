public class ScenarioVampireFamiliarReleaseFeedback : Scenario
{
	private string _narrative;

	private readonly SimVampireFamiliar _familiar;

	private readonly VampireFamiliarRelease _release;

	public ScenarioVampireFamiliarReleaseFeedback(SimVampireFamiliar familiar, VampireFamiliarRelease release = VampireFamiliarRelease.None)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
