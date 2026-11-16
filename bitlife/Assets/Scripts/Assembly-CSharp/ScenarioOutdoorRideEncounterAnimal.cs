public class ScenarioOutdoorRideEncounterAnimal : Scenario
{
	private readonly SimRecreationalVehicle _recreationalVehicle;

	private string _narrative;

	private readonly string _selectedPace;

	private readonly string _selectedTime;

	private DataAnimal _animal;

	public ScenarioOutdoorRideEncounterAnimal(SimRecreationalVehicle recreationalVehicle, string selectedPace, string selectedTime)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void PetAnimal(bool isRescue)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
