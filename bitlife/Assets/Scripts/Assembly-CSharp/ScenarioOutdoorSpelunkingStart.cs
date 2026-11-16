using System.Collections.Generic;
using Game;

public class ScenarioOutdoorSpelunkingStart : Scenario
{
	private string _narrative;

	private readonly SimOutdoor _outdoorLifestyle;

	private readonly DataOutdoorCave _dataOutdoorCave;

	private readonly int _cost;

	private readonly List<DataOutdoorCave> _caveList;

	public ScenarioOutdoorSpelunkingStart(SimOutdoor outdoorLifestyle, DataOutdoorCave dataOutdoorCave, List<DataOutdoorCave> caveList)
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
