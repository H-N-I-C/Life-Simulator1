using System.Collections;

public class ChallengeMetadataInfluencer : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsVerifiedRequirement()
	{
		return false;
	}

	private bool MeetsFollowerRequirement()
	{
		return false;
	}
}
