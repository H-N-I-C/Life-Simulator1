public class ScenarioFightClubPostLeagueFanInteraction : ScenarioLazyCsvBase
{
	private readonly FightClubPostLeagueInteractionActivity _activity;

	public ScenarioFightClubPostLeagueFanInteraction(FightClubPostLeagueInteractionActivity activity, ChoiceCallback choiceCallback = null)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
