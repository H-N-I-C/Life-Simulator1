using Game;

public abstract class ScenarioFightClubComplimentBase : ScenarioLazyCsvBase
{
	protected readonly FightClubComplimentActivity _activity;

	protected DataFightErCompliment _complimentData;

	protected ScenarioFightClubComplimentBase(FightClubComplimentActivity activity, string id, DismissCallback onDismiss = null)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	protected static string ResolveRandomBracketExpression(string text, out string selectedValue)
	{
		selectedValue = null;
		return null;
	}
}
