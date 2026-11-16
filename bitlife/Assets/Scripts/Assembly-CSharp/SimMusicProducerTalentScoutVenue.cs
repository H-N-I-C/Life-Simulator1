using System;
using System.Collections.Generic;
using Game;

public class SimMusicProducerTalentScoutVenue
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, MUSIC_PRODUCER_GENRE> _003C_003E9__35_0;

		internal MUSIC_PRODUCER_GENRE _003CGenerateUniqueNamesForNewTalent_003Eb__35_0(string x)
		{
			return MUSIC_PRODUCER_GENRE.NONE;
		}
	}

	private SimSpecialJobMusicProducer TalentScout__BackingField;

	private SimMusicProducerVenueVariant Variant__BackingField;

	private SimMusicProducerArtist PotentialTalent__BackingField;

	private List<MUSIC_PRODUCER_GENRE> Genres__BackingField;

	private bool IsTalentNameNew__BackingField;

	private bool IsTalentGenreNew__BackingField;

	private string RejectionString__BackingField;

	protected MUSIC_PRODUCER_GENRE _nextArtistGenre;

	private readonly Dictionary<string, MUSIC_PRODUCER_GENRE> _artistNameToGenres;

	private List<KeyValuePair<string, DataMusicArtistStageName>> _preGeneratedStageNames;

	private int _numActsListened;

	public SimSpecialJobMusicProducer TalentScout
	{
		get
		{
			return TalentScout__BackingField;
		}
		private set
		{
			TalentScout__BackingField = value;
		}
	}

	public SimMusicProducerVenueVariant Variant
	{
		get
		{
			return Variant__BackingField;
		}
		private set
		{
			Variant__BackingField = value;
		}
	}

	public SimMusicProducerArtist PotentialTalent
	{
		get
		{
			return PotentialTalent__BackingField;
		}
		private set
		{
			PotentialTalent__BackingField = value;
		}
	}

	public List<MUSIC_PRODUCER_GENRE> Genres
	{
		get
		{
			return Genres__BackingField;
		}
		private set
		{
			Genres__BackingField = value;
		}
	}

	public bool IsTalentNameNew
	{
		get
		{
			return IsTalentNameNew__BackingField;
		}
		private set
		{
			IsTalentNameNew__BackingField = value;
		}
	}

	public bool IsTalentGenreNew
	{
		get
		{
			return IsTalentGenreNew__BackingField;
		}
		private set
		{
			IsTalentGenreNew__BackingField = value;
		}
	}

	public string RejectionString
	{
		get
		{
			return RejectionString__BackingField;
		}
		private set
		{
			RejectionString__BackingField = value;
		}
	}

	public SimMusicProducerTalentScoutVenue(SimSpecialJobMusicProducer talentScout)
	{
	}

	private List<MUSIC_PRODUCER_GENRE> GenerateGenres(MUSIC_PRODUCER_GENRE recordLabelGenre)
	{
		return null;
	}

	public void FindPotentialTalent()
	{
	}

	private List<KeyValuePair<string, DataMusicArtistStageName>> GenerateUniqueNamesForNewTalent()
	{
		return null;
	}

	public bool TryListenToOtherActs()
	{
		return false;
	}

	public List<string> GenerateGenreOptions()
	{
		return null;
	}

	public List<string> GenerateStageNames(BracketParser parser)
	{
		return null;
	}

	public void ChangePotentialTalentImage(string selectedStageName, string selectedGenre)
	{
	}

	public bool TrySignArtistContract()
	{
		return false;
	}
}
