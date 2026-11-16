public class ScenarioFightClubPostLeagueSportsmanshipInteraction : ScenarioLazyCsvBase
{
	private readonly FightClubPostLeagueInteractionActivity _activity;

	private readonly FightClubPostLeagueData _data;

	public ScenarioFightClubPostLeagueSportsmanshipInteraction(FightClubPostLeagueInteractionActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
