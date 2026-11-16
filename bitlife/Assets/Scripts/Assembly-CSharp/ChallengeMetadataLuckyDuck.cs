using System.Collections;

public class ChallengeMetadataLuckyDuck : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementBornInIreland()
	{
		return false;
	}

	private bool MeetRequirementReceivedFromParents()
	{
		return false;
	}

	private bool MeetRequirementCasinoWinnings()
	{
		return false;
	}

	private bool MeetRequirementHookupWithoutSTI()
	{
		return false;
	}

	private bool MeetRequirementHaveChildren()
	{
		return false;
	}

	public static void CheckForHookup(SimLover lover)
	{
	}
}
