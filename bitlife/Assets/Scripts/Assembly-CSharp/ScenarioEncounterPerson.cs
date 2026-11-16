public class ScenarioEncounterPerson : Scenario
{
	protected bool _happenedOnWalk;

	protected DataStreetPerson _streetPerson;

	protected string _personAction;

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
