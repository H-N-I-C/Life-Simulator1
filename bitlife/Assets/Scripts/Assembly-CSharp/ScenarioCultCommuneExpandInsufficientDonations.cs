public class ScenarioCultCommuneExpandInsufficientDonations : Scenario
{
	private string _narrative;

	private readonly double _cost;

	private readonly double _fundsRaised;

	private readonly double _difference;

	private readonly CULT_PROPERTY_TYPE _plotType;

	private SimCult _cult;

	public ScenarioCultCommuneExpandInsufficientDonations(CULT_PROPERTY_TYPE plotType, double cost, double fundsRaised)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
