using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioMusicProducerProduceTrack : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimMusicProducerArtist, string> _003C_003E9__3_0;

		public static Func<SimMusicProducerArtist, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__3_0(SimMusicProducerArtist x)
		{
			return null;
		}

		internal string _003CGetArtists_003Eb__6_0(SimMusicProducerArtist x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public string stageName;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimMusicProducerArtist x)
		{
			return false;
		}
	}

	private readonly SimSpecialJobMusicProducer _simSpecialJobMusicProducer;

	private readonly List<SimMusicProducerArtist> _artists;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private List<SimMusicProducerArtist> GetArtists()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
