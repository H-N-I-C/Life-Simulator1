public class ScenarioZooAgeUpTheft : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private SimZooAnimal _animal;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private static SimZooAnimal GetRandomAnimalToSteal()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
