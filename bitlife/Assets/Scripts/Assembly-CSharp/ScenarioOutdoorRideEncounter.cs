using Game;

public class ScenarioOutdoorRideEncounter : Scenario
{
	private readonly SimRecreationalVehicle _recreationalVehicle;

	private string _narrative;

	private DataOutdoorBikeRideDumb _data;

	private BracketParser _parser;

	private readonly string _selectedPace;

	private readonly string _selectedTime;

	public ScenarioOutdoorRideEncounter(SimRecreationalVehicle recreationalVehicle, string selectedPace, string selectedTime)
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
