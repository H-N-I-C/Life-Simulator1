using System.Collections.Generic;

public class ScenarioRacingTrophyCaseProfile : Scenario
{
	private readonly SimRacing _racing;

	private readonly SimRacingCup _cup;

	private int _placement;

	public ScenarioRacingTrophyCaseProfile(SimRacing racing, SimRacingCup cup)
	{
	}

	public override void RefreshScenario()
	{
	}

	private Dictionary<string, string> CreateTableDictionary()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
