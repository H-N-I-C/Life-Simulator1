public class ScenarioZooPushZoomanity : Scenario
{
	private string _narrative;

	private BracketParser _parser;

	private SimPerson _friend;

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
