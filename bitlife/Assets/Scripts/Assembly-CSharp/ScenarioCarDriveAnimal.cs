public class ScenarioCarDriveAnimal : Scenario
{
	private string _narrative;

	private readonly SimCar _car;

	private readonly string _speedType;

	private readonly string _destination;

	private readonly SimPerson _passenger;

	private DataAnimal _animal;

	public ScenarioCarDriveAnimal(SimCar car, string speedType, string destination, SimPerson passenger)
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
