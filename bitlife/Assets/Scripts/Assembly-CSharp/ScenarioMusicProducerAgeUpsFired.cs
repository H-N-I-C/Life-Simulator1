using System.Collections.Generic;

public class ScenarioMusicProducerAgeUpsFired : ScenarioMusicProducerAgeUp
{
	private readonly MusicProducerFiredActivity _activity;

	private BracketParser _parser;

	private Dictionary<string, string> _bracketDictionary;

	public ScenarioMusicProducerAgeUpsFired(MusicProducerFiredActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void Beg()
	{
	}

	private void Fire()
	{
	}
}
