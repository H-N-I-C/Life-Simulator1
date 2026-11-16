public class ScenarioOutdoorRideQualityTime : Scenario
{
	private SimRecreationalVehicle _recreationalVehicle;

	private string _narrative;

	private string _selectedPace;

	private string _selectedTime;

	private bool _fromEncounterScenario;

	public ScenarioOutdoorRideQualityTime(SimRecreationalVehicle recreationalVehicle, string selectedPace, string selectedTime, bool fromEncounterScenario = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private float MilesBiked()
	{
		return 0f;
	}
}
