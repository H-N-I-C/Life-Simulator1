public class ScenarioCasinos : Scenario
{
	private CASINO_GAME_TYPE _gameType;

	private readonly string _originRv;

	public ScenarioCasinos()
	{
	}

	public ScenarioCasinos(CASINO_GAME_TYPE gameType = CASINO_GAME_TYPE.NONE, string originRv = "")
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
