public class MiniGameFightingBettingTurn
{
	private readonly MiniGameFightingFighter Fighter__BackingField;

	private int Data__BackingField;

	private readonly FIGHTING_MOVE_RESULT Result__BackingField;

	public MiniGameFightingFighter Fighter => Fighter__BackingField;

	public int Data
	{
		get
		{
			return Data__BackingField;
		}
		set
		{
			Data__BackingField = value;
		}
	}

	public FIGHTING_MOVE_RESULT Result => Result__BackingField;

	public MiniGameFightingBettingTurn(MiniGameFightingFighter fighter, FIGHTING_MOVE_RESULT result)
	{
	}
}
