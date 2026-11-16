using Game;

public class ScenarioFightingLeaguePressConferenceBuffFeedback : ScenarioLazyCsvBase
{
	private readonly FightingLeagueActivity _activity;

	private readonly bool _heroPerspective;

	private readonly DataFightPressConferenceQuestionCombination _combination;

	public ScenarioFightingLeaguePressConferenceBuffFeedback(FightingLeagueActivity activity, bool heroPerspective, DataFightPressConferenceQuestionCombination choice)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
