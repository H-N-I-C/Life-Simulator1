public class ScenarioFightClubHookupFeedback : ScenarioLazyCsvBase
{
	private FightClubHookupActivity _activity;

	public ScenarioFightClubHookupFeedback(FightClubHookupActivity activity, DismissCallback onDismissSelected)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
