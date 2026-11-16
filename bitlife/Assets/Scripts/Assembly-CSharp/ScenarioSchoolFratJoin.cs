public class ScenarioSchoolFratJoin : Scenario
{
	protected DataSchoolFrat _fratData;

	protected SimOccupationStudent _student;

	public ScenarioSchoolFratJoin(DataSchoolFrat theFratData)
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
