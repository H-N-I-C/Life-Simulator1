using System.Collections.Generic;
using Game;

public class DataMusicProducerDumbScenarioChoice
{
	private string Choice__BackingField;

	private string Narrative__BackingField;

	private int ReputationImpact__BackingField;

	public string Choice
	{
		get
		{
			return Choice__BackingField;
		}
		set
		{
			Choice__BackingField = value;
		}
	}

	public string Narrative
	{
		get
		{
			return Narrative__BackingField;
		}
		set
		{
			Narrative__BackingField = value;
		}
	}

	public int ReputationImpact
	{
		get
		{
			return ReputationImpact__BackingField;
		}
		set
		{
			ReputationImpact__BackingField = value;
		}
	}

	public static List<DataMusicProducerDumbScenarioChoice> CreateChoices(DataMusicProducerDumbScenario scenarioData)
	{
		return null;
	}

	private static void AddValidChoice(string choice, string narrative, int reputationImpact, List<DataMusicProducerDumbScenarioChoice> choiceDatas)
	{
	}
}
