public class ScenarioSchoolClassmateActUp : Scenario
{
	protected SimOccupationStudent _student;

	protected SimClassmate _classmate;

	protected SimTeacher _teacher;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimClassmate GetClassmateMiscreant()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
