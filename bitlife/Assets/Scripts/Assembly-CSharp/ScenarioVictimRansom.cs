public class ScenarioVictimRansom : Scenario
{
	protected SimLover _lover;

	protected SimPet _pet;

	protected float _ransomAmount;

	protected bool _warnedToAvoidPolice;

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
}
