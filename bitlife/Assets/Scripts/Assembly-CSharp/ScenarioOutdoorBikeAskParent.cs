public class ScenarioOutdoorBikeAskParent : Scenario
{
	private string _narrative;

	private SimRecreationalVehicle _bicycle;

	private readonly SimParent _parent;

	private readonly bool _refusal;

	private readonly bool _existingBicycle;

	public ScenarioOutdoorBikeAskParent(SimParent parent)
	{
	}

	public ScenarioOutdoorBikeAskParent(SimParent parent, SimRecreationalVehicle bicycle, bool isRefusal)
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
