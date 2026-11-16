using System.Collections;

public class ChallengeMetadataFindingNemo : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsDentistRequirement()
	{
		return false;
	}

	private bool MeetsChildRequirement()
	{
		return false;
	}
}
