using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioTrackProductionSignatureSound : Scenario
{
	private string _narrative;

	private MusicProducerTrackProductionActivity _activity;

	private Dictionary<string, DataMusicSignatureSound> _sounds;

	private BracketParser _parser;

	public ScenarioTrackProductionSignatureSound(MusicProducerTrackProductionActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void UpdateNarrative()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
