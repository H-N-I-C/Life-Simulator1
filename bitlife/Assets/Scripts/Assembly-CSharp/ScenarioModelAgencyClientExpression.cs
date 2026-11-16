using Game;

public class ScenarioModelAgencyClientExpression : Scenario
{
	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private readonly SimModelingAgencyClient _client;

	private readonly SimModelGig _gig;

	private DataModelPhotoShootFacialExpression _callShoot;

	private BracketParser _parser;

	public ScenarioModelAgencyClientExpression(SimModelingAgencyClient client, SimModelGig gig)
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
