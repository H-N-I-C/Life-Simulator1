using System;
using System.Collections.Generic;

public abstract class ScenarioMusicProducerAgeUpsBangerzAwards : ScenarioMusicProducerAgeUp
{
	protected readonly SimBangerzAwardsNomination _nomination;

	protected readonly Dictionary<string, string> _bracketDictionary;

	protected readonly BracketParser _parser;

	private readonly List<KeyValuePair<ScenarioChoice, Action>> _choiceActionList;

	public ScenarioMusicProducerAgeUpsBangerzAwards(SimBangerzAwardsNomination nomination)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	protected abstract void AttendEvent();

	private void SkipIt()
	{
	}
}
