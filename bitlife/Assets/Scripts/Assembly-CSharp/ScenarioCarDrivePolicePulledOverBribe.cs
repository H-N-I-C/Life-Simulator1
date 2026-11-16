using System.Collections;

public class ScenarioCarDrivePolicePulledOverBribe : Scenario
{
	protected ArrayList _crimeArray;

	private SimCar _car;

	private string _speedType;

	private string _destination;

	private SimPerson _passenger;

	public ScenarioCarDrivePolicePulledOverBribe(ArrayList theCrimeArray, SimCar car, string speedType, string destination, SimPerson passenger)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
