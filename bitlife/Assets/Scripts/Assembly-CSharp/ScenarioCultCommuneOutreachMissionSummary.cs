public class ScenarioCultCommuneOutreachMissionSummary : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private readonly SimPlace _destination;

	private readonly int _travelClass;

	private readonly string _recruitmentMessage;

	private readonly int _numberOfParticipants;

	private double _theCost;

	private string _destinationString;

	public ScenarioCultCommuneOutreachMissionSummary(SimPlace destination, int travelClass, string recruitmentMessage, int numberOfParticipants)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
