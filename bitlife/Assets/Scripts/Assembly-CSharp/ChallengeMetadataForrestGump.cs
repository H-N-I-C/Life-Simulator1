using System.Collections;

public class ChallengeMetadataForrestGump : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsArmyRequirement()
	{
		return false;
	}

	private bool MeetsFishermanRequirement()
	{
		return false;
	}

	private bool MeetsForrestSonRequirement()
	{
		return false;
	}
}
