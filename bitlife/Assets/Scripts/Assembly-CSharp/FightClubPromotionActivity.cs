using Game;

public class FightClubPromotionActivity : ActivityLazyCsvBase
{
	public enum ScenarioId
	{
		FC_PROMOTION = 0
	}

	private readonly GameVars _gameVars;

	public GameVars GameVars => null;

	public FightClubPromotionActivity(Life life)
		: base(null)
	{
	}

	private void PromotionChoiceMade(ChoiceSelection choiceSelection)
	{
	}
}
