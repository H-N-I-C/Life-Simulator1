public class ScenarioGameSportsBetting : Scenario
{
	protected SimCasinoSession _session;

	private SimCasinoGameSportsBetting _game;

	public ScenarioGameSportsBetting(SimCasinoSession theSession, bool isHeroBetting = false)
	{
	}

	public override void MiniGameCompleted()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
