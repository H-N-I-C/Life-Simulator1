using System.Collections;

public class ChallengeMetadataProblemChild : ChallengeMetadata
{
	private const int MIN_NUMBER_CLASSMATES_TO_INSULT = 5;

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementEscapeJuvie()
	{
		return false;
	}

	private bool MeetRequirementHaveZeroRelationshipWithParent()
	{
		return false;
	}

	private bool MeetRequirementExpelledFromSecondarySchool()
	{
		return false;
	}

	private bool MeetRequirementSuspendedFromPrimarySchool()
	{
		return false;
	}

	private bool MeetRequirementInsultClassmates()
	{
		return false;
	}
}
