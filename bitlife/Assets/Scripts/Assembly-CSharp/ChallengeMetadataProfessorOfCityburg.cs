using System.Collections;

public class ChallengeMetadataProfessorOfCityburg : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsBornMaleInNewYorkRequirement()
	{
		return false;
	}

	private bool MeetsScientistRequirement()
	{
		return false;
	}

	private bool MeetRequirementFatherTriplets()
	{
		return false;
	}

	public bool MeetsMonkeyRequirement()
	{
		return false;
	}

	public bool MeetsCatRelationshipRequirement()
	{
		return false;
	}
}
