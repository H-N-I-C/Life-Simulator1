public class ScenarioPetDiscipline : Scenario
{
	protected SimPet _pet;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimPet GetProblemPet()
	{
		return null;
	}

	public static DataPetDiscipline GetPetDisciplineForPet(SimPet thePet)
	{
		return null;
	}

	public static SimChild GetChildForPetDiscipline()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
