public class ScenarioChildWantsPet : Scenario
{
	protected SimChild _child;

	protected SimPet _pet;

	protected string _petString;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimChild GetChildToDemand()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
