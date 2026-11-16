using System.Collections.Generic;

public class ScenarioVampirePrisonConvertConfirmation : Scenario
{
	private readonly SimHero _heroVampire;

	private readonly SimPrisoner _toConvert;

	private readonly bool _anyRetaliation;

	private List<string> _narrativesReverseOrder;

	public ScenarioVampirePrisonConvertConfirmation(SimPrisoner person, bool anyRetaliation)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void FailToConvert()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
