using Game;

public class ScenarioModelAgencyClientProfile : Scenario
{
	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private readonly SimModelingAgencyClient _client;

	private DataModelCategory _categoryData;

	public ScenarioModelAgencyClientProfile(SimModelingAgencyClient client)
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
