using System.Collections;

public class ScenarioVacation : Scenario
{
	protected bool _isCruise;

	protected DataVacationDestination _vacationDestination;

	protected int _vacationClass;

	protected SimAircraft _aircraft;

	protected DataVacationCruise _cruise;

	protected int _cruiseCabin;

	protected bool _loverGoing;

	protected ArrayList _kidsGoingArray;

	protected float _cost;

	public ScenarioVacation(DataVacationDestination theDestination, int theClass, SimAircraft theAircraft, bool takingLover, ArrayList kidsArray)
	{
	}

	public ScenarioVacation(DataVacationCruise theCruise, int theCabin, bool takingLover, ArrayList kidsArray)
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
