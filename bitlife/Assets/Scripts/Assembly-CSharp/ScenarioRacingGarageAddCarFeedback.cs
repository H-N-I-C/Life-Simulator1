public class ScenarioRacingGarageAddCarFeedback : Scenario
{
	private string _narrative;

	private readonly SimRacing _racing;

	private readonly SimCar _simCar;

	private readonly SimAuctionItem _blackMarketCar;

	public ScenarioRacingGarageAddCarFeedback(SimRacing racing, SimCar simCar)
	{
	}

	public ScenarioRacingGarageAddCarFeedback(SimRacing racing, SimAuctionItem blackMarketCar)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
