using System.Collections.Generic;

public class AspiringArtistActivity
{
	private SimMusicProducerArtist Artist__BackingField;

	private string ColorString__BackingField;

	private string TrackTitle__BackingField;

	private SimMusicProducerRecordLabel RecordLabel__BackingField;

	private Dictionary<string, string> BracketDictionary__BackingField;

	private BracketParser Parser__BackingField;

	private Life _life;

	private SimHero _hero;

	private SimSpecialJobMusicProducer _specialJob;

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

	public string ColorString
	{
		get
		{
			return ColorString__BackingField;
		}
		private set
		{
			ColorString__BackingField = value;
		}
	}

	public string TrackTitle
	{
		get
		{
			return TrackTitle__BackingField;
		}
		private set
		{
			TrackTitle__BackingField = value;
		}
	}

	public SimMusicProducerRecordLabel RecordLabel
	{
		get
		{
			return RecordLabel__BackingField;
		}
		internal set
		{
			RecordLabel__BackingField = value;
		}
	}

	public Dictionary<string, string> BracketDictionary
	{
		get
		{
			return BracketDictionary__BackingField;
		}
		private set
		{
			BracketDictionary__BackingField = value;
		}
	}

	public BracketParser Parser
	{
		get
		{
			return Parser__BackingField;
		}
		private set
		{
			Parser__BackingField = value;
		}
	}

	public Scenario GetAgeUpScenario(Life life, SimSpecialJobMusicProducer specialJob)
	{
		return null;
	}

	public void SignTheArtist()
	{
	}
}
