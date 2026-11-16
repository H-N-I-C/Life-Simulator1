public class ScenarioCarDriveTrafficIncidentLawsuit : Scenario
{
	private string _narrative;

	private SimCar _car;

	private string _speedType;

	private string _destination;

	private SimPerson _passenger;

	private double _settlementAmount;

	private string _otherCarModel;

	public ScenarioCarDriveTrafficIncidentLawsuit(string otherCarModel, SimCar car, string speedType, string destination, SimPerson passenger)
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
