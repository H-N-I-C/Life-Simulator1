public class ScenarioVampireHuntVampire : Scenario
{
	private readonly VampireHuntActivity _activity;

	private readonly SimPerson _vampire;

	public ScenarioVampireHuntVampire(VampireHuntActivity activity)
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

	private bool IsCrazyAttack(float multiplier)
	{
		return false;
	}

	private void AttackHero()
	{
	}

	private bool IsAgreeingToHunt()
	{
		return false;
	}
}
