using System.Collections;

public class ScenarioRelationshipSpendTimeAll : Scenario
{
	protected bool _requireRewardedVideo;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}

	public static int NumLocalRelationships()
	{
		return 0;
	}

	public static int NumNonLocalRelationships()
	{
		return 0;
	}

	public static ArrayList FamilyMemberArray()
	{
		return null;
	}
}
