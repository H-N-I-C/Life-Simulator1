using System;
using System.Collections.Generic;
using Game;

public class ScenarioSoundCloudCoverArt : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__6_0;

		internal bool _003C_002Ector_003Eb__6_0(SimSoundCloudTrack x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public string trackGenreString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(string x)
		{
			return false;
		}
	}

	private readonly SimSoundCloud _soundCloud;

	private readonly SimSoundCloudTrack _track;

	private readonly SimHero _hero;

	private DataMusicCoverArtString _coverArtString;

	private BracketParser _bracketParser;

	private Dictionary<string, string> _bracketDictionary;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
