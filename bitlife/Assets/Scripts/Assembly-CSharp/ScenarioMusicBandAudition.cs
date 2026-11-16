public class ScenarioMusicBandAudition : Scenario
{
	protected SimMusicBand _band;

	private int _role;

	public ScenarioMusicBandAudition()
	{
	}

	public ScenarioMusicBandAudition(SimMusicBand theBand, int theRole)
	{
	}

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
