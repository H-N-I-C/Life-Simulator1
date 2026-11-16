using System.Collections;

public class ChallengeMetadataBurgerBob : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsFoodTruckRequirement()
	{
		return false;
	}

	public bool MeetsProfitRequirement()
	{
		return false;
	}
}
