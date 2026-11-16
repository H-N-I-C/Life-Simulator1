public class ScenarioLoveFirstKiss : Scenario
{
	protected SimClassmate _kissee;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimClassmate GetClassmateKissee()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
