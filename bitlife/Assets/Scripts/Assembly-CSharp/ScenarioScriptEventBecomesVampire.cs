public class ScenarioScriptEventBecomesVampire : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	public ScenarioScriptEventBecomesVampire(SimParent theParent)
	{
	}

	public ScenarioScriptEventBecomesVampire(SimSibling theSibling)
	{
	}

	public ScenarioScriptEventBecomesVampire(SimLover theLover)
	{
	}

	public ScenarioScriptEventBecomesVampire(SimChild theChild)
	{
	}

	public ScenarioScriptEventBecomesVampire(SimPerson theFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public new bool PreventRandomPushAfterScenario()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
