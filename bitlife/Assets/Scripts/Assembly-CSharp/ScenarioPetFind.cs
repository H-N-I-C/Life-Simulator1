public class ScenarioPetFind : Scenario
{
	protected bool _happenedOnWalk;

	protected SimPet _petCandidate;

	public ScenarioPetFind()
	{
	}

	public ScenarioPetFind(bool onWalk)
	{
	}

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

	public override string EmojiString()
	{
		return null;
	}
}
