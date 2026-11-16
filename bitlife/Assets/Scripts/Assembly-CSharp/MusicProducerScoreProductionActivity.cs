using Game;

public class MusicProducerScoreProductionActivity
{
	private const string ProjectTypeFilm = "film";

	private const string ProjectTypeGame = "video game";

	public const float MinBpm = 40f;

	public const float MaxBpm = 180f;

	private const int FailHappinessImpact = 1;

	private const int FailReputationImpact = 2;

	private string ProjectType__BackingField;

	private string ProjectName__BackingField;

	private float Commision__BackingField;

	private int Bpm__BackingField;

	private bool Sucess__BackingField;

	private DataMusicScoreProductionProjectTone ToneData__BackingField;

	private DataMusicSoundCloudTrackInstrument KeyInstrument__BackingField;

	private DataMusicSoundCloudTrackInstrument BgInstrument__BackingField;

	private bool IsBPMCorrect__BackingField;

	private bool IsInstrumentCorrect__BackingField;

	private SimSpecialJobMusicProducer SpecialJob__BackingField;

	public string ProjectType
	{
		get
		{
			return ProjectType__BackingField;
		}
		private set
		{
			ProjectType__BackingField = value;
		}
	}

	public string ProjectName
	{
		get
		{
			return ProjectName__BackingField;
		}
		set
		{
			ProjectName__BackingField = value;
		}
	}

	public float Commision
	{
		get
		{
			return Commision__BackingField;
		}
		set
		{
			Commision__BackingField = value;
		}
	}

	public int Bpm
	{
		get
		{
			return Bpm__BackingField;
		}
		set
		{
			Bpm__BackingField = value;
		}
	}

	public bool Sucess
	{
		get
		{
			return Sucess__BackingField;
		}
		set
		{
			Sucess__BackingField = value;
		}
	}

	public DataMusicScoreProductionProjectTone ToneData
	{
		get
		{
			return ToneData__BackingField;
		}
		set
		{
			ToneData__BackingField = value;
		}
	}

	public DataMusicSoundCloudTrackInstrument KeyInstrument
	{
		get
		{
			return KeyInstrument__BackingField;
		}
		set
		{
			KeyInstrument__BackingField = value;
		}
	}

	public DataMusicSoundCloudTrackInstrument BgInstrument
	{
		get
		{
			return BgInstrument__BackingField;
		}
		set
		{
			BgInstrument__BackingField = value;
		}
	}

	public bool IsBPMCorrect
	{
		get
		{
			return IsBPMCorrect__BackingField;
		}
		private set
		{
			IsBPMCorrect__BackingField = value;
		}
	}

	public bool IsInstrumentCorrect
	{
		get
		{
			return IsInstrumentCorrect__BackingField;
		}
		private set
		{
			IsInstrumentCorrect__BackingField = value;
		}
	}

	public SimSpecialJobMusicProducer SpecialJob
	{
		get
		{
			return SpecialJob__BackingField;
		}
		set
		{
			SpecialJob__BackingField = value;
		}
	}

	public void PitchProject()
	{
	}

	private bool _003CPitchProject_003Eb__51_0(string q)
	{
		return false;
	}

	private bool _003CPitchProject_003Eb__51_1(string q)
	{
		return false;
	}
}
