public class ScenarioHustleItem : Scenario
{
	protected string _narrative;

	protected SimStreetPerson _randomPerson;

	protected DataHustlerPanhandleItem _randomItem;

	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected int _hustleType;

	protected string _collectionBox;

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
