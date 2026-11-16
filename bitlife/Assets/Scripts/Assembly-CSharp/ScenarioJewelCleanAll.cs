using System.Collections.Generic;

public class ScenarioJewelCleanAll : Scenario
{
	private bool _requireRewardedVideo;

	private float _cost;

	private readonly bool _luxuryJewelry;

	private readonly List<SimJewel> _jewels;

	private readonly string _prefixString;

	private readonly float _costPerItem;

	public ScenarioJewelCleanAll(bool luxuryJewelry)
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
