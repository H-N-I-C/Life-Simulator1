public class FightClubChampionActivity : ActivityLazyCsvBase
{
	private FightClubPostLeagueData LeagueData__BackingField;

	private SimHero _hero;

	private SimFightClub _fightClub;

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

	public FightClubChampionActivity(Life life, FightClubPostLeagueData data)
		: base(null)
	{
	}

	private void OnChoiceSelected(ChoiceSelection selection)
	{
	}
}
