using Game;

public class ScenarioCarDriveDumb : Scenario
{
	private string _narrative;

	private SimCar _car;

	private string _speedType;

	private string _destination;

	private SimPerson _passenger;

	private DataRacingDriveDumbScenario _randomScenario;

	private BracketParser _bracketParser;

	public ScenarioCarDriveDumb(SimCar car, string speedType, string destination, SimPerson passenger)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private BracketParser CreateBracketParser(string genderBracket)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
