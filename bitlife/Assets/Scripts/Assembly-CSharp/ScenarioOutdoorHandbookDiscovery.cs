public class ScenarioOutdoorHandbookDiscovery : Scenario
{
	private string _narrative;

	private readonly SimOutdoor _outdoor;

	private readonly SimOutdoorPlant _plant;

	private readonly SimOutdoorAnimal _animal;

	private readonly string _location;

	public ScenarioOutdoorHandbookDiscovery(SimOutdoor outdoor, SimOutdoorPlant plant, string location)
	{
	}

	public ScenarioOutdoorHandbookDiscovery(SimOutdoor outdoor, SimOutdoorAnimal animal, string location)
	{
	}

	public override void RefreshScenario()
	{
	}

	private string GetEmoji()
	{
		return null;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private string GetSpeciesName()
	{
		return null;
	}

	private string GetSpeciesPlural()
	{
		return null;
	}

	private void CheckDiscovery(OUTDOOR_HANDBOOK_ENTRY_TYPE animalType)
	{
	}
}
