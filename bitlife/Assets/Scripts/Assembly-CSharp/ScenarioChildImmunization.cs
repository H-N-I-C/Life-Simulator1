using System.Collections;

public class ScenarioChildImmunization : Scenario
{
	protected ArrayList _childArray;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private string ChildrenDescriptionString()
	{
		return null;
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	public static ArrayList GetChildrenToImmunize()
	{
		return null;
	}
}
