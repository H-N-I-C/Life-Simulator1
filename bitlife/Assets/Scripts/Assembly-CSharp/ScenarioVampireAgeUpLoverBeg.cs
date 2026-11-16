using System.Collections.Generic;

public class ScenarioVampireAgeUpLoverBeg : Scenario
{
	private readonly SimHero _hero;

	private readonly SimLover _person;

	private readonly Dictionary<string, string> _bracketDictionary;

	private readonly BracketParser _parser;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	private string ParseBody(string bodyTemplate)
	{
		return null;
	}
}
