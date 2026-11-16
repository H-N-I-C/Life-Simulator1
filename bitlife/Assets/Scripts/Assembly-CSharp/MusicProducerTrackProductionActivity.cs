using System.Collections.Generic;
using Game;

public class MusicProducerTrackProductionActivity
{
	private readonly DataMusicProducerGenre _genreData;

	private SimMusicProducerArtist Artist__BackingField;

	private SimMusicProducerArtist RandomArtist__BackingField;

	private DataMusicTrackSubject SubjectData__BackingField;

	private DataMusicTrackLyricalHook HookData__BackingField;

	private DataMusicTrackBeat BeatData__BackingField;

	private string SubjectString__BackingField;

	private string HookString__BackingField;

	private string BeatString__BackingField;

	private string TrackName__BackingField;

	private bool ApprovedByLabel__BackingField;

	private List<string> RejectionReasons__BackingField;

	private int ProductionScenarioCount__BackingField;

	private SimSpecialJobMusicProducer SpecialJob__BackingField;

	private int GenreScore__BackingField;

	private int SubjectScore__BackingField;

	private int HookScore__BackingField;

	private int BeatScore__BackingField;

	private int EventScore__BackingField;

	private float Reception__BackingField;

	public SimMusicProducerArtist Artist
	{
		get
		{
			return Artist__BackingField;
		}
		private set
		{
			Artist__BackingField = value;
		}
	}

	public SimMusicProducerArtist RandomArtist
	{
		get
		{
			return RandomArtist__BackingField;
		}
		private set
		{
			RandomArtist__BackingField = value;
		}
	}

	public DataMusicTrackSubject SubjectData
	{
		get
		{
			return SubjectData__BackingField;
		}
		set
		{
			SubjectData__BackingField = value;
		}
	}

	public DataMusicTrackLyricalHook HookData
	{
		get
		{
			return HookData__BackingField;
		}
		set
		{
			HookData__BackingField = value;
		}
	}

	public DataMusicTrackBeat BeatData
	{
		get
		{
			return BeatData__BackingField;
		}
		set
		{
			BeatData__BackingField = value;
		}
	}

	public string SubjectString
	{
		get
		{
			return SubjectString__BackingField;
		}
		set
		{
			SubjectString__BackingField = value;
		}
	}

	public string HookString
	{
		get
		{
			return HookString__BackingField;
		}
		set
		{
			HookString__BackingField = value;
		}
	}

	public string BeatString
	{
		get
		{
			return BeatString__BackingField;
		}
		set
		{
			BeatString__BackingField = value;
		}
	}

	public string TrackName
	{
		get
		{
			return TrackName__BackingField;
		}
		set
		{
			TrackName__BackingField = value;
		}
	}

	public bool ApprovedByLabel
	{
		get
		{
			return ApprovedByLabel__BackingField;
		}
		private set
		{
			ApprovedByLabel__BackingField = value;
		}
	}

	public List<string> RejectionReasons
	{
		get
		{
			return RejectionReasons__BackingField;
		}
		private set
		{
			RejectionReasons__BackingField = value;
		}
	}

	public int ProductionScenarioCount
	{
		get
		{
			return ProductionScenarioCount__BackingField;
		}
		private set
		{
			ProductionScenarioCount__BackingField = value;
		}
	}

	public SimSpecialJobMusicProducer SpecialJob
	{
		get
		{
			return SpecialJob__BackingField;
		}
		private set
		{
			SpecialJob__BackingField = value;
		}
	}

	public int GenreScore
	{
		get
		{
			return GenreScore__BackingField;
		}
		private set
		{
			GenreScore__BackingField = value;
		}
	}

	public int SubjectScore
	{
		get
		{
			return SubjectScore__BackingField;
		}
		private set
		{
			SubjectScore__BackingField = value;
		}
	}

	public int HookScore
	{
		get
		{
			return HookScore__BackingField;
		}
		private set
		{
			HookScore__BackingField = value;
		}
	}

	public int BeatScore
	{
		get
		{
			return BeatScore__BackingField;
		}
		private set
		{
			BeatScore__BackingField = value;
		}
	}

	public int EventScore
	{
		get
		{
			return EventScore__BackingField;
		}
		private set
		{
			EventScore__BackingField = value;
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

	public MusicProducerTrackProductionActivity(SimMusicProducerArtist artist)
	{
	}

	public void CalculateScoreDetails()
	{
	}

	private int GetScoreByCompatibility(string genre)
	{
		return 0;
	}

	private int GetScenarioCount()
	{
		return 0;
	}

	private int GetSuccessOdds(int score)
	{
		return 0;
	}

	public void DoSampling(string genre, bool didit, bool insult, bool complain)
	{
	}

	public void AddSignatureSound(DataMusicSignatureSound selectedSound)
	{
	}

	public void AddArtisticFlair(bool letThemDoIt, DataMusicArtisticFlairString flair)
	{
	}

	public void CalculateReception()
	{
	}

	private bool _003CCalculateScoreDetails_003Eb__78_0(string q)
	{
		return false;
	}

	private bool _003CAddArtisticFlair_003Eb__84_0(string q)
	{
		return false;
	}

	private bool _003CAddArtisticFlair_003Eb__84_1(string q)
	{
		return false;
	}
}
