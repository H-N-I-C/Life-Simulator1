public class ScenarioRouletteCasinoBet : Scenario
{
	private readonly SimCasinoSession _session;

	private RouletteBet Bet__BackingField;

	public RouletteBet Bet
	{
		get
		{
			return Bet__BackingField;
		}
		set
		{
			Bet__BackingField = value;
		}
	}

	public ScenarioRouletteCasinoBet(SimCasinoSession theSession)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
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
