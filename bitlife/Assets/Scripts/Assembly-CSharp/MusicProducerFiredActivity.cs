public class MusicProducerFiredActivity
{
	private const int MinReputation = 30;

	private SimSpecialJobMusicProducer SpecialJob__BackingField;

	private string TerminationString__BackingField;

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

	public string TerminationString
	{
		get
		{
			return TerminationString__BackingField;
		}
		private set
		{
			TerminationString__BackingField = value;
		}
	}

	public Scenario GetAgeUpScenario(Life life, SimSpecialJobMusicProducer specialJob)
	{
		return null;
	}

	private bool ShouldBeFired(SimSpecialJobMusicProducer specialJob)
	{
		return false;
	}

	public bool TryBeg()
	{
		return false;
	}

	public void Fire()
	{
	}
}
