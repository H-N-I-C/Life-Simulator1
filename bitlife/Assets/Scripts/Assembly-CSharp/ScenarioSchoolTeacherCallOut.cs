public class ScenarioSchoolTeacherCallOut : Scenario
{
	protected SimOccupationStudent _student;

	protected SimTeacher _teacher;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimTeacher GetCallOutTeacher()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
