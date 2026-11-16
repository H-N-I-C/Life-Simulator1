using System.Collections;
using System.Collections.Generic;

public class ScenarioMediaOnlyFansWishlist : Scenario
{
	private readonly SimSocialMediaChannel _channel;

	private string _narrative;

	public ScenarioMediaOnlyFansWishlist(SimSocialMediaChannel channel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string GetFeedbackStringForLicenseCheck(List<string> list)
	{
		return null;
	}
}
