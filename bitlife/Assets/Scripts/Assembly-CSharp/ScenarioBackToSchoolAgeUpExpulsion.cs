public class ScenarioBackToSchoolAgeUpExpulsion : Scenario
{
	private readonly SimOccupationStudent _student;

	private readonly string _principalMidSentence;

	private readonly string _principalAtStartOfSentence;

	private readonly string _schoolName;

	private string _newSchoolName;

	private string _narrative;

	private readonly string _principalPronoun;

	private readonly string _principalPronounPossessive;

	private readonly SimTeacher _principal;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private string FormatKickedOutBody()
	{
		return null;
	}

	private string FormatKickedOutNarrative()
	{
		return null;
	}

	private string StartNewSchool()
	{
		return null;
	}

	private bool IsGoingToStayAfterBegging()
	{
		return false;
	}

	private void CheckForParentArgument(int chances)
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
