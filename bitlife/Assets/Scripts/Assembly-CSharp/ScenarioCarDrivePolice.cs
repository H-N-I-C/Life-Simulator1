public class ScenarioCarDrivePolice : Scenario
{
	private string _narrative;

	private SimCar _car;

	private string _speedType;

	private string _destination;

	private SimPerson _passenger;

	public ScenarioCarDrivePolice(SimCar car, string speedType, string destination, SimPerson passenger)
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
