public class ScenarioSportsCoachInteraction : Scenario
{
	protected bool _isPositive;

	protected string _narrative;

	protected SimSpecialJobAthlete _specialJob;

	public ScenarioSportsCoachInteraction()
	{
	}

	public ScenarioSportsCoachInteraction(SimSpecialJobAthlete theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public SimSportsTeam GetTeamToTradeTo()
	{
		return null;
	}

	public static DataSportsCoachScenario GetCoachScenario()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
