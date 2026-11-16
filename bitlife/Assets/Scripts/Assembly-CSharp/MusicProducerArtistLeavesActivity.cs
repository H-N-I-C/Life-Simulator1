public class MusicProducerArtistLeavesActivity
{
	private SimSpecialJobMusicProducer SpecialJob__BackingField;

	private SimMusicProducerRecordLabel RecordLabel__BackingField;

	private SimMusicProducerArtist Artist__BackingField;

	private string ArtistLeaveString__BackingField;

	private SimHero _hero;

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

	public SimMusicProducerRecordLabel RecordLabel
	{
		get
		{
			return RecordLabel__BackingField;
		}
		private set
		{
			RecordLabel__BackingField = value;
		}
	}

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

	public string ArtistLeaveString
	{
		get
		{
			return ArtistLeaveString__BackingField;
		}
		private set
		{
			ArtistLeaveString__BackingField = value;
		}
	}

	public Scenario GetAgeUpScenario(Life life, SimSpecialJobMusicProducer specialJob)
	{
		return null;
	}

	private SimMusicProducerArtist GetNeglectedArtist(SimSpecialJobMusicProducer specialJob)
	{
		return null;
	}

	public bool AskToStay()
	{
		return false;
	}

	public void RemoveArtist()
	{
	}
}
