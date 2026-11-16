using System.Collections;
using System.Collections.Generic;

public class ScenarioMediaOnlyFansCrossPromote : Scenario
{
	private readonly SimSocialMediaChannel _channel;

	private SimSocialMediaChannel _otherChannel;

	private string _narrative;

	public ScenarioMediaOnlyFansCrossPromote(SimSocialMediaChannel channel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public static List<SimSocialMediaChannel> HasOtherSocialsAvailable()
	{
		return null;
	}
}
