using Backend;

public class ScenarioScavengerHuntPrize : Scenario
{
	private bool IsReward__BackingField;

	public bool IsReward
	{
		get
		{
			return IsReward__BackingField;
		}
		set
		{
			IsReward__BackingField = value;
		}
	}

	public ScenarioScavengerHuntPrize()
	{
	}

	public ScenarioScavengerHuntPrize(IMyStuffUseCheck itemId)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}
}
