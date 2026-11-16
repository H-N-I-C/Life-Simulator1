using Game;

public class ScenarioOutdoorBirdSeeing : Scenario
{
	private readonly SimOutdoor _outdoor;

	private readonly DataOutdoorBird _bird;

	private int _enjoyment;

	private readonly DataOutdoorBirdDiscoveryScenario _birdDiscoveryScenario;

	private string _narrative;

	public ScenarioOutdoorBirdSeeing(SimOutdoor outdoor, DataOutdoorBird bird, DataOutdoorBirdDiscoveryScenario birdDiscoveryScenario)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void TryFeed(bool success)
	{
	}

	private void GetAttacked()
	{
	}

	private void ObserveBird()
	{
	}

	private void PushEndScenario()
	{
	}

	private void HeroDied()
	{
	}

	private void CheckDiscovery(OUTDOOR_HANDBOOK_ENTRY_TYPE animalType)
	{
	}
}
