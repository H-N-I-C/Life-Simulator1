public class ScenarioSportsContractRelease : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected SimSportsInjury _injury;

	private string _narrative;

	public ScenarioSportsContractRelease(SimSpecialJobAthlete theSpecialJob)
	{
	}

	public ScenarioSportsContractRelease(SimSpecialJobAthlete theSpecialJob, SimSportsInjury theInjury)
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
