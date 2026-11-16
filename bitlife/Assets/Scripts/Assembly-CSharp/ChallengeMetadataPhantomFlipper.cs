using System.Collections;

public class ChallengeMetadataPhantomFlipper : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsHauntedMansionRequirement()
	{
		return false;
	}

	private bool MeetsExorcistRequirement()
	{
		return false;
	}
}
