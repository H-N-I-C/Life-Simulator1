using System.Collections;

public class ChallengeMetadataBarbie : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsPlasticSurgeryRequirement()
	{
		return false;
	}

	public bool HasAllPlasticSurgeryForGender(int gender)
	{
		return false;
	}

	private bool MeetsModelRequirement()
	{
		return false;
	}

	private bool MeetsMansionRequirement()
	{
		return false;
	}
}
