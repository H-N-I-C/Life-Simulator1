public class ScenarioMafiaCrimeEarnings : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected string _crimeTypeString;

	protected float _earnings;

	public ScenarioMafiaCrimeEarnings(SimSpecialJobMafia theSpecialJob, string theCrimeTypeString, float theEarnings)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
