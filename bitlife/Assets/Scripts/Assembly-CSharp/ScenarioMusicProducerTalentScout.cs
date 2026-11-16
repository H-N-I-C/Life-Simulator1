using System.Collections.Generic;

public class ScenarioMusicProducerTalentScout : Scenario
{
	private BracketParser _parser;

	private readonly Dictionary<string, string> _bracketDictionary;

	private readonly SimSpecialJobMusicProducer _specialJob;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	private SimMusicProducerRecordLabel _recordLabel;

	public ScenarioMusicProducerTalentScout()
	{
	}

	public ScenarioMusicProducerTalentScout(SimSpecialJobMusicProducer specialJob)
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

	private void HasAcquiredTalentFeedback()
	{
	}

	private void NoEventsRemainingFeedback()
	{
	}

	private void GoToEvent()
	{
	}

	private void SendTalentScout()
	{
	}
}
