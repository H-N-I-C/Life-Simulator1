public class ScenarioChildDemands : Scenario
{
	protected string _demandString;

	protected float _demandAmount;

	protected SimChild _child;

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

	public static SimChild GetChildToDemand()
	{
		return null;
	}
}
