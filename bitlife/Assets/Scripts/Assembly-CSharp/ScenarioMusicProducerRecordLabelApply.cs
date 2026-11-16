using System.Collections.Generic;

public class ScenarioMusicProducerRecordLabelApply : Scenario
{
	private readonly SimMusicProducerRecordLabel _recordLabel;

	private BracketParser _parser;

	private Dictionary<string, string> _bracketDictionary;

	public ScenarioMusicProducerRecordLabelApply(SimMusicProducerRecordLabel recordLabel)
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

	private void ApplyToRecordLabel()
	{
	}
}
