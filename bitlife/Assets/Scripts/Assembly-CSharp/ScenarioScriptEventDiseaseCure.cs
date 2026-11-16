public class ScenarioScriptEventDiseaseCure : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimDisease _disease;

	public ScenarioScriptEventDiseaseCure(SimParent theParent, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseCure(SimSibling theSibling, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseCure(SimLover theLover, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseCure(SimChild theChild, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseCure(SimPerson theFriend, SimDisease theDisease)
	{
	}

	public override void RefreshScenario()
	{
	}

	public new bool PreventRandomPushAfterScenario()
	{
		return false;
	}
}
