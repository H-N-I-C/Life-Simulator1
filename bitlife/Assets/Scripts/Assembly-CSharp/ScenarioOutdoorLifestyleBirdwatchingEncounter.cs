using Game;

public class ScenarioOutdoorLifestyleBirdwatchingEncounter : Scenario
{
	private DataOutdoorBird _bird;

	private DataOutdoorBirdDiscoveryScenario _birdDiscoveryScenario;

	private string _site;

	private readonly SimOutdoor _outdoor;

	private string _narrative;

	public ScenarioOutdoorLifestyleBirdwatchingEncounter(SimOutdoor outdoor, string site)
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
