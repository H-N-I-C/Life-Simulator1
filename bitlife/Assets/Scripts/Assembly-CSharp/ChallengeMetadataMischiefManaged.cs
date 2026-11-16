using System.Collections;

public class ChallengeMetadataMischiefManaged : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsJuvieRequirement()
	{
		return false;
	}
}
