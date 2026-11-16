using System.Collections;

public class ChallengeMetadataTheO : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsJournalismRequirement()
	{
		return false;
	}

	private bool MeetsBookBestSellerRequirement()
	{
		return false;
	}

	private bool MeetsFamousReporterRequirement()
	{
		return false;
	}
}
