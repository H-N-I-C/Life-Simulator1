using Backend;

public class ScenarioMyStuffSuccess : Scenario
{
	private readonly StuffItem _stuffItem;

	private readonly IMyStuffUseCheck _check;

	public ScenarioMyStuffSuccess(IMyStuffUseCheck check, StuffItem stuffItem)
	{
	}

	public override void RefreshScenario()
	{
	}
}
