public class ScenarioSportsDraftResults : Scenario
{
	protected DataSport _sport;

	protected int _schoolType;

	private float _prospectScore;

	protected SimSportsTeam _draftingTeam;

	private string _narrative;

	public ScenarioSportsDraftResults(int theSport, int theSchoolType, float theProspectScore)
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
