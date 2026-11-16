public class ScenarioVampireHuntPersonPrey : Scenario
{
	private readonly VampireHuntActivity _activity;

	private readonly SimPerson _person;

	private readonly DrinkBloodActivity _bloodActivity;

	public ScenarioVampireHuntPersonPrey(VampireHuntActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void DrinkBlood(bool fullDrain)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private bool IsVampireHunterAppearingByDrain(bool fullDrain)
	{
		return false;
	}

	private void ImpactTurnVictimIntoVampire()
	{
	}
}
