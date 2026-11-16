public class ScenarioJobPressure : Scenario
{
	protected int _pressurer;

	protected int _pressurerGender;

	protected SimPerson _person;

	protected string _narrative;

	protected string _whoString;

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
}
