public class ScenarioHustleFood : Scenario
{
	protected string _narrative;

	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected SimStreetPerson _streetPerson;

	protected DataHustlerPanhandleFood _foodItem;

	protected string _genericFoodString;

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
