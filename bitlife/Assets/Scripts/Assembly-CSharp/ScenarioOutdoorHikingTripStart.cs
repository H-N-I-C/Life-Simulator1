public class ScenarioOutdoorHikingTripStart : Scenario
{
	private string _narrative;

	private readonly OutdoorHikingActivityData _hikingActivityData;

	private readonly SimOutdoor _outdoor;

	private int _cost;

	public ScenarioOutdoorHikingTripStart(SimOutdoor outdoor, OutdoorHikingActivityData hikingActivityData)
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
