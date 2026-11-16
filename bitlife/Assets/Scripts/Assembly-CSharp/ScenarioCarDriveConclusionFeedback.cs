public class ScenarioCarDriveConclusionFeedback : Scenario
{
	private string _narrative;

	private readonly SimCar _car;

	private string _speedType;

	private readonly string _destination;

	private readonly SimPerson _passenger;

	private readonly int _enjoymentImpact;

	public ScenarioCarDriveConclusionFeedback(SimCar car, string speedType, string destination, SimPerson passenger, int enjoymentImpact)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
