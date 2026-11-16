using Game;

public class FightingMoveSkillShopUnlockedActivity : ActivityLazyCsvBase
{
	public enum ScenarioID
	{
		FC_STREET_CRED_BALANCE = 0,
		FC_MOVE_PURCHASE = 1,
		FC_MOVE_PURCHASE_INSUFFICIENT_FUNDS_FEEDBACK = 2
	}

	private readonly DataFightIngMove _moveToUnlock;

	private readonly DataFightIngMove _upgradesTo;

	private readonly SimFightClub _fightClub;

	private readonly int _moveCost;

	public FightingMoveSkillShopUnlockedActivity(Life life)
		: base(null)
	{
	}

	public FightingMoveSkillShopUnlockedActivity(Life life, DataFightIngMove moveToUnlock, int cost)
		: base(null)
	{
	}

	public string GetMoveEmoji()
	{
		return null;
	}

	private void PurchaseMoveChoiceMade(ChoiceSelection choiceSelection)
	{
	}
}
