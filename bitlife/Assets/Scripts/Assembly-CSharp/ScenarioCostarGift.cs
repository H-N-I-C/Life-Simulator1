using System.Collections;

public class ScenarioCostarGift : Scenario
{
	protected SimCostar _costar;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioCostarGift(SimCostar theCostar)
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
