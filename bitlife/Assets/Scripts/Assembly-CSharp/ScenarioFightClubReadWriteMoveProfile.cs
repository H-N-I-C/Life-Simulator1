public class ScenarioFightClubReadWriteMoveProfile : ScenarioLazyCsvBase
{
	private FightClubViewMoveSetActivity _activity;

	public ScenarioFightClubReadWriteMoveProfile(FightClubViewMoveSetActivity activity, ChoiceCallback choiceCallback, bool isReadOnly)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	private string GetUnlockConditionTitle()
	{
		return null;
	}

	private string GetUnlockCondition()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
