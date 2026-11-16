public class FightClubAgeUpAnimalScenario : ScenarioLazyCsvBase
{
	private FightClubAgeUpAnimalActivity _activity;

	public FightClubAgeUpAnimalScenario(FightClubAgeUpAnimalActivity activity, ChoiceCallback choiceCallback = null, DismissCallback dismissCallback = null)
		: base(null)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
