public class ScenarioRetentionCareer : Scenario
{
	private List _jobsList;

	private SimJob _recommendedJob;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}
}
