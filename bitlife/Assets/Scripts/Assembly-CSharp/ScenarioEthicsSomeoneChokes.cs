using System.Collections.Generic;

public class ScenarioEthicsSomeoneChokes : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _person;

	protected string _food;

	protected string _narrative;

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

	private static List<SimPerson> GetRelativesThatCanDie(Life life)
	{
		return null;
	}

	private static void AddIfNearbyAndCanDie(List<SimPerson> relativesThatCanDie, SimPerson person)
	{
	}
}
