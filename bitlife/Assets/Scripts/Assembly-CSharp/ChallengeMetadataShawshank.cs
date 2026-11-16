using System.Collections;

public class ChallengeMetadataShawshank : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsConvictedForMurderInUSARequirement()
	{
		return false;
	}

	private bool MeetsYearsInAmericanPrisonRequirement()
	{
		return false;
	}

	private bool MeetsEscapeRequirement()
	{
		return false;
	}

	private bool MeetsEmigrationRequirement()
	{
		return false;
	}
}
