using System.Collections;

public class ChallengeMetadataWetNWild : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsWaterSlideTesterRequirement()
	{
		return false;
	}

	private bool MeetsSleepWithMenRequirement()
	{
		return false;
	}
}
