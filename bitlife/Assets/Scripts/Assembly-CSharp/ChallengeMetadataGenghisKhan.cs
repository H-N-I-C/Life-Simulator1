using System.Collections;

public class ChallengeMetadataGenghisKhan : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsGeneralRequirement()
	{
		return false;
	}

	private bool MeetsHorseRequirement()
	{
		return false;
	}

	public bool MeetsChildrenRequirement()
	{
		return false;
	}
}
