public class FightClubSportmanshipActivity : ActivityLazyCsvBase
{
	private FightClubPostLeagueData LeagueData__BackingField;

	public string CurrentScenarioId => null;

	public FightClubPostLeagueData LeagueData
	{
		get
		{
			return LeagueData__BackingField;
		}
		set
		{
			LeagueData__BackingField = value;
		}
	}

	public FightClubSportmanshipActivity(Life life, FightClubPostLeagueData data)
		: base(null)
	{
	}
}
