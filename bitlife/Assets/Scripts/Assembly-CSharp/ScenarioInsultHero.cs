public class ScenarioInsultHero : Scenario
{
	protected int _insulter;

	protected int _personGender;

	protected SimPerson _person;

	protected DataStreetPerson _insulterStreetPerson;

	protected string _insult;

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
