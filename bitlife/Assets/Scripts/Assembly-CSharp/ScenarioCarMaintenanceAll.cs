using System.Collections.Generic;

public class ScenarioCarMaintenanceAll : Scenario
{
	private bool _requireRewardedVideo;

	private float _cost;

	private readonly bool _isLuxury;

	private readonly List<SimCar> _cars;

	private readonly string _prefixString;

	public ScenarioCarMaintenanceAll(bool isLuxury)
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
