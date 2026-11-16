using Game;

public class ScenarioFightClubDumb : DumbScenarioLazyCsvBase<DataClubUpdateUFLDumbScenario>
{
	private readonly FightClubDumbActivity _activity;

	public ScenarioFightClubDumb(FightClubDumbActivity activity)
		: base((DumbScenarioLazyActivity<DataClubUpdateUFLDumbScenario>)null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	protected override string GetExternalScenarioTitle()
	{
		return null;
	}

	protected override string GetEmojiString()
	{
		return null;
	}
}
