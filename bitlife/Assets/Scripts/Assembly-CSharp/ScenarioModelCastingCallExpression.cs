using Game;

public class ScenarioModelCastingCallExpression : Scenario
{
	private string _narrative;

	private readonly SimModelGig _gig;

	private SimSpecialJobModel _modelJob;

	private DataModelPhotoShootFacialExpression _callShoot;

	private BracketParser _parser;

	public ScenarioModelCastingCallExpression(SimModelGig gig)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
