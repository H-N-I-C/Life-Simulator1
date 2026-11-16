public class ScenarioSchoolTeacherSeduce : Scenario
{
	protected SimOccupationStudent _student;

	protected SimTeacher _teacher;

	protected SimLover _loverCandidate;

	protected bool _targeted;

	protected bool _gettingSeduced;

	public ScenarioSchoolTeacherSeduce()
	{
	}

	public ScenarioSchoolTeacherSeduce(SimTeacher theTeacher, bool initatedByHero)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimTeacher GetSeducingTeacher()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
