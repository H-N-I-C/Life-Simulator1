public class ScenarioParentsOfferPet : Scenario
{
	protected SimPet _pet;

	protected int _source;

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

	public static bool DoesOtherFamilyMemberShareName(string name)
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
