using System.Collections.Generic;

public class ScenarioMusicProducerNewArtistSigned : Scenario
{
	private BracketParser _parser;

	private readonly Dictionary<string, string> _bracketDictionary;

	private readonly SimMusicProducerTalentScoutVenue _venue;

	private readonly SimMusicProducerArtist _artist;

	public ScenarioMusicProducerNewArtistSigned(SimMusicProducerTalentScoutVenue venue)
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
