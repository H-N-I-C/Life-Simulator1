using System.Collections;

public class ChallengeMetadataUnderTheSea : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsUrsulaRequirement()
	{
		return false;
	}

	private bool MeetsVoiceRequirement()
	{
		return false;
	}

	private bool MeetsMarineBiologistRequirement()
	{
		return false;
	}
}
