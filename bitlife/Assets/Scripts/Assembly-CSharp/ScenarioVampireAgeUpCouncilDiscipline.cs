using Game;

public class ScenarioVampireAgeUpCouncilDiscipline : Scenario
{
	private string _narrative;

	private SimPerson _person;

	private float _severityPercentage;

	private DataVampireCouncilDisciplineScenario _councilScenario;

	private BracketParser _parser;

	public override void RefreshScenario()
	{
	}

	private float CalculateSeverity()
	{
		return 0f;
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
