using System.Collections;

public class ScenarioVacationPlanningFamily : Scenario
{
	protected bool _isCruise;

	protected DataVacationDestination _vacationDestination;

	protected int _vacationClass;

	protected SimAircraft _aircraft;

	protected DataVacationCruise _cruise;

	protected int _cruiseCabin;

	protected bool _loverGoing;

	protected ArrayList _kidsGoingArray;

	protected int _buttonSoloIndexZI;

	protected int _buttonLoverIndexZI;

	protected int _buttonKidsIndexZI;

	protected int _buttonAllIndexZI;

	public ScenarioVacationPlanningFamily(DataVacationDestination theDestination, int theClass, SimAircraft theAircraft, bool loverWantsToGo, ArrayList kidsArray)
	{
	}

	public ScenarioVacationPlanningFamily(DataVacationCruise theCruise, int theCabin, bool loverWantsToGo, ArrayList kidsArray)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
