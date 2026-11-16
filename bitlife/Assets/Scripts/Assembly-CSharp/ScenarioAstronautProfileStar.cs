public class ScenarioAstronautProfileStar : Scenario
{
	private readonly SimSpaceStar _star;

	private readonly SimSpecialJobAstronaut _special;

	private MiniGameDataCelestialBody _gameData;

	private readonly bool _showPublish;

	public ScenarioAstronautProfileStar(SimSpecialJobAstronaut special, SimSpaceStar star, bool showPublish = true)
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
