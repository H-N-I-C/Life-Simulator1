using System.Collections.Generic;

public class ScenarioVampireFamilarHonorific : Scenario
{
	private string _narrative;

	private readonly SimVampireFamiliar _familiar;

	private readonly SimHero _hero;

	private readonly List<SimVampireHonorific> _honorificStrings;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
