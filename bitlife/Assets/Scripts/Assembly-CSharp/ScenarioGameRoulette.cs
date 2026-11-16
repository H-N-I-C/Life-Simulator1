public class ScenarioGameRoulette : Scenario
{
	private readonly SimCasinoSession _session;

	private readonly RouletteBet Bet__BackingField;

	public RouletteBet Bet => Bet__BackingField;

	public ScenarioGameRoulette(SimCasinoSession theSession, RouletteBet theBet)
	{
	}

	public void DisableTable()
	{
	}

	public void UpdateBodyText(string text)
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
