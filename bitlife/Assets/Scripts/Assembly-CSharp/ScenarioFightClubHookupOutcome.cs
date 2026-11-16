public class ScenarioFightClubHookupOutcome : ScenarioLazyCsvBase
{
	private FightClubHookupActivity _activity;

	public ScenarioFightClubHookupOutcome(FightClubHookupActivity activity, DismissCallback dismissCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
