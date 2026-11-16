using Game;

public class ScenarioOutdoorCaveStuff : Scenario
{
	private string _narrative;

	private DataOutdoorCaveDiscoverie _dataDumb;

	private readonly OutdoorCaveSpelunkActivityData _data;

	private BracketParser _parser;

	public ScenarioOutdoorCaveStuff(OutdoorCaveSpelunkActivityData data)
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
