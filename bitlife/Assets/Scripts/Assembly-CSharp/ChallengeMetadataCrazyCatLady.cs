using System.Collections;

public class ChallengeMetadataCrazyCatLady : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private int GetNumCatsOwned()
	{
		return 0;
	}

	private bool MeetsNumCrazyCatsRequirement()
	{
		return false;
	}

	private bool MeetsNumCatTypesRequirement()
	{
		return false;
	}

	public int GetCatTypesCount()
	{
		return 0;
	}

	private bool MeetsVetRequirement()
	{
		return false;
	}
}
