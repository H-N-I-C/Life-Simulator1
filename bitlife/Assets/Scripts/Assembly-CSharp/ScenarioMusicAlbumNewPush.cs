public class ScenarioMusicAlbumNewPush : Scenario
{
	protected SimSpecialJobMusician _specialJob;

	private string _narrative;

	public ScenarioMusicAlbumNewPush(SimSpecialJobMusician theSpecialJob)
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
}
