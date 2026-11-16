using System.Collections;

public class ChallengeMetadataSchwarzenegger : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsMovieStarRequirement()
	{
		return false;
	}

	public bool MeetsGovernorRequirement()
	{
		return false;
	}
}
