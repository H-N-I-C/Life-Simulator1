public class ScenarioHustleDrug : Scenario
{
	protected string _narrative;

	protected DataDrug _theDrug;

	protected string _theDrugNickname;

	protected SimStreetPerson _randomPerson;

	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected int _hustleType;

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
