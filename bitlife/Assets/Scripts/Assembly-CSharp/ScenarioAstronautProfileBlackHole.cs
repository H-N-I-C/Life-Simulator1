public class ScenarioAstronautProfileBlackHole : Scenario
{
	private readonly SimSpaceBlackHole _blackHole;

	private readonly SimSpecialJobAstronaut _special;

	private MiniGameDataCelestialBody _gameData;

	private readonly bool _showPublish;

	public ScenarioAstronautProfileBlackHole(SimSpecialJobAstronaut special, SimSpaceBlackHole blackHole, bool showPublish = true)
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
