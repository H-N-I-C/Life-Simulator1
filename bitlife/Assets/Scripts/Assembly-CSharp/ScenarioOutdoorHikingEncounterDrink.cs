using Game;

public class ScenarioOutdoorHikingEncounterDrink : Scenario
{
	private string _narrative;

	private readonly OutdoorHikingActivityData _hikingActivityData;

	private DataOutdoorHikingDrink _data;

	public ScenarioOutdoorHikingEncounterDrink(OutdoorHikingActivityData hikingActivityData)
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
