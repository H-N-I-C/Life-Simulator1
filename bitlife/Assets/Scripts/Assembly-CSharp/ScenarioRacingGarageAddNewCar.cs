public class ScenarioRacingGarageAddNewCar : Scenario
{
	private string _narrative;

	private readonly SimRacing _racing;

	private readonly SimRaceCar _raceCar;

	private readonly SimCar _simCar;

	private readonly SimAuctionItem _blackMarketCar;

	public ScenarioRacingGarageAddNewCar(SimRacing racing, SimCar simCar)
	{
	}

	public ScenarioRacingGarageAddNewCar(SimRacing racing, SimAuctionItem blackMarketCar)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
