namespace Game.Fight_Club.Fighting_League
{
	public class ScenarioFightingLeagueSponsorship : ScenarioLazyCsvBase
	{
		private readonly FightingLeagueActivity _activity;

		public ScenarioFightingLeagueSponsorship(FightingLeagueActivity activity, ChoiceCallback choiceCallback)
			: base(null)
		{
		}

		public override void RefreshScenario()
		{
		}

		private ScenarioBarGraph GetBarGraph(FIGHT_CLUB_ATTRIBUTE attribute)
		{
			return null;
		}
	}
}
