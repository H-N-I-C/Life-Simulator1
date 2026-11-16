using System.Collections;

public class ChallengeMetadataMyWay : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsSoloArtistRequirement()
	{
		return false;
	}

	private bool MeetsMafiaRequirement()
	{
		return false;
	}

	private ArrayList GetSpecialJobMafiaArray()
	{
		return null;
	}

	private bool MeetsRecordRequirement()
	{
		return false;
	}
}
