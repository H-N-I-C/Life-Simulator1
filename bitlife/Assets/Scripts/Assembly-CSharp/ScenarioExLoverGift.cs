using System.Collections;

public class ScenarioExLoverGift : Scenario
{
	protected SimLover _exLover;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioExLoverGift(SimLover theExLover)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
