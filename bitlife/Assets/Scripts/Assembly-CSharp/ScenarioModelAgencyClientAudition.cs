using System.Collections;
using Game;

public class ScenarioModelAgencyClientAudition : Scenario
{
	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private readonly SimModelingAgencyClient _client;

	private readonly SimModelGig _gig;

	private DataModelAuditionSnippet _data;

	public ScenarioModelAgencyClientAudition(SimModelingAgencyClient client, SimModelGig gig)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
