using System.Collections;

public class ChallengeMetadataGetAround : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsLoverRequirement()
	{
		return false;
	}

	private bool MeetsChildRequirement()
	{
		return false;
	}

	private bool MeetsThreesomeRequirement()
	{
		return false;
	}

	private bool MeetsSTDRequirement()
	{
		return false;
	}

	public int GetNumberOfStdContracted()
	{
		return 0;
	}
}
