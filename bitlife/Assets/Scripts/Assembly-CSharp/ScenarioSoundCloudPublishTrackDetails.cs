using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioSoundCloudPublishTrackDetails : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataMusicTrackVibe, string> _003C_003E9__4_0;

		public static Func<DataMusicSoundCloudTrackInstrument, string> _003C_003E9__4_1;

		internal string _003CRefreshScenario_003Eb__4_0(DataMusicTrackVibe q)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__4_1(DataMusicSoundCloudTrackInstrument q)
		{
			return null;
		}
	}

	private SoundCloudTrackPublishingActivity _activity;

	private BracketParser _parser;

	private Dictionary<string, string> instrumentUpperToOriginaLookup;

	public ScenarioSoundCloudPublishTrackDetails(SoundCloudTrackPublishingActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void BuildInstrumentCaseLookup()
	{
	}

	private void BuildSoloTrackScenario()
	{
	}

	private void BuildCollabTrackScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private void LogNarrative(string trackName, string vibe, string instrument, string genre)
	{
	}

	private string _003CBuildSoloTrackScenario_003Eb__6_0(string q)
	{
		return null;
	}

	private string _003CBuildCollabTrackScenario_003Eb__7_0(string q)
	{
		return null;
	}
}
