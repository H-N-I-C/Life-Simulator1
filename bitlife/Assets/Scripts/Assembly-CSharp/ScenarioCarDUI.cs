public class ScenarioCarDUI : Scenario
{
	protected DataDrug _drug;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public static DataDrug DuiDrug()
	{
		return null;
	}
}
