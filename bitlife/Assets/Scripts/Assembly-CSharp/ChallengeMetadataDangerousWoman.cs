using System.Collections;

public class ChallengeMetadataDangerousWoman : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsSingerRequirement()
	{
		return false;
	}

	private bool MeetsRecordRequirement()
	{
		return false;
	}
}
