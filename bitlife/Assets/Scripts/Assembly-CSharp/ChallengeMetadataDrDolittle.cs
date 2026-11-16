using System.Collections;

public class ChallengeMetadataDrDolittle : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsVetRequirement()
	{
		return false;
	}

	private bool MeetsFarmRequirement()
	{
		return false;
	}

	private int GetNumAlivePets()
	{
		return 0;
	}

	private bool MeetsPetRelationshipRequirement()
	{
		return false;
	}

	private bool MeetsNoSickPetsRequirement()
	{
		return false;
	}
}
