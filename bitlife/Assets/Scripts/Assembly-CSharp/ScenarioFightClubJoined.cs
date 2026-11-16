public class ScenarioFightClubJoined : ScenarioLazyCsvBase
{
	private readonly FightClub_NewFighterActivity _activity;

	public ScenarioFightClubJoined(FightClub_NewFighterActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
