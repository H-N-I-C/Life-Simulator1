using System.Collections;

public class ChallengeMetadataTitanic : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsSwimRequirement()
	{
		return false;
	}

	private bool MeetsNudeArtModelRequirement()
	{
		return false;
	}
}
