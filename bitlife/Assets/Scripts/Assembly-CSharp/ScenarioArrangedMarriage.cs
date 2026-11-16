public class ScenarioArrangedMarriage : Scenario
{
	protected int _count;

	protected SimLover _loverCandidate;

	protected string _parentsString;

	public ScenarioArrangedMarriage()
	{
	}

	public ScenarioArrangedMarriage(int theCount, SimLover theLoverCandidate)
	{
	}

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
