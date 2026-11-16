public class ScenarioMartialArt : Scenario
{
	protected DataMartialArt _martialArtData;

	protected float _cost;

	protected bool _requireRewardedVideo;

	public ScenarioMartialArt(DataMartialArt theMartialArtData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
