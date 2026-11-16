using Game;

public class ScenarioFightClubTrashTalk : ScenarioLazyCsvBase
{
	private readonly FightClubTrashTalkActivity _activity;

	private DataFightErTrashTalk _trashTalkData;

	public ScenarioFightClubTrashTalk(FightClubTrashTalkActivity activity, string id)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
