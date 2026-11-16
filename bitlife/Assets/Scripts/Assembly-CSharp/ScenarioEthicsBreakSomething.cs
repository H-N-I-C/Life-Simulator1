public class ScenarioEthicsBreakSomething : Scenario
{
	protected DataChildBreakItem _brokenItem;

	protected string _parentsString;

	protected string _parentsPronoun;

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
