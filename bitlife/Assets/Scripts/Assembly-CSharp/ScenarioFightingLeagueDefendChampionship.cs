public class ScenarioFightingLeagueDefendChampionship : ScenarioLazyCsvBase
{
	private readonly FightClubDefendChampionshipActivity _activity;

	public ScenarioFightingLeagueDefendChampionship(FightClubDefendChampionshipActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
