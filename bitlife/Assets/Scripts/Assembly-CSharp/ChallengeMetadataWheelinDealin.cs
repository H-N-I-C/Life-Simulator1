using System.Collections;

public class ChallengeMetadataWheelinDealin : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsFoodTruckRequirement()
	{
		return false;
	}

	public bool MeetsMarijuanaRequirement()
	{
		return false;
	}

	public bool MeetsBonusRequirement()
	{
		return false;
	}
}
