using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioMusicProducerClubTakeover : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimMusicProducerArtist, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(SimMusicProducerArtist x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public SimCompany club;

		public ScenarioMusicProducerClubTakeover _003C_003E4__this;

		internal void _003CRefreshScenario_003Eb__1()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public string selectedArtistName;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimMusicProducerArtist x)
		{
			return false;
		}
	}

	private readonly ClubTakeoverActivity _activity;

	private List<SimMusicProducerArtist> _artistsByPopularity;

	private SimMusicProducerArtist _selectedArtist;

	private DataMusicClubTakeoverString _outcomeString;

	private BracketParser _parser;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void ClubSelected(SimCompany theClub)
	{
	}

	private void TakeOverClubSuccess()
	{
	}

	private void Viral()
	{
	}

	private void Stunt()
	{
	}

	private void Gift()
	{
	}

	private void SecondThoughts()
	{
	}

	private void DoNothing()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
