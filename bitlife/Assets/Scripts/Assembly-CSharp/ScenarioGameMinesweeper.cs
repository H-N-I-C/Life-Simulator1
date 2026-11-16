public class ScenarioGameMinesweeper : Scenario
{
	protected MiniGameDataMinesweeper _gameData;

	public ScenarioGameMinesweeper(MiniGameDataMinesweeper theGameData)
	{
	}

	public override void MiniGameCompleted()
	{
	}

	public bool OfferRewardedVideoToIdentifyMines()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
