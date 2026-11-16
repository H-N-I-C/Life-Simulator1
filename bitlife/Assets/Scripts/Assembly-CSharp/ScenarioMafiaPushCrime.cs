public class ScenarioMafiaPushCrime : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected SimMobster _boss;

	protected DataMafiaPushCrime _pushCrime;

	protected string _pushCrimeParsedPresentSnippet;

	protected string _pushCrimeParsedPastSnippet;

	public ScenarioMafiaPushCrime(SimSpecialJobMafia theSpecialJob)
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
