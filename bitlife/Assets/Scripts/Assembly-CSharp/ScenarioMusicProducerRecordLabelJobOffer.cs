using System;
using System.Collections.Generic;

public class ScenarioMusicProducerRecordLabelJobOffer : Scenario
{
	protected readonly SimSpecialJobMusicProducer _specialJob;

	protected readonly BracketParser _parser;

	protected readonly List<KeyValuePair<ScenarioChoice, Action>> _choiceActionList;

	public ScenarioMusicProducerRecordLabelJobOffer(SimSpecialJobMusicProducer specialJob, string bodyTemplate, BracketParser parser)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	protected string FormatSalary()
	{
		return null;
	}

	private void AcceptTheirOffer()
	{
	}

	private void TurnThemDown()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
