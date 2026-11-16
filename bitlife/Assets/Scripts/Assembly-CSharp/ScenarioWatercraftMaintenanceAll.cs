using System.Collections.Generic;

public class ScenarioWatercraftMaintenanceAll : Scenario
{
	private bool _requireRewardedVideo;

	private float _cost;

	private readonly bool _isLuxury;

	private readonly List<SimWatercraft> _watercraft;

	private readonly string _prefixString;

	public ScenarioWatercraftMaintenanceAll(bool isLuxury)
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
