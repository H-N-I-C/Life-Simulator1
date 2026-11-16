public class ScenarioFightClubChampion : ScenarioLazyCsvBase
{
	private FightClubChampionActivity _activity;

	private readonly FightClubPostLeagueData _data;

	public ScenarioFightClubChampion(FightClubChampionActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
