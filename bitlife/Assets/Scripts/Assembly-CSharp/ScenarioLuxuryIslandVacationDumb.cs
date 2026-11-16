using Game;

public class ScenarioLuxuryIslandVacationDumb : Scenario
{
	private string _narrative;

	private SimLuxury _luxury;

	private readonly SimPerson _guest;

	private readonly string _guestType;

	private DataLuxuryIslandVacationDumbScenario _data;

	private readonly BracketParser _parser;

	public ScenarioLuxuryIslandVacationDumb(SimLuxuryIsland island, SimPerson guest, string guestType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
