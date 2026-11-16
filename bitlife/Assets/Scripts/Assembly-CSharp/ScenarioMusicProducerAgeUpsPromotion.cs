using System.Collections.Generic;

public class ScenarioMusicProducerAgeUpsPromotion : Scenario
{
	private readonly Dictionary<string, string> _bracketDictionary;

	private BracketParser _parser;

	public ScenarioMusicProducerAgeUpsPromotion(SimSpecialJobMusicProducer specialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
