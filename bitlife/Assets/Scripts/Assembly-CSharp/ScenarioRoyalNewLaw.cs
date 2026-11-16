public class ScenarioRoyalNewLaw : Scenario
{
	protected DataPoliticalLaw _law;

	protected string _lawString;

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
