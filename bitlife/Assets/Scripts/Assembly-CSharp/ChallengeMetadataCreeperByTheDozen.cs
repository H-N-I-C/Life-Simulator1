using System.Collections;

public class ChallengeMetadataCreeperByTheDozen : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsHouseRequirement()
	{
		return false;
	}

	private bool MeetsNaturalChildrenRequirement()
	{
		return false;
	}

	private bool MeetsAdoptedChildrenRequirement()
	{
		return false;
	}
}
