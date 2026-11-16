public class ScenarioMafiaPushShakedown : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected SimMobster _boss;

	protected DataMafiaPushTarget _pushTarget;

	protected int _pushTargetGender;

	public ScenarioMafiaPushShakedown(SimSpecialJobMafia theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
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
