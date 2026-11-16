public class ScenarioSchoolActivityLeadership : Scenario
{
	protected SimSchoolActivity _activity;

	protected SimOccupationStudent _student;

	protected string _leadershipRole;

	protected string _narrative;

	public ScenarioSchoolActivityLeadership(SimSchoolActivity theActivity, SimOccupationStudent theStudent, bool mainRole)
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
