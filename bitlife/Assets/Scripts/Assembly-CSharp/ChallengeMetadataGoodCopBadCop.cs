using System.Collections;

public class ChallengeMetadataGoodCopBadCop : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsBecomePoliceOfficerRequirement()
	{
		return false;
	}
}
