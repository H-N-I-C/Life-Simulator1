public class ScenarioFinancialAdvisorCandidate : Scenario
{
	protected SimFinancialAdvisor _advisor;

	protected bool _isPush;

	public ScenarioFinancialAdvisorCandidate()
	{
	}

	public ScenarioFinancialAdvisorCandidate(bool isPush)
	{
	}

	public override void RefreshScenario()
	{
	}

	private static SimFinancialAdvisor FindCandidate(bool pushOverride, bool simulateLife = true)
	{
		return null;
	}

	private static bool IsIneligibleForFinancialAdvisor()
	{
		return false;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
