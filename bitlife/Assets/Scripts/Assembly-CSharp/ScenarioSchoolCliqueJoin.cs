public class ScenarioSchoolCliqueJoin : Scenario
{
	protected DataSchoolClique _cliqueData;

	protected SimOccupationStudent _student;

	public ScenarioSchoolCliqueJoin(DataSchoolClique theCliqueData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool IsEligibleToJoin()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
