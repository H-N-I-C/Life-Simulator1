using System;
using Game;

[Serializable]
public class SimSoundCloudArtist : SimPerson
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataMusicSoundCloudScreenName, WeightedEntry<DataMusicSoundCloudScreenName>> _003C_003E9__25_0;

		internal WeightedEntry<DataMusicSoundCloudScreenName> _003CSetScreenName_003Eb__25_0(DataMusicSoundCloudScreenName x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass25_0
	{
		public BracketParser bracketParser;

		internal string _003CSetScreenName_003Eb__1(string q)
		{
			return null;
		}
	}

	private string ScreenName__BackingField;

	private MUSIC_PRODUCER_GENRE Genre__BackingField;

	private int Followers__BackingField;

	private string TopTrack__BackingField;

	private string Vibe__BackingField;

	private float Popularity__BackingField;

	public string ScreenName
	{
		get
		{
			return ScreenName__BackingField;
		}
		set
		{
			ScreenName__BackingField = value;
		}
	}

	public MUSIC_PRODUCER_GENRE Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public int Followers
	{
		get
		{
			return Followers__BackingField;
		}
		set
		{
			Followers__BackingField = value;
		}
	}

	public string TopTrack
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

	public string Vibe
	{
		get
		{
			return Vibe__BackingField;
		}
		set
		{
			Vibe__BackingField = value;
		}
	}

	public float Popularity
	{
		get
		{
			return Popularity__BackingField;
		}
		set
		{
			Popularity__BackingField = value;
		}
	}

	public SimSoundCloudArtist(bool initHere)
	{
	}

	public void SetScreenName()
	{
	}

	public override void IncrementAge()
	{
	}
}
