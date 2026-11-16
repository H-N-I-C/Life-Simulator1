public class ScenarioSportsPivotalMoment : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected SimSportsTeam _opponentTeam;

	protected DataSportsPivotalMomentScenario _data;

	private string _narrative;

	public BracketParser _parser;

	public ScenarioSportsPivotalMoment()
	{
	}

	public ScenarioSportsPivotalMoment(SimSpecialJobAthlete theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataSportsPivotalMomentScenario GetPivotalMomentScenario()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
