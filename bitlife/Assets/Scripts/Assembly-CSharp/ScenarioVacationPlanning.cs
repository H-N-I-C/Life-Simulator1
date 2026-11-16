using System.Collections;

public class ScenarioVacationPlanning : Scenario
{
	protected bool _isCruise;

	public ScenarioVacationPlanning()
	{
	}

	public ScenarioVacationPlanning(bool isCruise)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public bool WillLoverWantToGoToDestinationAndClass(DataVacationDestination destination, int theClass)
	{
		return false;
	}

	public ArrayList GetKidsWhoWantToGoToDestinationAndClass(DataVacationDestination destination, int theClass)
	{
		return null;
	}

	public bool WillLoverWantToGoOnCruiseAndCabin(DataVacationCruise cruise, int cabin)
	{
		return false;
	}

	public ArrayList GetKidsWhoWantToGoOnCruiseAndCabin(DataVacationCruise cruise, int cabin)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
