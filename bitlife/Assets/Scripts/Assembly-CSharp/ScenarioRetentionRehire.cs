public class ScenarioRetentionRehire : Scenario
{
	private SimJob _newJob;

	private readonly SimJob _oldJob;

	public ScenarioRetentionRehire(SimJob oldJob)
	{
	}

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

	private SimJob GetNewJobWithSameCareerAndStanding(SimJob currentJob)
	{
		return null;
	}

	public static bool QualifiesForRehire(SimOccupationEmployee employee)
	{
		return false;
	}
}
