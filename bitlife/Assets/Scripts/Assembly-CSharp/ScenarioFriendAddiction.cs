public class ScenarioFriendAddiction : Scenario
{
	protected SimPerson _friend;

	protected string _drugString;

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
