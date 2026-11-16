using System;
using System.Collections.Generic;

[Serializable]
public class SoundCloudInsight
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MUSIC_PRODUCER_GENRE, string> _003C_003E9__18_0;

		public static Func<string, string, string> _003C_003E9__18_1;

		public static Func<SimPerson, string> _003C_003E9__19_0;

		public static Func<string, string, string> _003C_003E9__19_1;

		internal string _003CGetTopGenres_003Eb__18_0(MUSIC_PRODUCER_GENRE x)
		{
			return null;
		}

		internal string _003CGetTopGenres_003Eb__18_1(string current, string next)
		{
			return null;
		}

		internal string _003CGetTopArtists_003Eb__19_0(SimPerson x)
		{
			return null;
		}

		internal string _003CGetTopArtists_003Eb__19_1(string current, string next)
		{
			return null;
		}
	}

	private List<MUSIC_PRODUCER_GENRE> TopGenres__BackingField;

	private List<SimPerson> TopArtists__BackingField;

	private string TopTrack__BackingField;

	private string TopVibe__BackingField;

	public List<MUSIC_PRODUCER_GENRE> TopGenres
	{
		get
		{
			return TopGenres__BackingField;
		}
		private set
		{
			TopGenres__BackingField = value;
		}
	}

	public List<SimPerson> TopArtists
	{
		get
		{
			return TopArtists__BackingField;
		}
		private set
		{
			TopArtists__BackingField = value;
		}
	}

	private string TopTrack
	{
		get
		{
			return TopTrack__BackingField;
		}
		set
		{
			TopTrack__BackingField = value;
		}
	}

	private string TopVibe
	{
		get
		{
			return TopVibe__BackingField;
		}
		set
		{
			TopVibe__BackingField = value;
		}
	}

	public SoundCloudInsight(MUSIC_PRODUCER_GENRE[] genres, SimPerson[] topArtists, string track, string vibe)
	{
	}

	public bool IsPopular(MUSIC_PRODUCER_GENRE genre)
	{
		return false;
	}

	public string GetTopGenres()
	{
		return null;
	}

	public string GetTopArtists()
	{
		return null;
	}

	public string GetTopTrack()
	{
		return null;
	}

	public string GetTopVibe()
	{
		return null;
	}
}
