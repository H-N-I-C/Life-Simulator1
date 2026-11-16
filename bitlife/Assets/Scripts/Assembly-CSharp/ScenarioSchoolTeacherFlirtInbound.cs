public class ScenarioSchoolTeacherFlirtInbound : Scenario
{
	protected SimOccupationStudent _student;

	protected SimTeacher _teacher;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimTeacher GetFlirtingTeacher()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
