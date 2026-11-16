using System;
using System.Collections.Generic;

public abstract class ScenarioMusicProducerAgeUpsBangerzAwardsLost : ScenarioMusicProducerAgeUp
{
	private BracketParser _parser;

	private Dictionary<string, string> _bracketDictionary;

	private List<KeyValuePair<ScenarioChoice, Action>> _choiceActionList;

	public ScenarioMusicProducerAgeUpsBangerzAwardsLost()
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
