public class ScenarioPushSpendTime : Scenario
{
	protected SimParent _parent;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimSibling _sibling;

	protected SimPerson _person;

	protected bool _takingHero;

	protected string _pastime;

	protected bool _isPersonYourFather;

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

	public static SimParent GetSpendTimeParent()
	{
		return null;
	}

	public static SimLover GetSpendTimeLover()
	{
		return null;
	}

	public static SimChild GetSpendTimeChild()
	{
		return null;
	}

	public static SimSibling GetSpendTimeSibling()
	{
		return null;
	}
}
