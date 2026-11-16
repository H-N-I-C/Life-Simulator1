using System.Collections.Generic;

public class ScenarioMusicProducerNewArtistPitch : Scenario
{
	private BracketParser _parser;

	private readonly Dictionary<string, string> _bracketDictionary;

	private readonly SimMusicProducerTalentScoutVenue _venue;

	private readonly SimMusicProducerArtist _artist;

	public ScenarioMusicProducerNewArtistPitch(SimMusicProducerTalentScoutVenue venue)
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

	private void SwitchUpImage()
	{
	}

	private void PitchAsIs()
	{
	}

	public static void PushOutcomeScenario(Scenario currentScenario, SimMusicProducerTalentScoutVenue venue)
	{
	}
}
