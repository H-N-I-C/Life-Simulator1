using System.Collections;

public class ChallengeMetadataSlyDuper : ChallengeMetadata
{
	private const int MIN_NUMBER_HOUSES_TO_ROB = 5;

	private const int MIN_NUMBER_PORCHES_TO_PIRATE = 5;

	private const int MIN_NUMBER_COPS_TO_RUN_FROM = 2;

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementRunFromTheCops()
	{
		return false;
	}

	private bool MeetRequirementFurryWearingBankRobber()
	{
		return false;
	}

	private bool MeetRequirementPiratePorches()
	{
		return false;
	}

	private bool MeetRequirementRobHouses()
	{
		return false;
	}

	private bool MeetRequirementBornMale()
	{
		return false;
	}
}
