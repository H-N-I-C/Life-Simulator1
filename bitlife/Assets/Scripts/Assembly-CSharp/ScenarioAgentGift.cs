using System.Collections;

public class ScenarioAgentGift : Scenario
{
	protected SimAgent _agent;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioAgentGift(SimAgent theAgent)
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
