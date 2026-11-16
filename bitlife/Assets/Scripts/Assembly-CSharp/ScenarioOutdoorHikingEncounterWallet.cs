public class ScenarioOutdoorHikingEncounterWallet : Scenario
{
	private string _narrative;

	private readonly OutdoorHikingActivityData _hikingActivityData;

	private string _selectedItem;

	private int _amount;

	public ScenarioOutdoorHikingEncounterWallet(OutdoorHikingActivityData hikingActivityData)
	{
	}

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
