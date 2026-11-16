using System.Collections;

public class ScenarioOpportunityClub : Scenario
{
	protected ArrayList _nightclubArray;

	protected SimCompany _nightclubCompany;

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
