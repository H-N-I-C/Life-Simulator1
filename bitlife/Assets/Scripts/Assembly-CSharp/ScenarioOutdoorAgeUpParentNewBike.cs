public class ScenarioOutdoorAgeUpParentNewBike : Scenario
{
	private string _narrative;

	private readonly SimRecreationalVehicle _bike;

	private readonly string _parentsString;

	private readonly bool _isRefusal;

	public ScenarioOutdoorAgeUpParentNewBike(SimRecreationalVehicle bike, bool isRefusal, string parentsString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void ImpactParentRelation()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
