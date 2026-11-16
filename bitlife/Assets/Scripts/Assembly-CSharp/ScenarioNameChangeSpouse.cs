public class ScenarioNameChangeSpouse : Scenario
{
	protected string _heroNewLastName;

	protected string _spouseNewLastName;

	protected string _narrative;

	private bool ShouldForceHyphenatedName()
	{
		return false;
	}

	public ScenarioNameChangeSpouse()
	{
	}

	public ScenarioNameChangeSpouse(string newLastName)
	{
	}

	public ScenarioNameChangeSpouse(string newLastName, bool heroChange)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
