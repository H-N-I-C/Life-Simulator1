public class ScenarioAstronautProfileComet : Scenario
{
	private readonly SimSpaceComet _comet;

	private readonly SimSpecialJobAstronaut _special;

	private MiniGameDataCelestialBody _gameData;

	private readonly bool _showPublish;

	public ScenarioAstronautProfileComet(SimSpecialJobAstronaut special, SimSpaceComet comet, bool showPublish = true)
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
