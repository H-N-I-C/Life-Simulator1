public class ScenarioPrisonGangRecruitment : Scenario
{
	protected DataPrisonGang _gang;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataPrisonGang GetEligibleGang()
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
