using System.Collections;

public class ChallengeMetadataLandSeaAir : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsCarRequirement()
	{
		return false;
	}

	private bool MeetsWatercraftRequirement()
	{
		return false;
	}

	private bool MeetsAircraftRequirement()
	{
		return false;
	}
}
