public class ScenarioWatercraftDanger : Scenario
{
	protected SimWatercraft _watercraft;

	protected int _dangerScore;

	protected bool _watercraftLost;

	protected string _narrative;

	public ScenarioWatercraftDanger()
	{
	}

	public ScenarioWatercraftDanger(SimWatercraft theWatercraft)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public static SimWatercraft GetWatercraftForDangerScenario()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
