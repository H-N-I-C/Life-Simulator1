public class ScenarioRoyalSchoolNegative : Scenario
{
	protected SimOccupationStudent _student;

	protected SimClassmate _classmate;

	protected SimTeacher _teacher;

	protected SimPerson _person;

	protected string _personNameScenario;

	protected string _personNameNarrative;

	protected string _feedbackSnippet;

	protected string _logVersionSnippet;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimTeacher GetTeacher()
	{
		return null;
	}

	public static SimClassmate GetClassmate()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
