using System.Collections;

public class ChallengeMetadataGenerousBoss : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsFacilityRequirement()
	{
		return false;
	}

	public bool MeetsProductsRequirement()
	{
		return false;
	}
}
