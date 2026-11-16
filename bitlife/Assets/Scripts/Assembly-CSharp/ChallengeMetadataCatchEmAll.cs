using System.Collections;

public class ChallengeMetadataCatchEmAll : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsRelationshipRequirement()
	{
		return false;
	}

	private bool MeetsUniqueCatBreedRequirement()
	{
		return false;
	}

	private bool MeetsUniqueDogBreedRequirement()
	{
		return false;
	}

	private bool MeetsRabbitRequirement()
	{
		return false;
	}

	private bool MeetsProfessorRequirement()
	{
		return false;
	}
}
