public class ScenarioSchoolBully : Scenario
{
	protected SimOccupationStudent _student;

	protected SimClassmate _bully;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimClassmate GetClassmateBully()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
