using System.Collections;

public class ScenarioCultCommunardGift : Scenario
{
	protected SimCommunard _communard;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioCultCommunardGift(SimCommunard theCommunard)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
