using Game;

public class FightingMoveUnlockedActivity : ActivityLazyCsvBase
{
	public enum ScenarioID
	{
		FC_NEW_MOVE = 0,
		FC_ADD_MOVESET = 1,
		FC_MOVE_ADDED_FEEDBACK = 2
	}

	protected readonly DataFightIngMove _moveToUnlock;

	protected readonly SimFightClub _fightClub;

	protected readonly DataFightIngMove _moveUpgrade;

	protected readonly SimFightClubFighterStats _stats;

	protected readonly SimHero _hero;

	private SimFightClubMove _learnedMove;

	public FightingMoveUnlockedActivity(Life life, DataFightIngMove moveToUnlock, string unlockNarrative)
		: base(null)
	{
	}

	public SimFightClubMove[] GetCurrentMoveSet()
	{
		return null;
	}

	private void MoveUnlockedChoiceMade(ChoiceSelection choiceSelection)
	{
	}

	private void ReassignmentChoiceMade(ChoiceSelection choiceSelection)
	{
	}
}
