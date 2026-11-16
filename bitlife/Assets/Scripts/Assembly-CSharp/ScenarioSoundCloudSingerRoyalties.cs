using System.Collections.Generic;

public class ScenarioSoundCloudSingerRoyalties : Scenario
{
	private readonly SimSoundCloud _soundCloud;

	private readonly SimSoundCloudTrack _theTrack;

	private readonly SimPerson _theArtist;

	private BracketParser _bracketParser;

	private Dictionary<string, string> _bracketDictionary;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void DinnerFeedback()
	{
	}

	private void IgnoreFeedback()
	{
	}

	private void LogIgnoreNarrative()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
