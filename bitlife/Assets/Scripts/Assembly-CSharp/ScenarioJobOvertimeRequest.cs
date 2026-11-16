public class ScenarioJobOvertimeRequest : Scenario
{
	protected SimOccupationEmployee _employee;

	protected SimCoworker _supervisor;

	protected int _overtimeHours;

	public ScenarioJobOvertimeRequest(SimOccupationEmployee theEmployee, int theHours)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
