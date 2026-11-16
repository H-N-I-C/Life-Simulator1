public class ScenarioZooAnimalRelocateRepercussion : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _preyAnimal;

	private readonly SimZooAnimal _predatorAnimal;

	private readonly SimZooAnimal _newAnimal;

	private readonly bool _dangerToNewAnimal;

	public ScenarioZooAnimalRelocateRepercussion(SimZooAnimal newAnimal, SimZooAnimal preyAnimal, SimZooAnimal predatorAnimal)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
