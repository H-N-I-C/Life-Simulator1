public class ScenarioInheritance : Scenario
{
	protected SimParent _parent;

	protected SimLover _lover;

	protected string _narrative;

	public ScenarioInheritance(SimParent theParent)
	{
	}

	public ScenarioInheritance(SimLover theLover)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
