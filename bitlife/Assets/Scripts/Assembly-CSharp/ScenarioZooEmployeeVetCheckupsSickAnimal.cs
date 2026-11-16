public class ScenarioZooEmployeeVetCheckupsSickAnimal : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooVeterinarian _vet;

	private readonly SimZooAnimal _animal;

	private SimZooAnimalDisease _disease;

	private double _treatmentCost;

	private bool _treatableDisease;

	public ScenarioZooEmployeeVetCheckupsSickAnimal(SimZooVeterinarian vet, SimZooAnimal animal, bool directFromList = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private bool WillVetEuthanizeAnimal()
	{
		return false;
	}

	private bool CanInjureFromTreatment()
	{
		return false;
	}

	private bool WillBeKilledFromTreatment()
	{
		return false;
	}

	private bool CanBeCured()
	{
		return false;
	}

	private string GetTreatmentString()
	{
		return null;
	}

	private void _003CChoiceSelectedAtIndex_003Eb__9_0(int index)
	{
	}
}
