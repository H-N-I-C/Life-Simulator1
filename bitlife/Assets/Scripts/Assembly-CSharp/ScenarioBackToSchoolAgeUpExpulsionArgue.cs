using System.Collections.Generic;

public class ScenarioBackToSchoolAgeUpExpulsionArgue : Scenario
{
	private const string OldSchoolTag = "<old-school>";

	private const string NewSchoolTag = "<new-school>";

	private readonly BracketParser _parser;

	private readonly Life _life;

	private List<SimParent> _parents;

	public ScenarioBackToSchoolAgeUpExpulsionArgue(string oldSchool, string newSchool)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private string ParseBody(string body)
	{
		return null;
	}

	private ScenarioChoice ParseScenarioChoice(string template)
	{
		return null;
	}

	private void LogNarrative(string narrative)
	{
	}
}
