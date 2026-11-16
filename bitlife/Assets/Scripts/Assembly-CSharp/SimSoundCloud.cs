using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SimSoundCloud : IRichSocialMediaIncomeSource, IRichSocialMedia
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__13_0;

		public static Func<SimSoundCloudTrack, double> _003C_003E9__13_1;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__15_0;

		public static Func<SimSoundCloudTrack, double> _003C_003E9__15_1;

		public static Func<SimSoundCloudTrack, double> _003C_003E9__17_0;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__31_0;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__41_0;

		public static Func<SimSoundCloudArtist, string> _003C_003E9__42_0;

		public static Func<SimSoundCloudArtist, MUSIC_PRODUCER_GENRE> _003C_003E9__47_0;

		public static Func<SimSoundCloudArtist, MUSIC_PRODUCER_GENRE> _003C_003E9__47_1;

		public static Func<IGrouping<MUSIC_PRODUCER_GENRE, SimSoundCloudArtist>, MUSIC_PRODUCER_GENRE> _003C_003E9__47_2;

		public static Func<SimSoundCloudArtist, bool> _003C_003E9__47_3;

		public static Func<SimSoundCloudArtist, int> _003C_003E9__47_4;

		public static Func<SimSoundCloudTrack, int> _003C_003E9__47_5;

		public static Func<SimSoundCloudTrack, double> _003C_003E9__48_0;

		public static Func<SimSoundCloudTrack, int> _003C_003E9__49_0;

		internal bool _003Cget_EarningsThisYear_003Eb__13_0(SimSoundCloudTrack q)
		{
			return false;
		}

		internal double _003Cget_EarningsThisYear_003Eb__13_1(SimSoundCloudTrack q)
		{
			return 0.0;
		}

		internal bool _003Cget_EarningsLastYear_003Eb__15_0(SimSoundCloudTrack q)
		{
			return false;
		}

		internal double _003Cget_EarningsLastYear_003Eb__15_1(SimSoundCloudTrack q)
		{
			return 0.0;
		}

		internal double _003Cget_TotalEarnings_003Eb__17_0(SimSoundCloudTrack q)
		{
			return 0.0;
		}

		internal bool _003CIncrementAge_003Eb__31_0(SimSoundCloudTrack x)
		{
			return false;
		}

		internal bool _003CGetRandomRoyaltyRequestedTrack_003Eb__41_0(SimSoundCloudTrack x)
		{
			return false;
		}

		internal string _003CGetAllUsedScreenNames_003Eb__42_0(SimSoundCloudArtist x)
		{
			return null;
		}

		internal MUSIC_PRODUCER_GENRE _003CCalculateNewInsight_003Eb__47_0(SimSoundCloudArtist x)
		{
			return MUSIC_PRODUCER_GENRE.NONE;
		}

		internal MUSIC_PRODUCER_GENRE _003CCalculateNewInsight_003Eb__47_1(SimSoundCloudArtist x)
		{
			return MUSIC_PRODUCER_GENRE.NONE;
		}

		internal MUSIC_PRODUCER_GENRE _003CCalculateNewInsight_003Eb__47_2(IGrouping<MUSIC_PRODUCER_GENRE, SimSoundCloudArtist> x)
		{
			return MUSIC_PRODUCER_GENRE.NONE;
		}

		internal bool _003CCalculateNewInsight_003Eb__47_3(SimSoundCloudArtist x)
		{
			return false;
		}

		internal int _003CCalculateNewInsight_003Eb__47_4(SimSoundCloudArtist x)
		{
			return 0;
		}

		internal int _003CCalculateNewInsight_003Eb__47_5(SimSoundCloudTrack x)
		{
			return 0;
		}

		internal double _003CGetTotalEarnings_003Eb__48_0(SimSoundCloudTrack q)
		{
			return 0.0;
		}

		internal int _003CGetAgeAtLastEarning_003Eb__49_0(SimSoundCloudTrack track)
		{
			return 0;
		}
	}

	private const int FOLLOWER_MAX = 10000000;

	private const int FOLLOWER_SOFT_MAX = 5000000;

	private string ScreenName__BackingField;

	private int YearsActive__BackingField;

	private SoundCloudInsight CurrentInsight__BackingField;

	private int _followerCount;

	private List<SimSoundCloudArtist> _soundCloudArtists;

	private List<SimSoundCloudTrack> _tracks;

	[NonSerialized]
	private long _PreviousYearTrackPlaysTotal;

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

	public int YearsActive
	{
		get
		{
			return YearsActive__BackingField;
		}
		set
		{
			YearsActive__BackingField = value;
		}
	}

	public int Posts => 0;

	public double EarningsThisYear => 0.0;

	public double EarningsLastYear => 0.0;

	public double TotalEarnings => 0.0;

	public int AgeAtLastEarnings => 0;

	private SoundCloudInsight CurrentInsight
	{
		get
		{
			return CurrentInsight__BackingField;
		}
		set
		{
			CurrentInsight__BackingField = value;
		}
	}

	public SimSoundCloud(string screenName)
	{
	}

	public void InitialSetup()
	{
	}

	public string GetEmoji()
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public void AdjustFollowerCount(int amount)
	{
	}

	public void SetFollowerCount(int amount)
	{
	}

	public void AddSoundCloudArtist(SimSoundCloudArtist artist)
	{
	}

	public void RemoveSoundCloudArtist(SimSoundCloudArtist artist)
	{
	}

	public List<SimSoundCloudArtist> GetSoundCloudArtists()
	{
		return null;
	}

	public void AddTrack(SimSoundCloudTrack track)
	{
	}

	public List<SimSoundCloudTrack> GetTracks()
	{
		return null;
	}

	public int GetFollowerCount()
	{
		return 0;
	}

	public SoundCloudInsight GetThisYearsInsight()
	{
		return null;
	}

	public SimSoundCloudTrack GetRandomRoyaltyRequestedTrack()
	{
		return null;
	}

	public List<string> GetAllUsedScreenNames()
	{
		return null;
	}

	private float GetTrackPlaybackDeltaPercentage()
	{
		return 0f;
	}

	private float GetTrackDeltaCoefficient(float delta)
	{
		return 0f;
	}

	private void IncrementAgeAddFollowers(List<SimSoundCloudTrack> tracksCreatedThisYear)
	{
	}

	private void IncrementAgeBleedFollowers()
	{
	}

	private SoundCloudInsight CalculateNewInsight()
	{
		return null;
	}

	public double GetTotalEarnings()
	{
		return 0.0;
	}

	private int GetAgeAtLastEarning()
	{
		return 0;
	}

	private bool _003CIncrementAge_003Eb__31_1(SimSoundCloudTrack x)
	{
		return false;
	}
}
