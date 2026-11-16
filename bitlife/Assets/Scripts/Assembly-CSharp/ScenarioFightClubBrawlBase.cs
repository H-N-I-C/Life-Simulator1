public abstract class ScenarioFightClubBrawlBase : ScenarioLazyCsvBase
{
	protected readonly FightClubBrawlActivityBase _brawl;

	protected readonly SimFightClubFighterStats _heroStats;

	protected ScenarioFightClubBrawlBase(FightClubBrawlActivityBase activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}
}
