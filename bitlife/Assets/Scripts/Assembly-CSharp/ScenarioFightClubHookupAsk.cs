public class ScenarioFightClubHookupAsk : ScenarioLazyCsvBase
{
	private FightClubHookupActivity _activity;

	public ScenarioFightClubHookupAsk(FightClubHookupActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
