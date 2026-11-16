public class ScenarioCasinoHighroller : Scenario
{
	protected string _casinoName;

	protected string _bait;

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public static string EligibleCasino()
	{
		return null;
	}
}
