public class ScenarioRoyalTabloid : Scenario
{
	protected string _newspaper;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataRoyalTabloidScenario GetTabloidScenarioData()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
