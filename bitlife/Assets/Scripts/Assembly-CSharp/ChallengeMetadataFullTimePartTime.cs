using System.Collections;

public class ChallengeMetadataFullTimePartTime : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsNeverHoldFullTimeJobRequirement()
	{
		return false;
	}

	private bool MeetsHighSchoolPartTimeRequirement()
	{
		return false;
	}
}
