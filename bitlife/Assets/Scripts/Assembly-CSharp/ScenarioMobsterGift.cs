using System.Collections;

public class ScenarioMobsterGift : Scenario
{
	protected SimMobster _mobster;

	protected SimSpecialJobMafia _specialJob;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioMobsterGift(SimMobster theMobster)
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
