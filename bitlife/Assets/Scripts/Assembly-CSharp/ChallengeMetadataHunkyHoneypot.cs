using System.Collections;

public class ChallengeMetadataHunkyHoneypot : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsMafiaRequirement()
	{
		return false;
	}

	private bool MeetsInformantRequirement()
	{
		return false;
	}
}
