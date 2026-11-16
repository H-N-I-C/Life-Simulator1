using System.Collections;

public class ChallengeMetadataHumbug : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsCEORequirement()
	{
		return false;
	}
}
