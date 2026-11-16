using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimMusicProducerRecordLabel
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<SimMusicProducerArtist> _003C_003E9__32_0;

		public static Func<SimMusicProducerArtist, float> _003C_003E9__34_0;

		internal bool _003CPruneDeadArtists_003Eb__32_0(SimMusicProducerArtist artist)
		{
			return false;
		}

		internal float _003CGetTopArtists_003Eb__34_0(SimMusicProducerArtist x)
		{
			return 0f;
		}
	}

	private string Name__BackingField;

	private MUSIC_PRODUCER_GENRE Genre__BackingField;

	private int AwardsCount__BackingField;

	private bool _isSetupComplete;

	private int _initialArtistCount;

	private readonly List<SimMusicProducerArtist> _artists;

	private float _popularity;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
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

	public int AwardsCount
	{
		get
		{
			return AwardsCount__BackingField;
		}
		set
		{
			AwardsCount__BackingField = value;
		}
	}

	public float Popularity => 0f;

	public int ArtistsCount => 0;

	public SimMusicProducerRecordLabel()
	{
	}

	public SimMusicProducerRecordLabel(MUSIC_PRODUCER_GENRE desiredGenre, string excludedName = "")
	{
	}

	public void IncrementAge()
	{
	}

	public void CompleteSetup()
	{
	}

	public static HashSet<string> GetAllUsedArtistNamesInPlace(SimPlace place)
	{
		return null;
	}

	public static Dictionary<string, DataMusicArtistStageName> GetAllUnusedStageNames(SimPlace place, MUSIC_PRODUCER_GENRE genre, GENDER gender, int skinColor, BracketParser parser)
	{
		return null;
	}

	public static WeightedList<KeyValuePair<string, DataMusicArtistStageName>> ConvertToWeightedList(Dictionary<string, DataMusicArtistStageName> allValidNames)
	{
		return null;
	}

	public static Dictionary<string, DataMusicArtistStageName> ParseKeys(BracketParser parser, Dictionary<string, DataMusicArtistStageName> allValidNames)
	{
		return null;
	}

	public static Dictionary<string, DataMusicArtistStageName> GetAllValidStageNamesAvailableForWithBrackets(MUSIC_PRODUCER_GENRE genre, GENDER gender, int skinColor)
	{
		return null;
	}

	public int GetArtistCount()
	{
		return 0;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	private void PruneDeadArtists()
	{
	}

	public SimMusicProducerArtist GetTopArtist()
	{
		return null;
	}

	public List<SimMusicProducerArtist> GetTopArtists(int count)
	{
		return null;
	}

	public List<SimMusicProducerArtist> GetArtists()
	{
		return null;
	}

	public bool WillWorkWithHero(SimMusicProducerArtist artist)
	{
		return false;
	}

	public void AdjustAwardsCount(int numberOfAwards)
	{
	}

	public string GetRejectionString(SimMusicProducerArtist artist, MUSIC_PRODUCER_GENRE stageNameGenre, float scoutReputation)
	{
		return null;
	}

	public MUSIC_PRODUCER_GENRE[] GetCompatibleGenres()
	{
		return null;
	}

	public void AddArtist(SimMusicProducerArtist artist)
	{
	}

	public void RemoveArtist(SimMusicProducerArtist artist)
	{
	}

	public bool ArtistExistsAtRecordLabel(SimMusicProducerArtist artist)
	{
		return false;
	}

	public string FormatOtherCompatibleGenres()
	{
		return null;
	}

	public static SimMusicProducerRecordLabel GetCurrentOccupationRecordLabel()
	{
		return null;
	}

	private float GetCompatibilityMultiplier(MUSIC_PRODUCER_GENRE artistGenre, MUSIC_PRODUCER_GENRE labelGenre)
	{
		return 0f;
	}
}
