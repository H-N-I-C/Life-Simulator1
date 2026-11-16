using System.Collections;

public class ScenarioLandlordTenantGift : Scenario
{
	protected SimTenant _tenant;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioLandlordTenantGift(SimTenant theTenant)
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
