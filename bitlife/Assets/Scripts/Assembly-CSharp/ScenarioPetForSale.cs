public class ScenarioPetForSale : Scenario
{
	protected SimPet _pet;

	protected int _source;

	protected bool _askParentsContext;

	protected float _cost;

	protected bool _requireRewardedVideo;

	public ScenarioPetForSale(SimPet thePet, int theSource)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
