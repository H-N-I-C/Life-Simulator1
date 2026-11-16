public class ScenarioSportsSeasonStats : Scenario
{
	private int _sport;

	protected SimSportsTeam _team;

	protected SimAthleteSeasonStats _seasonStats;

	protected SimSpecialJobAthlete _specialJob;

	private string _narrative;

	public ScenarioSportsSeasonStats(int theSport, SimAthleteSeasonStats theSeasonStats)
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
