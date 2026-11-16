public class ScenarioInstrument : Scenario
{
	private float _cost;

	private bool _requireRewardedVideo;

	protected DataMusicInstrument _instrumentData;

	public ScenarioInstrument(DataMusicInstrument theInstrumentData)
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

	public override string EmojiString()
	{
		return null;
	}
}
