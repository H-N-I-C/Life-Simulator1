using System.Collections;

public class ChallengeMetadataAchilles : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsArmyRequirement()
	{
		return false;
	}

	public bool MeetsDeploymentRequirement()
	{
		return false;
	}

	public bool MeetsAttributeRequirement()
	{
		return false;
	}

	private bool MeetsAthletesFootRequirement()
	{
		return false;
	}
}
