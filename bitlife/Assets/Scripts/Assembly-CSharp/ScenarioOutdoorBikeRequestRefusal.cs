public class ScenarioOutdoorBikeRequestRefusal : Scenario
{
	private string _narrative;

	private readonly SimParent _parent;

	public ScenarioOutdoorBikeRequestRefusal(SimParent parent)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public static bool WillGetBicycle(SimParent parent)
	{
		return false;
	}
}
