using System.Collections;

public class ScenarioPetKills : Scenario
{
	protected SimPet _petPredator;

	protected SimPet _petPrey;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static ArrayList GetFightingPets()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
