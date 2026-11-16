using Game;

public class ScenarioGameFightingMoveUpgrade : ScenarioLazyCsvBase
{
	private readonly SimFightClubMove _move;

	private readonly DataFightIngMove _moveData;

	private readonly string _previousStageName;

	public ScenarioGameFightingMoveUpgrade(GameFightingOutcomeActivity activity, SimFightClubMove move)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	private string GetUpgradeInfo()
	{
		return null;
	}

	private string GetAccuracyString()
	{
		return null;
	}
}
