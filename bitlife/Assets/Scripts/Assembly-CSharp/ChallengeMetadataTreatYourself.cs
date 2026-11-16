using System.Collections;

public class ChallengeMetadataTreatYourself : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsCatAdoptionRequirement()
	{
		return false;
	}

	private bool MeetsDogAdoptionRequirement()
	{
		return false;
	}
}
