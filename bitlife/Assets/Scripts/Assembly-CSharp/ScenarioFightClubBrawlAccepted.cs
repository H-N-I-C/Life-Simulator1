public class ScenarioFightClubBrawlAccepted : ScenarioLazyCsvBase
{
	private readonly FightClubBrawlActivityBase _brawl;

	public ScenarioFightClubBrawlAccepted(FightClubBrawlActivityBase activity, DismissCallback onDismissSelected)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
