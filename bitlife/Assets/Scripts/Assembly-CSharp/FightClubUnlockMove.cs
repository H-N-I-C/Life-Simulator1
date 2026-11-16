using Game;

public class FightClubUnlockMove : UnlockTrigger<FightClubMoveContext>
{
	private readonly string _moveID;

	private readonly bool _skip;

	private readonly string TriggerID__BackingField;

	public string MoveID => null;

	public override string TriggerID => TriggerID__BackingField;

	public FightClubUnlockMove(string moveID, bool markedToSkip = false)
	{
	}

	public override bool IsValid()
	{
		return false;
	}

	protected override Scenario UnlockContent(FightClubMoveContext context)
	{
		return null;
	}

	protected override bool MeetsUnlockConditions(FightClubMoveContext context)
	{
		return false;
	}

	private DataFightIngMove GetMove()
	{
		return null;
	}

	public override string ToString()
	{
		return null;
	}
}
