using System.Collections.Generic;
using Game;

public class MusicProducerDumbScenarioActivity
{
	private SimSpecialJobMusicProducer SpecialJob__BackingField;

	private SimMusicProducerArtist Artist__BackingField;

	private SimLover Lover__BackingField;

	private DataMusicProducerDumbScenario Data__BackingField;

	private List<DataMusicProducerDumbScenarioChoice> Choices__BackingField;

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

	public SimLover Lover
	{
		get
		{
			return Lover__BackingField;
		}
		private set
		{
			Lover__BackingField = value;
		}
	}

	public DataMusicProducerDumbScenario Data
	{
		get
		{
			return Data__BackingField;
		}
		private set
		{
			Data__BackingField = value;
		}
	}

	public List<DataMusicProducerDumbScenarioChoice> Choices
	{
		get
		{
			return Choices__BackingField;
		}
		private set
		{
			Choices__BackingField = value;
		}
	}

	public Scenario GetAgeUpScenario(Life life, SimSpecialJobMusicProducer specialJob)
	{
		return null;
	}

	public void Choose(DataMusicProducerDumbScenarioChoice scenarioChoice)
	{
	}

	private DataMusicProducerDumbScenario GetScenarioData(SimHero hero)
	{
		return null;
	}
}
