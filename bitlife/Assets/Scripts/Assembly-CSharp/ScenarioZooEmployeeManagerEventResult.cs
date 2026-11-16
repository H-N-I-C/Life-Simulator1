using Game;

public class ScenarioZooEmployeeManagerEventResult : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooOperationsManager _manager;

	private DataZooFundraiser _data;

	private readonly SimZooAnimal _animal;

	private readonly double _entryFee;

	private readonly string _eventType;

	private readonly double _cost;

	public ScenarioZooEmployeeManagerEventResult(SimZooOperationsManager manager, DataZooFundraiser data, SimZooAnimal animal, float entryFee, float cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private bool IsOverchargedEvent()
	{
		return false;
	}

	private bool IsUnpopularAnimalPicked()
	{
		return false;
	}
}
