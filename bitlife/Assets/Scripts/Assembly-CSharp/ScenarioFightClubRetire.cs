public class ScenarioFightClubRetire : ScenarioLazyCsvBase
{
	private FightClubRetirementActivity _activity;

	private const string CsvFile = "FightClub/Fight Club Scenarios - Retirement";

	public ScenarioFightClubRetire(FightClubRetirementActivity activity, ChoiceCallback choiceCallback = null, DismissCallback dismissCallback = null)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
