using Game.BackToSchool.SleepOver;

public class ScenarioBackToSchoolAgeUpSleepover : Scenario
{
	private string _narrative;

	private readonly SleepoverActivity _activity;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void PushExcuseRejection()
	{
	}

	private void PushSimpleRejection()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
