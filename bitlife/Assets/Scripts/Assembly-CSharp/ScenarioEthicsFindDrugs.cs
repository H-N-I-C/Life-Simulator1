public class ScenarioEthicsFindDrugs : Scenario
{
	protected bool _happenedOnWalk;

	protected string _narrative;

	protected string _containerString;

	protected DataDrug _drug;

	protected float _value;

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
