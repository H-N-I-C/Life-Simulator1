using System.Collections;

public class ChallengeMetadataBabyMama : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsChildrenRequirement()
	{
		return false;
	}

	private bool MeetsWivesRequirement()
	{
		return false;
	}

	private bool MeetsChildrenBornToDifferentWivesRequirement()
	{
		return false;
	}
}
