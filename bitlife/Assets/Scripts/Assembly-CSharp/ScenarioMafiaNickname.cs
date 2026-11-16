public class ScenarioMafiaNickname : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected string _nickname;

	protected string _narrative;

	public ScenarioMafiaNickname(SimSpecialJobMafia theSpecialJob, string theNickname)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
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
