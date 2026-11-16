public class ScenarioSocialMediaTrollOutboundComplete : Scenario
{
	protected SimSocialMediaChannel _socialMediaChannel;

	protected SimSocialMediaTroll _troll;

	protected bool _isDropDownPerson;

	protected bool _isBitLifeApp;

	protected SimPerson _victimPerson;

	protected string _victimStringScenario;

	protected string _victimStringNarrative;

	protected int _victimGender;

	protected string _narrative;

	public ScenarioSocialMediaTrollOutboundComplete(int theChannel, SimSocialMediaTroll theTroll)
	{
	}

	public ScenarioSocialMediaTrollOutboundComplete(int theChannel, DataCelebrity theCelebrity)
	{
	}

	public ScenarioSocialMediaTrollOutboundComplete(int theChannel, SimDropDownPerson theDropDownPerson)
	{
	}

	public ScenarioSocialMediaTrollOutboundComplete(int theChannel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
