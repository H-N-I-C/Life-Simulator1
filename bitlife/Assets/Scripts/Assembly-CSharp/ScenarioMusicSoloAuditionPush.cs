public class ScenarioMusicSoloAuditionPush : Scenario
{
	protected int _role;

	protected int _genre;

	protected SimCompany _recordCompany;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataMusicRole GetMusicRoleForHero()
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
