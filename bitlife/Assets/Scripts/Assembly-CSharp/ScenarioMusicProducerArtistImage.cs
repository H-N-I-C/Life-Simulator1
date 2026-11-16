using System.Collections;
using System.Collections.Generic;

public class ScenarioMusicProducerArtistImage : Scenario
{
	private BracketParser _parser;

	private readonly Dictionary<string, string> _bracketDictionary;

	private readonly SimMusicProducerTalentScoutVenue _venue;

	private readonly SimMusicProducerArtist _artist;

	private string _selectedStageName;

	private string _selectedGenre;

	public ScenarioMusicProducerArtistImage(SimMusicProducerTalentScoutVenue venue)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private void Pitch()
	{
	}
}
