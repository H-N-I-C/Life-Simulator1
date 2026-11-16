public class ScenarioRacingGarageAddCarProfile : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCar _raceCar;

	private readonly SimCar _simCar;

	private readonly SimAuctionItem _blackMarketCar;

	public ScenarioRacingGarageAddCarProfile(SimRacing racing, SimCar simCar)
	{
	}

	public ScenarioRacingGarageAddCarProfile(SimRacing racing, SimAuctionItem blackMarketCar)
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
