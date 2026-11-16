public class ScenarioAircraftDanger : Scenario
{
	protected SimAircraft _aircraft;

	protected int _dangerScore;

	protected bool _fatal;

	protected string _narrative;

	public ScenarioAircraftDanger()
	{
	}

	public ScenarioAircraftDanger(SimAircraft theAircraft)
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

	public static SimAircraft GetAircraftForDangerScenario()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
