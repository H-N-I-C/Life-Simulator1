public class ScenarioCrimeDelinquencyPush : Scenario
{
	protected SimDelinquentAccomplice _accomplice;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimDelinquentAccomplice GetAccomplice()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
