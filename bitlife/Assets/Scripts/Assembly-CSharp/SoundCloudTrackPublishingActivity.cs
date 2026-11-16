using System;
using System.Collections.Generic;
using Game;

public class SoundCloudTrackPublishingActivity
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__64_0;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__64_1;

		public static Func<SimPerson, bool> _003C_003E9__67_2;

		public static Func<SimChild, bool> _003C_003E9__67_3;

		public static Func<SimMusicInstrument, bool> _003C_003E9__69_0;

		internal bool _003C_002Ector_003Eb__64_0(SimSoundCloudTrack q)
		{
			return false;
		}

		internal bool _003C_002Ector_003Eb__64_1(SimSoundCloudTrack q)
		{
			return false;
		}

		internal bool _003CCreateTrack_003Eb__67_2(SimPerson friend)
		{
			return false;
		}

		internal bool _003CCreateTrack_003Eb__67_3(SimChild child)
		{
			return false;
		}

		internal bool _003CCalculateGenreReceptionScore_003Eb__69_0(SimMusicInstrument q)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass67_0
	{
		public string vibe;

		public string instrument;

		internal bool _003CCreateTrack_003Eb__0(DataMusicTrackVibe q)
		{
			return false;
		}

		internal bool _003CCreateTrack_003Eb__1(DataMusicSoundCloudTrackInstrument q)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass70_0
	{
		public string mainGenre;

		public string compatibleGenre1;

		public string compatibleGenre2;

		public string compatibleGenre3;

		internal bool _003CCalculateCompatibilityScore_003Eb__0(string chosenGenre)
		{
			return false;
		}

		internal bool _003CCalculateCompatibilityScore_003Eb__1(string chosenGenre)
		{
			return false;
		}

		internal bool _003CCalculateCompatibilityScore_003Eb__2(string chosenGenre)
		{
			return false;
		}

		internal bool _003CCalculateCompatibilityScore_003Eb__3(string chosenGenre)
		{
			return false;
		}
	}

	private const float MinHeroSingerRelationshipStrength = 50f;

	private const float MinSingerAttribute = 20f;

	private const int MinSingerAge = 15;

	private DataMusicProducerGenre GenreData__BackingField;

	private bool IsCollab__BackingField;

	private SimSoundCloudArtist Collaborator__BackingField;

	private List<DataMusicTrackVibe> Vibes__BackingField;

	private List<DataMusicSoundCloudTrackInstrument> Instruments__BackingField;

	private MUSIC_PRODUCER_GENRE Genre__BackingField;

	private SimSoundCloudTrack Track__BackingField;

	private DataMusicSoundCloudDumbScenario DumbScenario__BackingField;

	private float Reception__BackingField;

	private SimPerson Singer__BackingField;

	private int FollowerGain__BackingField;

	private bool CollabAcceptedCompatibility__BackingField;

	private bool CollabAcceptedPopularity__BackingField;

	private string RefusalReason__BackingField;

	private bool CanLaunchMoreTracks__BackingField;

	private SimSoundCloud _account;

	public DataMusicProducerGenre GenreData
	{
		get
		{
			return GenreData__BackingField;
		}
		private set
		{
			GenreData__BackingField = value;
		}
	}

	public bool IsCollab
	{
		get
		{
			return IsCollab__BackingField;
		}
		private set
		{
			IsCollab__BackingField = value;
		}
	}

	public SimSoundCloudArtist Collaborator
	{
		get
		{
			return Collaborator__BackingField;
		}
		private set
		{
			Collaborator__BackingField = value;
		}
	}

	public List<DataMusicTrackVibe> Vibes
	{
		get
		{
			return Vibes__BackingField;
		}
		private set
		{
			Vibes__BackingField = value;
		}
	}

	public List<DataMusicSoundCloudTrackInstrument> Instruments
	{
		get
		{
			return Instruments__BackingField;
		}
		private set
		{
			Instruments__BackingField = value;
		}
	}

	public MUSIC_PRODUCER_GENRE Genre
	{
		get
		{
			return Genre__BackingField;
		}
		private set
		{
			Genre__BackingField = value;
		}
	}

	public SimSoundCloudTrack Track
	{
		get
		{
			return Track__BackingField;
		}
		private set
		{
			Track__BackingField = value;
		}
	}

	public DataMusicSoundCloudDumbScenario DumbScenario
	{
		get
		{
			return DumbScenario__BackingField;
		}
		private set
		{
			DumbScenario__BackingField = value;
		}
	}

	public float Reception
	{
		get
		{
			return Reception__BackingField;
		}
		private set
		{
			Reception__BackingField = value;
		}
	}

	public SimPerson Singer
	{
		get
		{
			return Singer__BackingField;
		}
		private set
		{
			Singer__BackingField = value;
		}
	}

	public int FollowerGain
	{
		get
		{
			return FollowerGain__BackingField;
		}
		private set
		{
			FollowerGain__BackingField = value;
		}
	}

	public bool CollabAcceptedCompatibility
	{
		get
		{
			return CollabAcceptedCompatibility__BackingField;
		}
		private set
		{
			CollabAcceptedCompatibility__BackingField = value;
		}
	}

	public bool CollabAcceptedPopularity
	{
		get
		{
			return CollabAcceptedPopularity__BackingField;
		}
		private set
		{
			CollabAcceptedPopularity__BackingField = value;
		}
	}

	public string RefusalReason
	{
		get
		{
			return RefusalReason__BackingField;
		}
		private set
		{
			RefusalReason__BackingField = value;
		}
	}

	public bool CanLaunchMoreTracks
	{
		get
		{
			return CanLaunchMoreTracks__BackingField;
		}
		private set
		{
			CanLaunchMoreTracks__BackingField = value;
		}
	}

	public SoundCloudTrackPublishingActivity(SimSoundCloudArtist collaborator = null)
	{
	}

	public void SetGenre(string genre)
	{
	}

	public string GetEmojiByGenre()
	{
		return null;
	}

	public void CreateTrack(string title, string vibe, string instrument)
	{
	}

	private void FindSinger()
	{
	}

	private void CalculateGenreReceptionScore(DataMusicTrackVibe vibeData, DataMusicSoundCloudTrackInstrument instrumentData)
	{
	}

	private float CalculateCompatibilityScore(string[] chosenGenres, string mainGenre, string compatibleGenre1, string compatibleGenre2, string compatibleGenre3)
	{
		return 0f;
	}

	public void ApplyDumbReceptionImpact(int receptionImpact)
	{
	}

	public void AllowSingerToJoin(bool refused)
	{
	}

	private int GetSingerImpactScore()
	{
		return 0;
	}

	public void CalculateFinalResult()
	{
	}

	internal static bool _003CFindSinger_003Eg__IsSinger_007C68_0(SimPerson person)
	{
		return false;
	}
}
