using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioPodcastRecordFirstSeason : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__3_0;

		public static Func<int, string> _003C_003E9__3_1;

		internal string _003CRefreshScenario_003Eb__3_0(string q)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__3_1(int i)
		{
			return null;
		}
	}

	private readonly SimPodcast _podcast;

	private BracketParser _parser;

	public ScenarioPodcastRecordFirstSeason(SimPodcast podcast)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private ArrayList GetRandomTitles(List<string> titles)
	{
		return null;
	}

	private ScenarioInputField BuildInputField(string title, ArrayList randomChoices)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}

	private string _003CGetRandomTitles_003Eb__5_0(string t)
	{
		return null;
	}
}
