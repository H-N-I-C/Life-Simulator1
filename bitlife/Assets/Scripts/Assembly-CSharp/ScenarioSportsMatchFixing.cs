public class ScenarioSportsMatchFixing : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected SimSportsTeam _opponentTeam;

	private string _narrative;

	private string _offerPerson;

	private int _offerAmount;

	public ScenarioSportsMatchFixing()
	{
	}

	public ScenarioSportsMatchFixing(SimSpecialJobAthlete theSpecialJob)
	{
	}

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
