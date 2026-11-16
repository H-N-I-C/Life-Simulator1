using System.Collections;

public class ScenarioCarDrive : Scenario
{
	private string _narrative;

	private readonly SimCar _car;

	private ArrayList _passengers;

	private ArrayList _passengerDropDownContents;

	public ScenarioCarDrive(SimCar car)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private ArrayList CreatePassengerList()
	{
		return null;
	}

	private string RelationshipString(SimPerson person)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
