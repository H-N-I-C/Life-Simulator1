using Game;

public class ScenarioOutdoorLifestyleSnorkelingEncounter : Scenario
{
	private DataOutdoorMarineLife _marineLife;

	private DataOutdoorSnorkelingDiscoveryScenario _discoveryScenario;

	private string _site;

	private readonly SimOutdoor _outdoor;

	private string _narrative;

	public ScenarioOutdoorLifestyleSnorkelingEncounter(SimOutdoor outdoor, string site)
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
