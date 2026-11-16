using System.Collections;

public class ChallengeMetadataBadSanta : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsSantaRequirement()
	{
		return false;
	}

	private bool MeetsDepressionRequirement()
	{
		return false;
	}
}
