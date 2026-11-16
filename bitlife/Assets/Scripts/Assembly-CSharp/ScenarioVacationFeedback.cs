using System.Collections;

public class ScenarioVacationFeedback : Scenario
{
	protected bool _isCruise;

	protected DataVacationDestination _vacationDestination;

	protected int _vacationClass;

	protected SimAircraft _aircraft;

	protected DataVacationCruise _cruise;

	protected int _cruiseCabin;

	protected bool _loverGoing;

	protected ArrayList _kidsGoingArray;

	protected string _feedbackGuestString;

	protected string _narrative;

	public ScenarioVacationFeedback(DataVacationDestination theDestination, int theClass, SimAircraft theAircraft, bool takingLover, ArrayList kidsArray, string theFeedbackGuestString)
	{
	}

	public ScenarioVacationFeedback(DataVacationCruise theCruise, int theCabin, bool takingLover, ArrayList kidsArray, string theFeedbackGuestString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
