using System.Collections;

public class ScenarioBandmateGift : Scenario
{
	protected SimBandmate _bandmate;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioBandmateGift(SimBandmate theBandmate)
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
