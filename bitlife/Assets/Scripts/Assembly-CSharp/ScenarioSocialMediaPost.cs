using System.Collections;

public class ScenarioSocialMediaPost : Scenario
{
	protected SimSocialMediaChannel _socialMediaChannel;

	protected ArrayList _postTypeArray;

	protected int _followChannel;

	protected bool _requireRewardedVideo;

	public ScenarioSocialMediaPost(int theChannel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool RequireRewardedVideoToGet()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
