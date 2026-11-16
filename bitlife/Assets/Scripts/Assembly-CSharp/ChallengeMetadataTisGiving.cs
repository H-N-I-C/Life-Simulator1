using System.Collections;

public class ChallengeMetadataTisGiving : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementGiveFatherMillionWorthGift()
	{
		return false;
	}

	private bool MeetRequirementPrisonGuard()
	{
		return false;
	}

	private bool MeetRequirementGiveBlood()
	{
		return false;
	}

	private bool MeetRequirementGiveMomMillionWorthGift()
	{
		return false;
	}

	private bool MeetRequirementGiveMansionToFriend()
	{
		return false;
	}

	public static void GiveGiftToPerson(SimPerson person, float value)
	{
	}
}
