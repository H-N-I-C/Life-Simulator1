public class ScenarioAstronautProfilePlanet : Scenario
{
	private readonly SimSpacePlanet _planet;

	private readonly SimDiscovery _discovery;

	private readonly SimSpecialJobAstronaut _special;

	private MiniGameDataCelestialBody _gameData;

	private readonly bool _showPublish;

	public ScenarioAstronautProfilePlanet(SimSpecialJobAstronaut special, SimSpacePlanet planet, bool showPublish = true)
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
