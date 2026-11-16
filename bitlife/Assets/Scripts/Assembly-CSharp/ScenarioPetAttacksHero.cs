public class ScenarioPetAttacksHero : Scenario
{
	protected SimPet _pet;

	protected string _narrative;

	public ScenarioPetAttacksHero()
	{
	}

	public ScenarioPetAttacksHero(SimPet thePet)
	{
	}

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
