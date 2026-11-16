using Game;

public class ScenarioModelFashionWeekShow : Scenario
{
	private readonly SimFashionWeek _fashionWeek;

	private SimModelingAgencyClient _client;

	private SimSpecialJobModel _specialJob;

	private DataModelFashionShowPivotalMoment _data;

	private SimModel _chosenModel;

	private string _narrative;

	private BracketParser _parser;

	public ScenarioModelFashionWeekShow(SimFashionWeek fashionWeek, SimModelingAgencyClient client)
	{
	}

	public ScenarioModelFashionWeekShow(SimFashionWeek fashionWeek)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataModelFashionShowPivotalMoment GetPivotalMomentScenario(SimModelingAgencyClient client)
	{
		return null;
	}
}
