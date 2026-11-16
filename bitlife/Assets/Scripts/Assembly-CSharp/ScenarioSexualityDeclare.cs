public class ScenarioSexualityDeclare : Scenario
{
	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public SimPerson GetFriendForSexualityScenario(DataSexualityScenario scenario)
	{
		return null;
	}

	public SimClassmate GetClassmateForSexualityScenario(DataSexualityScenario scenario)
	{
		return null;
	}

	public SimTeacher GetTeacherForSexualityScenario(DataSexualityScenario scenario)
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
