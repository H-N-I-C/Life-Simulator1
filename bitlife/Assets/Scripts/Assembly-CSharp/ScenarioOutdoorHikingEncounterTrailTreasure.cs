using Game;

public class ScenarioOutdoorHikingEncounterTrailTreasure : Scenario
{
	private readonly OutdoorHikingActivityData _hikingActivityData;

	private string _narrative;

	private DataOutdoorHikingItemDiscoverie _item;

	private int _value;

	public ScenarioOutdoorHikingEncounterTrailTreasure(OutdoorHikingActivityData hikingActivityData)
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
