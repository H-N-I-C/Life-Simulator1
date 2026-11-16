public class ScenarioGameFightingLevelUp : ScenarioLazyCsvBase
{
	private readonly GameFightingOutcomeActivity _activity;

	private readonly SimFightClubFighterStats _heroStats;

	public const string CSV_FILE = "FightClub/Fight Club Scenarios - Fighters - Brawl";

	public ScenarioGameFightingLevelUp(GameFightingOutcomeActivity activity)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public override void RefreshScenario()
	{
	}
}
