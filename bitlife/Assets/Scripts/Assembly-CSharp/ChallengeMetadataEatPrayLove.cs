using System.Collections;

public class ChallengeMetadataEatPrayLove : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsFamousAuthorRequirement()
	{
		return false;
	}

	private bool MeetsDivorceRequirement()
	{
		return false;
	}
}
