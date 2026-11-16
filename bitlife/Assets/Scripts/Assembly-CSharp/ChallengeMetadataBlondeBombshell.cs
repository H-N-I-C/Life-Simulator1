using System.Collections;

public class ChallengeMetadataBlondeBombshell : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsPianoRequirement()
	{
		return false;
	}

	public bool MeetsFactoryWorkerRequirement()
	{
		return false;
	}
}
