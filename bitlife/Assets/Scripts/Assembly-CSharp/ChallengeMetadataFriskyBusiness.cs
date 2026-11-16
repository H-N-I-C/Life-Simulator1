using System.Collections;

public class ChallengeMetadataFriskyBusiness : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsChildrenWithCoworkerRequirement()
	{
		return false;
	}

	public bool MeetsCorporateRequirement()
	{
		return false;
	}
}
