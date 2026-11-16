public class ScenarioSchoolPostUniversity : Scenario
{
	protected int _schoolType;

	public ScenarioSchoolPostUniversity(int theType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool WillBeAccepted()
	{
		return false;
	}

	private bool FulfillsEducationalPrerequisites()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
