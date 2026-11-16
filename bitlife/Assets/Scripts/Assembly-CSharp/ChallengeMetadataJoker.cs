using System.Collections;

public class ChallengeMetadataJoker : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsClownRequirement()
	{
		return false;
	}
}
