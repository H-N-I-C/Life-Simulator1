using System.Collections.Generic;

public class ScenarioAircraftMaintenanceAll : Scenario
{
	private bool _requireRewardedVideo;

	private float _cost;

	private readonly bool _isLuxury;

	private readonly List<SimAircraft> _aircraft;

	private readonly string _prefixString;

	public ScenarioAircraftMaintenanceAll(bool isLuxury)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
