using System.Collections;

public class ChallengeMetadataUrbanCountry : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsRNBArtistRequirement()
	{
		return false;
	}

	private bool MeetsBornFemaleInLouisianaRequirement()
	{
		return false;
	}

	public static bool MeetsRBRecordRequirement()
	{
		return false;
	}
}
