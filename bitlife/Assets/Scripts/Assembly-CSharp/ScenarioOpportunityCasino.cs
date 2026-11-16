public class ScenarioOpportunityCasino : Scenario
{
	protected string _casinoNameString;

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

	public static string EligibleCasino()
	{
		return null;
	}
}
