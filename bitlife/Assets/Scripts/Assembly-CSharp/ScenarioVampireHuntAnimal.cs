public class ScenarioVampireHuntAnimal : Scenario
{
	private readonly VampireHuntActivity _activity;

	private readonly DataAnimal _animal;

	private string _narrative;

	public ScenarioVampireHuntAnimal(VampireHuntActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private bool IsAnimalAttacking()
	{
		return false;
	}

	private bool IsAnimalFleeing()
	{
		return false;
	}

	private void AnimalAttackVampire()
	{
	}

	private int AnimalAttackHero(int maxImpact)
	{
		return 0;
	}

	private void AttemptRescue()
	{
	}
}
