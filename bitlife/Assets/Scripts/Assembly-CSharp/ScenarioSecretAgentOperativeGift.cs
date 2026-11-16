using System.Collections;

public class ScenarioSecretAgentOperativeGift : Scenario
{
	protected SimSecretAgent _agent;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioSecretAgentOperativeGift(SimSecretAgent agent)
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
