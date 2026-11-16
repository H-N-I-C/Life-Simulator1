public class ScenarioRetentionHeadhunter : Scenario
{
	private SimJob _newJob;

	private SimOccupationEmployee _currentOccupation;

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

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	private static bool EmployedAndWillGetOffer()
	{
		return false;
	}

	private static SimJob GetQualifiedNewJob()
	{
		return null;
	}
}
