using System.Collections;

public class ChallengeMetadataBelko : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsCorporateRequirement()
	{
		return false;
	}

	private bool MeetsExecutiveRequirement()
	{
		return false;
	}
}
