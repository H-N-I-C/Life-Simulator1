namespace Game.Fight_Club.Stats
{
	public class ScenarioFightClubMoveSetModifiedFeedback : ScenarioLazyCsvBase
	{
		private readonly FightClubViewMoveSetActivity _activity;

		public ScenarioFightClubMoveSetModifiedFeedback(FightClubViewMoveSetActivity activity, ChoiceCallback choiceCallback)
			: base(null)
		{
		}

		protected override void PrepareParser(BracketParser parser)
		{
		}
	}
}
