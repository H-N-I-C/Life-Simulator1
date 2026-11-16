using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioMusicProducerScoreProductionPitch : Scenario
{
	private string _narrative;

	private MusicProducerScoreProductionActivity _activity;

	private Dictionary<string, DataMusicSoundCloudTrackInstrument> _keyInstruments;

	private Dictionary<string, DataMusicSoundCloudTrackInstrument> _bgInstruments;

	public ScenarioMusicProducerScoreProductionPitch(MusicProducerScoreProductionActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void PushFailScenarioFeedback()
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
