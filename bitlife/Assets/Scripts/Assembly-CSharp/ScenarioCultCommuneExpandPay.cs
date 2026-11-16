public class ScenarioCultCommuneExpandPay : Scenario
{
	private string _narrative;

	private readonly CULT_PROPERTY_TYPE _plotType;

	private double _cost;

	private readonly bool _rejectedByFollowers;

	private SimCult _cult;

	public ScenarioCultCommuneExpandPay(CULT_PROPERTY_TYPE plotType, double cost = 0.0, bool rejectedByFollowers = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
