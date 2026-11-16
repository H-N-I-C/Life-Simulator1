using Game;

public class ScenarioOutdoorHikingDumb : Scenario
{
	private string _narrative;

	private readonly OutdoorHikingActivityData _hikingActivityData;

	private DataOutdoorHikingDumbScenario _data;

	private BracketParser _parser;

	public ScenarioOutdoorHikingDumb(OutdoorHikingActivityData hikingActivityData)
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
