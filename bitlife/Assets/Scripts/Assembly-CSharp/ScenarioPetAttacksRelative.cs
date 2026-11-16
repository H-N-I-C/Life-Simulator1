public class ScenarioPetAttacksRelative : Scenario
{
	protected SimPet _pet;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _person;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public static SimPet GetMostAttackPronePet()
	{
		return null;
	}
}
