using System.Collections;

public class ScenarioModelAgencyBookingAgentGift : Scenario
{
	private SimSpecialJobModel _modelJob;

	protected SimBookingAgent _agent;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioModelAgencyBookingAgentGift(SimBookingAgent theAgent)
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
