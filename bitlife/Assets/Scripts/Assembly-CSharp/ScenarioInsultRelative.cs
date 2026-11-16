public class ScenarioInsultRelative : Scenario
{
	protected int _insultee;

	protected int _insulterGender;

	protected string _insult;

	protected SimChild _insulteeChild;

	protected SimSibling _insulteeSibling;

	protected DataStreetPerson _insulterStreetPerson;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
