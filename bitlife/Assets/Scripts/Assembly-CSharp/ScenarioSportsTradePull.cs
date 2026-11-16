public class ScenarioSportsTradePull : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected SimSportsTeam _newTeam;

	protected SimSportsTeam _desiredTeam;

	private string _narrative;

	public ScenarioSportsTradePull(SimSpecialJobAthlete theSpecialJob)
	{
	}

	public ScenarioSportsTradePull(SimSpecialJobAthlete theSpecialJob, SimSportsTeam theDesiredTeam)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
