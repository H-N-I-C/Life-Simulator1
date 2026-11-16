using System.Collections.Generic;

public class ScenarioOutdoorHikingTripDenied : Scenario
{
	private string _narrative;

	private readonly OutdoorHikingActivityData _hikingActivityData;

	private readonly SimOutdoor _outdoor;

	private readonly List<SimPerson> _companions;

	public ScenarioOutdoorHikingTripDenied(SimOutdoor outdoor, OutdoorHikingActivityData hikingActivityData, List<SimPerson> companions)
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
