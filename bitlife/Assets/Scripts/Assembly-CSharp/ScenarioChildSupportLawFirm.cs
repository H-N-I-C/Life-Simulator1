using System.Collections.Generic;

public class ScenarioChildSupportLawFirm : Scenario
{
	private readonly SimLover _exLover;

	private readonly List<SimChild> _children;

	private float[] _firmCost;

	public ScenarioChildSupportLawFirm(List<SimChild> children, SimLover exLover)
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
