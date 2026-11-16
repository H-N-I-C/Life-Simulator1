public class ScenarioCasinoExpandSummary : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly CASINO_TYPE _oldType;

	public ScenarioCasinoExpandSummary(CASINO_TYPE oldType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private int GetIncreaseInGamingUnitCapacity()
	{
		return 0;
	}
}
