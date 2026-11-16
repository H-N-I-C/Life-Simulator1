using System.Collections;

public class ChallengeMetadataMotorhead : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsMechanicRequirement()
	{
		return false;
	}

	private bool MeetsCarConditionRequirement()
	{
		return false;
	}

	private bool MeetsOldCarRequirement()
	{
		return false;
	}
}
