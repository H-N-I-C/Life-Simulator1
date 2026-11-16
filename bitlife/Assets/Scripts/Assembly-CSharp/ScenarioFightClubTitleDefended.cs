public class ScenarioFightClubTitleDefended : ScenarioLazyCsvBase
{
	private readonly SimFightClub _club;

	private readonly GameFightingOutcomeActivity _activity;

	public ScenarioFightClubTitleDefended(GameFightingOutcomeActivity activity, ChoiceCallback choiceCallback = null)
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
