using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioSoundCloudSellTrackToLabel : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__6_0;

		public static Func<SimMusicProducerRecordLabel, string> _003C_003E9__8_2;

		internal string _003CRefreshScenario_003Eb__6_0(string t)
		{
			return null;
		}

		internal string _003CCreateRecordLabels_003Eb__8_2(SimMusicProducerRecordLabel x)
		{
			return null;
		}
	}

	private readonly SimSoundCloudTrack _trackToSell;

	private readonly SimHero _hero;

	private readonly SimPlace _heroPlaceOfResidence;

	private readonly SimMusicProfession _heroMusicProfession;

	private Dictionary<string, SimMusicProducerRecordLabel> _labelsByName;

	public ScenarioSoundCloudSellTrackToLabel(SimSoundCloudTrack trackToSell)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private List<SimMusicProducerRecordLabel> CreateRecordLabels()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}

	private bool _003CCreateRecordLabels_003Eb__8_0(SimMusicProducerRecordLabel x)
	{
		return false;
	}

	private bool _003CCreateRecordLabels_003Eb__8_1(SimMusicProducerRecordLabel x)
	{
		return false;
	}
}
