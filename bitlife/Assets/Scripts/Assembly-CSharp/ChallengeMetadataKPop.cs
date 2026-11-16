using System.Collections;

public class ChallengeMetadataKPop : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsKpopRequirement()
	{
		return false;
	}

	private bool MeetsYouTubeRequirement()
	{
		return false;
	}

	private bool MeetsChildRequirement()
	{
		return false;
	}
}
