using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioPodcastCreation : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataPodcastGenre, string> _003C_003E9__4_0;

		internal string _003CRefreshScenario_003Eb__4_0(DataPodcastGenre g)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string selectedGenre;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataPodcastGenre g)
		{
			return false;
		}
	}

	private List<DataPodcastGenre> _genreList;

	private readonly bool _disableDismissButton;

	public ScenarioPodcastCreation()
	{
	}

	public ScenarioPodcastCreation(bool disableDismissButton)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
