using System;

[Serializable]
public class ScenarioHustleStreetNew : Scenario
{
	protected SimHustleStreet _theHustleStreet;

	protected bool _hasCurrentStreet;

	public ScenarioHustleStreetNew(SimHustleStreet theHustleStreet)
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
