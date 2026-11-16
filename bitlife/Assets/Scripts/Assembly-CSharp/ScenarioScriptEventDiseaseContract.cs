public class ScenarioScriptEventDiseaseContract : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimDisease _disease;

	public ScenarioScriptEventDiseaseContract(SimParent theParent, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseContract(SimSibling theSibling, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseContract(SimLover theLover, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseContract(SimChild theChild, SimDisease theDisease)
	{
	}

	public ScenarioScriptEventDiseaseContract(SimPerson theFriend, SimDisease theDisease)
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
