using System.Collections;

public class ChallengeMetadataGambler : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsGamblingAddictionRequirement()
	{
		return false;
	}

	private bool MeetsLasVegasRequirement()
	{
		return false;
	}
}
