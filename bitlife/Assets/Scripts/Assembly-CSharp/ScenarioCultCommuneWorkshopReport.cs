public class ScenarioCultCommuneWorkshopReport : Scenario
{
	private string _narrative;

	private readonly string _productString;

	private readonly string _locationString;

	private readonly string _reportString;

	private int _unitsCreated;

	private readonly int _unitsSold;

	private readonly int _unitsSeized;

	private readonly int _followersGained;

	private readonly int _followersDetained;

	private readonly int _followersArrested;

	private readonly double _profit;

	public ScenarioCultCommuneWorkshopReport(string productString, string locationString, string reportString, int unitsCreated, int unitsSold, int unitsSeized, double profit, int followersGained, int followersDetained, int followersArrested)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
