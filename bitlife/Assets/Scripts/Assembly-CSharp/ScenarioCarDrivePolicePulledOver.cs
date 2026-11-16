using Game;

public class ScenarioCarDrivePolicePulledOver : Scenario
{
	private string _narrative;

	private readonly SimCar _car;

	private readonly string _speedType;

	private readonly string _destination;

	private readonly SimPerson _passenger;

	private DataRacingDrivePolicePullOverColor _colorScenario;

	public ScenarioCarDrivePolicePulledOver(SimCar car, string speedType, string destination, SimPerson passenger)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private int GetOfficerProfessionalism()
	{
		return 0;
	}

	public override string EmojiString()
	{
		return null;
	}
}
