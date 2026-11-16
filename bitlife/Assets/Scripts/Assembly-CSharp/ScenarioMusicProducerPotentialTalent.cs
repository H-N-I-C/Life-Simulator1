using System;
using System.Collections.Generic;

public class ScenarioMusicProducerPotentialTalent : Scenario
{
	protected readonly List<KeyValuePair<ScenarioChoice, Action>> _choiceActionList;

	protected string _bodyIntro;

	private BracketParser _parser;

	private readonly SimMusicProducerTalentScoutVenue _venue;

	private readonly SimMusicProducerArtist _artist;

	public ScenarioMusicProducerPotentialTalent(SimMusicProducerTalentScoutVenue venue)
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

	private void Pitch()
	{
	}

	private void ListenToOtherActs()
	{
	}

	private void Leave()
	{
	}
}
