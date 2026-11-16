using Game;

public class BirthdayInvitePromotionActivity : ActivityLazyCsvBase
{
	public enum ScenarioId
	{
		SC_BIRTHDAY_INVITE = 0
	}

	private readonly GameVars _gameVars;

	public GameVars GameVars => null;

	public BirthdayInvitePromotionActivity(Life life)
		: base(null)
	{
	}

	private void PromotionChoiceMade(ChoiceSelection choiceSelection)
	{
	}
}
