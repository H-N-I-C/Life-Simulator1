public class ScenarioSiblingSquabble : Scenario
{
	protected SimSibling _sibling;

	protected bool _siblingIgnoresHero;

	protected bool _siblingWillAttack;

	protected string _narrative;

	public ScenarioSiblingSquabble()
	{
	}

	public ScenarioSiblingSquabble(SimSibling theSibling)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public static SimSibling GetSquabbleSibling()
	{
		return null;
	}
}
