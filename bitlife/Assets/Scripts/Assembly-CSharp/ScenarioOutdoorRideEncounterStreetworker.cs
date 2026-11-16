public class ScenarioOutdoorRideEncounterStreetworker : Scenario
{
	private SimRecreationalVehicle _recreationalVehicle;

	private string _narrative;

	private string _selectedPace;

	private string _selectedTime;

	protected int _prostituteGender;

	protected string _prostituteTerm;

	protected string _prostituteAction;

	public ScenarioOutdoorRideEncounterStreetworker(SimRecreationalVehicle recreationalVehicle, string selectedPace, string selectedTime)
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
