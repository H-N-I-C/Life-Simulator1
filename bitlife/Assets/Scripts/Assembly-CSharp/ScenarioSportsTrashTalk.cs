public class ScenarioSportsTrashTalk : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected DataSportsTrashTalkScenario _data;

	protected SimSportsTeam _opponentTeam;

	protected BracketParser _parser;

	private string _narrative;

	public ScenarioSportsTrashTalk()
	{
	}

	public ScenarioSportsTrashTalk(SimSpecialJobAthlete theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataSportsTrashTalkScenario GetTrashTalkScenario()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
