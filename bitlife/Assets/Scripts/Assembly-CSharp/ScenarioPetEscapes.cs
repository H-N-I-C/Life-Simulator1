public class ScenarioPetEscapes : Scenario
{
	protected SimPet _pet;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private string GetDeathAfterEscapeDescription()
	{
		return null;
	}

	public static SimPet GetEscapePronePet()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
