public class ScenarioMilitaryRetire : Scenario
{
	protected SimOccupationMilitary _military;

	public ScenarioMilitaryRetire()
	{
	}

	public ScenarioMilitaryRetire(SimOccupationMilitary theMilitary)
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
