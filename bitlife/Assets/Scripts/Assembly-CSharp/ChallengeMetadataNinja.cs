using System.Collections;

public class ChallengeMetadataNinja : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsTwitchMonetizationRequirement()
	{
		return false;
	}

	private bool MeetsTwitchSubscribersRequirement()
	{
		return false;
	}
}
