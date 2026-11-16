using System.Collections;

public class ScenarioCoworkerGift : Scenario
{
	protected SimCoworker _coworker;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioCoworkerGift(SimCoworker theCoworker)
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
