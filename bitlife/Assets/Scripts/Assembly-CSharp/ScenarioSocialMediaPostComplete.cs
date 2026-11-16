public class ScenarioSocialMediaPostComplete : Scenario
{
	protected SimSocialMediaChannel _socialMediaChannel;

	protected DataSocialMediaPostType _postType;

	protected string _narrative;

	private int _hoursStreamed;

	public ScenarioSocialMediaPostComplete(int theChannel, DataSocialMediaPostType thePostType, int hours = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
