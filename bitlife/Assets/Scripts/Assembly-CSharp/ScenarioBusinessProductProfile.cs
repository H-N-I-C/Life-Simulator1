public class ScenarioBusinessProductProfile : Scenario
{
	protected SimSpecialJobBusiness _currentBusinessJob;

	protected SimProduct _theProduct;

	protected bool _isPush;

	public ScenarioBusinessProductProfile(SimSpecialJobBusiness businessJob, SimProduct theProduct, bool isPush = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
