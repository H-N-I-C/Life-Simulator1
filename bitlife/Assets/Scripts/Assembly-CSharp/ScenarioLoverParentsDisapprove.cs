public class ScenarioLoverParentsDisapprove : Scenario
{
	protected SimParent _disapprovingParent;

	protected bool _bothParentsDisapprove;

	protected string _parentsStringScenario;

	protected string _parentsStringFeedback;

	protected string _parentsPronoun;

	protected string _parentsPronounPossessive;

	protected string _narrative;

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
