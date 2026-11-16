using System.Collections;

public class ChallengeMetadataVampire : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsHauntedHouseRequirement()
	{
		return false;
	}
}
