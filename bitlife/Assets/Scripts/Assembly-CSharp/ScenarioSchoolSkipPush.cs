public class ScenarioSchoolSkipPush : Scenario
{
	protected SimOccupationStudent _student;

	protected SimClassmate _classmate;

	protected string _pastime;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimClassmate GetClassmateSkipper(bool isFromRefresh = false)
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
