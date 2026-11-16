public class ScenarioEncounterAnimal : Scenario
{
	protected bool _happenedOnWalk;

	protected DataAnimal _animal;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
