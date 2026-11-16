using System.Collections;

public class ChallengeMetadataFathersDay : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsNumChildrenRequirement()
	{
		return false;
	}

	private bool MeetsChildCarRequirement()
	{
		return false;
	}

	private bool MeetsChildRelationshipRequirement()
	{
		return false;
	}
}
