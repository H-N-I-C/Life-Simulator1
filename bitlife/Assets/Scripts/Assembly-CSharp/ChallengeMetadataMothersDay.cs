using System.Collections;

public class ChallengeMetadataMothersDay : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsNumChildrenRequirement()
	{
		return false;
	}

	private bool MeetsChildHouseRequirement()
	{
		return false;
	}

	private bool MeetsChildRelationshipRequirement()
	{
		return false;
	}
}
