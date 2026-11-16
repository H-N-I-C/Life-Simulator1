using System.Collections;

public class ChallengeMetadataSmittenKitten : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool HasMetCatRelationshipRequirement()
	{
		return false;
	}
}
