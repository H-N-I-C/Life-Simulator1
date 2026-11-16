using System.Collections;

public class ScenarioFinancialAdvisorGift : Scenario
{
	protected SimFinancialAdvisor _advisor;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioFinancialAdvisorGift(SimFinancialAdvisor theAdvisor)
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
