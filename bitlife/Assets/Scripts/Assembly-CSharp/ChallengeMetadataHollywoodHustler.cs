using System.Collections;

public class ChallengeMetadataHollywoodHustler : ChallengeMetadata
{
	private const int MIN_YEARS_HUSTLING_REQUIREMENT = 5;

	private const int MIN_PEOPLE_VIP_TICKET_SCAMMED_REQUIREMENT = 15;

	private const int MIN_BITTY_AWARD_COUNT_REQUIREMENT = 2;

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementBornCalifornia()
	{
		return false;
	}

	private bool MeetRequirementVIPTicketScam()
	{
		return false;
	}

	private bool MeetRequirementBecomeFamousActor()
	{
		return false;
	}

	private bool MeetsRequirementWon2PlusBittyAwards()
	{
		return false;
	}
}
