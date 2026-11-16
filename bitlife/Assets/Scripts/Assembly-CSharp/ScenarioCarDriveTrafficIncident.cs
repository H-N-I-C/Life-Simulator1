using Game;

public class ScenarioCarDriveTrafficIncident : Scenario
{
	private string _narrative;

	private SimCar _car;

	private string _speedType;

	private string _destination;

	private SimPerson _passenger;

	private BracketParser _bracketParser;

	private DataRacingDriveTrafficIncident _Scenario;

	public ScenarioCarDriveTrafficIncident(SimCar car, string speedType, string destination, SimPerson passenger)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private string GetPassengerType(SimPerson person)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
