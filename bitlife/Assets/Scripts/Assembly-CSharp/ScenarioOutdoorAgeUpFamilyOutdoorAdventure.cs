using Game;

public class ScenarioOutdoorAgeUpFamilyOutdoorAdventure : Scenario
{
	private string _narrative;

	private bool _isHiking;

	private DataOutdoorDestination _data;

	private string _parentsString;

	private string _siblingsString;

	public override void RefreshScenario()
	{
	}

	private string GetLocationName()
	{
		return null;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private float ParentRelationship()
	{
		return 0f;
	}

	private void ImpactFamilyRelationships()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
