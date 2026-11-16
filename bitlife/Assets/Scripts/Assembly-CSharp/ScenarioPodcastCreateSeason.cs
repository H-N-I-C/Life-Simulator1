using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioPodcastCreateSeason : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__4_0;

		public static Func<int, string> _003C_003E9__4_1;

		internal string _003CRefreshScenario_003Eb__4_0(string q)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__4_1(int i)
		{
			return null;
		}
	}

	private readonly SimPodcastActivity _podcastActivity;

	private BracketParser _parser;

	public ScenarioPodcastCreateSeason()
	{
	}

	public ScenarioPodcastCreateSeason(SimPodcastActivity podcastActivity)
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

	private string _003CGetRandomTitles_003Eb__6_0(string t)
	{
		return null;
	}
}
