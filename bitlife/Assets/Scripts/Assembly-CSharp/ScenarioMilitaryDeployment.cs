public class ScenarioMilitaryDeployment : Scenario
{
	protected DataCountry _deploymentCountry;

	protected string _deploymentString;

	protected SimCoworker _supervisor;

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

	public override string EmojiString()
	{
		return null;
	}
}
