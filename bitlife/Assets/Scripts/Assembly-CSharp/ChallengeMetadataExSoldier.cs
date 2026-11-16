using System.Collections;

public class ChallengeMetadataExSoldier : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsBornMaleInGermany()
	{
		return false;
	}

	private bool MeetsArmyRequirement()
	{
		return false;
	}
}
