public class ScenarioJobTermination : Scenario
{
	protected SimOccupationEmployee _employee;

	protected int _reason;

	protected SimCoworker _supervisor;

	protected bool _forcedRetirementSituation;

	protected string _narrative;

	public ScenarioJobTermination(int theReason)
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
