public class ScenarioZooAnimalDeath : Scenario
{
	private readonly string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private readonly string _deathString;

	public ScenarioZooAnimalDeath(SimZooAnimal animal, string deathString, string narrative)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
