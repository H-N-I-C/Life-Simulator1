using System.Text;

public class ScenarioCarDriveOtherCarCrashFeedback : Scenario
{
	private readonly StringBuilder _builder;

	private readonly SimCar _car;

	private readonly string _speedType;

	private readonly string _destination;

	private readonly SimPerson _passenger;

	private readonly bool _heroDeathAllowed;

	private readonly bool _passengerDeathAllowed;

	private readonly int _fault;

	public ScenarioCarDriveOtherCarCrashFeedback(SimCar car, string speedType, string destination, SimPerson passenger, bool heroDeathAllowed, bool passengerDeathAllowed, int fault)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void PrintLine(string message)
	{
	}

	public override void DismissSelected()
	{
	}
}
