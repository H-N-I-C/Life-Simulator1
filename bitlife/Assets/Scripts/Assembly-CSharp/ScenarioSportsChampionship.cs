public class ScenarioSportsChampionship : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected SimAthleteSeasonStats _seasonStats;

	protected SimJewel _jewel;

	private string _narrative;

	public ScenarioSportsChampionship(SimSpecialJobAthlete theSpecialJob, SimAthleteSeasonStats theSeasonStats)
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
