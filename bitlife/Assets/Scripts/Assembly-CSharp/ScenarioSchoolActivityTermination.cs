public class ScenarioSchoolActivityTermination : Scenario
{
	protected SimSchoolActivity _activity;

	protected SimOccupationStudent _student;

	protected int _reason;

	protected string _narrative;

	public ScenarioSchoolActivityTermination(SimSchoolActivity theActivity, SimOccupationStudent theStudent, int theReason)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
